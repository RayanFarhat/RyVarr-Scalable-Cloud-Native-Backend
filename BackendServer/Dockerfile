FROM mcr.microsoft.com/dotnet/sdk:7.0 as Base
WORKDIR /app
COPY *.csproj ./
RUN apt-get update

RUN apt-get install -y python3
RUN dotnet tool install --global dotnet-ef --version 7.0.14
ENV PATH $PATH:/root/.dotnet/tools

RUN dotnet restore
# Copy the source code and build the project
COPY . ./
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish
COPY ./static ./static

ENTRYPOINT ["dotnet", "publish/BackendServer.dll"]