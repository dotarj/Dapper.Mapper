Dapper.Mapper
========================================

Easier Multi Mapping
---------------------
Dapper allows you to map a single row to multiple objects. Dapper.Mapper figures out the relations between the different returned objects and automatically assigns them.

Example:

```csharp
var sql = @"select * from Employee 
inner join Department on Department.Id = Employee.DepartmentId";
```
Instead of explicitly writing this:

```csharp
var employee = connection.Query<Employee, Department, Employee>(sql, (employee, department) => { employee.Department = department; return employee;});
```
Dapper.Mapper allows you to write this:

```csharp
var employee = connection.Query<Employee, Department>(sql);
```