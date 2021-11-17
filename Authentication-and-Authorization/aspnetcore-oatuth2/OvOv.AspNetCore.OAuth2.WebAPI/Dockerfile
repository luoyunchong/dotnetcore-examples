#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OvOv.AspNetCore.OAuth2.WebAPI/OvOv.AspNetCore.OAuth2.WebAPI.csproj", "OvOv.AspNetCore.OAuth2.WebAPI/"]
RUN dotnet restore "OvOv.AspNetCore.OAuth2.WebAPI/OvOv.AspNetCore.OAuth2.WebAPI.csproj"
COPY . .
WORKDIR "/src/OvOv.AspNetCore.OAuth2.WebAPI"
RUN dotnet build "OvOv.AspNetCore.OAuth2.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OvOv.AspNetCore.OAuth2.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OvOv.AspNetCore.OAuth2.WebAPI.dll"]