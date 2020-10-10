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
    ('amaro', 'amarildo', 'amaro@aws.com', 0),
    ('hebret', 'sen30', 'hebert@ibm.com', 1),
    ('teste', '123456', 'teste@teste.com', 1);

CREATE TABLE Compras (
    Id INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Valor DECIMAL(11, 2) NOT NULL,
    DataVenda DATETIME NOT NULL,
    Itens JSON
)
ENGINE=InnoDB
;

INSERT INTO Compras (Valor, DataVenda, Itens)
VALUES
    ( 15.99, now(), JSON_ARRAY(
        JSON_OBJECT('id', 1)
    )),
    ( 30.01, now(), JSON_ARRAY(
        JSON_OBJECT('id', 2), JSON_OBJECT('id', 3)
    )),
    ( 123456789.11, now(), JSON_ARRAY(
        JSON_OBJECT('id', 4), JSON_OBJECT('id', 5), JSON_OBJECT('id', 6)
    ));

CREATE TABLE Itens (
    Id INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(60) NOT NULL,
    Descricao VARCHAR(255) NOT NULL,
    Valor DECIMAL(11, 2) NOT NULL,
    DataValidade DATETIME NOT NULL,
    UnidadeMedida VARCHAR(5),
    Quantificador DECIMAL(11, 2)
)
ENGINE=InnoDB
;

INSERT INTO Itens (Nome, Descricao, Valor, DataValidade)
VALUES
    ('H2OH', 'Refrigerante de limão', 2.69, DATE_ADD(NOW(), INTERVAL 2 YEAR)),
    ('Omo', 'Sabão em pó', 44, DATE_ADD(NOW(), INTERVAL 3 YEAR)),
    ('Detergente Ypê', 'Detergente para remoção de sujeiras', 3.52, DATE_ADD(NOW(), INTERVAL 3 YEAR)),
    ('Clear', 'Shampoo Clear', 16.9, DATE_ADD(NOW(), INTERVAL 5 YEAR)),
    ('Cheetos Mix', 'Salgadinho Elma Chips Cheetos', 5.5, DATE_ADD(NOW(), INTERVAL 12 YEAR)),
    ('AN-225', 'Aeronave Antonov 225', 44, DATE_ADD(NOW(), INTERVAL 50 YEAR));
