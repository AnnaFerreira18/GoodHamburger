# GoodHamburger 🍔
![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat&logo=sqlite&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat&logo=swagger&logoColor=black)

API REST de um sistema de gestão de hamburgueria. O sistema gerencia pedidos, realiza cálculos automáticos de descontos baseados em combos e valida regras de negócio específicas.

## 🚀 Tecnologias
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQLite** (Banco de dados local)
* **xUnit** para testes automatizados de unidade.
* **Swagger** para documentação e teste interativo da API.

## ⚙️ Como Executar o Projeto

Pensando na facilidade, o projeto utiliza um banco de dados local SQLite. Não é necessário configurar conexões externas (SQL Server, Docker, etc). Ao rodar a aplicação, o cardápio padrão já é injetado automaticamente.

**Pré-requisitos:**
* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) ou 10 instalado.
* Git
* Entity Framework Core Tools instalado globalmente.

**Passo a Passo (via terminal) :**

1. Clone o repositório: git clone https://github.com/AnnaFerreira18/GoodHamburger.git

   cd GoodHamburger

2. Restaurar dependências: dotnet restore

3. Instale o EF Core Tools (caso ainda não tenha): dotnet tool install --global dotnet-ef
   
4. Atualize o Banco de Dados (Migrations): dotnet ef database update -p Infrastructure -s Api

5. Rodar a API: dotnet run --project Api

  Acessar a documentação: Abra o navegador na porta disponibilizada no terminal e adicione /swagger/index.html.

  Ex: http://localhost:5093/swagger/index.html
  

**🧪 Testes Unitários**

Para garantir que os cálculos de desconto e as validações de duplicidade estão funcionando corretamente, execute na raiz do projeto: dotnet test

## 🏗️ Estrutura do Projeto

O projeto foi construído separando as responsabilidades:

* **Domain**: Contém as entidades de negócio, enums e as interfaces dos repositórios.
* **Application**:  Onde residem os DTOs (Data Transfer Objects) e a implementação do PedidoService, responsável pelas regras de desconto e validações.
* **Infrastructure**:  Implementação do acesso a dados com EF Core, Repositórios e o Contexto do Banco de Dados.
* **Api**: Porta de entrada do sistema, contendo os Controllers e configurações de Injeção de Dependência.

**💰 Regras de Negócio Implementadas**

Descontos Automáticos:

* 20% OFF: Sanduíche + Batata + Refrigerante.

* 15% OFF: Sanduíche + Refrigerante.

* 10% OFF: Sanduíche + Batata.

Validações:

* Limite por categoria: Cada pedido permite apenas 1 item de cada categoria (1 Sanduíche, 1 Batata, 1 Bebida).

* Bloqueio de duplicados: O sistema impede o envio de IDs repetidos no mesmo pedido.

## 🚧 O que ficou de fora 

* Frontend em Blazor: Optei por focar 100% em uma API robusta, validada e coberta por testes.

* Banco de Dados Relacional: Em produção, eu substituiria o SQLite por um banco relacional como PostgreSQL ou SQL Server.

* Autenticação/Autorização: A API está pública. Em um cenário real, os endpoints de criar/deletar pedidos seriam protegidos por tokens JWT (identificando garçons ou clientes).

* Logs e Monitoramento: Adição de ferramentas como Serilog para rastrear os eventos e erros da API.

* Middleware Global de Erros: Para centralizar o tratamento de exceções.

* Paginação: Implementação de paginação padrão no endpoint de listagem de pedidos.

#

**Desenvolvido por Anna Ferreira 🚀**
