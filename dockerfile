FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContactManagement.Api/ContactManagement.Api.csproj", "ContactManagement.Api/"]
COPY ["ContactManagement.Application/ContactManagement.Application.csproj", "ContactManagement.Application/"]
COPY ["ContactManagement.Domain/ContactManagement.Domain.csproj", "ContactManagement.Domain/"]
COPY ["ContactManagement.InfraStructure/ContactManagement.InfraStructure.csproj", "ContactManagement.InfraStructure/"]
RUN dotnet restore "./ContactManagement.Api/ContactManagement.Api.csproj"
COPY . .
WORKDIR "/src/ContactManagement.Api"
RUN dotnet build "./ContactManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ContactManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManagement.Api.dll"]
