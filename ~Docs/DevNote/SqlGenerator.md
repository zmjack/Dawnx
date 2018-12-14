## DawnIQureryable

This class provides some extension functions to generate SQL to query database record.



### How to test?

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



### Extension Simples

The Entity Framework provides some basic query extensions, but using it to develop business applications is not simple enough. So, we provide more query extensions to help developers solve their business problems simply.

- **WhereSearch**

  **Declare**:

  ```C#
  public static IQueryable<TEntity> WhereMatch<TEntity>(
      this IQueryable<TEntity> @this,
      string searchString, 
      Expression<Func<TEntity, object>> searchMembers)
  ```

  Queries records which is contains the specified string in any fields.

  For example, if you want to query ***Bill*** in field ***First_Name*** or ***Last_Name***:

  ```C#
  _context.Employees.WhereSearch("Bill", x => new { x.First_Name, x.Last_Name });
  ```

  This invoke will generate a SQL query string like this:

  ```mssql
  /* SQL Server */
  SELECT [x].[Id], [x].[First_Name], [x].[Last_Name]
  FROM [Emplyees] AS [x]
  WHERE (CHARINDEX(N'Bill', [x].[First_Name]) > 0)
  	OR (CHARINDEX(N'Bill', [x].[Last_Name]) > 0);
  ```

  ```mysql
  /* MySql */
  SELECT `x`.`Id`, `x`.`First_Name`, `x`.`Last_Name`,
  FROM `Emplyees` AS `x`
  WHERE (LOCATE('Bill', `x`.`First_Name`) > 0)
  	OR (LOCATE('Bill', `x`.`Last_Name`) > 0);
  ```

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

- WhereBefore

- WhereAfter

