using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Chinese
{
    public class PinyinString
    {
        public string Chinese { get; }
        private string _Pinyin;
        private string _PurePinyin;

        public PinyinString(string chinese)
        {
            Chinese = chinese;
        }

        public string Pinyin
        {
            get
            {
                if (_Pinyin == null)
                {
                    var sb = new StringBuilder();
                    foreach (var ch in Chinese)
                    {
                        var chineseChar = new ChineseChar(ch);
                        var pinyin = chineseChar.Pinyins[0].ToString();

                        sb.Append(pinyin);
                    }

                    _Pinyin = sb.ToString();
                }

                return _Pinyin;
            }
        }

        public string PurePinyin
        {
            get
            {
                if (_PurePinyin == null)
                {
                    var sb = new StringBuilder();
                    foreach (var ch in Chinese)
                    {
                        var chineseChar = new ChineseChar(ch);
                        var pinyin = chineseChar.Pinyins[0].ToString();

                        sb.Append(pinyin.Substring(0, pinyin.Length - 1));
                    }

                    _PurePinyin = sb.ToString();
                }

                return _PurePinyin;
            }
        }

    }

}
