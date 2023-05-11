# Use the official .NET SDK 7.0 image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the project files and restore any dependencies
COPY *.sln ./
COPY Webshop/*.csproj ./Webshop/
COPY Domain/*.csproj ./Domain/
RUN dotnet restore

# Copy the rest of the application code and publish the Webshop project
COPY . ./
RUN dotnet publish Webshop -c Release -o out

# Use the official .NET Runtime 7.0 image as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port and start the application
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "Webshop.dll"]