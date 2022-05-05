FROM mcr.microsoft.com/dotnet/runtime:6.0-focal AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
# Install EF Tools and add them to PATH
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /src
COPY ["moodle-external-database-integration.sln", "."]
COPY ["moodle-external-database-integration.Client/moodle-external-database-integration.Client.csproj", "moodle-external-database-integration.Client/"]
COPY ["moodle-external-database-integration.Core/moodle-external-database-integration.Core.csproj", "moodle-external-database-integration.Core/"]
COPY ["moodle-external-database-integration.Data/moodle-external-database-integration.Data.csproj", "moodle-external-database-integration.Data/"]
RUN dotnet restore
COPY . .
# Create migration bundle
RUN dotnet ef --startup-project moodle-external-database-integration.Client migrations bundle
WORKDIR "/src/moodle-external-database-integration.Client"
RUN dotnet build "moodle-external-database-integration.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "moodle-external-database-integration.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/efbundle .
# Apply migration bundle to DB and run the app
ENTRYPOINT ["/bin/sh", "-c", "./efbundle && dotnet moodle-external-database-integration.Client.dll"]
