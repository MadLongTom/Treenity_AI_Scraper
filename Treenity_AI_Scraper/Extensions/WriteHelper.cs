namespace Treenity_AI_Scraper.Extensions
{
    public static class WriteHelper
    {
        public static async Task WriteFileAsync(string path, string text)
        {
            await writeLock.WaitAsync();
            try
            {
                using StreamWriter sw = new(path, true);
                await sw.WriteLineAsync(text);
            }
            finally
            {
                writeLock.Release();
            }
        }
        private static readonly SemaphoreSlim writeLock = new(1, 1);
    }
}
