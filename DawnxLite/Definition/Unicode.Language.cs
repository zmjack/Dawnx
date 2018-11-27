using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Definition
{
    public static partial class Unicode
    {
        public static class Language
        {
            /// <summary>
            /// The Chinese includes
            ///     CJK_Radicals_Supplement,
            ///     CJK_Unified_Ideographs_Extension_A,
            ///     CJK_Unified_Ideographs,
            ///     CJK_Compatibility_Ideographs.
            /// </summary>
            public const string Chinese = "[" +
                @"\u2E80-\u2EFF\u3400-\u4DBF\u4E00-\u9FFF\uF900-\uFAFF" + "]";

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
                @"\u2E80-\u2EFF\u3400-\u4DBF\u4E00-\u9FFF\uF900-\uFAFF" +
                @"\u3040-\u309F\u31F0-\u31FF" +
                @"\uFF00-\uFFEF" + "]";

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
                @"\u2E80-\u2EFF\u3400-\u4DBF\u4E00-\u9FFF\uF900-\uFAFF" +
                @"\u1100-\u11FF\u3130-\u318F\uAC00-\uD7AF" +
                @"\uFF00-\uFFEF" + "]";

        }

    }
}
