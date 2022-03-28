using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace DearVoid.Skia.Tizen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new DearVoid.App(), args);
            host.Run();
        }
    }
}
