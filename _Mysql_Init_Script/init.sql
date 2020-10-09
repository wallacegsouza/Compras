CREATE TABLE CLIENTES (
    ID INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    LOGIN VARCHAR(60) NOT NULL,
    SENHA VARCHAR(60) NOT NULL,
    EMAIL VARCHAR(60) NOT NULL
)
ENGINE=InnoDB
;

INSERT INTO CLIENTES (LOGIN, SENHA, EMAIL)
VALUES
    ('elvis', 'pass123', 'elvis@google.com'),
    ('asdf', 'asdfasdf', 'asdf@asdf.com'),
    ('teste', '123456', 'teste@teste.com');