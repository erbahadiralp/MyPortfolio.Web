#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyPortfolio.Web.csproj", "."]
RUN dotnet restore "./MyPortfolio.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MyPortfolio.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyPortfolio.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPortfolio.Web.dll"]