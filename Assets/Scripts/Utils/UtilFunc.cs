
public class UtilFunc
{
    public static string FormatSecondsToMinutes(int sec)
    {
        string minutes = FormatTimeToString(sec / 60);
        string seconds = FormatTimeToString(sec % 60);
        return string.Format("{0}:{1}", minutes, seconds);
    }

    private static string FormatTimeToString(int time)
    {
        if (time < 10)
        {
            return string.Format("0{0}", time);
        }
        return time.ToString();
    }

    /// <summary>
    /// Change value from one range to another
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fromMin"></param>
    /// <param name="fromMax"></param>
    /// <param name="toMin"></param>
    /// <param name="toMax"></param>
    /// <returns></returns>
    public static float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) / (fromMax - fromMin) * (toMax - toMin);
    }
}
