# 🚀 Guia de Início Rápido

## Execução em 3 Passos

### 1. Pré-requisitos
- Docker e Docker Compose instalados

### 2. Executar
```bash
cd seguro-veiculos
docker compose up --build
```

### 3. Acessar
- **Frontend**: http://localhost
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger

## 📱 Como Usar

1. Abra http://localhost no navegador
2. Visualize o relatório de médias dos seguros
3. Use os botões para atualizar ou exportar dados

## 🧪 Testar API

```bash
# Criar um seguro
curl -X POST http://localhost:5000/api/seguros \
  -H "Content-Type: application/json" \
  -d '{
    "cpf": "12345678901",
    "veiculoValor": 25000,
    "veiculoMarcaModelo": "Honda Civic 2023"
  }'

# Ver relatório
curl http://localhost:5000/api/seguros/relatorio
```

## 🛑 Parar

```bash
docker compose down
```

Para mais detalhes, consulte o [README.md](README.md) completo.

