#Compile
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
#Copy and restore everything under it
COPY . ./
RUN dotnet restore
#Publish to out directory
RUN dotnet publish -c Release -o out

#Prepare an image for execution
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "MudTracker.Server.dll"]