#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/MyCompany.ClaimService.Api/MyCompany.ClaimService.Api.csproj", "src/MyCompany.ClaimService.Api/"]
COPY ["src/MyCompany.ClaimService.Infrastructure/MyCompany.ClaimService.Infrastructure.csproj", "src/MyCompany.ClaimService.Infrastructure/"]
COPY ["src/MyCompany.ClaimService.Domain/MyCompany.ClaimService.Domain.csproj", "src/MyCompany.ClaimService.Domain/"]
COPY ["src/MyCompany.Common/MyCompany.Common.csproj", "src/MyCompany.Common/"]
COPY ["src/MyCompany.ClaimService.Application/MyCompany.ClaimService.Application.csproj", "src/MyCompany.ClaimService.Application/"]
RUN dotnet restore "src/MyCompany.ClaimService.Api/MyCompany.ClaimService.Api.csproj"
COPY . .
WORKDIR "/src/src/MyCompany.ClaimService.Api"
RUN dotnet build "MyCompany.ClaimService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompany.ClaimService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCompany.ClaimService.Api.dll"]
