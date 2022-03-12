#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Helpdesk.Service/Helpdesk.Service.csproj", "Helpdesk.Service/"]
COPY ["EmailService/EmailService.csproj", "EmailService/"]
COPY ["Helpdesk.Business.Repositories/Helpdesk.Business.Repositories.csproj", "Helpdesk.Business.Repositories/"]
COPY ["Helpdesk.Business.Interfaces/Helpdesk.Business.Interfaces.csproj", "Helpdesk.Business.Interfaces/"]
COPY ["Helpdesk.Shared.DataAccess/Helpdesk.Shared.DataAccess.csproj", "Helpdesk.Shared.DataAccess/"]
COPY ["Helpdesk.Shared.Entities/Helpdesk.Shared.Entities.csproj", "Helpdesk.Shared.Entities/"]
COPY ["Helpdesk.Shared.Common/Helpdesk.Shared.Common.csproj", "Helpdesk.Shared.Common/"]
COPY ["Helpdesk.Business.Logger/Helpdesk.Business.Logger.csproj", "Helpdesk.Business.Logger/"]
RUN dotnet restore "Helpdesk.Service/Helpdesk.Service.csproj"
COPY . .
WORKDIR "/src/Helpdesk.Service"
RUN dotnet build "Helpdesk.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Helpdesk.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Helpdesk.Service.dll"]