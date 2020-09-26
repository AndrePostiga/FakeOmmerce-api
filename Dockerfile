FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY ["FakeOmmerce-api.csproj", "./"]
RUN dotnet restore "./FakeOmmerce-api.csproj"
COPY . /app
WORKDIR /app
RUN dotnet build "FakeOmmerce-api.csproj" -c Release -o app


FROM build AS publish
RUN dotnet publish "FakeOmmerce-api.csproj" -c Release -o app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "app/FakeOmmerce-api.dll"]