# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 

# for wasm compiling
RUN dotnet workload install wasm-tools
RUN apt-get update
RUN apt-get install -y python3

WORKDIR /app

# Copy the project file and restore dependencies
COPY . ./
WORKDIR /app/RyVarr.Browser
RUN dotnet build
# Set the final image
ENTRYPOINT ["dotnet", "run"]