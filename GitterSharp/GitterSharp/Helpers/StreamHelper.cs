using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Helpers
{
    internal static class StreamHelper
    {
        public static async Task<string> ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true))
            {
                return await reader.ReadLineAsync();
            }
        }
    }
}
