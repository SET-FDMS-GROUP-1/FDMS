#  NAME          : Dockerfile
#  PROJECT       : SENG3020 - Flight Data Management System
#  PROGRAMMER    : Alex Simko
#  FIRST VERSION : 2025-11-28
#  DESCRIPTION   : DockerBuild file for FDMS Ground Station GUI application.
#                : reference this https://www.docker.com/blog/how-to-dockerize-react-app/


# install node and build the application
FROM node:20 AS build
WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn install --frozen-lockfile
COPY . .
RUN yarn build

# deploy the application to web server
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]