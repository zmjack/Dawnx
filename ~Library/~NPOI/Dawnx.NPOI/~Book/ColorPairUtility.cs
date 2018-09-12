using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dawnx.NPOI
{
    public static class ColorPairUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDeclare"></typeparam>
        /// <param name="declareObject"></param>
        /// <param name="indexPropExp">Use this function when workbook is "Excel 2003"</param>
        /// <param name="indexPropExp">Use this function when workbook is "Excel 2007"</param>
        /// <returns></returns>
        public static RGBColor Get<TDeclare>(TDeclare declareObject, Func<TDeclare, short> indexProp, Func<TDeclare, IColor> colorProp)
        {
            var index = indexProp(declareObject);

            if (index != 0) return RGBColor.ParseIndexed(index);
            else return colorProp(declareObject)?.For(_ => new RGBColor(_.RGB));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDeclare"></typeparam>
        /// <param name="declareObject"></param>
        /// <param name="indexPropExp">Use this expression when workbook is "Excel 2003"</param>
        /// <param name="colorPropExp">Use this expression when workbook is "Excel 2007"</param>
        /// <param name="value"></param>
        public static void Set<TDeclare>(
            TDeclare declareObject,
            Expression<Func<TDeclare, short>> indexPropExp,
            Expression<Func<TDeclare, IColor>> colorPropExp,
            RGBColor value)
        {
            //TODO: Use TypeReflectionCacheContainer to optimize it in the futrue.
            var indexProp = (indexPropExp.Body as MemberExpression).For(_ => typeof(TDeclare).GetProperty(_.Member.Name));
            var colorProp = (colorPropExp.Body as MemberExpression)?.For(_ => typeof(TDeclare).GetProperty(_.Member.Name));

            if (colorProp != null)
                colorProp?.SetValue(declareObject, value?.For(_ => new XSSFColor(_.Bytes)));
            else indexProp.SetValue(declareObject, value?.Index ?? 0);
        }

    }

}
