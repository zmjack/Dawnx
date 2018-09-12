
namespace Dawnx.NPOI
{
    public partial class RGBColor
    {
        public int Value { get; private set; }
        public byte[] Bytes { get; private set; }
        public short Index { get; set; }

        public RGBColor(int rgbValue)
        {
            Bytes = new[]
            {
                (byte)(rgbValue & 0xFF0000 >> (8 * 2)),
                (byte)(rgbValue & 0xFF00 >> (8 * 1)),
                (byte)(rgbValue & 0xFF),
            };
            Value = rgbValue;
        }

        public RGBColor(byte red, byte green, byte blue)
        {
            Bytes = new[] { red, green, blue };
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
