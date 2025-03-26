```sql
DROP TABLE IF EXISTS OpenIddictApplications
DROP TABLE IF EXISTS OpenIddictAuthorizations
DROP TABLE IF EXISTS OpenIddictScopes
DROP TABLE IF EXISTS OpenIddictTokens
DROP TABLE IF EXISTS __EFMigrationsHistory
```

```sh
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```