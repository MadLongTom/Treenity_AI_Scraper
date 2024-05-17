using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Treenity_AI_Scraper.Services.Cipher;
using Treenity_AI_Scraper.Models.Database;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using LogLevel = OpenQA.Selenium.LogLevel;

namespace Treenity_AI_Scraper.Services
{
    internal class TreenityCookieService(ProgramDbContext db, ILogger<TreenityCookieService> logger)
    {
        public ChromeDriver driver;
        bool collected = false;
        bool canRelease = false;
        ReadOnlyCollection<OpenQA.Selenium.Cookie> cookies;
        public string SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            string result = string.Empty;
            using (MemoryStream stream = new())
            {
                formatter.Serialize(stream, obj);
                byte[] byt = new byte[stream.Length];
                byt = stream.ToArray();
                //result = Encoding.UTF8.GetString(byt, 0, byt.Length);
                result = Convert.ToBase64String(byt);
                stream.Flush();
            }
            return result;
        }
        //将二进制序列字符串转换为Object类型对象
        public object DeserializeObject(string str)
        {
            IFormatter formatter = new BinaryFormatter();
            //byte[] byt = Encoding.UTF8.GetBytes(str);
            byte[] byt = Convert.FromBase64String(str);
            object obj = null;
            using (Stream stream = new MemoryStream(byt, 0, byt.Length))
            {
                obj = formatter.Deserialize(stream);
            }
            return obj;
        }
        public CookieCollection GetSavedCookies(EntityStore entityStore)
        {
            return DeserializeObject(entityStore.cookie!) as CookieCollection;
        }
        public async Task SaveCookies(EntityStore entityStore)
        {
            db.Attach(entityStore);
            CookieCollection cookieCollection = [];
            foreach (var c in cookies)
            {
                cookieCollection.Add(new System.Net.Cookie(c.Name, c.Value, c.Path, c.Domain));
            }
            entityStore.CookieExpired = DateTime.Now + TimeSpan.FromHours(23);
            entityStore.cookie = SerializeObject(cookieCollection);
            await db.SaveChangesAsync();
        }
        public async Task<ReadOnlyCollection<OpenQA.Selenium.Cookie>> GetCookies(EntityStore entityStore)
        {
            ChromeOptions options = new();
            options.AddArguments(
            [
                "--no-sandbox",
                //"--allow-running-insecure-content",
                "--ignore-certificate-errors",
                "--disable-single-click-autofill",
                "--disable-autofill-keyboard-accessory-view[8]",
                "--disable-full-form-autofill-ios",
                "--incognito",
                "--log-level=3",
                "--disable-dev-shm-usage",
                "--disable-blink-features=AutomationControlled",
                "--mute-audio",
                "--window-size=1920x1080",
                "--headless"
            ]);
            options.PerformanceLoggingPreferences = new ChromiumPerformanceLoggingPreferences
            {
                IsCollectingNetworkEvents = true,
                IsCollectingPageEvents = false
            };
            options.SetLoggingPreference("browser", LogLevel.All);
            options.SetLoggingPreference("performance", LogLevel.All);
            var driverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            driverService.HideCommandPromptWindow = true;
            logger.LogInformation("Starting ChromeDriver");
            driver = new ChromeDriver(driverService, options);
            driver.ExecuteCdpCommand("Page.addScriptToEvaluateOnNewDocument", new() { ["source"] = "Object.defineProperty(navigator, 'webdriver', { get: () => undefined })" });
            driver.ExecuteCdpCommand("Network.setBlockedURLs", new() { ["urls"] = (string[])["*.mp4*"] });
            driver.Manage().Network.NetworkResponseReceived += Network_NetworkResponseReceived;
            await driver.Manage().Network.StartMonitoring();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(@"https://ai.zhihuishu.com");
            _ = LoginUser(entityStore);
            while (!collected)
            {
                await Task.Delay(1000);
            }
            canRelease = true;
            await SaveCookies(entityStore);
            return cookies;
        }

        private async void Network_NetworkResponseReceived(object? sender, NetworkResponseReceivedEventArgs e)
        {
            if (e.ResponseUrl.Contains(@"https://ai.zhihuishu.com/?ticket"))
            {
                /*foreach (var item in e.ResponseHeaders.Where(e => e.Key == "Set-Cookie").Select(e => e.Value))
                {
                    var prop = item.Split(';').Select(p => p.Trim()).ToList();
                    var kv = prop[0].Split('=').ToList();
                    var domain = prop.FirstOrDefault(p => p.Contains("Domain"))?.Split('=')[1];
                    var path = prop.FirstOrDefault(p => p.Contains("Path"))?.Split('=')[1];
                    cookies.Add(new(kv[0], kv[1],path,domain));
                };*/
                _ = Task.Factory.StartNew(async () =>
                {
                    logger.LogInformation("Disposing ChromeDriver");
                    cookies = driver.Manage().Cookies.AllCookies;
                    collected = true;
                    while (!canRelease)
                    {
                        await Task.Delay(1000);
                    }
                    driver.Close();
                    driver.Quit();
                });
            }
            if (e.ResponseUrl.Contains(@"https://passport.zhihuishu.com/user/validateAccountAndPassword") || e.ResponseUrl.Contains(@"https://passport.zhihuishu.com/user/validateCodeAndPassword"))
            {
                logger.LogInformation("LoginResult:" + e.ResponseBody);
                if (JsonSerializer.Deserialize<validateReturn>(e.ResponseBody)!.status != 1)
                {
                    cookies = new([]);
                    collected = true;
                    while (!canRelease)
                    {
                        await Task.Delay(1000);
                    }
                    driver.Close();
                    driver.Quit();
                }
            }
        }
        public class validateReturn
        {
            public int status { get; set; }
        }
        public async Task LoginUser(EntityStore entityStore)
        {
            await WaitFor(By.XPath("//*[@id=\"qStudentID\"]"), 10);
            if (entityStore.username.Length == 10)
            {
                FindElement(By.XPath("//*[@id=\"qStudentID\"]"), 1).Click();
                await Task.Delay(1000);
                //FindElement(By.XPath("//*[@id=\"layui-layer1\"]/div/div/div/a"), 10).Click();
                FindElement(By.XPath("//*[@id=\"quickSearch\"]"), 1).Click();
                FindElement(By.XPath("//*[@id=\"quickSearch\"]"), 1).Clear();
                FindElement(By.XPath("//*[@id=\"quickSearch\"]"), 1).SendKeys("哈尔滨工程大学");
                FindElement(By.XPath("//*[@id=\"schoolListCode\"]/li[2]"), 1).Click();
                FindElement(By.XPath("//*[@id=\"clCode\"]"), 1).SendKeys(entityStore.username);
                FindElement(By.XPath("//*[@id=\"clPassword\"]"), 1).SendKeys(entityStore.password);
            }
            else
            {
                FindElement(By.XPath("//*[@id=\"lUsername\"]"), 1).SendKeys(entityStore.username);
                FindElement(By.XPath("//*[@id=\"lPassword\"]"), 1).SendKeys(entityStore.password);
            }
            FindElement(By.XPath("//*[@id=\"f_sign_up\"]/div[1]/span"), 1).Click();
            await WaitFor(By.XPath("//*[@class=\"yidun_bg-img\"]"), 10);
            await Task.Delay(1000);
            driver.ExecuteJavaScript("var data=document.querySelector('iframe[class=\"yidun_cover-frame\"]').remove()");
            driver.SwitchTo().DefaultContent();
            string curl = driver.Url;
            do
            {
                string back = FindElement(By.XPath("//*[@class=\"yidun_bg-img\"]"), 1).GetAttribute("src");
                string front = FindElement(By.XPath("//*[@class=\"yidun_jigsaw\"]"), 1).GetAttribute("src");
                int length = 0;
                try
                {
                    length = await TreenityCryptoProvider.SlideMatch(back, front);
                }
                catch (Exception) { }
                IWebElement slider = FindElement(By.ClassName("yidun_slider"), 1);
                Actions action = new(driver);
                action.MoveToElement(slider).Perform();
                action.ClickAndHold(slider).Perform();
                foreach (var le in GetTracks(length))
                {
                    action.MoveByOffset(le, Random.Shared.Next(-5, 6)).Perform();
                }
                action.Release().Perform();
                logger.LogInformation("Slide Action Done");
                await Task.Delay(5000);
            } while (curl == driver.Url);
        }
        private async Task<bool> WaitFor(By by, int timeout)
        {
            timeout *= 1000;
            while (timeout > 0)
            {
                if (driver.FindElements(by).Count == 0)
                {
                    timeout -= 1000;
                    await Task.Delay(1000);
                }
                else return true;
            }
            return false;
        }
        public IWebElement FindElement(By by, int timeoutInSeconds)
        {
            logger.LogInformation(by.Criteria);
            return _FindElement(by, timeoutInSeconds).Result;
        }
        private async Task<IWebElement> _FindElement(By by, int timeoutInSeconds)
        {
            await WaitFor(by, timeoutInSeconds);
            return driver.FindElement(by);
        }
        public static List<int> GetTracks(int distance)
        {
            int v = Random.Shared.Next(0, 3);
            int t = 1;
            List<int> tracks = [];
            int cur = 0;
            double mid = distance * 0.8;
            while (cur < distance)
            {
                int a;
                if (cur < mid)
                {
                    a = Random.Shared.Next(2, 5);
                }
                else
                {
                    a = -Random.Shared.Next(3, 6);
                }

                int s = v * t + (int)(0.5 * a * Math.Pow(t, 2));
                cur += s;
                v += a * t;
                tracks.Add(s);
            }
            tracks.Add(distance - tracks.Sum());
            return tracks;
        }
    }
}
