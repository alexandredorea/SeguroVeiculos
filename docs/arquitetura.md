# Arquitetura do Sistema de Cálculo de Seguro de Veículos

## Visão Geral

Este documento descreve a arquitetura do sistema de cálculo de seguro de veículos, implementado seguindo os princípios da Clean Architecture, utilizando .NET 8.0 para a API backend e Vue.js para o frontend.

## Arquitetura Clean Architecture

O projeto segue os princípios da Clean Architecture, organizando o código em camadas bem definidas:

### 1. Camada de Domínio (Domain Layer)
- **Entidades**: Representam os conceitos centrais do negócio
  - `Seguro`: Entidade principal que agrega veículo e segurado
  - `Veiculo`: Value object com valor e marca/modelo
  - `Segurado`: Entidade com dados pessoais
- **Value Objects**: Objetos imutáveis que representam conceitos do domínio
- **Interfaces de Repositório**: Contratos para acesso a dados
- **Serviços de Domínio**: Lógica de negócio complexa

### 2. Camada de Aplicação (Application Layer)
- **Commands**: Operações que modificam o estado (CQRS)
  - `CriarSeguroCommand`: Criar novo seguro
  - `CalcularSeguroCommand`: Calcular valor do seguro
- **Queries**: Operações de consulta (CQRS)
  - `ObterSeguroQuery`: Buscar seguro por ID
  - `ObterRelatorioMediasQuery`: Gerar relatório de médias
- **Handlers**: Implementações usando MediatR
- **Validators**: Validações usando FluentValidation
- **DTOs**: Objetos de transferência de dados

### 3. Camada de Infraestrutura (Infrastructure Layer)
- **Repositórios**: Implementações concretas dos repositórios
- **Entity Framework**: Configuração do ORM
- **Serviços Externos**: Integração com APIs externas
- **Configurações**: Mapeamentos e configurações

### 4. Camada de Apresentação (Presentation Layer)
- **Controllers**: Endpoints da API REST
- **Middlewares**: Tratamento de erros e logging
- **Configurações**: Startup e DI

## Padrões Implementados

### CQRS (Command Query Responsibility Segregation)
- Separação clara entre operações de comando e consulta
- Commands para operações que modificam estado
- Queries para operações de leitura

### MediatR
- Implementação do padrão Mediator
- Desacoplamento entre controllers e handlers
- Pipeline de comportamentos (validação, logging)

### FluentValidation
- Validações expressivas e reutilizáveis
- Separação da lógica de validação
- Mensagens de erro customizadas

### Repository Pattern
- Abstração do acesso a dados
- Facilita testes unitários
- Inversão de dependência

## Cálculo de Seguro

### Fórmulas Implementadas
```
Variáveis Estáticas:
- MARGEM_SEGURANÇA = 3%
- LUCRO = 5%

Cálculos:
- Taxa de Risco = (Valor do Veículo * 5) / (2 * Valor do Veículo)
- Prêmio de Risco = Taxa de Risco * Valor do Veículo
- Prêmio Puro = Prêmio de Risco * (1 + MARGEM_SEGURANÇA)
- Prêmio Comercial = LUCRO * Prêmio Puro
```

### Exemplo de Cálculo
```
Valor do Veículo: R$ 10.000,00
Taxa de Risco: 50.000 / (2 * 10.000) = 2,5%
Prêmio de Risco: 2,5% * 10.000 = R$ 250,00
Prêmio Puro: 250 * (1 + 0,03) = R$ 257,50
Prêmio Comercial: 5% * 257,50 = R$ 270,37
Valor Final do Seguro: R$ 270,37
```

## Estrutura do Banco de Dados

### Tabela Seguros
- Id (int, PK)
- SeguradorId (int, FK)
- VeiculoValor (decimal)
- VeiculoMarcaModelo (string)
- TaxaRisco (decimal)
- PremioRisco (decimal)
- PremioPuro (decimal)
- PremioComercial (decimal)
- ValorFinal (decimal)
- DataCriacao (datetime)

### Tabela Segurados
- Id (int, PK)
- Nome (string)
- CPF (string)
- Idade (int)

## API Endpoints

### Seguros
- `POST /api/seguros` - Criar novo seguro
- `GET /api/seguros/{id}` - Obter seguro por ID
- `GET /api/seguros/relatorio` - Obter relatório de médias

### Segurados (Mock Service)
- `GET /api/segurados/{cpf}` - Obter dados do segurado

## Frontend Vue.js

### Componentes
- **RelatorioComponent**: Tela principal com relatório de médias
- **ApiService**: Serviço para comunicação com a API

### Funcionalidades
- Exibição de relatório com médias aritméticas
- Interface responsiva
- Integração com API REST

## Testes Unitários

### Cobertura de Testes
- Testes para cálculo de seguro
- Testes para commands e queries
- Testes para validações
- Testes para repositórios (mocks)

### Frameworks Utilizados
- xUnit
- Moq
- FluentAssertions

## Containerização

### Docker
- Dockerfile para API .NET
- Dockerfile para frontend Vue.js
- Dockerfile para JSON Server (mock)

### Docker Compose
- Orquestração de todos os serviços
- Configuração de redes
- Volumes para persistência

## Princípios SOLID Aplicados

### Single Responsibility Principle (SRP)
- Cada classe tem uma única responsabilidade
- Separação clara entre camadas

### Open/Closed Principle (OCP)
- Extensível através de interfaces
- Fechado para modificação

### Liskov Substitution Principle (LSP)
- Implementações podem ser substituídas
- Contratos bem definidos

### Interface Segregation Principle (ISP)
- Interfaces específicas e coesas
- Clientes não dependem de métodos não utilizados

### Dependency Inversion Principle (DIP)
- Dependência de abstrações
- Inversão de controle através de DI

## Tecnologias Utilizadas

### Backend
- .NET 8.0
- Entity Framework Core
- MediatR
- FluentValidation
- SQLite
- xUnit

### Frontend
- Vue.js 3
- Axios
- Bootstrap/CSS

### DevOps
- Docker
- Docker Compose
- JSON Server

## Estrutura de Diretórios

```
seguro-veiculos/
├── api/
│   ├── src/
│   │   ├── SeguroVeiculos.Domain/
│   │   ├── SeguroVeiculos.Application/
│   │   ├── SeguroVeiculos.Infrastructure/
│   │   └── SeguroVeiculos.API/
│   └── tests/
│       └── SeguroVeiculos.Tests/
├── frontend/
│   ├── src/
│   ├── public/
│   └── package.json
├── mock-service/
│   ├── db.json
│   └── package.json
├── docker/
│   ├── Dockerfile.api
│   ├── Dockerfile.frontend
│   └── docker-compose.yml
└── docs/
    ├── arquitetura.md
    └── README.md
```

