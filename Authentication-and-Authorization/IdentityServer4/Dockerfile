#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityServer4/IdentityServer4.Service.csproj", "IdentityServer4/"]
RUN dotnet restore "IdentityServer4/IdentityServer4.Service.csproj"
COPY . .
WORKDIR "/src/IdentityServer4"
RUN dotnet build "IdentityServer4.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer4.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer4.Service.dll"]