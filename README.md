# Dapper.Mapper

[Dapper](https://github.com/StackExchange/Dapper) is a micro-ORM that extends the `IDbConnection` interface with methods that map query results to objects. With Dapper's  multi mapping feature you can map a single row to multiple objects and define the relations between those objects. Dapper.Mapper is an extension to Dapper which simplifies multi mapping by figuring out the relations between the different returned objects and automatically assigning them.

For example, for the following query:

```csharp
var sql = @"select * from Employee 
inner join Department on Department.Id = Employee.DepartmentId";
```

Instead of explicitly writing this:

```csharp
var employee = connection.Query<Employee, Department, Employee>(sql, (employee, department) =>
{
    employee.Department = department;

    return employee;
});
```

Dapper.Mapper allows you to write this:

```csharp
var employee = connection.Query<Employee, Department>(sql);
```

## Getting started

First, add a dependency to [Dapper.Mapper](https://www.nuget.org/packages/Dapper.Mapper) using the NuGet package manager (console) or by adding a package reference to the .csproj:

```xml
<ItemGroup>
  <PackageReference Include="Dapper.Mapper" Version="x.x.x" />
</ItemGroup>
```
Then, add the `Dapper.Mapper` namespace to your file:

```csharp
using Dapper.Mapper;
```

That's it! Now you can use the `IDbConnection.Query`, `IDbConnection.QueryAsync`, and `SqlMapper.GridReader.Read` extension methods, just like you would do with Dapper, but now you don't have to explicitly specify the relation between the different returned objects.