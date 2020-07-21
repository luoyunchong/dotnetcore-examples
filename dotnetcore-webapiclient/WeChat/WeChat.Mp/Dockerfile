FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WeChat.Mp/WeChat.Mp.csproj", "WeChat.Mp/"]
RUN dotnet restore "WeChat.Mp/WeChat.Mp.csproj"
COPY . .
WORKDIR "/src/WeChat.Mp"
RUN dotnet build "WeChat.Mp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WeChat.Mp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WeChat.Mp.dll"]