This is a ticketing scenario. In this scenario, if many people buy the ticket in the same time, the lock(*string*) will be going wrong. Because the *string* is not a same reference.

```c#
class Program
{
    static string NewConstString => new StringBuilder()
        .Self(_ => _.Append("a")).ToString();
    static int RemainingTicket = 1_000_000;

    static void Main(string[] args)
    {
        Sell(1_000_000);
        Console.WriteLine(RemainingTicket);
    }

    static void Sell(int level)
    {
        using (var probe = PerformanceProbe.Create(nameof(Sell)))
        {
            var ret = Concurrency.Run((runId) =>
            {
                lock (NewConstString)
                {
                    return RemainingTicket--;
                }
            }, level);
        }
    }
}
```
The result may be an nonzero number:

```
41527
```

The correct way is use ***lock (Locker.Get(NewConstString))*** to instead of ***lock(NewConstString)***:

```C#
class Program
{
    static string NewConstString => new StringBuilder()
        .Self(_ => _.Append("a")).ToString();
    static int RemainingTicket = 1_000_000;

    static void Main(string[] args)
    {
        Sell(1_000_000);
        Console.WriteLine(RemainingTicket);
    }

    static void Sell(int level)
    {
        using (var probe = PerformanceProbe.Create(nameof(Sell)))
        {
            var ret = Concurrency.Run((runId) =>
            {
                lock (Locker.Get(NewConstString))
                {
                    return RemainingTicket--;
                }
            }, level);
        }
    }
}
```

The result is:

```
0
```