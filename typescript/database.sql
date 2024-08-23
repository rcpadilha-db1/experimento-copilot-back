CREATE DATABASE IF NOT EXISTS example CHARACTER SET UTF8 COLLATE utf8_bin;

USE example;

CREATE TABLE IF NOT EXISTS `users` (
  `id` INT(11) NOT NULL AUTO_INCREMENT, -- ID USUARIO (MOTORISTA/PASSAGEIRO)
  `name` VARCHAR(200) NOT NULL, -- NOME USUÁRIO
  `email` VARCHAR(200) NOT NULL, -- EMAIL USUÁRIO
  PRIMARY KEY (`id`),
  UNIQUE(`email`) 
);

CREATE TABLE IF NOT EXISTS `vehicles` (
  `id` INT(11) NOT NULL AUTO_INCREMENT, -- ID DO VEICULO
  `plate` VARCHAR(20) NOT NULL, -- PLACA (ajustado para VARCHAR para representar melhor as placas de veículos)
  `capacity` INT(11) NOT NULL, -- CAPACIDADE DE PASSAGEIROS
  `owner_id` INT(11) NOT NULL, -- PROPRIETARIO
  PRIMARY KEY (`id`), 
  FOREIGN KEY (`owner_id`) REFERENCES `users`(`id`)
);

CREATE TABLE IF NOT EXISTS `rides` (
  `id` INT(11) NOT NULL AUTO_INCREMENT, -- ID DA CARONA
  `vehicle_id` INT(11) NOT NULL, -- ID DO VEICULO
  `rider_id` INT(11) NOT NULL, -- ID DO USUARIO PASSAGEIRO
  `date` DATE NOT NULL, -- DATA DA CARONA
  PRIMARY KEY (`id`),
  FOREIGN KEY (`vehicle_id`) REFERENCES `vehicles`(`id`),
  FOREIGN KEY (`rider_id`) REFERENCES `users`(`id`)
);