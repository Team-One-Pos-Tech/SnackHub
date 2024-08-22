#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

ARG PASSWORD_ENV_SEEDED
ENV PASSWORD_ENV_SEEDED=dGVzdGU=
RUN dotnet dev-certs https -ep /src/aspnetapp.pfx -p ${PASSWORD_ENV_SEEDED}

COPY ["src/SnackHub.Api/SnackHub.Api.csproj", "src/SnackHub.Api/"]
COPY ["src/SnackHub.Application/SnackHub.Application.csproj", "src/SnackHub.Application/"]
COPY ["src/SnackHub.Domain/SnackHub.Domain.csproj", "src/SnackHub.Domain/"]
COPY ["src/SnackHub.Infra/SnackHub.Infra.csproj", "src/SnackHub.Infra/"]
RUN dotnet restore "./src/SnackHub.Api/./SnackHub.Api.csproj"
COPY . .
WORKDIR "/src/src/SnackHub.Api"
RUN dotnet build "./SnackHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SnackHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/aspnetapp.pfx .
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
RUN chown -R www-data:www-data /app
USER www-data

ENTRYPOINT ["dotnet", "SnackHub.Api.dll"]