CREATE TABLE Clientes (
    Id INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Login VARCHAR(60) NOT NULL,
    Senha VARCHAR(60) NOT NULL,
    Email VARCHAR(60) NOT NULL,
    Tipo TINYINT,
    Endereco JSON
)
ENGINE=InnoDB
;

INSERT INTO Clientes (Login, Senha, Email, Tipo)
VALUES
    ('elvis', 'pass123', 'elvis@google.com', 1),
    ('asdf', 'asdfasdf', 'asdf@asdf.com', 0),
    ('teste', '123456', 'teste@teste.com', 1);