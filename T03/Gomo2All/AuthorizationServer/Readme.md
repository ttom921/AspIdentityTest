Pomelo.EntityFrameworkCore.MySql
IdentityServer4
IdentityServer4.EntityFramework
IdentityServer4.AspNetIdentity

connect string
"DefaultConnection": "Data Source=127.0.0.1;Initial Catalog=AuthServer;User ID=sa;Password=12345678;Min Pool Size=10;"

資料庫建立
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb


