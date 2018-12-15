namespace Dawnx.Generators
{
    public interface IGenerator<T>
    {
        T[] Take(int count);
        T TakeOne();
    }

}