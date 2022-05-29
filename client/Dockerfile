#stage 1
FROM node:16.15.0 as node
WORKDIR /app
COPY . .
RUN npm ci
RUN npm run build

#stage 2
#FROM nginx:alpine
#COPY --from=node /app/dist/findfulclient /usr/share/nginx/html
