namespace Dawnx.Generators
{
    public interface IGenerator
    {
        string[] Take(int count);
        string TakeOne();
    }

}