FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MongoDBTest/MongoDBTest.csproj", "MongoDBTest/"]
RUN dotnet restore "MongoDBTest/MongoDBTest.csproj"
COPY . .
WORKDIR "/src/MongoDBTest"
RUN dotnet build "MongoDBTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MongoDBTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongoDBTest.dll"]
