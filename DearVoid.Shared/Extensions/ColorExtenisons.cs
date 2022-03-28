using System.Drawing;

namespace DearVoid.Extensions
{
    public static class ColorExtenisons
    {
        public static string ToHex(this Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}
