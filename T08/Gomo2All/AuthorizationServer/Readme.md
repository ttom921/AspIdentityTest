﻿

Pomelo.EntityFrameworkCore.MySql
IdentityServer4
IdentityServer4.EntityFramework
IdentityServer4.AspNetIdentity

connect string
"DefaultConnection": "Data Source=127.0.0.1;Initial Catalog=AuthServer;User ID=sa;Password=12345678;Min Pool Size=10;"

資料庫建立
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb


1.先執行上一個專案AuthServerCreateDBSln來建立DB
2.將其中的一張表刪除，PersistedGrants
3.直接執行

//登出登入問題
1.在AuthoriaztionServer在設定client端時要加入PostLogoutRedirectUris的登出網址
2.在NG2端的設定不用加

//RoleBase 
dotnet ef migrations add AspIdentityServerMigration -c ApplicationDbContext -o Data/Migrations -v


