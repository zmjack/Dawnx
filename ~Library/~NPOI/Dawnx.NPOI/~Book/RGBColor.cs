
namespace Dawnx.NPOI
{
    public class RGBColor
    {
        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }
        public int Value { get; private set; }

        public byte[] Bytes => new[] { Red, Green, Blue };

        public RGBColor(int rgb)
        {
            Red = (byte)(rgb & 0xFF0000 >> (8 * 2));
            Green = (byte)(rgb & 0xFF00 >> (8 * 1));
            Blue = (byte)(rgb & 0xFF);
        }

        public RGBColor(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Value = (red << (8 * 2)) + (green << (8 * 1)) + blue;
        }

        public RGBColor(byte[] rgb) : this(rgb[0], rgb[1], rgb[2]) { }

        public override bool Equals(object obj)
        {
            var instance = obj as RGBColor;
            if (instance is null) return false;
            return GetHashCode() == instance.GetHashCode();
        }
        public override int GetHashCode() => Value;

        public static bool operator ==(RGBColor instance1, RGBColor instance2) => instance1.Equals(instance2);
        public static bool operator !=(RGBColor instance1, RGBColor instance2) => !instance1.Equals(instance2);
    }

}
