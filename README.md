# EFCoreDefaultIfEmpty_Max_Bug
Repro of bug found in EFCore 3.0 RC1 for DefalutIfEmpty().Max()
Copy of entry in [StackOverflow](https://stackoverflow.com/questions/58039509/defaultifempty-max-still-throws-sequence-contains-no-elements)

After I updated my project to dotnet core 3.0RC1 (might be in preview9 as well) my code that used to work 
```
var value = context.Products.Where(t => t.CategoryId == catId).Select(t => t.Version).DefaultIfEmpty().Max();
```
started throwing 
`System.InvalidOperationException: Sequence contains no elements`. The table in question is empty.

If I add `ToList()` so it looks like this `DeafultIfEmpty().ToList().Max()`, it starts to work again. Could not find any information about a breaking change. When I run 
```
var expectedZero = new List<int>().DefaultIfEmpty().Max();
```
it works fine. That made me think maybe something wrong with EF Core. Then I created test in xUnit with exactly the same setup but there tests are passing (table is empty as well, uses InMemoryDatabase instead of live SQL Server instance).

I am truly puzzled. 
Relevant stack trace:
```
System.InvalidOperationException: Sequence contains no elements.
   at int lambda_method(Closure, QueryContext, DbDataReader, ResultContext, int[], ResultCoordinator)
   at bool Microsoft.EntityFrameworkCore.Query.RelationalShapedQueryCompilingExpressionVisitor+QueryingEnumerable<T>+Enumerator.MoveNext()
   at TSource System.Linq.Enumerable.Single<TSource>(IEnumerable<TSource> source)
   at TResult Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.Execute<TResult>(Expression query)
   at TResult Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryProvider.Execute<TResult>(Expression expression)
   at TSource System.Linq.Queryable.Max<TSource>(IQueryable<TSource> source)
   at ... (my method that run the code)
```

Product class:
```
   [Table("tmpExtProduct", Schema = "ext")]
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Version { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ImportDate { get; set; }

        public int CategoryId { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Ext.Category))]
        public int CategoryId { get; set; }        

        [InverseProperty(nameof(Ext.Category.Products))]
        public virtual Category Category { get; set; }
    }
```


Sql produced by ef core
```
exec sp_executesql N'SELECT MAX([t0].[Version])
FROM (
    SELECT NULL AS [empty]
) AS [empty]
LEFT JOIN (
    SELECT [t].[Version], [t].[CategoryId], [t].[Description], [t].[ImporDate]
    FROM [ext].[tmpExtProduct] AS [t]
    WHERE ([t].[CategoryId] = @__catId_0) AND @__catId_0 IS NOT NULL
) AS [t0] ON 1 = 1',N'@__catId_0 int',@__catId_0=2
```
