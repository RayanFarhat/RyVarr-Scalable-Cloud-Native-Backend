# RyVarr
This is the main repo for RyVarr project.
This project is fullstack (backend oriented) that handle user identity and payment with Paypal. And once the user has paid the subscribtion, He can use Autocad Plugin library called hamzacad that only allow paid users use it. while the main page of the frontend is also a portfolio for the developer of this project.

The backend is cloud native so it can scale easly with caching using Orleans.

The frontend client is build with AvaloniaUI(WPF like framework) that make my frontend work on Web, Windows, Mac, Linux, Android, IOS.
## Backend Architecture:
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/149dc50c-c4a0-4b81-b085-b06537c1e873)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/adfc0eaf-50c1-455d-b28d-d3343f73e20a)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/55ea845f-3894-4db2-85c5-f701d9ed6e1a)
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
- [ ] Add Signalr/Orleans realtime api to how the server work so the client read it and visualizate it, and scale it with build in redis support.
### Frontend
- [X] Make the frontend work on Windows, Mac, Linux, Android, IOS and Web.
- [X] Adding main page as my portfolio .
- [X] Handling user authentication and authorization API.
- [X] Handling Payment API.
- [ ] Adding HamzaCAD service page.
- [ ] Adding profile page to allow user edit there account info.
- [ ] Add Realtime visualization to how the server handle the the requests and responses with Signalr Client.
- [ ] add sqlite to save localdata(even on webassembly).
### HamzaCAD
- [ ] Making HamzaCAD addon work in UI instead of CLI inside AutoCAD.
- [ ] Analyzing user architecture as an input and sending it to the server.
- [ ] Allowing access only by pro users by providing there token id.
- [ ] Making the server handle calculations logic and sending the final drawing to the client.
