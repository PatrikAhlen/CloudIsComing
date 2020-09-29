FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CloudIsComing.Api/CloudIsComing.Api.csproj", "CloudIsComing.Api/"]
RUN dotnet restore "CloudIsComing.Api/CloudIsComing.Api.csproj"
COPY . .
WORKDIR /src/CloudIsComing.Api
RUN dotnet build "CloudIsComing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudIsComing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudIsComing.Api.dll"]
