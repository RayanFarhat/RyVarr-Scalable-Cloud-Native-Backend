# RyVarr
This is the main repo for RyVarr company project.

## Features
### Backend
- [X] API that handle user authentication and authorization.
- [ ] NGINX using HTTPS/TLS.
- [X] Saving and managing users and accounts data in Postgresql database.
- [X] Adding NGINX as an API Gateway, Load Balancer and Reverse Proxy.
- [X] Making the backend cloud native and can be scaled and distributed, so it works on monolithic or microservices architecture the same way.
- [X] Caching the most used data from database by spreading them across the distributed containers.
- [X] Adding subscription with payment using Paypal API and make the user as Pro User.
- [ ] Adding and managing users login using google OAuth 2.0 instead of my user managing system.
- [ ] Working with HamzaCAD service to allow only Pro users to use it.
- [ ] Allow Users to read/change/delete there account.(rename email,username, or delete there entire account).
### Frontend
- [X] Make the frontend work on Windows, Mac, Linux, Android, IOS and Web.
- [X] Adding main page as my portfolio .
- [X] Handling user authentication and authorization API.
- [ ] Handling Payment API.
- [ ] Adding HamzaCAD service page.
- [ ] Adding profile page to allow user edit there account info.
### HamzaCAD
- [ ] Making the UI instead of CLI inside AutoCAD.
- [ ] Analyzing user architecture as an input and sending it to the server.
- [ ] Allowing access only by pro users by providing there token id.
- [ ] Making the server handle calculations logic and sending the final drawing to the client.
