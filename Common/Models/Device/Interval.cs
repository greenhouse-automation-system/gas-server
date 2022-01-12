namespace Gas.Common.Models.Device;

public class Interval
{
    public int Begin { get; set; }
    public int End { get; set; }
    public int Size
    {
        get { return End - Begin; }
    }

    public Interval ZeroEnd()
    {
        return new Interval
        {
            Begin = 0,
            End = End,
        };
    }

    public Interval BeginMax(int max)
    {
        return new Interval
        {
            Begin = Begin,
            End = max,
        };
    }

}