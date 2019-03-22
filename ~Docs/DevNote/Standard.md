## Standard

### AsVI

If you want to print the index of element which is in **foreach** scope:

```c#
int i = 0;
IEnumerable<int> numbers = new[] { 1, 2, 3 };
foreach(var number in numbers)
{
    Console.Write($"{i}: {number}");
    i++;
}
```

You can use the function **AsVI** to simplify it:

```c#
IEnumerable<int> numbers = new[] { 1, 2, 3 };
foreach(var number in numbers.AsVI())
{
    Console.Write($"{i.Index}: {number.Value}");
}
```

So, we don't need another variable to record the index of each elements.

