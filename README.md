# Dawnx

Dawnx is rapid development support library for .net standard 2.0+.

Simpler coding, more readable.



## Why I create this library?

C# is a good language, but it can be simpler. 

C# has been around for 16 years. In every major version upgrade, Microsoft has added many useful features to this young language. These features make it easier for us to write programs, but this is only stand at the language level. So, I created this library to extend its application capabilities in some common application scenarios.

This library consists of three parts: 

- Extension Functions
- Warppers
- Fully innovative part



## Basic methods

### For

Returns a new instance from another instance. This method can help you write more compact code, especially if you want to initialize some properties which are computed by complex logic in a class or struct without use another variable.

```C#
new SimpleClass
{
    Today = DateTime.Now.For(_ =>
    {
        if (_.Day <= 7) return "Holiday";
        else if (_.DayOfWeek == DayOfWeek.Saturday)
            || _.DayOfWeek == DayOfWeek.Sunday) return "Weekend";
        else return "Weekday";
    }),
}
```



## Next

Document is coming...

