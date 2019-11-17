# Postman

```curl
curl -X POST \
  http://localhost:5000/api/accounts/ \
  -H 'content-type: application/json' \
  -d '{
	"email": "xinhnguyen@sample.com",
	"password": "Abc@123",
	"firstName": "Xinh",
	"lastName": "Nguyen",
	"location": "Vietnam"
}'

curl -X POST \
  http://localhost:5000/api/auth/login \
  -H 'content-type: application/json' \
  -d '{
	"userName": "xinhnguyen@sample.com",
	"password": "Abc@123"
}'

curl -X GET \
  http://localhost:5000/api/dashboard/home \
  -H 'authorization: Bearer {TOKEN}' \
  -H 'content-type: application/json'
```

# References
- https://github.com/mmacneil/AngularASPNETCore2WebApiAuth
- https://github.com/mmacneil/AngularASPNETCoreAuthentication
- https://github.com/jatarga/WebApiJwt
- https://github.com/mmacneil/AngularASPNETCoreOAuth