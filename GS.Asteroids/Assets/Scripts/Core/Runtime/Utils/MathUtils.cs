namespace GS.Asteroids.Core.Utils
{
    public static class MathUtils
    {
        public static float ReMap(float value, float fromRangeStart, float fromRangeEnd, float toRangeStart, float toRangeEnd)
        {
            return toRangeStart + (value - fromRangeStart) * (toRangeEnd - toRangeStart) / (fromRangeEnd - fromRangeStart);
        }
    }
}
