## Http Status

- **500: Internal Server Error** (Form value count limit 1024 exceeded)

  ```C#
  public void ConfigureServices(IServiceCollection services)
  {
      services.Configure<FormOptions>(options =>
      {
          options.ValueCountLimit = int.MaxValue;
      });
  }
  ```


