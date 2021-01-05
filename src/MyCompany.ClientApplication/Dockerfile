FROM node:12.7-alpine AS build
WORKDIR /usr/src/app
#COPY ["src/MyCompany.ClientApplication/package.json", "./"]
#COPY ["src/MyCompany.ClientApplication/package-lock.json", "./"]
COPY package.json package-lock.json ./ 
RUN npm install
COPY . .
RUN npm run build

FROM nginx:1.17.1-alpine
#COPY ["src/MyCompany.ClientApplication/nginx.conf", "/etc/nginx/nginx.conf"]
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/InsuranceClientApp /usr/share/nginx/html

