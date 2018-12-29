## DawnIQureryable

This class provides some extension functions to generate SQL to query database record.



### How to try it?

Firstly, you should install the **SimpleData** package through **Nuget**.

The **SimpleData** package provides a simple database of **Sqlite** and code first definitions of **Northwnd**.

```powershell
install-package SimpleData
```

And, you can use this simple database to test your own queries. Just like this:

```C#
using (var sqlite = new NorthwndContext(SqliteOptions))
{
    var query = sqlite.Employees.Where(x => x.City == "London");
    var sql = query.ToSql();
}
```

If you want to get the generated SQL string, you can use **ToSql()**. This method is defined in **Dawnx**. The above query is:

```sqlite
SELECT "x"."EmployeeID", "x"."Address", "x"."BirthDate", "x"."City", "x"."Country", "x"."Extension", "x"."FirstName", "x"."HireDate", "x"."HomePhone", "x"."LastName", "x"."Notes", "x"."Photo", "x"."PhotoPath", "x"."PostalCode", "x"."Region", "x"."ReportsTo", "x"."Title", "x"."TitleOfCourtesy"
FROM "Employees" AS "x"
WHERE "x"."City" = 'London';
```



### Use simple database (Northwnd)

The Entity Framework provides some basic query extensions, but using it to develop business applications is not simple enough. So, we provide more query extensions to help developers solve their business problems.

First of all, "**DbContext sqlite**" is defined as:

```C#
var sqlite = new NorthwndContext(SqliteOptions);
```

The source of database is "**%userprofile%/.nuget/simpledata/{version}/source/northwnd.db**".



### Extension Simples

- **WhereSearch**

  Queries records which is contains the specified string in one or any fields.

  For example, if you want to query ***Sweet*** in the field ***Description*** (table **Categories**):

  ```C#
  sqlite.Employees.WhereSearch("Steven", x => x.First_Name);
  ```

  This invoke will generate a SQL query string:

  ```sqlite
  SELECT "x"."CategoryID", "x"."CategoryName", "x"."Description", "x"."Picture"
  FROM "Categories" AS "x"
  WHERE instr("x"."Description", 'Sweet') > 0;
  ```

  ----

  And, if you want to query ***An*** in the field ***FirstName*** or ***LastName*** (table ***employees***):

  ```C#
  sqlite.Employees.WhereSearch("An", x => new { x.FirstName, x.LastName })
  ```

  The SQL is:

  ```sqlite
  SELECT "x"."EmployeeID", "x"."Address", "x"."BirthDate", "x"."City", "x"."Country", "x"."Extension", "x"."FirstName", "x"."HireDate", "x"."HomePhone", "x"."LastName", "x"."Notes", "x"."Photo", "x"."PhotoPath", "x"."PostalCode", "x"."Region", "x"."ReportsTo", "x"."Title", "x"."TitleOfCourtesy"
  FROM "Employees" AS "x"
  WHERE (instr("x"."FirstName", 'An') > 0) OR (instr("x"."LastName", 'An') > 0);
  ```

  ----

  As you see, this method supports some abilities to search  single string in more than one field. In some complex scenarios, we also allowed you to query a string in any table which is connected by foreign keys.

  For example, if you want to query 

- **WhereMatch**
  Different from **WhereSearch**, this statement will perform an exact match:

  ```mssql
  /* SQL Server */
  SELECT [x].[Id], [x].[First_Name], [x].[Last_Name]
  FROM [Emplyees] AS [x]
  WHERE [x].[First_Name] = N'Bill' 
  	OR [x].[Last_Name] = N'Bill'
  ```

  ```mysql
  /* MySql */
  SELECT `x`.`Id`, `x`.`First_Name`, `x`.`Last_Name`
  FROM `Emplyees` AS `x`
  WHERE `x`.`First_Name` = 'Bill' 
  	OR `x`.`Last_Name` = 'Bill'
  ```

- **WhereBetween**
  (Not supportted yet)
  Queries records which is start at a time and end at another time.

- **WhereBefore**

- **WhereAfter**

- **WhereMax**

- **WhereMin**

- **OrderByCase**

- **OrderByCaseDescending**

- **WhereMultiOr**

  ```C#
  sqlite.Employees.WhereMultiOr(_ => _
  	.GroupBy(x => x.TitleOfCourtesy)
  	.Select(g => new
  	{
  		TitleOfCourtesy = g.Key,
  		BirthDate = g.Max(x => x.BirthDate),
  	}));
  ```

  This invoke will generated two SQL string, the first is:

  ```sqlite
  SELECT "x"."TitleOfCourtesy", MAX("x"."BirthDate") AS "BirthDate"
  FROM "Employees" AS "x"
  GROUP BY "x"."TitleOfCourtesy";
  ```

  the follow SQL will use all the field of the first result as it's where condition. So, the follow SQL string is:

  ```sqlite
  SELECT "e"."EmployeeID", "e"."Address", "e"."BirthDate", "e"."City", "e"."Country", "e"."Extension", "e"."FirstName", "e"."HireDate", "e"."HomePhone", "e"."LastName", "e"."Notes", "e"."Photo", "e"."PhotoPath", "e"."PostalCode", "e"."Region", "e"."ReportsTo", "e"."Title", "e"."TitleOfCourtesy"
  FROM "Employees" AS "e"
  WHERE (((("e"."TitleOfCourtesy" = 'Dr.') AND ("e"."BirthDate" = '1952-02-19 00:00:00')) OR (("e"."TitleOfCourtesy" = 'Mr.') AND ("e"."BirthDate" = '1963-07-02 00:00:00'))) OR (("e"."TitleOfCourtesy" = 'Mrs.') AND ("e"."BirthDate" = '1937-09-19 00:00:00'))) OR (("e"."TitleOfCourtesy" = 'Ms.') AND ("e"."BirthDate" = '1966-01-27 00:00:00'));
  ```

- **TryUpdate**

  ```C#
  sqlite.Orders
  	.TryUpdate(x => x.Order_Details.Any(y => y.Discount >= 0.02))
      .Set(x => x.ShipCity, "Reims");
  ```

  ```sqlite
  UPDATE "Orders" SET "ShipCity"='Reims' WHERE EXISTS (
      SELECT 1
      FROM "Order Details" AS "y"
      WHERE ("y"."Discount" >= 0.02) AND ("Orders"."OrderID" = "y"."OrderID"));
  ```

- **TryDelete**

  ```C#
  sqlite.Orders.TryDelete(x => x.Order_Details.Any(y => y.Discount >= 0.02));
  ```

  ```sqlite
  DELETE FROM "Orders" WHERE EXISTS (
      SELECT 1
      FROM "Order Details" AS "y"
      WHERE ("y"."Discount" >= 0.02) AND ("Orders"."OrderID" = "y"."OrderID"));
  ```

