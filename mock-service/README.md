# Mock Service - Dados de Segurados

Este é um serviço mock que simula uma API REST para fornecer dados de segurados usando JSON Server.

## Funcionalidades

- API REST completa para dados de segurados
- Suporte a operações CRUD (Create, Read, Update, Delete)
- Busca por CPF
- Dados de exemplo pré-carregados

## Endpoints Disponíveis

### Listar todos os segurados
```
GET http://localhost:3001/segurados
```

### Buscar segurado por ID
```
GET http://localhost:3001/segurados/{id}
```

### Buscar segurado por CPF
```
GET http://localhost:3001/segurados?cpf={cpf}
```

### Criar novo segurado
```
POST http://localhost:3001/segurados
Content-Type: application/json

{
  "nome": "Nome do Segurado",
  "cpf": "12345678901",
  "idade": 30
}
```

### Atualizar segurado
```
PUT http://localhost:3001/segurados/{id}
Content-Type: application/json

{
  "nome": "Nome Atualizado",
  "cpf": "12345678901",
  "idade": 31
}
```

### Deletar segurado
```
DELETE http://localhost:3001/segurados/{id}
```

## Como executar

### Desenvolvimento
```bash
npm run dev
```

### Produção
```bash
npm start
```

O serviço será executado em `http://localhost:3001`

## Dados de Exemplo

O arquivo `db.json` contém 15 segurados de exemplo com dados fictícios:

- João Silva Santos (CPF: 12345678901)
- Maria Oliveira Costa (CPF: 98765432109)
- Carlos Eduardo Ferreira (CPF: 11122233344)
- Ana Paula Rodrigues (CPF: 55566677788)
- Pedro Henrique Lima (CPF: 99988877766)
- E mais 10 registros...

## Integração com a API Principal

A API principal (.NET 8.0) está configurada para buscar dados de segurados neste mock service quando um CPF não é encontrado no banco de dados local.

### Configuração na API Principal

No arquivo `appsettings.json` da API principal:

```json
{
  "SeguradorService": {
    "BaseUrl": "http://localhost:3001"
  }
}
```

## Docker

Este serviço também pode ser executado via Docker usando o docker-compose.yml na raiz do projeto.

## Tecnologias Utilizadas

- Node.js
- JSON Server
- NPM

## Estrutura de Dados

Cada segurado possui a seguinte estrutura:

```json
{
  "id": 1,
  "nome": "João Silva Santos",
  "cpf": "12345678901",
  "idade": 35
}
```

## CORS

O JSON Server automaticamente habilita CORS para todas as origens, permitindo que o frontend e a API principal acessem os dados sem problemas de política de mesma origem.

