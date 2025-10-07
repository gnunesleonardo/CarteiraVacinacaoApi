# CarteiraVacinacaoApi

Implementação de API para gerenciamento de uma carteira de vacinas, como parte de teste técnico para a vaga de Engenheiro de Software Pleno no **[BTG Pactual](https://empresas.btgpactual.com/)**

## Requisitos da API
O sistema deve permitir o cadastro de vacinas recebidas por uma pessoa, registrando informações como nome da vacina, data da aplicação e doses aplicadas.

As funcionalidades devem estar acessíveis via APIs REST, possibilitando o cadastro, consulta e exclusão das vacinas no cartão de vacinação.

## Funcionalidades
- Cadastrar uma vacina: Uma vacina consiste em um nome e um identificador único.
- Cadastrar uma pessoa: Uma pessoa consiste em um nome e um número de identificação único.
- Remover uma pessoa: Uma pessoa pode ser removida do sistema, o que também implica na exclusão de seu cartão de vacinação e todos os registros associados.
- Cadastrar uma vacinação: Para uma pessoa cadastrada, é possível registrar uma vacinação, fornecendo informações como o identificador da vacina e a dose aplicada (A dose deve ser validada pelo sistema).
- Consultar o cartão de vacinação de uma pessoa: Permite visualizar todas as vacinas registradas no cartão de vacinação de uma pessoa, incluindo detalhes como o nome da vacina, data de aplicação e doses recebidas.
- Excluir registro de vacinação: Permite excluir um registro de vacinação específico do cartão de vacinação de uma pessoa.

## Tecnologias utilizadas
- [.NET Core 8](https://dotnet.microsoft.com/)  
- [Entity Framework Core](https://learn.microsoft.com/ef/)  
- [MediatR](https://github.com/jbogard/MediatR)  
- [FluentValidation](https://docs.fluentvalidation.net/)  
- [Swagger / Swashbuckle](https://swagger.io/)  
- Banco de dados: **SQLite** (para simplicidade no teste)

## Decições Arquiteturais
Este projeto foi desenvolvido utilizando .NET 8, com foco em boas práticas de arquitetura em camadas e separação de responsabilidades.

### Estrutura de Camadas

- Domain: Contém as entidades e regras de negócio puras. Não possui dependência de outras camadas.

- Application: Contém os casos de uso (Commands/Queries com MediatR), DTOs, validações (FluentValidation) e interfaces de repositório.

- Infrastructure: Contém a implementação dos repositórios, DbContext (EF Core/SQLite) e serviços auxiliares (ex.: cache em memória).

- API: Camada de entrada (Controllers). Expõe os endpoints REST, realiza a autenticação/autorização, injeta dependências e retorna os DTOs para o cliente.

### Padrões Utilizados
- CQRS com MediatR: Separação entre comandos (escrita) e consultas (leitura).
Controllers não conhecem a lógica de negócio, apenas delegam para o Mediator.

- Repository Pattern: Acesso ao banco de dados via interfaces. A camada de Application depende apenas de abstrações.

- DTOs (Data Transfer Objects): Utilizados para requests/responses nas Controllers, evitando expor diretamente as entidades do Domain.

- Validation (FluentValidation): Todas as entradas da API são validadas por Validators específicos antes de chegar ao Handler.

- In-Memory Cache: Informações de vacinas armazenadas em cache para melhorar a performance.

- Autenticação Básica (Basic Auth): Implementada de forma simples de autenticação com Token JWT.


## Rodando o projeto
### 1. Clonar repositório
```bash
git clone https://github.com/gnunesleonardo/CarteiraVacinacaoApi.git
cd CarteiraVacinacaoApi
```

### 2. Restaurar depedências
```bash
dotnet restore
```

### 3. Criar banco de dados e aplicar migrations
```bash
dotnet ef database update --project CarteiraVacinacaoApi.Infrastructure --startup-project CarteiraVacinacaoApi.Api
```

### 4. Executar
```bash
dotnet run --project CarteiraVacinacaoApi.Api
```
A API ficará disponível em http://localhost:5256/swagger/index.html

## Principais Endpoints
### Autenticação
#### POST /api/auth/login
Usado para gerar Token JWT que deverá ser utilizado no `Header` de todas as requisições subsequentes. 

`Uso: Bearer {token}`

As credenciais de Admin podem ser configuradas em `appsettings.json`

**Exemplo:**
- Requisição:

```json
{
    "Username": "Admin",
    "Password": "Admin"
}
```

- Resposta
> `200 OK`
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJleHAiOjE3NTk4MDA1ODZ9.NzVbzJwcyQx33WuulRen4unvAiADrbY0nhxGwHvtYko"
}
```

### Pessoas
#### POST /persons
Usado para adicionar um pessoa.

**Exemplo:**
- Requisição:

```json
{
  "personName": "Leonardo"
}
```

- Resposta
> `201 OK`
```json
{
  "personId": 1,
  "personName": "Leonardo"
}
```

#### DELETE /persons/[personId]
Usado para remover um pessoa através de seu Id.

**Exemplo:**
- Resposta
> `200 OK`

### Vacinas
#### GET /api/vaccines
Recupera todas as vacinas cadastradas.

**Exemplo:**
- Resposta
> `200 OK`
```json
[
  {
    "vaccineId": 1,
    "vaccineName": "Covid-19",
    "dosesRequired": 2
  },
  {
    "vaccineId": 2,
    "vaccineName": "Febre Amarela",
    "dosesRequired": 2
  },
  {
    "vaccineId": 3,
    "vaccineName": "Gripe 2025",
    "dosesRequired": 1
  }
]
```

#### POST /vaccine
Usado para adicionar uma vacina.

**Exemplo:**
- Requisição:

```json
{
  "vaccineName": "HPV",
  "dosesRequired": 4
}
```

- Resposta
> `201 OK`
```json
{
  "vaccineId": 1,
  "vaccineName": "HPV",
  "dosesRequired": 4
}
```

### Registro de Vacinação
#### POST /api/vaccine-records
Usado para adicionar um registro de vacinação a uma pessoa cadastrada.

**Exemplo:**
- Requisição:

```json
{
  "personId": 1,
  "vaccineId": 4,
  "doseNumber": 2,
  "appliedDate": "2025-10-07T01:03:59.117Z"
}
```

- Resposta
> `201 OK`
```json
{
  "recordId": 7,
  "doseNumber": 2,
  "appliedDate": "2025-10-07T01:03:59.117Z",
  "vaccine": {
    "vaccineId": 4,
    "vaccineName": "HPV",
    "dosesRequired": 4
  }
}
```

#### GET /api/vaccine-records/card/[personId]
Usado para recuperar o cartão de vacina de determinada pessoa, contendo todos seus registros de vacinação.

**Exemplo:**
- Resposta
> `200 OK`
```json
{
  "personId": 1,
  "personName": "Leonardo Gomes",
  "vaccineRecords": [
    {
      "recordId": 1,
      "doseNumber": 1,
      "appliedDate": "2025-02-05T00:00:00",
      "vaccine": {
        "vaccineId": 1,
        "vaccineName": "Covid-19",
        "dosesRequired": 2
      }
    },
    {
      "recordId": 2,
      "doseNumber": 2,
      "appliedDate": "2025-08-05T00:00:00",
      "vaccine": {
        "vaccineId": 1,
        "vaccineName": "Covid-19",
        "dosesRequired": 2
      }
    },
    {
      "recordId": 5,
      "doseNumber": 1,
      "appliedDate": "2025-08-05T00:00:00",
      "vaccine": {
        "vaccineId": 3,
        "vaccineName": "Gripe 2025",
        "dosesRequired": 1
      }
    }
  ]
}
```

#### DELETE /vaccine-record/[id]
Usado para remover determinado registro de vacina.

**Exemplo:**
- Resposta
> `200 OK`