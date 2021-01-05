#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/MyCompany.LogService.Api/MyCompany.LogService.Api.csproj", "src/MyCompany.LogService.Api/"]
COPY ["src/MyComapany.LogService.Infrastructure/MyComapany.LogService.Infrastructure.csproj", "src/MyComapany.LogService.Infrastructure/"]
COPY ["src/MyCompany.LogService.Domain/MyCompany.LogService.Domain.csproj", "src/MyCompany.LogService.Domain/"]
COPY ["src/MyCompany.Common/MyCompany.Common.csproj", "src/MyCompany.Common/"]
COPY ["src/MyCompany.LogService.Application/MyCompany.LogService.Application.csproj", "src/MyCompany.LogService.Application/"]
RUN dotnet restore "src/MyCompany.LogService.Api/MyCompany.LogService.Api.csproj"
COPY . .
WORKDIR "/src/src/MyCompany.LogService.Api"
RUN dotnet build "MyCompany.LogService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompany.LogService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCompany.LogService.Api.dll"]