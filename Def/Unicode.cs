using System;
using System.Collections.Generic;
using System.Text;

namespace Def
{
    public static class Unicode
    {
        /// <summary>
        /// The Chinese includes
        ///     CJK_Radicals_Supplement,
        ///     CJK_Unified_Ideographs_Extension_A,
        ///     CJK_Unified_Ideographs,
        ///     CJK_Compatibility_Ideographs.
        /// </summary>
        public const string Chinese = "[" +
            UnicodeMap.CJK_Radicals_Supplement +
            UnicodeMap.CJK_Unified_Ideographs_Extension_A +
            UnicodeMap.CJK_Unified_Ideographs +
            UnicodeMap.CJK_Compatibility_Ideographs + "]";

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
            UnicodeMap.CJK_Radicals_Supplement +
            UnicodeMap.CJK_Unified_Ideographs_Extension_A +
            UnicodeMap.CJK_Unified_Ideographs +
            UnicodeMap.CJK_Compatibility_Ideographs +
            UnicodeMap.Hiragana +
            UnicodeMap.Katakana_Phonetic_Extensions +
            UnicodeMap.Halfwidth_and_Fullwidth_Forms + "]";

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
            UnicodeMap.CJK_Radicals_Supplement +
            UnicodeMap.CJK_Unified_Ideographs_Extension_A +
            UnicodeMap.CJK_Unified_Ideographs +
            UnicodeMap.CJK_Compatibility_Ideographs +
            UnicodeMap.Hangul_Jamo +
            UnicodeMap.Hangul_Compatibility_Jamo +
            UnicodeMap.Hangul_Syllables +
            UnicodeMap.Halfwidth_and_Fullwidth_Forms + "]";

    }
}
