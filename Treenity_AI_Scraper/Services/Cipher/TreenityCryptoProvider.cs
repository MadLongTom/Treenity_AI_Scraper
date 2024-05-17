using OpenCvSharp;
using System.Dynamic;
using System.Text.Json;
using Treenity_AI_Scraper.Models.Treenity;

namespace Treenity_AI_Scraper.Services.Cipher
{
    internal class TreenityCryptoProvider
    {
        private readonly TreenityAesProvider _aesProvider = new();
        private readonly TreenityRsaProvider _rsaProvider = new();
        private readonly Dictionary<string, string> rsacipher = new(){
            { "6","KomLn7VDcgvS93cRo1Uu9T71ZdZ4aLssymobN2neRpuiWOI/xV2JrgFuY9GvWIGeXdfUhtPMlsvCz3JniCPfb4lmp/k+/yDva4PVyk/ant6bASs/X1zitVYTZB+QW7Qyx9fgMWBYvfISMfjrHZlnIFrLRqPHh5wlQIgEzM4GVik=" },
            { "3","MvvI6LxWDXiULHdxgGkpyKNBYNmLKocPqUjTId7M/47jQn5akrEAL5Swv7HX3T/Vuz4UsU552qp7eR55UX6gZ/lLhdOioo6BgRBPmreHZHO0vfYlJ9dN3LHD/k8FaebO3R9e684JIdjJBRT2VhgHozJDp5qRO3/WpeK25qruy2U=" }
    };
        public string yxyz(string data, string module)
        {
            var result = _rsaProvider.Decrypt(rsacipher[module]);
            return _aesProvider.Encrypt(data, JsonSerializer.Deserialize<sl>(result).cKey);
        }
        public string reyxyz(string data, string module)
        {
            var result = _rsaProvider.Decrypt(rsacipher[module]);
            return _aesProvider.Decrypt(data, JsonSerializer.Deserialize<sl>(result).cKey);
        }
        public async Task<string> Labc(int cryptModuleSerial)
        {
            var cryptJson = new { module = cryptModuleSerial };
            var url = cryptModuleSerial == 10 ? "https://appcomm-user.zhihuishu.com/app-commserv-user/c/hasV2" : "https://appcomm-user.zhihuishu.com/app-commserv-user/c/has";
            var cipher = _rsaProvider.Encrypt(JsonSerializer.Serialize(cryptJson));
            using var client = new HttpClient();
            var content = new Dictionary<string, string> { { "uid", cipher } };
            var serialized = await new FormUrlEncodedContent(content).ReadAsStringAsync();
            var response = await client.GetAsync($"{url}?{serialized}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonSerializer.Deserialize<ExpandoObject>(responseContent);
                return responseObject.rt.sl;
            }
            else
            {
                throw new Exception("Error occurred while making the request.");
            }
        }
        public static async Task<int> SlideMatch(string b64, string f64)
        {
            using HttpClient client = new();
            var back = await client.GetByteArrayAsync(b64);
            var front = await client.GetByteArrayAsync(f64);
            return ((int[])SlideMatch(front, back)["target"])[0] + 7;
        }
        private static Dictionary<string, object> SlideMatch(byte[] targetBytes = null, byte[] backgroundBytes = null, bool simpleTarget = false, bool flag = false)
        {
            Mat target;
            int targetY = 0;

            if (!simpleTarget)
            {
                try
                {
                    (target, _, targetY) = GetTarget(targetBytes);
                }
                catch (Exception e)
                {
                    if (flag)
                    {
                        throw e;
                    }
                    return SlideMatch(targetBytes, backgroundBytes, true, true);
                }
            }
            else
            {
                target = Cv2.ImDecode(targetBytes, ImreadModes.AnyColor);
            }

            Mat background = Cv2.ImDecode(backgroundBytes, ImreadModes.AnyColor);
            Mat cbackground = new();
            Mat ctarget = new();
            Cv2.Canny(background, cbackground, 100, 200);
            Cv2.Canny(target, ctarget, 100, 200);

            Cv2.CvtColor(cbackground, background, ColorConversionCodes.GRAY2BGR);
            Cv2.CvtColor(ctarget, target, ColorConversionCodes.GRAY2BGR);

            Mat res = new();
            Cv2.MatchTemplate(background, target, res, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(res, out _, out _, out _, out Point maxLoc);

            Size s = target.Size();
            Point bottomRight = new(maxLoc.X + s.Width, maxLoc.Y + s.Height);

            return new Dictionary<string, object>
    {
        { "target_y", targetY },
        { "target", new int[] { maxLoc.X, maxLoc.Y, bottomRight.X, bottomRight.Y } }
    };
        }
        public static (Mat, int, int) GetTarget(byte[] imgBytes = null)
        {

            using var ms = new MemoryStream(imgBytes);
            var image = Mat.FromStream(ms, ImreadModes.AnyColor);

            int w = image.Width, h = image.Height;
            int starttx = 0, startty = 0, endX = 0, endY = 0, endynext = 0, endxnext = 0;

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    var p = image.Get<Vec4b>(y, x);

                    if (p.Item2 == 0)
                    {
                        if (startty != 0 && endY < endynext)
                        {
                            endY = endynext;
                        }

                        if (starttx != 0 && endxnext > endX)
                        {
                            endX = endxnext;
                        }
                    }
                    else
                    {
                        if (startty == 0)
                        {
                            startty = y;

                            endY = 0;
                        }
                        else
                        {
                            if (y < startty)
                            {
                                startty = y;
                                endynext = 0;
                                endY = 0;
                            }
                            else
                            {

                                endynext = y;
                            }
                        }
                        if (starttx != 0 && endxnext < x)
                        {
                            endxnext = x;
                        }

                    }

                }
                if (starttx == 0 && startty != 0)
                {
                    starttx = x;
                }

                if (endY != 0 && endxnext > endX)
                {
                    endX = endxnext;
                }
            }

            var rect = new Rect(starttx, startty, endX - starttx + 1, endY - startty + 1);
            return (image.Clone(rect), starttx, startty);
        }
    }
}
