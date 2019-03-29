﻿namespace Dawnx.CConsole
{
    public partial class ConUtility
    {
        /// <summary>
        /// Prints console row.
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="colLengths"></param>
        public static string CreateRow(string[] cols, int[] colLengths)
        {
            var options = new AlignLineOptions
            {
                Lengths = colLengths,
                Borders = new[] { "", " ", "" },
                TreatDBytesTableLineAsByte = false,
                OverflowHidden = true,
            };
            return GetAlignConsoleLine(cols, options);
        }

    }
}
