FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LearningAWS/LearningAWS.csproj", "LearningAWS/"]
RUN dotnet restore "LearningAWS/LearningAWS.csproj"
COPY . .
WORKDIR "/src/LearningAWS"
RUN dotnet build "LearningAWS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearningAWS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearningAWS.dll"]
