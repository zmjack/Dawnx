using Dawnx.Utilities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    /// <summary>
    /// Use <see cref="IEntity"/> to define entity classes to get some useful extension methods.
    /// </summary>
    public interface IEntity { }

    public static class DawnIEntity
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
            //Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .Where(x => !x.GetCustomAttributes(false).For(attrs =>
                {
                    return attrs.Any(attr => attr is NotAcceptable)
                        || attrs.Any(attr => attr.GetType().Name.In(new[]
                        {
                            "KeyAttribute",
                            nameof(TrackCreationTimeAttribute),
                            nameof(TrackLastWriteTimeAttribute)
                        }));
                }))
                .Where(x => x.PropertyType.FullName.In(BasicTypeUtility.AllFullNames) || x.PropertyType.IsValueType);

            //Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model, null), null);

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

            //Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(a => propNames.Contains(a.Name));

            //Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model, null), null);

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

            //Filter
            var type = typeof(TEntity);
            var props = type.GetProperties()
                .Where(x => x.CanRead && x.CanWrite)
                .Where(x => !x.GetCustomAttributes(false).For(attrs =>
                {
                    return attrs.Any(attr => attr is NotAcceptable)
                        || attrs.Any(attr => attr.GetType().Name.In(new[]
                        {
                            "KeyAttribute",
                            nameof(TrackCreationTimeAttribute),
                            nameof(TrackLastWriteTimeAttribute)
                        }));
                }))
                .Where(x => x.PropertyType.FullName.In(BasicTypeUtility.AllFullNames) || x.PropertyType.IsValueType);

            props = props.Where(x => !propNames.Contains(x.Name));

            //Copy values
            foreach (var prop in props)
                prop.SetValue(@this, prop.GetValue(model, null), null);

            return @this;
        }

    }
}
