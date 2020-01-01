# Features
- OData

```
dotnet add package Microsoft.AspNetCore.OData --version 7.3.0
https://www.nuget.org/packages/Microsoft.AspNetCore.OData/
```

```
http://localhost:5000/odata
http://localhost:5000/odata/products
http://localhost:5000/odata/products?$select=name
http://localhost:5000/odata/products?$orderby=quantity
http://localhost:5000/odata/products?$orderby=name
http://localhost:5000/odata/products?$filter=quantity gt 110
```

# References
- https://docs.microsoft.com/en-us/odata/webapi/netcore