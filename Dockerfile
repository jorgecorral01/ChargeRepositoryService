FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

COPY ["Charge.Repository.Service/Charge.Repository.Service.csproj", "Charge.Repository.Service/"]
COPY ["Charge.Repository.Service.Business/Charge.Repository.Service.Business.csproj", "Charge.Repository.Service.Business/"]
COPY ["Charge.Repository.Service.Repository/Charge.Repository.Service.Repository.csproj", "Charge.Repository.Service.Repository/"]
COPY ["Charge.Repository.Service.Repository.Entity/Charge.Repository.Service.Repository.Entity.csproj", "Charge.Repository.Service.Repository.Entity/"]

RUN dotnet restore "Charge.Repository.Service/Charge.Repository.Service.csproj"

COPY . .
WORKDIR "/src"

FROM build AS publish

RUN dotnet publish "Charge.Repository.Service/Charge.Repository.Service.csproj" -c Release -o /app

# Build runtime image
FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Charge.Repository.Service.dll"]