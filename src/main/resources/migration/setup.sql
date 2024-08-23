create database shared_ride;

CREATE TABLE `user` (
  `id` varchar(255) NOT NULL PRIMARY KEY,
  `name` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL
);

CREATE TABLE `vehicle` (
  `id` varchar(255) NOT NULL PRIMARY KEY,
  `plate` varchar(255) DEFAULT NULL,
  `capacity` int(1) DEFAULT NULL,
  `user_id` varchar(255) DEFAULT null,
  CONSTRAINT `user_vehicle` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
);


CREATE TABLE `ride` (
  `id` varchar(255) NOT NULL PRIMARY KEY,
  `vehicle_id` varchar(255) NOT NULL,
  `user_id` varchar(255) NOT NULL,
  `date` datetime NOT null,
  CONSTRAINT `vehicle_ride` FOREIGN KEY (`vehicle_id`) REFERENCES `vehicle` (`id`),
  CONSTRAINT `user_ride` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
);