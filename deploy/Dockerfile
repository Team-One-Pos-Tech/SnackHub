FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src /src

RUN dotnet restore "SnackHub.Api/SnackHub.Api.csproj"
WORKDIR "/src/SnackHub.Api"

FROM build AS publish
RUN dotnet publish "SnackHub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnackHub.Api.dll"]
