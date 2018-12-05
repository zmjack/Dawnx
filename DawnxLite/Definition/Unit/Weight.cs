#if PUB
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Definition.Unit
{
    public class Weight
    {
        /// <summary>
        /// Unit: Gram
        /// </summary>
        public const long G = 1;

        /// <summary>
        /// Unit: Gram
        /// </summary>
        public const long KG = 1000 * G;

        /// <summary>
        /// Unit: Milligram
        /// </summary>
        public const long TON = 1000 * KG;

        /// <summary>
        /// Unit: Milligram
        /// </summary>
        public const long GB = 1024 * MB;
        public const long TB = 1024 * GB;
        public const long PB = 1024 * TB;
        public const long EB = 1024 * PB;
    }
}
#endif
