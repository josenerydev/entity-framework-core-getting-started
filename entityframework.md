Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.Logging

get-help entityframework

add-migration init
add-migration newrelationships
script-migration
update-database -verbose
update-database

scaffold-dbcontext
PM > scaffold-dbcontext -provider Microsoft.EntityFrameworkCore.SqlServer -connection "Data Source = (localdb)\mssqllocaldb; Initial Catalog = SamuraiAppData"

EFCorePowerTool
DGML editor
Multtarget framework!

.EnableSensitiveDataLogging() i OnConfiguring method


DbSet.Find(key)
If key is found in change tracker, avoids unneeded database query.

Update: Context will start tracking object, and mark its state as "Modified"
All properties are sent for the disconnected update.


In contructor of DbContext set ChangeTracker.QueryTrackingBehavior = QueryTranckingBehavior.NoTracking;
All queries on context will default to no tracking.
Use DbSet.AsTracking() for special queries to be tracked

As child's key value is not set, state will automatically be "Added"

"Foreign keys? NEVER! They will make my classes dirty!"
"Foreign keys in my classes make my life so much simpler!"

EF Core can only track entities recognized by the DbContext model.

Loading Related Data for Object in memory
With samurai object already in memory
_context.Entry(samurai).Collections(s => s.Quotes).Load();
_context.Entry(samurai).Reference(s => s.Horse).Load();

Explicit Loading
Explicitly retrieve related data for objects already in memory
DbContext.Entry().Collection().Load()
DbContext.Entry().Reference().Load
You can only load from a single object

Lazy Loading
Happens implicitly by mention of the navigation
Enable with these requirements:
Every navigation property must be virtual
Microsoft.EntityFramework.Proxies package
ModelBuilder.UseLazyLoadingProxies()

Connected Object
DbContext is aware of all changes made to objects that is it tracking

Disconnected Object
DbContext has no clue about history of objects before they are attached

One-To-One
Changing the Child of an Existing Parent
Is foreign key nullable?
Is the child object in memory?
Are the objects being tracked?

_context.ChangeTracke.Entries(),results in QuickWatch to see object are tracked.

DbSet Methods to Run Raw SQL
Synchonous and asynchronous options
Special method for interpolated strings
Use parameters to avoid SQL injection !!

Raw SQL Results Rules (Limitations)
Must return data for all properties of the entity type
Column names in results match mapped column names
Query can't contain related data
Only query entities and keyless entities known by DbContext
You can't select navigation properties in SQL

Wiring up ASP.NET Core with EF Core
## Web API Project
1. Add references to projects with entities and DbContext
2. Build
3. Add controller (EF & API actions) (adds ef core & other packages to csproj)
## Startup
4. Add services.DbContext with UseSqlServer to startup Configure()
## appsettings.json
5. Add connection string config
6. Add EF Core logging config
## DbContext
7. Add Contructor that takes in DbContextOptions
8. Set ChangeTracker.QueryBehaviorOptions to NoQuery in contructor
9. Remove optionsBuilder from OnConfiguring
10. Remove ConsoleLoggerFactory
11. Clean up using statements