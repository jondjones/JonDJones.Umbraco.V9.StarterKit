FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["JonDJones.Core/JonDJones.Core.csproj", "JonDJones.Core/"]
RUN dotnet restore "JonDJones.Core/JonDJones.Core.csproj"

COPY ["Umbraco.Models/Umbraco.Models.csproj", "Umbraco.Models/"]
RUN dotnet restore "Umbraco.Models/Umbraco.Models.csproj"

COPY ["JonDJones.Website/JonDJonesUmbraco9SampleSite.csproj", "JonDJones.Website/"]
RUN dotnet restore "JonDJones.Website/JonDJonesUmbraco9SampleSite.csproj"

COPY . .
RUN dotnet publish "JonDJones.Website/JonDJonesUmbraco9SampleSite.csproj" -c Release  -o /app/out 

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT Docker
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JonDJonesUmbraco9SampleSite.dll"]
