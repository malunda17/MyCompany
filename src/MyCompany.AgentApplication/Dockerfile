#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5007
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/MyCompany.AgentApplication/MyCompany.AgentApplication.csproj", "src/MyCompany.AgentApplication/"]
RUN dotnet restore "src/MyCompany.AgentApplication/MyCompany.AgentApplication.csproj"
COPY . .
WORKDIR "/src/src/MyCompany.AgentApplication"
RUN dotnet build "MyCompany.AgentApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompany.AgentApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCompany.AgentApplication.dll"]