namespace Dawnx.Entity
{
    /// <summary>
    /// Use <see cref="IEntity"/> to define entity classes to get some useful extension methods.
    /// </summary>
    public interface IEntity { }

    /// <summary>
    /// Use <see cref="IEntity"/> to define entity classes to get some useful extension methods.
    /// </summary>
    public interface IEntity<TSelf> : IEntity
        where TSelf : class, IEntity<TSelf>, new()
    {
    }

}
