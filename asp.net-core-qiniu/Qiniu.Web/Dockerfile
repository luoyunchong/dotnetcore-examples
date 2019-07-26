FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
COPY . /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "Qiniu.Web.dll"]

