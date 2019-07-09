FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["RESTful.FreeSql.Repository/RESTful.FreeSql.Repository.csproj", "RESTful.FreeSql.Repository/"]
RUN dotnet restore "RESTful.FreeSql.Repository/RESTful.FreeSql.Repository.csproj"
COPY . .
WORKDIR "/src/RESTful.FreeSql.Repository"
RUN dotnet build "RESTful.FreeSql.Repository.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RESTful.FreeSql.Repository.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RESTful.FreeSql.Repository.dll"]
