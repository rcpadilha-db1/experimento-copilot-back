FROM node:18

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

# Wait for MySQL to be ready
COPY wait-for-it.sh /usr/local/bin/wait-for-it.sh
RUN chmod +x /usr/local/bin/wait-for-it.sh

CMD ["sh", "-c", "wait-for-it.sh mysql:3306 -- npm run migration:up && npm run start:dev"]
