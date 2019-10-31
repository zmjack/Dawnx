﻿namespace Def
{
    public static class UnicodeMap
    {
        // Refer: https://unicode-table.com/en/blocks/control-character/
        public const string Control_character = @"\u0000-\u001F";
        public const string Basic_Latin = @"\u0020-\u007F";
        public const string Latin_1_Supplement = @"\u0080-\u00FF";
        public const string Latin_Extended_A = @"\u0100-\u017F";
        public const string Latin_Extended_B = @"\u0180-\u024F";
        public const string IPA_Extensions = @"\u0250-\u02AF";
        public const string Spacing_Modifier_Letters = @"\u02B0-\u02FF";
        public const string Combining_Diacritical_Marks = @"\u0300-\u036F";
        public const string Greek_and_Coptic = @"\u0370-\u03FF";
        public const string Cyrillic = @"\u0400-\u04FF";
        public const string Cyrillic_Supplement = @"\u0500-\u052F";
        public const string Armenian = @"\u0530-\u058F";
        public const string Hebrew = @"\u0590-\u05FF";
        public const string Arabic = @"\u0600-\u06FF";
        public const string Syriac = @"\u0700-\u074F";
        public const string Arabic_Supplement = @"\u0750-\u077F";
        public const string Thaana = @"\u0780-\u07BF";
        public const string NKo = @"\u07C0-\u07FF";
        public const string Samaritan = @"\u0800-\u083F";
        public const string Mandaic = @"\u0840-\u085F";
        public const string Syriac_Supplement = @"\u0860-\u086F";
        public const string Arabic_Extended_A = @"\u08A0-\u08FF";
        public const string Devanagari = @"\u0900-\u097F";
        public const string Bengali = @"\u0980-\u09FF";
        public const string Gurmukhi = @"\u0A00-\u0A7F";
        public const string Gujarati = @"\u0A80-\u0AFF";
        public const string Oriya = @"\u0B00-\u0B7F";
        public const string Tamil = @"\u0B80-\u0BFF";
        public const string Telugu = @"\u0C00-\u0C7F";
        public const string Kannada = @"\u0C80-\u0CFF";
        public const string Malayalam = @"\u0D00-\u0D7F";
        public const string Sinhala = @"\u0D80-\u0DFF";
        public const string Thai = @"\u0E00-\u0E7F";
        public const string Lao = @"\u0E80-\u0EFF";
        public const string Tibetan = @"\u0F00-\u0FFF";
        public const string Myanmar = @"\u1000-\u109F";
        public const string Georgian = @"\u10A0-\u10FF";
        public const string Hangul_Jamo = @"\u1100-\u11FF";
        public const string Ethiopic = @"\u1200-\u137F";
        public const string Ethiopic_Supplement = @"\u1380-\u139F";
        public const string Cherokee = @"\u13A0-\u13FF";
        public const string Unified_Canadian_Aboriginal_Syllabics = @"\u1400-\u167F";
        public const string Ogham = @"\u1680-\u169F";
        public const string Runic = @"\u16A0-\u16FF";
        public const string Tagalog = @"\u1700-\u171F";
        public const string Hanunoo = @"\u1720-\u173F";
        public const string Buhid = @"\u1740-\u175F";
        public const string Tagbanwa = @"\u1760-\u177F";
        public const string Khmer = @"\u1780-\u17FF";
        public const string Mongolian = @"\u1800-\u18AF";
        public const string Unified_Canadian_Aboriginal_Syllabics_Extended = @"\u18B0-\u18FF";
        public const string Limbu = @"\u1900-\u194F";
        public const string Tai_Le = @"\u1950-\u197F";
        public const string New_Tai_Lue = @"\u1980-\u19DF";
        public const string Khmer_Symbols = @"\u19E0-\u19FF";
        public const string Buginese = @"\u1A00-\u1A1F";
        public const string Tai_Tham = @"\u1A20-\u1AAF";
        public const string Combining_Diacritical_Marks_Extended = @"\u1AB0-\u1AFF";
        public const string Balinese = @"\u1B00-\u1B7F";
        public const string Sundanese = @"\u1B80-\u1BBF";
        public const string Batak = @"\u1BC0-\u1BFF";
        public const string Lepcha = @"\u1C00-\u1C4F";
        public const string Ol_Chiki = @"\u1C50-\u1C7F";
        public const string Cyrillic_Extended_C = @"\u1C80-\u1C8F";
        public const string Sundanese_Supplement = @"\u1CC0-\u1CCF";
        public const string Vedic_Extensions = @"\u1CD0-\u1CFF";
        public const string Phonetic_Extensions = @"\u1D00-\u1D7F";
        public const string Phonetic_Extensions_Supplement = @"\u1D80-\u1DBF";
        public const string Combining_Diacritical_Marks_Supplement = @"\u1DC0-\u1DFF";
        public const string Latin_Extended_Additional = @"\u1E00-\u1EFF";
        public const string Greek_Extended = @"\u1F00-\u1FFF";
        public const string General_Punctuation = @"\u2000-\u206F";
        public const string Superscripts_and_Subscripts = @"\u2070-\u209F";
        public const string Currency_Symbols = @"\u20A0-\u20CF";
        public const string Combining_Diacritical_Marks_for_Symbols = @"\u20D0-\u20FF";
        public const string Letterlike_Symbols = @"\u2100-\u214F";
        public const string Number_Forms = @"\u2150-\u218F";
        public const string Arrows = @"\u2190-\u21FF";
        public const string Mathematical_Operators = @"\u2200-\u22FF";
        public const string Miscellaneous_Technical = @"\u2300-\u23FF";
        public const string Control_Pictures = @"\u2400-\u243F";
        public const string Optical_Character_Recognition = @"\u2440-\u245F";
        public const string Enclosed_Alphanumerics = @"\u2460-\u24FF";
        public const string Box_Drawing = @"\u2500-\u257F";
        public const string Block_Elements = @"\u2580-\u259F";
        public const string Geometric_Shapes = @"\u25A0-\u25FF";
        public const string Miscellaneous_Symbols = @"\u2600-\u26FF";
        public const string Dingbats = @"\u2700-\u27BF";
        public const string Miscellaneous_Mathematical_Symbols_A = @"\u27C0-\u27EF";
        public const string Supplemental_Arrows_A = @"\u27F0-\u27FF";
        public const string Braille_Patterns = @"\u2800-\u28FF";
        public const string Supplemental_Arrows_B = @"\u2900-\u297F";
        public const string Miscellaneous_Mathematical_Symbols_B = @"\u2980-\u29FF";
        public const string Supplemental_Mathematical_Operators = @"\u2A00-\u2AFF";
        public const string Miscellaneous_Symbols_and_Arrows = @"\u2B00-\u2BFF";
        public const string Glagolitic = @"\u2C00-\u2C5F";
        public const string Latin_Extended_C = @"\u2C60-\u2C7F";
        public const string Coptic = @"\u2C80-\u2CFF";
        public const string Georgian_Supplement = @"\u2D00-\u2D2F";
        public const string Tifinagh = @"\u2D30-\u2D7F";
        public const string Ethiopic_Extended = @"\u2D80-\u2DDF";
        public const string Cyrillic_Extended_A = @"\u2DE0-\u2DFF";
        public const string Supplemental_Punctuation = @"\u2E00-\u2E7F";
        public const string CJK_Radicals_Supplement = @"\u2E80-\u2EFF";
        public const string Kangxi_Radicals = @"\u2F00-\u2FDF";
        public const string Ideographic_Description_Characters = @"\u2FF0-\u2FFF";
        public const string CJK_Symbols_and_Punctuation = @"\u3000-\u303F";
        public const string Hiragana = @"\u3040-\u309F";
        public const string Katakana = @"\u30A0-\u30FF";
        public const string Bopomofo = @"\u3100-\u312F";
        public const string Hangul_Compatibility_Jamo = @"\u3130-\u318F";
        public const string Kanbun = @"\u3190-\u319F";
        public const string Bopomofo_Extended = @"\u31A0-\u31BF";
        public const string CJK_Strokes = @"\u31C0-\u31EF";
        public const string Katakana_Phonetic_Extensions = @"\u31F0-\u31FF";
        public const string Enclosed_CJK_Letters_and_Months = @"\u3200-\u32FF";
        public const string CJK_Compatibility = @"\u3300-\u33FF";
        public const string CJK_Unified_Ideographs_Extension_A = @"\u3400-\u4DBF";
        public const string Yijing_Hexagram_Symbols = @"\u4DC0-\u4DFF";
        public const string CJK_Unified_Ideographs = @"\u4E00-\u9FFF";
        public const string Yi_Syllables = @"\uA000-\uA48F";
        public const string Yi_Radicals = @"\uA490-\uA4CF";
        public const string Lisu = @"\uA4D0-\uA4FF";
        public const string Vai = @"\uA500-\uA63F";
        public const string Cyrillic_Extended_B = @"\uA640-\uA69F";
        public const string Bamum = @"\uA6A0-\uA6FF";
        public const string Modifier_Tone_Letters = @"\uA700-\uA71F";
        public const string Latin_Extended_D = @"\uA720-\uA7FF";
        public const string Syloti_Nagri = @"\uA800-\uA82F";
        public const string Common_Indic_Number_Forms = @"\uA830-\uA83F";
        public const string Phags_pa = @"\uA840-\uA87F";
        public const string Saurashtra = @"\uA880-\uA8DF";
        public const string Devanagari_Extended = @"\uA8E0-\uA8FF";
        public const string Kayah_Li = @"\uA900-\uA92F";
        public const string Rejang = @"\uA930-\uA95F";
        public const string Hangul_Jamo_Extended_A = @"\uA960-\uA97F";
        public const string Javanese = @"\uA980-\uA9DF";
        public const string Myanmar_Extended_B = @"\uA9E0-\uA9FF";
        public const string Cham = @"\uAA00-\uAA5F";
        public const string Myanmar_Extended_A = @"\uAA60-\uAA7F";
        public const string Tai_Viet = @"\uAA80-\uAADF";
        public const string Meetei_Mayek_Extensions = @"\uAAE0-\uAAFF";
        public const string Ethiopic_Extended_A = @"\uAB00-\uAB2F";
        public const string Latin_Extended_E = @"\uAB30-\uAB6F";
        public const string Cherokee_Supplement = @"\uAB70-\uABBF";
        public const string Meetei_Mayek = @"\uABC0-\uABFF";
        public const string Hangul_Syllables = @"\uAC00-\uD7AF";
        public const string Hangul_Jamo_Extended_B = @"\uD7B0-\uD7FF";
        public const string High_Surrogates = @"\uD800-\uDB7F";
        public const string High_Private_Use_Surrogates = @"\uDB80-\uDBFF";
        public const string Low_Surrogates = @"\uDC00-\uDFFF";
        public const string Private_Use_Area = @"\uE000-\uF8FF";
        public const string CJK_Compatibility_Ideographs = @"\uF900-\uFAFF";
        public const string Alphabetic_Presentation_Forms = @"\uFB00-\uFB4F";
        public const string Arabic_Presentation_Forms_A = @"\uFB50-\uFDFF";
        public const string Variation_Selectors = @"\uFE00-\uFE0F";
        public const string Vertical_Forms = @"\uFE10-\uFE1F";
        public const string Combining_Half_Marks = @"\uFE20-\uFE2F";
        public const string CJK_Compatibility_Forms = @"\uFE30-\uFE4F";
        public const string Small_Form_Variants = @"\uFE50-\uFE6F";
        public const string Arabic_Presentation_Forms_B = @"\uFE70-\uFEFF";
        public const string Halfwidth_and_Fullwidth_Forms = @"\uFF00-\uFFEF";
        public const string Specials = @"\uFFF0-\uFFFF";
        public const string Linear_B_Syllabary = @"\u10000-\u1007F";
        public const string Linear_B_Ideograms = @"\u10080-\u100FF";
        public const string Aegean_Numbers = @"\u10100-\u1013F";
        public const string Ancient_Greek_Numbers = @"\u10140-\u1018F";
        public const string Ancient_Symbols = @"\u10190-\u101CF";
        public const string Phaistos_Disc = @"\u101D0-\u101FF";
        public const string Lycian = @"\u10280-\u1029F";
        public const string Carian = @"\u102A0-\u102DF";
        public const string Coptic_Epact_Numbers = @"\u102E0-\u102FF";
        public const string Old_Italic = @"\u10300-\u1032F";
        public const string Gothic = @"\u10330-\u1034F";
        public const string Old_Permic = @"\u10350-\u1037F";
        public const string Ugaritic = @"\u10380-\u1039F";
        public const string Old_Persian = @"\u103A0-\u103DF";
        public const string Deseret = @"\u10400-\u1044F";
        public const string Shavian = @"\u10450-\u1047F";
        public const string Osmanya = @"\u10480-\u104AF";
        public const string Osage = @"\u104B0-\u104FF";
        public const string Elbasan = @"\u10500-\u1052F";
        public const string Caucasian_Albanian = @"\u10530-\u1056F";
        public const string Linear_A = @"\u10600-\u1077F";
        public const string Cypriot_Syllabary = @"\u10800-\u1083F";
        public const string Imperial_Aramaic = @"\u10840-\u1085F";
        public const string Palmyrene = @"\u10860-\u1087F";
        public const string Nabataean = @"\u10880-\u108AF";
        public const string Hatran = @"\u108E0-\u108FF";
        public const string Phoenician = @"\u10900-\u1091F";
        public const string Lydian = @"\u10920-\u1093F";
        public const string Meroitic_Hieroglyphs = @"\u10980-\u1099F";
        public const string Meroitic_Cursive = @"\u109A0-\u109FF";
        public const string Kharoshthi = @"\u10A00-\u10A5F";
        public const string Old_South_Arabian = @"\u10A60-\u10A7F";
        public const string Old_North_Arabian = @"\u10A80-\u10A9F";
        public const string Manichaean = @"\u10AC0-\u10AFF";
        public const string Avestan = @"\u10B00-\u10B3F";
        public const string Inscriptional_Parthian = @"\u10B40-\u10B5F";
        public const string Inscriptional_Pahlavi = @"\u10B60-\u10B7F";
        public const string Psalter_Pahlavi = @"\u10B80-\u10BAF";
        public const string Old_Turkic = @"\u10C00-\u10C4F";
        public const string Old_Hungarian = @"\u10C80-\u10CFF";
        public const string Rumi_Numeral_Symbols = @"\u10E60-\u10E7F";
        public const string Brahmi = @"\u11000-\u1107F";
        public const string Kaithi = @"\u11080-\u110CF";
        public const string Sora_Sompeng = @"\u110D0-\u110FF";
        public const string Chakma = @"\u11100-\u1114F";
        public const string Mahajani = @"\u11150-\u1117F";
        public const string Sharada = @"\u11180-\u111DF";
        public const string Sinhala_Archaic_Numbers = @"\u111E0-\u111FF";
        public const string Khojki = @"\u11200-\u1124F";
        public const string Multani = @"\u11280-\u112AF";
        public const string Khudawadi = @"\u112B0-\u112FF";
        public const string Grantha = @"\u11300-\u1137F";
        public const string Newa = @"\u11400-\u1147F";
        public const string Tirhuta = @"\u11480-\u114DF";
        public const string Siddham = @"\u11580-\u115FF";
        public const string Modi = @"\u11600-\u1165F";
        public const string Mongolian_Supplement = @"\u11660-\u1167F";
        public const string Takri = @"\u11680-\u116CF";
        public const string Ahom = @"\u11700-\u1173F";
        public const string Warang_Citi = @"\u118A0-\u118FF";
        public const string Zanabazar_Square = @"\u11A00-\u11A4F";
        public const string Soyombo = @"\u11A50-\u11AAF";
        public const string Pau_Cin_Hau = @"\u11AC0-\u11AFF";
        public const string Bhaiksuki = @"\u11C00-\u11C6F";
        public const string Marchen = @"\u11C70-\u11CBF";
        public const string Masaram_Gondi = @"\u11D00-\u11D5F";
        public const string Cuneiform = @"\u12000-\u123FF";
        public const string Cuneiform_Numbers_and_Punctuation = @"\u12400-\u1247F";
        public const string Early_Dynastic_Cuneiform = @"\u12480-\u1254F";
        public const string Egyptian_Hieroglyphs = @"\u13000-\u1342F";
        public const string Anatolian_Hieroglyphs = @"\u14400-\u1467F";
        public const string Bamum_Supplement = @"\u16800-\u16A3F";
        public const string Mro = @"\u16A40-\u16A6F";
        public const string Bassa_Vah = @"\u16AD0-\u16AFF";
        public const string Pahawh_Hmong = @"\u16B00-\u16B8F";
        public const string Miao = @"\u16F00-\u16F9F";
        public const string Ideographic_Symbols_and_Punctuation = @"\u16FE0-\u16FFF";
        public const string Tangut = @"\u17000-\u187FF";
        public const string Tangut_Components = @"\u18800-\u18AFF";
        public const string Kana_Supplement = @"\u1B000-\u1B0FF";
        public const string Kana_Extended_A = @"\u1B100-\u1B12F";
        public const string Nushu = @"\u1B170-\u1B2FF";
        public const string Duployan = @"\u1BC00-\u1BC9F";
        public const string Shorthand_Format_Controls = @"\u1BCA0-\u1BCAF";
        public const string Byzantine_Musical_Symbols = @"\u1D000-\u1D0FF";
        public const string Musical_Symbols = @"\u1D100-\u1D1FF";
        public const string Ancient_Greek_Musical_Notation = @"\u1D200-\u1D24F";
        public const string Tai_Xuan_Jing_Symbols = @"\u1D300-\u1D35F";
        public const string Counting_Rod_Numerals = @"\u1D360-\u1D37F";
        public const string Mathematical_Alphanumeric_Symbols = @"\u1D400-\u1D7FF";
        public const string Sutton_SignWriting = @"\u1D800-\u1DAAF";
        public const string Glagolitic_Supplement = @"\u1E000-\u1E02F";
        public const string Mende_Kikakui = @"\u1E800-\u1E8DF";
        public const string Adlam = @"\u1E900-\u1E95F";
        public const string Arabic_Mathematical_Alphabetic_Symbols = @"\u1EE00-\u1EEFF";
        public const string Mahjong_Tiles = @"\u1F000-\u1F02F";
        public const string Domino_Tiles = @"\u1F030-\u1F09F";
        public const string Playing_Cards = @"\u1F0A0-\u1F0FF";
        public const string Enclosed_Alphanumeric_Supplement = @"\u1F100-\u1F1FF";
        public const string Enclosed_Ideographic_Supplement = @"\u1F200-\u1F2FF";
        public const string Miscellaneous_Symbols_and_Pictographs = @"\u1F300-\u1F5FF";
        public const string Emoticons_Emoji = @"\u1F600-\u1F64F";
        public const string Ornamental_Dingbats = @"\u1F650-\u1F67F";
        public const string Transport_and_Map_Symbols = @"\u1F680-\u1F6FF";
        public const string Alchemical_Symbols = @"\u1F700-\u1F77F";
        public const string Geometric_Shapes_Extended = @"\u1F780-\u1F7FF";
        public const string Supplemental_Arrows_C = @"\u1F800-\u1F8FF";
        public const string Supplemental_Symbols_and_Pictographs = @"\u1F900-\u1F9FF";
        public const string CJK_Unified_Ideographs_Extension_B = @"\u20000-\u2A6DF";
        public const string CJK_Unified_Ideographs_Extension_C = @"\u2A700-\u2B73F";
        public const string CJK_Unified_Ideographs_Extension_D = @"\u2B740-\u2B81F";
        public const string CJK_Unified_Ideographs_Extension_E = @"\u2B820-\u2CEAF";
        public const string CJK_Unified_Ideographs_Extension_F = @"\u2CEB0-\u2EBEF";
        public const string CJK_Compatibility_Ideographs_Supplement = @"\u2F800-\u2FA1F";
        public const string Tags = @"\uE0000-\uE007F";
        public const string Variation_Selectors_Supplement = @"\uE0100-\uE01EF";

    }
}
