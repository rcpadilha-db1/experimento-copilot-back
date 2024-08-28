CREATE TABLE VEHICLE (
    id SERIAL PRIMARY KEY,
    plate VARCHAR(255) NOT NULL,
    capacity INT NOT NULL,
    user_id BIGINT,
    CONSTRAINT fk_user_owner FOREIGN KEY (user_id) REFERENCES "USER"(id)
);