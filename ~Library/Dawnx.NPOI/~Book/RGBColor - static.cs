
namespace Dawnx.NPOI
{
    public partial class RGBColor
    {
        public static short AutomaticIndex = 64;

        public static RGBColor Black = new RGBColor(0x000000) { Index = 8 };
        public static RGBColor White = new RGBColor(0xffffff) { Index = 9 };
        public static RGBColor Red = new RGBColor(0xff0000) { Index = 10 };
        public static RGBColor BrightGreen = new RGBColor(0x00ff00) { Index = 11 };
        public static RGBColor Blue = new RGBColor(0x0000ff) { Index = 12 };
        public static RGBColor Yellow = new RGBColor(0xffff00) { Index = 13 };
        public static RGBColor Pink = new RGBColor(0xff00ff) { Index = 14 };
        public static RGBColor Turquoise = new RGBColor(0x00ffff) { Index = 15 };
        public static RGBColor DarkRed = new RGBColor(0x800000) { Index = 16 };
        public static RGBColor Green = new RGBColor(0x008000) { Index = 17 };
        public static RGBColor DarkBlue = new RGBColor(0x000080) { Index = 18 };
        public static RGBColor DarkYellow = new RGBColor(0x808000) { Index = 19 };
        public static RGBColor Violet = new RGBColor(0x800080) { Index = 20 };
        public static RGBColor Teal = new RGBColor(0x008080) { Index = 21 };
        public static RGBColor Grey25Percent = new RGBColor(0xc0c0c0) { Index = 22 };
        public static RGBColor Grey50Percent = new RGBColor(0x808080) { Index = 23 };
        public static RGBColor CornflowerBlue = new RGBColor(0x9999ff) { Index = 24 };
        public static RGBColor Maroon = new RGBColor(0x7f0000) { Index = 25 };
        public static RGBColor LemonChiffon = new RGBColor(0xffffcc) { Index = 26 };
        public static RGBColor Orchid = new RGBColor(0x660066) { Index = 28 };
        public static RGBColor Coral = new RGBColor(0xff8080) { Index = 29 };
        public static RGBColor RoyalBlue = new RGBColor(0x0066cc) { Index = 30 };
        public static RGBColor LightCornflowerBlue = new RGBColor(0xccccff) { Index = 31 };
        public static RGBColor SkyBlue = new RGBColor(0x00ccff) { Index = 40 };
        public static RGBColor LightTurquoise = new RGBColor(0xccffff) { Index = 41 };
        public static RGBColor LightGreen = new RGBColor(0xccffcc) { Index = 42 };
        public static RGBColor LightYellow = new RGBColor(0xffff99) { Index = 43 };
        public static RGBColor PaleBlue = new RGBColor(0x99ccff) { Index = 44 };
        public static RGBColor Rose = new RGBColor(0xff99cc) { Index = 45 };
        public static RGBColor Lavender = new RGBColor(0xcc99ff) { Index = 46 };
        public static RGBColor Tan = new RGBColor(0xffcc99) { Index = 47 };
        public static RGBColor LightBlue = new RGBColor(0x3366ff) { Index = 48 };
        public static RGBColor Aqua = new RGBColor(0x33cccc) { Index = 49 };
        public static RGBColor Lime = new RGBColor(0x99cc00) { Index = 50 };
        public static RGBColor Gold = new RGBColor(0xffcc00) { Index = 51 };
        public static RGBColor LightOrange = new RGBColor(0xff9900) { Index = 52 };
        public static RGBColor Orange = new RGBColor(0xff6600) { Index = 53 };
        public static RGBColor BlueGrey = new RGBColor(0x666699) { Index = 54 };
        public static RGBColor Grey40Percent = new RGBColor(0x969696) { Index = 55 };
        public static RGBColor DarkTeal = new RGBColor(0x003366) { Index = 56 };
        public static RGBColor SeaGreen = new RGBColor(0x339966) { Index = 57 };
        public static RGBColor DarkGreen = new RGBColor(0x003300) { Index = 58 };
        public static RGBColor OliveGreen = new RGBColor(0x333300) { Index = 59 };
        public static RGBColor Brown = new RGBColor(0x993300) { Index = 60 };
        public static RGBColor Plum = new RGBColor(0x993366) { Index = 61 };
        public static RGBColor Indigo = new RGBColor(0x333399) { Index = 62 };
        public static RGBColor Grey80Percent = new RGBColor(0x333333) { Index = 63 };
        public static RGBColor Automatic = new RGBColor(0x000000) { Index = 64 };

        public static RGBColor ParseIndexed(int index)
        {
            switch (index)
            {
                case 8: return Black;
                case 9: return White;
                case 10: return Red;
                case 11: return BrightGreen;
                case 12: return Blue;
                case 13: return Yellow;
                case 14: return Pink;
                case 15: return Turquoise;
                case 16: return DarkRed;
                case 17: return Green;
                case 18: return DarkBlue;
                case 19: return DarkYellow;
                case 20: return Violet;
                case 21: return Teal;
                case 22: return Grey25Percent;
                case 23: return Grey50Percent;
                case 24: return CornflowerBlue;
                case 25: return Maroon;
                case 26: return LemonChiffon;
                case 28: return Orchid;
                case 29: return Coral;
                case 30: return RoyalBlue;
                case 31: return LightCornflowerBlue;
                case 40: return SkyBlue;
                case 41: return LightTurquoise;
                case 42: return LightGreen;
                case 43: return LightYellow;
                case 44: return PaleBlue;
                case 45: return Rose;
                case 46: return Lavender;
                case 47: return Tan;
                case 48: return LightBlue;
                case 49: return Aqua;
                case 50: return Lime;
                case 51: return Gold;
                case 52: return LightOrange;
                case 53: return Orange;
                case 54: return BlueGrey;
                case 55: return Grey40Percent;
                case 56: return DarkTeal;
                case 57: return SeaGreen;
                case 58: return DarkGreen;
                case 59: return OliveGreen;
                case 60: return Brown;
                case 61: return Plum;
                case 62: return Indigo;
                case 63: return Grey80Percent;
                case 64: return Automatic;
                default: goto case 64;
            }
        }
    }

}
