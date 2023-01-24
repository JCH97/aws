FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SqsPublisher/SqsPublisher.csproj", "SqsPublisher/"]
RUN dotnet restore "SqsPublisher/SqsPublisher.csproj"
COPY . .
WORKDIR "/src/SqsPublisher"
RUN dotnet build "SqsPublisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SqsPublisher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SqsPublisher.dll"]
