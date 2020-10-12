# Projeto de teste

Para rodar a aplicação

```bash
> docker-compose run --rm api-compras

```

Você pode passar como parametro os segundos que o servico deve espera para iniciar, um suficiente para todas as dependências levantarem(o padrão é 40 segundos).

```bash
> docker-compose run --rm api-compras 20

```

Para limpar as imagens e volumes criados

```bash
> docker-compose down -v && docker rmi api-compras

```

Usuario para testar a aplucação

```bash
> curl --location --request POST 'localhost:5000/v1/account/login' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Login": "teste",
    "Senha": "123456"
}'

```