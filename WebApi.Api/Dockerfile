#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi.Api/WebApi.Api.csproj", "WebApi.Api/"]
COPY ["WebApi.DAL/WebApi.DAL.csproj", "WebApi.DAL/"]
COPY ["WebApi.Domain/WebApi.Domain.csproj", "WebApi.Domain/"]
COPY ["WebApi.Application/WebApi.Application.csproj", "WebApi.Application/"]
RUN dotnet restore "WebApi.Api/WebApi.Api.csproj"
COPY . .
WORKDIR "/src/WebApi.Api"
RUN dotnet build "WebApi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.Api.dll"]