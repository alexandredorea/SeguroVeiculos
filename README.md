# Sistema de Cálculo de Seguro de Veículos

Sistema completo para cálculo de seguros de veículos desenvolvido com .NET 8.0, Vue.js e arquitetura limpa (Clean Architecture).

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura](#arquitetura)
- [Funcionalidades](#funcionalidades)
- [Pré-requisitos](#pré-requisitos)
- [Instalação e Execução](#instalação-e-execução)
- [Uso da Aplicação](#uso-da-aplicação)
- [API Endpoints](#api-endpoints)
- [Testes](#testes)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Contribuição](#contribuição)

## 🎯 Visão Geral

Este sistema implementa um calculador de seguros de veículos seguindo uma fórmula específica de cálculo de risco e prêmios. O projeto demonstra a aplicação de conceitos avançados de desenvolvimento de software, incluindo Clean Architecture, CQRS, testes unitários e containerização.

### Fórmula de Cálculo

O sistema utiliza as seguintes fórmulas para calcular o seguro:

```
Variáveis Estáticas:
- MARGEM_SEGURANÇA = 3%
- LUCRO = 5%

Cálculos:
- Taxa de Risco = (Valor do Veículo × 5) ÷ (2 × Valor do Veículo)
- Prêmio de Risco = Taxa de Risco × Valor do Veículo
- Prêmio Puro = Prêmio de Risco × (1 + MARGEM_SEGURANÇA)
- Prêmio Comercial = LUCRO × Prêmio Puro
- Valor Final do Seguro = Prêmio Comercial
```

### Exemplo de Cálculo

Para um veículo de R$ 10.000,00:
- Taxa de Risco: 50.000 ÷ (2 × 10.000) = 2,5%
- Prêmio de Risco: 2,5% × 10.000 = R$ 250,00
- Prêmio Puro: 250 × (1 + 0,03) = R$ 257,50
- Prêmio Comercial: 5% × 257,50 = R$ 270,37
- **Valor Final: R$ 270,37**

## 🚀 Tecnologias Utilizadas

### Backend
- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - API REST
- **Entity Framework Core** - ORM
- **SQLite** - Banco de dados
- **MediatR** - Implementação do padrão Mediator
- **FluentValidation** - Validações
- **xUnit** - Testes unitários
- **Moq** - Mocks para testes
- **FluentAssertions** - Assertions para testes

### Frontend
- **Vue.js 3** - Framework JavaScript
- **TypeScript** - Tipagem estática
- **Vite** - Build tool
- **Axios** - Cliente HTTP
- **Pinia** - Gerenciamento de estado
- **Vue Router** - Roteamento

### DevOps e Infraestrutura
- **Docker** - Containerização
- **Docker Compose** - Orquestração de containers
- **Nginx** - Servidor web para frontend
- **JSON Server** - Mock service para dados de segurados

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture** com separação clara de responsabilidades:

### Camadas da API

1. **Domain Layer** (`SeguroVeiculos.Domain`)
   - Entidades de negócio
   - Value Objects
   - Interfaces de repositório
   - Regras de negócio

2. **Application Layer** (`SeguroVeiculos.Application`)
   - Commands e Queries (CQRS)
   - Handlers (MediatR)
   - DTOs
   - Validadores (FluentValidation)
   - Interfaces de serviços

3. **Infrastructure Layer** (`SeguroVeiculos.Infrastructure`)
   - Implementação de repositórios
   - Entity Framework DbContext
   - Serviços externos
   - Configurações de banco

4. **Presentation Layer** (`SeguroVeiculos.API`)
   - Controllers
   - Configuração de DI
   - Middlewares
   - Startup

### Princípios SOLID Aplicados

- **SRP**: Cada classe tem uma única responsabilidade
- **OCP**: Extensível através de interfaces
- **LSP**: Implementações podem ser substituídas
- **ISP**: Interfaces específicas e coesas
- **DIP**: Dependência de abstrações, não implementações

## ✨ Funcionalidades

### API Backend
- ✅ Criar seguro de veículo
- ✅ Calcular valor do seguro automaticamente
- ✅ Buscar seguro por ID
- ✅ Gerar relatório com médias aritméticas
- ✅ Integração com serviço externo de segurados
- ✅ Validações de entrada
- ✅ Tratamento de erros
- ✅ Padrão de resposta consistente

### Frontend
- ✅ Tela de relatório de médias
- ✅ Interface responsiva
- ✅ Carregamento de dados da API
- ✅ Tratamento de erros
- ✅ Exportação de dados em JSON
- ✅ Design moderno e intuitivo

### Mock Service
- ✅ API REST para dados de segurados
- ✅ Busca por CPF
- ✅ Dados de exemplo pré-carregados
- ✅ Operações CRUD completas

## 📋 Pré-requisitos

Para executar este projeto, você precisa ter instalado:

- **Docker** (versão 20.10 ou superior)
- **Docker Compose** (versão 2.0 ou superior)

### Verificação dos Pré-requisitos

```bash
# Verificar versão do Docker
docker --version

# Verificar versão do Docker Compose
docker compose version
```

## 🚀 Instalação e Execução

### Opção 1: Execução com Docker Compose (Recomendado)

1. **Clone ou extraia o projeto**
   ```bash
   # Se usando Git
   git clone <url-do-repositorio>
   cd seguro-veiculos
   
   # Ou extraia o arquivo ZIP e navegue para o diretório
   ```

2. **Execute todos os serviços**
   ```bash
   docker compose up --build
   ```

3. **Aguarde a inicialização**
   - O processo pode levar alguns minutos na primeira execução
   - Aguarde até ver as mensagens de que todos os serviços estão rodando

4. **Acesse a aplicação**
   - **Frontend**: http://localhost
   - **API**: http://localhost:5000
   - **Mock Service**: http://localhost:3001
   - **Swagger (API Docs)**: http://localhost:5000/swagger

### Opção 2: Execução Individual dos Serviços

#### Mock Service
```bash
cd mock-service
npm install
npm start
# Rodará em http://localhost:3001
```

#### API .NET
```bash
cd api
dotnet restore
dotnet run --project src/SeguroVeiculos.API
# Rodará em http://localhost:5000
```

#### Frontend Vue.js
```bash
cd frontend/seguro-veiculos-frontend
npm install
npm run dev
# Rodará em http://localhost:5173
```

### Comandos Docker Úteis

```bash
# Parar todos os serviços
docker compose down

# Rebuild e restart
docker compose up --build --force-recreate

# Ver logs de um serviço específico
docker compose logs api
docker compose logs frontend
docker compose logs mock-service

# Executar em background
docker compose up -d

# Remover volumes (reset do banco)
docker compose down -v
```

## 📱 Uso da Aplicação

### Acessando o Sistema

1. **Abra o navegador** e acesse http://localhost
2. **Visualize o relatório** de médias dos seguros
3. **Use os botões** para atualizar dados ou exportar JSON

### Criando um Novo Seguro (via API)

```bash
# Exemplo usando curl
curl -X POST http://localhost:5000/api/seguros \
  -H "Content-Type: application/json" \
  -d '{
    "cpf": "12345678901",
    "veiculoValor": 25000,
    "veiculoMarcaModelo": "Honda Civic 2023"
  }'
```

### Consultando Relatório (via API)

```bash
# Obter relatório de médias
curl http://localhost:5000/api/seguros/relatorio
```

## 🔌 API Endpoints

### Seguros

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/seguros` | Criar novo seguro |
| GET | `/api/seguros/{id}` | Buscar seguro por ID |
| GET | `/api/seguros/relatorio` | Obter relatório de médias |

### Mock Service - Segurados

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/segurados` | Listar todos os segurados |
| GET | `/segurados/{id}` | Buscar segurado por ID |
| GET | `/segurados?cpf={cpf}` | Buscar segurado por CPF |
| POST | `/segurados` | Criar novo segurado |
| PUT | `/segurados/{id}` | Atualizar segurado |
| DELETE | `/segurados/{id}` | Deletar segurado |

### Exemplos de Requisições

#### Criar Seguro
```json
POST /api/seguros
{
  "cpf": "12345678901",
  "veiculoValor": 30000,
  "veiculoMarcaModelo": "Toyota Corolla 2023"
}
```

#### Resposta de Sucesso
```json
{
  "success": true,
  "status": 200,
  "message": "Seguro criado com sucesso",
  "data": {
    "id": 1,
    "seguradorId": 1,
    "veiculoValor": 30000,
    "veiculoMarcaModelo": "Toyota Corolla 2023",
    "taxaRisco": 2.5,
    "premioRisco": 75000,
    "premioPuro": 77250,
    "premioComercial": 3862.5,
    "valorFinal": 3862.5,
    "dataCriacao": "2025-01-15T10:30:00Z",
    "segurado": {
      "id": 1,
      "nome": "João Silva Santos",
      "cpf": "12345678901",
      "idade": 35
    }
  },
  "error": null
}
```

## 🧪 Testes

### Executar Testes Unitários

```bash
# Via Docker
docker compose exec api dotnet test

# Localmente
cd api
dotnet test
```

### Cobertura de Testes

O projeto inclui testes para:
- ✅ Cálculo de seguro (fórmulas matemáticas)
- ✅ Validações de entrada
- ✅ Handlers de commands e queries
- ✅ Value objects e entidades
- ✅ Repositórios (com mocks)

### Executar Testes com Relatório

```bash
cd api
dotnet test --logger "console;verbosity=detailed"
```

## 📁 Estrutura do Projeto

```
seguro-veiculos/
├── api/                              # Backend .NET 8.0
│   ├── src/
│   │   ├── SeguroVeiculos.API/       # Camada de apresentação
│   │   ├── SeguroVeiculos.Application/ # Camada de aplicação
│   │   ├── SeguroVeiculos.Domain/    # Camada de domínio
│   │   └── SeguroVeiculos.Infrastructure/ # Camada de infraestrutura
│   └── tests/
│       └── SeguroVeiculos.Tests/     # Testes unitários
├── frontend/                         # Frontend Vue.js
│   └── seguro-veiculos-frontend/
│       ├── src/
│       │   ├── components/           # Componentes Vue
│       │   ├── services/             # Serviços (API)
│       │   └── router/               # Configuração de rotas
│       └── dist/                     # Build de produção
├── mock-service/                     # Mock service (JSON Server)
│   ├── db.json                       # Dados de exemplo
│   └── package.json
├── docker/                           # Configurações Docker
│   ├── Dockerfile.api
│   ├── Dockerfile.frontend
│   ├── Dockerfile.mock
│   └── nginx.conf
├── docs/                             # Documentação
│   └── arquitetura.md
├── docker-compose.yml                # Orquestração de containers
└── README.md                         # Este arquivo
```

## 🔧 Configuração Avançada

### Variáveis de Ambiente

#### API (.NET)
```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:5000
ConnectionStrings__DefaultConnection=Data Source=/app/data/seguro_veiculos.db
SeguradorService__BaseUrl=http://mock-service:3001
```

#### Frontend (Vue.js)
```bash
VITE_API_BASE_URL=http://localhost:5000/api
```

### Personalização do Docker Compose

Para modificar portas ou configurações, edite o arquivo `docker-compose.yml`:

```yaml
services:
  frontend:
    ports:
      - "8080:80"  # Mudar porta do frontend para 8080
  
  api:
    ports:
      - "5001:5000"  # Mudar porta da API para 5001
```

## 🐛 Solução de Problemas

### Problemas Comuns

1. **Erro de porta em uso**
   ```bash
   # Verificar processos usando as portas
   netstat -tulpn | grep :80
   netstat -tulpn | grep :5000
   netstat -tulpn | grep :3001
   
   # Parar containers existentes
   docker compose down
   ```

2. **Erro de build do Docker**
   ```bash
   # Limpar cache do Docker
   docker system prune -a
   
   # Rebuild forçado
   docker compose build --no-cache
   ```

3. **Banco de dados não inicializa**
   ```bash
   # Remover volume do banco
   docker compose down -v
   docker compose up --build
   ```

4. **Frontend não carrega dados**
   - Verificar se a API está rodando em http://localhost:5000
   - Verificar logs do container da API: `docker compose logs api`
   - Verificar configuração de CORS na API

### Logs e Debugging

```bash
# Ver logs de todos os serviços
docker compose logs

# Ver logs de um serviço específico
docker compose logs -f api

# Entrar no container para debug
docker compose exec api bash
docker compose exec frontend sh
```

## 📊 Monitoramento

### Health Checks

O sistema inclui health checks para todos os serviços:

```bash
# Verificar status dos containers
docker compose ps

# Verificar health check da API
curl http://localhost:5000/health

# Verificar health check do mock service
curl http://localhost:3001/segurados
```

### Métricas de Performance

- **API**: Tempo de resposta < 200ms para operações simples
- **Frontend**: First Contentful Paint < 1.5s
- **Mock Service**: Tempo de resposta < 50ms

## 🤝 Contribuição

### Como Contribuir

1. **Fork** o projeto
2. **Crie** uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. **Push** para a branch (`git push origin feature/AmazingFeature`)
5. **Abra** um Pull Request

### Padrões de Código

- **C#**: Seguir convenções do .NET e princípios SOLID
- **TypeScript/Vue**: Seguir guia de estilo do Vue.js
- **Commits**: Usar Conventional Commits
- **Testes**: Manter cobertura > 80%

### Reportar Bugs

Use as [Issues do GitHub](link-para-issues) para reportar bugs, incluindo:
- Descrição detalhada do problema
- Passos para reproduzir
- Ambiente (OS, Docker version, etc.)
- Logs relevantes

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
