using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dawnx.Utilities
{
#if DEBUG
    public static class ColorUtility
    {
        public static Color CreateFromAhsv(float hue, float saturation, float value)
            => CreateFromAhsv(255, hue, saturation, value);
        public static Color CreateFromAhsv(int alpha, float hue, float saturation, float value)
        {
            if (hue < 0) throw new ArgumentException("The hue must be greater than 0.");
            if (saturation < 0 || saturation > 1) throw new ArgumentException("The saturation must be between 0 and 1.");
            if (value < 0 || value > 1) throw new ArgumentException("The value must be between 0 and 1.");

            var ahColor = GetAbsoluteHueColor(alpha, hue);
            var ahsColor = Color.FromArgb(alpha,
                (int)Math.Round((double)ahColor.R + (255 - ahColor.R) * (1 - saturation)),
                (int)Math.Round((double)ahColor.G + (255 - ahColor.G) * (1 - saturation)),
                (int)Math.Round((double)ahColor.B + (255 - ahColor.B) * (1 - saturation)));
            var ahsvColor = Color.FromArgb(alpha,
                (int)Math.Round((double)ahsColor.R * value),
                (int)Math.Round((double)ahsColor.G * value),
                (int)Math.Round((double)ahsColor.B * value));

            return ahsvColor;
        }

        public static float GetSaturationOfHsv() => 0;

        private static Color GetAbsoluteHueColor(int alpha, float hue)
        {
            hue = hue % 360;
            var unit = 255d / 60;

            switch (hue)
            {
                default:
                case float h when h < 0 || h > 360: throw new NotSupportedException();

                case float h when h == 0
                               || h == 360: // R
                    return Color.FromArgb(alpha, 255, 0, 0);

                case float h when h < 60:   // R +G
                    return Color.FromArgb(alpha, 255, (int)(unit * (h % 60)), 0);

                case float h when h == 60:  // RG
                    return Color.FromArgb(alpha, 255, 255, 0);

                case float h when h < 120:  // RG -R
                    return Color.FromArgb(alpha, 255 - (int)(unit * (h % 60)), 255, 0);

                case float h when h == 120: // G
                    return Color.FromArgb(alpha, 0, 255, 0);

                case float h when h < 180:  // G +B
                    return Color.FromArgb(alpha, 0, 255, (int)(unit * (h % 60)));

                case float h when h == 180: // GB
                    return Color.FromArgb(alpha, 0, 255, 255);

                case float h when h < 240:  // GB -G
                    return Color.FromArgb(alpha, 0, 255 - (int)(unit * (h % 60)), 255);

                case float h when h == 240: // B
                    return Color.FromArgb(alpha, 0, 0, 255);

                case float h when h < 300:  // B +R
                    return Color.FromArgb(alpha, (int)(unit * (h % 60)), 0, 255);

                case float h when h == 300: // BR
                    return Color.FromArgb(alpha, 255, 0, 255);

                case float h when h < 360:  // BR -B
                    return Color.FromArgb(alpha, 255, 0, 255 - (int)(unit * (h % 60)));
            }
        }

    }
#endif

}
