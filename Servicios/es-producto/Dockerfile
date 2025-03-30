#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
#USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Actualizar los repositorios 
RUN apk update && \
    apk add --no-cache icu-libs tzdata && \
    apk upgrade

# Configurar zona horaria
ENV TZ=America/Guayaquil
RUN ln -sf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone && date

# Crear usuario container_user
RUN adduser -D -u 1014 -s /bin/bash container_user

# Asignar usuario container_user
USER container_user

# Configura UTF 8
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=es_ES.UTF-8

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["es-producto.csproj", "."]
RUN dotnet restore "./es-producto.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./es-producto.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./es-producto.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "es-producto.dll"]