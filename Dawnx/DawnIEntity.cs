using Dawnx.Entity;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEntity
    {
        /// <summary>
        /// Accept all property values which are can be read and write from another model.
        ///     (Only ValueTypes, exclude 'KeyAttribute' and attributes which are start with 'Track')
        /// </summary>
        /// <typeparam name="TEntity">Instance of IEntity</typeparam>
        /// <param name="this">Source model</param>
        /// <param name="model">The model which provide values</param>
        /// <returns></returns>
        public static TEntity Accept<TEntity>(this TEntity @this, TEntity model)
            where TEntity : class, IEntity
        {
            // Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .Where(x => !x.GetCustomAttributes(false).For(attrs =>
                {
                    return attrs.Any(attr => attr is NotAcceptable)
                        || attrs.Any(attr => attr.GetType().Name.In(new[]
                        {
                            nameof(KeyAttribute),
                            nameof(TrackCreationTimeAttribute),
                            nameof(TrackLastWriteTimeAttribute)
                        }));
                }))
                .Where(x => x.PropertyType.FullName.In(BasicTypeUtility.AllFullNames) || x.PropertyType.IsValueType);

            // Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model));

            return @this;
        }

        /// <summary>
        /// Accept the specified property values from another model.
        /// </summary>
        /// <typeparam name="TEntity">Instance of IEntity</typeparam>
        /// <param name="this">Source model</param>
        /// <param name="model">The model which provide values</param>
        /// <param name="includes">Specifies properties that are applied to the source model.</param>
        /// <returns></returns>
        public static TEntity Accept<TEntity>(this TEntity @this, TEntity model, Expression<Func<TEntity, object>> includes)
            where TEntity : class, IEntity
        {
            string[] propNames;
            switch (includes.Body)
            {
                case MemberExpression exp:
                    propNames = new[] { exp.Member.Name };
                    break;

                case NewExpression exp:
                    propNames = exp.Members.Select(x => x.Name).ToArray();
                    break;

                default:
                    throw new NotSupportedException("This argument 'includes' must be MemberExpression or NewExpression.");
            }

            // Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(a => propNames.Contains(a.Name));

            // Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model));

            return @this;
        }

        /// <summary>
        /// Accept all property values which are can be read and write from another model,
        ///     but exclude the specified properties.
        ///     (Only ValueTypes, exclude 'KeyAttribute' and attributes which are start with 'Track')
        /// </summary>
        /// <typeparam name="TEntity">Instance of IEntity</typeparam>
        /// <param name="this">Source model</param>
        /// <param name="model">The model which provide values</param>
        /// <param name="excludes">Specifies properties that aren't applied to the source model.</param>
        /// <returns></returns>
        public static TEntity AcceptBut<TEntity>(this TEntity @this, TEntity model, Expression<Func<TEntity, object>> excludes)
            where TEntity : class, IEntity
        {
            string[] propNames;
            switch (excludes.Body)
            {
                case MemberExpression exp:
                    propNames = new[] { exp.Member.Name };
                    break;

                case NewExpression exp:
                    propNames = exp.Members.Select(x => x.Name).ToArray();
                    break;

                default:
                    throw new NotSupportedException("This argument 'includes' must be MemberExpression or NewExpression.");
            }

            // Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .Where(x => !x.GetCustomAttributes(false).For(attrs =>
                {
                    return attrs.Any(attr => attr is NotAcceptable)
                        || attrs.Any(attr => attr.GetType().Name.In(new[]
                        {
                            nameof(KeyAttribute),
                            nameof(TrackCreationTimeAttribute),
                            nameof(TrackLastWriteTimeAttribute)
                        }));
                }))
                .Where(x => x.PropertyType.FullName.In(BasicTypeUtility.AllFullNames) || x.PropertyType.IsValueType);

            props = props.Where(x => !propNames.Contains(x.Name));

            // Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model), null);

            return @this;
        }

        public static void SetValue(this IEntity @this, string propName, object value)
            => @this.GetType().GetProperty(propName).SetValue(@this, value);
        public static object GetValue(this IEntity @this, string propName)
            => @this.GetType().GetProperty(propName).GetValue(@this);

        public static string Display(this IEntity @this, LambdaExpression expression, string defaultReturn = "")
            => DataAnnotationUtility.GetDisplayString(@this, expression, defaultReturn);

        public static Dictionary<string, string> ToDisplayDictionary(this IEntity @this)
        {
            // Filter
            var type = @this.GetType();
            var props = type.GetProperties();

            // Copy Values
            var ret = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                var parameter = Expression.Parameter(type);
                var property = Expression.Property(parameter, prop.Name);
                var lambda = Expression.Lambda(property, parameter);

                ret.Add(prop.Name, @this.Display(lambda));
            }

            return ret;
        }

        public static Dictionary<string, string> ToDisplayDictionary(this IEntity @this, params string[] propNames)
        {
            // Filter
            var type = @this.GetType();
            var props = type.GetProperties()
                .Where(a => propNames.Contains(a.Name));

            // Copy Values
            var ret = new Dictionary<string, string>();
            foreach (var prop in props)
                ret.Add(prop.Name, DataAnnotationUtility.GetDisplayString(@this, prop.Name));

            return ret;
        }

    }
}
