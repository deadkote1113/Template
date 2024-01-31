#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Directory.Packages.props", "Src/"]
COPY ["Src/Template.Client/Template.Client/Template.Client.csproj", "Src/Template.Client/Template.Client/"]
COPY ["Src/Template.Common/Template.Common.DI/Template.Common.DI.csproj", "Src/Template.Common/Template.Common.DI/"]
COPY ["Src/Template.Common/Template.Common.RabbitMQ/Template.Common.RabbitMQ.csproj", "Src/Template.Common/Template.Common.RabbitMQ/"]
COPY ["Src/Template.Common/Template.Common.Contracts/Template.Common.Contracts.csproj", "Src/Template.Common/Template.Common.Contracts/"]
COPY ["Src/Template.Common/Template.Common.SystemClients/Template.Common.SystemClients.csproj", "Src/Template.Common/Template.Common.SystemClients/"]
COPY ["Src/Template.Client/Template.Client.DI/Template.Client.DI.csproj", "Src/Template.Client/Template.Client.DI/"]
COPY ["Src/Template.Client/Template.Client.Services/Template.Client.Services.csproj", "Src/Template.Client/Template.Client.Services/"]
COPY ["Src/Template.Client/Template.Client.Repositories/Template.Client.Repositories.csproj", "Src/Template.Client/Template.Client.Repositories/"]
RUN dotnet restore "./Src/Template.Client/Template.Client/./Template.Client.csproj"
COPY . .
WORKDIR "/src/Src/Template.Client/Template.Client"
RUN dotnet build "./Template.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Template.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Client.dll"]