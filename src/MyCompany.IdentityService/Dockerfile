#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/MyCompany.IdentityService/MyCompany.IdentityService.csproj", "src/MyCompany.IdentityService/"]
COPY ["src/MyCompany.Common/MyCompany.Common.csproj", "src/MyCompany.Common/"]
RUN dotnet restore "src/MyCompany.IdentityService/MyCompany.IdentityService.csproj"
COPY . .
WORKDIR "/src/src/MyCompany.IdentityService"
RUN dotnet build "MyCompany.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompany.IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCompany.IdentityService.dll"]