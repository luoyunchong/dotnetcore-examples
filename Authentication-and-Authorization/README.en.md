**English** | [简体中文](README.md)

- After startup, http://localhost:5000/.well-known/openid-configuration is the OpenID Connect discovery endpoint used to obtain the other endpoints and related information.


1. token_endpoint
- http://localhost:5000/connect/token

POST form data
```
client_id:client.api.service
client_secret:clientsecret
grant_type:password
username:edison@hotmail.com
password:edisonpassword
```

Response
```
{
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6ImMyNWZlMWRhZmI4ZmQ0ZmZiOTExODRkM2IwYTViNGU3IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE1OTUxNzczMzIsImV4cCI6MTU5NTE4MDkzMiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiY2xpZW50c2VydmljZSIsImNsaWVudF9pZCI6ImNsaWVudC5hcGkuc2VydmljZSIsInN1YiI6IjEwMDAxIiwiYXV0aF90aW1lIjoxNTk1MTc3MzMyLCJpZHAiOiJsb2NhbCIsInNjb3BlIjpbImNsaWVudHNlcnZpY2UiXSwiYW1yIjpbInB3ZCJdfQ.TU4AcwmUxeIchDXB-G0D_SPa2GQ2rrlfqYwtEk9e27MBx2Dz7ibaYN5yXsG5JAiAf_Klv3CgbsV0DDbqZbzrPBZxjY7TPBfgx0kcpFR-Uk3eVvQK6EjNAdLXaaeAtJt2vtJofrDPLYntPw4DZeszfgYQa7dyxYR1OdbYcHLVRMZ139bf-jsQI3EsIo8m2_LN6riK67HZIvr7UrpB4Wmc4JLmnWUN11ZYXkAomlBvbeUCXO4EQdaaZsIgR5R9vNt5iHbI6ZyT6Mr6ZnWz59nl94BOAAM4-0xZLAdrY1o71a65bxP8rMvwSYbeGuOSd0PF0QQncRcQv1B0F2CVXtQ7pw",
    "expires_in": 3600,
    "token_type": "Bearer",
    "scope": "clientservice"
}
```


- https://localhost:5003/connect/userinfo
- GET
```
Authorization Bearer access_token
```

```
{
    "sub": "10001"
}
```
