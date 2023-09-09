# RyVarr
This is the main repo for RyVarr company project.
this project keeps private but i show how the backend work with realtime visualization. so now i can show off my projects Commercial projects without afraid of stealing it.

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
- [ ] Add Signalr/Orleans realtime api to how the server work so the client read it and visualizate it.
- [ ] Add api for RyPrimal game.
### Frontend
- [X] Make the frontend work on Windows, Mac, Linux, Android, IOS and Web.
- [X] Adding main page as my portfolio .
- [X] Handling user authentication and authorization API.
- [X] Handling Payment API.
- [ ] Adding HamzaCAD service page.
- [ ] Adding profile page to allow user edit there account info.
- [ ] Add Realtime visualization to how the server handle the the requests and responses with Signalr Client.
### HamzaCAD
- [ ] Making HamzaCAD addon work in UI instead of CLI inside AutoCAD.
- [ ] Analyzing user architecture as an input and sending it to the server.
- [ ] Allowing access only by pro users by providing there token id.
- [ ] Making the server handle calculations logic and sending the final drawing to the client.
### RyPrimal
imagine the game realtime multiplayer, but instead of realtime we do time based that the player can move or shoot with object or multiple objects every 5 secs so not turnbased and not expensive.
- [ ] Make login with ryvarr account
- [ ] join room from match making service in the backend.
- [ ] visualizate game.
