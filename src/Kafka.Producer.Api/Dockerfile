#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Kafka.Producer.Api/Kafka.Producer.Api.csproj", "src/Kafka.Producer.Api/"]
RUN dotnet restore "src/Kafka.Producer.Api/Kafka.Producer.Api.csproj"
COPY . .
WORKDIR "/src/src/Kafka.Producer.Api"
RUN dotnet build "Kafka.Producer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka.Producer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka.Producer.Api.dll"]