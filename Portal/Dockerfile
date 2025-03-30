# Etapa 1: Construir la aplicación Angular
FROM node:18-alpine AS builder

# Configurar directorio de trabajo
WORKDIR /app

# Copiar archivos necesarios para instalar dependencias
COPY package.json package-lock.json ./

# Instalar dependencias
RUN npm install --force

# Copiar el resto del código fuente
COPY . .

# Construir la aplicación Angular en modo producción
RUN npm run build

# Etapa 2: Configurar el servidor Nginx para servir la aplicación
FROM nginx:stable-alpine-slim

# Instalar utilidades necesarias
RUN apk add --no-cache openssl

# Copiar la aplicación compilada desde la etapa de construcción
#COPY --from=builder /app/dist/Portal /usr/share/nginx/html 
COPY --from=builder /app/dist/Portal/browser /usr/share/nginx/html


# Copiar configuración personalizada de Nginx
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

# Crear un usuario no root para mayor seguridad
RUN adduser -D -u 1014 -s /bin/bash container_user

# Ajustar permisos para el usuario no root
RUN mkdir -p /var/run/nginx /var/cache/nginx /usr/share/nginx/html && \
    touch /var/run/nginx/nginx.pid && \
    chown -R container_user:container_user /var/run/nginx /var/cache/nginx /usr/share/nginx/html /var/log/nginx && \
    chmod -R 755 /var/run/nginx
# Modificar configuración de Nginx para evitar problemas con el archivo PID
RUN sed -i 's|pid\s\+.*|pid /dev/null;|' /etc/nginx/nginx.conf
# Comentar la línea 'user' en el archivo nginx.conf para evitar conflictos
RUN sed -i '/^user/s/^/#/' /etc/nginx/nginx.conf

# Cambiar al usuario no root
USER container_user

# Exponer el puerto 80 para servir la aplicación
EXPOSE 80

# Comando por defecto para iniciar Nginx
CMD ["nginx", "-g", "daemon off;"]
