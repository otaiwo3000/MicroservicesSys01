#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["UserMgt.Service/UserMgt.Service.csproj", "UserMgt.Service/"]
COPY ["UserMgr.Business.EmailService/UserMgr.Business.EmailService.csproj", "UserMgr.Business.EmailService/"]
COPY ["UserMgt.Business.Repositories/UserMgt.Business.Repositories.csproj", "UserMgt.Business.Repositories/"]
COPY ["UserMgt.Business.Interfaces/UserMgt.Business.Interfaces.csproj", "UserMgt.Business.Interfaces/"]
COPY ["UserMgt.Shared.DataAccess/UserMgt.Shared.DataAccess.csproj", "UserMgt.Shared.DataAccess/"]
COPY ["UserMgt.Shared.Entities/UserMgt.Shared.Entities.csproj", "UserMgt.Shared.Entities/"]
COPY ["UserMgt.Shared.Common/UserMgt.Shared.Common.csproj", "UserMgt.Shared.Common/"]
COPY ["UserMgt.Business.Logger/UserMgt.Business.Logger.csproj", "UserMgt.Business.Logger/"]
RUN dotnet restore "UserMgt.Service/UserMgt.Service.csproj"
COPY . .
WORKDIR "/src/UserMgt.Service"
RUN dotnet build "UserMgt.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserMgt.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserMgt.Service.dll"]