## Dawnx for EntityFrameworkCore

### View SQL string

```C#
using Dawnx.AspNetCore;

...
    IQueryable queryable = (...);
    var s = queryable.ToSql();
...
```





