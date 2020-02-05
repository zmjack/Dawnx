using NStandard;
using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace Dawnx.CVision
{
    public static class ComputerVision
    {
        public delegate bool MatchFilterDelegate(int index, double value);

        public static void Rectangle(Mat source, MatchResult[] matchResults, Scalar color, int thickness = 1, LineTypes lineType = LineTypes.Link8, int shift = 0)
        {
            foreach (var result in matchResults)
            {
                Cv2.Rectangle(source, result.LeftTop, result.RightBottom, color, thickness, lineType, shift);
            }
        }

        public static MatchResult[] MatchTemplate(Mat source, Mat template, int? take = null, double? max = null)
        {
            var resultList = new List<MatchResult>();
            var match = new Mat();

            match.Create(source.Cols - template.Cols + 1, source.Rows - template.Cols + 1, MatType.CV_32FC1);
            Cv2.MatchTemplate(source, template, match, TemplateMatchModes.SqDiff);
            Cv2.MinMaxLoc(match, out var minValue, out double maxValue);

            for (int index = 0; ; index++)
            {
                Cv2.MinMaxLoc(match, out var _minValue, out var _maxValue, out var minLocation, out var maxLocation);
                if (minValue <= _minValue && _minValue <= maxValue)
                {
                    if (_minValue > max) break;
                    if (take.HasValue ? index >= take.Value : false) break;

                    resultList.Add(new MatchResult
                    {
                        LeftTop = minLocation,
                        RightBottom = new OpenCvSharp.Point(minLocation.X + template.Cols - 1, minLocation.Y + template.Rows - 1),
                        Value = _minValue,
                    });
                    var rowStart = (minLocation.Y - template.Rows).For(x => x < 0 ? 0 : x);
                    var rowStop = (minLocation.Y + template.Rows).For(x => x > match.Rows ? match.Rows : x);
                    var colStart = (minLocation.X - template.Cols).For(x => x < 0 ? 0 : x);
                    var colStop = (minLocation.X + template.Cols).For(x => x > match.Cols ? match.Cols : x);
                    var length = (rowStop - rowStart) * (colStop - colStart);

                    match[minLocation.Y, rowStop, minLocation.X, colStop].GetArray(out float[] s);
                    match[rowStart, rowStop, colStart, colStop].SetArray(new float[length].Let(() => float.MaxValue));
                }
                else break;
            }

            return resultList.ToArray();
        }

    }
}
