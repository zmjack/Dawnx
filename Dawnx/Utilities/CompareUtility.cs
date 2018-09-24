namespace Dawnx.Utilities
{
    public static class CompareUtility
    {
        public static bool UsingEquals(object left, object right)
        {
            if (left is null && right is null) return true;
            else if (left is null && !(right is null)) return false;
            else if (!(left is null) && right is null) return false;
            else return left.Equals(right);
        }

    }
}
