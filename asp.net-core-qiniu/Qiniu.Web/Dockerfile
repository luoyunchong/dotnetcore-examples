FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Qiniu.Web/Qiniu.Web.csproj", "Qiniu.Web/"]
RUN dotnet restore "Qiniu.Web/Qiniu.Web.csproj"
COPY . .
WORKDIR "/src/Qiniu.Web"
RUN dotnet build "Qiniu.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Qiniu.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Qiniu.Web.dll"]