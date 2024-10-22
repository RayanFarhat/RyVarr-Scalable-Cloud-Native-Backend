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
- [X] NGINX using HTTPS/TLS.
- [X] Saving and managing users and accounts data in Postgresql database.
- [X] Adding NGINX as an API Gateway, Load Balancer and Reverse Proxy.
- [X] Making the backend cloud native and can be scaled and distributed, so it works on monolithic or microservices architecture the same way.
- [X] Caching the most used data from database by spreading them across the distributed containers.
- [X] Adding subscription with payment using Paypal API and make the user as Pro User.
- [X] Make subscription option for month or year, And enables the extension of the subscription period in cases where the user is already a pro member.
- [X] Terminate the Pro subscription once the subscription period has elapsed, Including reset the role of the user.
- [X] Working with HamzaCAD service to allow only Pro users to use it.
- [X] Sending email for user to conform there email using Smtp protocol.
- [X] Allow Users to reset there password when they forgot it by sending email to the user.
- [X] Create two separate Docker Compose files, each with different configurations, to distinguish between the production and development environments.
- [ ] Setup continuous deployment to VPS.
- [X] Adding basic DDOS attack protection using NGINX.
- [X] Adding SSL connection to the database

### Frontend
- [X] Adding main page.
- [X] Handling user authentication and authorization API.
- [X] Handling Payment API.
- [X] Adding HamzaCAD service page.
- [X] Adding profile page.
- [X] Adding Terms of use and Privacy policy pages.
### HamzaCAD
- [X] convert Rectilinear Polygons to Rectangles.
- [X] make the text structure editor.
- [ ] detect Trapezoidal or Triangle and make the text editor special for them.
- [ ] seperate the drawing into layers, make the user name the layers(while define default layer names).
- [ ] make other btn for edit individual or multie bars, show the same window when draw slab, but for that we need to create local data that link the bar polyline to his text and arrows. 

## Frontend photos
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/f67e0a2e-bbb9-4a45-a49e-09966f747c37)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/17e405ad-7a9d-4bd9-b92a-9ad002451613)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/86f26ca0-66c4-419f-b735-53859389d8dd)
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/5df8c545-c00d-4157-9768-1b0ac7f0fddd)

email sent to user that is registered
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/8471dd17-184a-4330-b604-351b6d67acc3)

press subscription button while sign in and pay with paypal
![image](https://github.com/RayanFarhat/RyVarr/assets/100049997/def0b81b-8505-4ca3-a208-3e1429d7197d)


## Database Maintenance
- connect to https://ryvarr.com:5432 with pgadmin
- use `dump` to download the .sql file of the database and save it as a backup
- now i can update database version and use `restore` to add the data.

#### EFcore migrations commands i need to create the database and tables
* `dotnet tool install --global dotnet-ef` for installing
* `dotnet ef migrations add [name]`
* `docker compose -f docker-compose.yml exec backendserver dotnet ef database drop --force` if i want to reset the database
* `docker compose -f docker-compose-dev.yml exec backendserver dotnet ef database update`
* if I have faced that there is not tables created only __efmigrationshistory table then i delete files in Migrations after backup















