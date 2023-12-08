# RyVarr
This is the main repo for RyVarr project.

This is a comprehensive SaaS website that includes a well-designed front-end, a securely scalable server-side infrastructure, and the software that this SaaS website sells.

Including  handle user identity and payment with Paypal. And once the user has paid the subscribtion, He can use Autocad Plugin library called hamzacad that only allow paid users use it.

The backend is cloud native so it can scale easly with caching using Orleans.

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
- [X] Make subscription option for month or year, And enables the extension of the subscription period in cases where the user is already a pro member.
- [X] Terminate the Pro subscription once the subscription period has elapsed, Including reset the role of the user.
- [X] Working with HamzaCAD service to allow only Pro users to use it.
- [X] Sending email for user to conform there email using Smtp protocol.
- [ ] Prevent sharing accounts with SignalR.
- [ ] Allow Users to read/change/delete there account.(rename email,username, or delete there entire account).
- [ ] Setup continuous deployment to VPS.
### Frontend
- [X] Make the frontend work on Windows, Mac, Linux, Android, IOS and Web.
- [X] Adding main page.
- [X] Handling user authentication and authorization API.
- [X] Handling Payment API.
- [X] Adding HamzaCAD service page.
- [X] Adding profile page to allow user edit there account info.
### HamzaCAD
- [X] Making HamzaCAD addon work in UI instead of CLI inside AutoCAD.
- [X] Analyzing user architecture as an input and sending it to the server.
- [X] Allowing access only by pro users by providing there token id.
- [X] Making the server handle calculations logic and sending the final drawing to the client.

## Frontend photos
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/f67e0a2e-bbb9-4a45-a49e-09966f747c37)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/26f4b9cb-e085-4122-a4f5-20015ff8f1cd)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/17e405ad-7a9d-4bd9-b92a-9ad002451613)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/86f26ca0-66c4-419f-b735-53859389d8dd)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/5df8c545-c00d-4157-9768-1b0ac7f0fddd)

email sent to user that is registered
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/8471dd17-184a-4330-b604-351b6d67acc3)

press subscription button while sign in and pay with paypal
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/def0b81b-8505-4ca3-a208-3e1429d7197d)




