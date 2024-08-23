# Teste Copilot

O teste realizado através do desafio **_"Experimento Github Copilot
Cenário Backend"_**.

Para esse teste foi utilizado somente o Github Copilot e Github Copilot Chat.
Toda a criação da estrutura, classes e configurações foram feitas através de prompts no Chat. Somente o projeto Spring foi iniciado através do https://start.spring.io/.

### Rodar o projeto:
Para rodar o projeto basta clonar, rodar o maven e iniciar o mesmo com o java 17.
Banco de dados: H2 - http://localhost:8080/h2-console/ (não tem senha)

Scripts para inserção de dados:
-- Insert data into the User table
INSERT INTO table_user (name, email) VALUES ('John Doe', 'john.doe@example.com');
INSERT INTO table_user (name, email) VALUES ('Jane Smith', 'jane.smith@example.com');

-- Insert data into the Vehicle table
INSERT INTO table_vehicle (plate, capacity, owner_id) 
VALUES ('ABC1234', 4, 
    (SELECT id FROM table_user WHERE name = 'John Doe')
);

INSERT INTO table_vehicle (plate, capacity, owner_id) 
VALUES ('XYZ5678', 2, 
    (SELECT id FROM table_user WHERE name = 'Jane Smith')
);

-- Insert data into the Ride table
INSERT INTO table_ride (vehicle_id, rider_id, date) 
VALUES (
    (SELECT id FROM table_vehicle WHERE plate = 'ABC1234'),
    (SELECT id FROM table_user WHERE name = 'John Doe'),
    CURRENT_TIMESTAMP()
);

INSERT INTO table_ride (vehicle_id, rider_id, date) 
VALUES (
    (SELECT id FROM table_vehicle WHERE plate = 'XYZ5678'),
    (SELECT id FROM table_user WHERE name = 'Jane Smith'),
    CURRENT_TIMESTAMP()
);

Curls para consulta:
curl --silent --location --request GET 'http://localhost:8080/rides/1'
curl --silent --location --request DELETE 'http://localhost:8080/rides/1'
curl --silent --location --request POST 'http://localhost:8080/rides/1/3' \
--header 'Content-Type: application/json' \
--data '{
    "date": "2022-12-31T23:59:59.999Z"
}'
