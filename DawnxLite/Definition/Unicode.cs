using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Definition
{
    public static partial class Unicode
    {
        /// <summary>
        /// The Chinese includes
        ///     CJK_Radicals_Supplement,
        ///     CJK_Unified_Ideographs_Extension_A,
        ///     CJK_Unified_Ideographs,
        ///     CJK_Compatibility_Ideographs.
        /// </summary>
        public const string Chinese = "[" +
            Origin.CJK_Radicals_Supplement +
            Origin.CJK_Unified_Ideographs_Extension_A +
            Origin.CJK_Unified_Ideographs +
            Origin.CJK_Compatibility_Ideographs + "]";

        /// <summary>
        /// The Japanese includes
        ///     CJK_Radicals_Supplement,
        ///     CJK_Unified_Ideographs_Extension_A,
        ///     CJK_Unified_Ideographs,
        ///     CJK_Compatibility_Ideographs,
        ///     Hiragana,
        ///     Katakana_Phonetic_Extensions,
        ///     Halfwidth_and_Fullwidth_Forms.
        /// </summary>
        public const string Japanese = "[" +
            Origin.CJK_Radicals_Supplement +
            Origin.CJK_Unified_Ideographs_Extension_A +
            Origin.CJK_Unified_Ideographs +
            Origin.CJK_Compatibility_Ideographs +
            Origin.Hiragana +
            Origin.Katakana_Phonetic_Extensions +
            Origin.Halfwidth_and_Fullwidth_Forms + "]";

        /// <summary>
        /// The Korean includes
        ///     CJK_Radicals_Supplement,
        ///     CJK_Unified_Ideographs_Extension_A,
        ///     CJK_Unified_Ideographs,
        ///     CJK_Compatibility_Ideographs,
        ///     Hangul_Jamo,
        ///     Hangul_Compatibility_Jamo,
        ///     Hangul_Syllables,
        ///     Halfwidth_and_Fullwidth_Forms.
        /// </summary>
        public const string Korean = "[" +
            Origin.CJK_Radicals_Supplement +
            Origin.CJK_Unified_Ideographs_Extension_A +
            Origin.CJK_Unified_Ideographs +
            Origin.CJK_Compatibility_Ideographs +
            Origin.Hangul_Jamo +
            Origin.Hangul_Compatibility_Jamo +
            Origin.Hangul_Syllables +
            Origin.Halfwidth_and_Fullwidth_Forms + "]";

    }
}
