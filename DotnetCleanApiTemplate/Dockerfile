FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DotnetCleanApiTemplate.sln ./
COPY DotnetCleanApiTemplate/DotnetCleanApiTemplate.csproj DotnetCleanApiTemplate/
COPY DotnetCleanApiTemplate.Tests/DotnetCleanApiTemplate.Tests.csproj DotnetCleanApiTemplate.Tests/
RUN dotnet restore DotnetCleanApiTemplate.sln

COPY . .
RUN dotnet publish DotnetCleanApiTemplate/DotnetCleanApiTemplate.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DotnetCleanApiTemplate.dll"]
