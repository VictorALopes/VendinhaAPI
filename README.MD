![](https://img.shields.io/badge/.NET%207-v7.0.2-purple?style=for-the-badge&logo=dotnet&labelColor=white&logoColor=purple)
  
![](https://img.shields.io/badge/Database-SQLite-blue?style=for-the-badge&logo=sqlite&labelColor=white&logoColor=blue)

##
# Vendinha API

Vendinha API é um projeto demonstrativo que implementa uma API RESTful onde é possível gerenciar clientes e dívidas.


### Cenário (fictício)
Uma vendinha precisa informatizar o controle de contas de seus clientes (dívidas penduradas) para facilitar a busca e o cadastro desses dados que antes eram feitos por papel. O cliente chega na loja, faz a compra e pede para o atendente pendurar para que seja acertado no final do mês.

Pensando nisso, é necessário criar um sistema simples de cadastro para que o dono da venda consiga controlar as dívidas de seus clientes.

## Instalações necessárias

### .NET ⚙
Para execução deste projeto é necessário a instalação da [SDK do .NET 7](https://dotnet.microsoft.com/en-us/download)

No desenvolvimento foi utilizado a versão 7.0.2 lançada em 10 de janeiro de 2023.

### Banco de dados e Migrações 🧰
O banco de dados utilizado para este projeto é o [SQLite](https://sqlite.org/index.html). É um banco de dados relacional com sintaxe SQL, leve e simples de utilizar, ideal para aplicações pequenas ou demonstrativas como o caso deste projeto.
Pode ser instalado pelo gerenciador de pacotes do .NET, o [NuGet](https://www.nuget.org/), pela interface gráfica do Visual Studio ou o seguinte comando no terminal (na pasta do projeto):
```
dotnet add package Microsoft.EntityFrameworkCore.SqLite
```

Para execução das migrações do banco de dados é necessário também o .NET Entity Framework, que pode ser instalado com o comando:
```
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Após isso, na pasta do projeto execute o comando `dotnet ef database update` para criar corretamente o banco de dados e as tabelas.

## Executando e utilizando a API
### Execução da API 🖥

Abra a pasta do projeto no terminal linha de comando de sua preferência ou no Visual Studio Code e execute o seguinte comando no terminal integrado:
```
dotnet watch run
```
Isto deixará o projeto executando e abrirá em seu navegador padrão a rota do Swagger da API.

Exemplo: http://localhost:5225/swagger/index.html

Agora pode utilizar a API pelo Swagger ou chamando os Endpoints pelo Postman ou programa similar.
##
