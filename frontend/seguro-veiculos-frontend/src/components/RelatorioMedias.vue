<template>
  <div class="relatorio-container">
    <div class="header">
      <h1>Relatório de Médias - Seguros de Veículos</h1>
      <p class="subtitle">Médias aritméticas dos seguros cadastrados no sistema</p>
    </div>

    <div class="loading" v-if="loading">
      <div class="spinner"></div>
      <p>Carregando dados...</p>
    </div>

    <div class="error" v-if="error">
      <div class="error-icon">⚠️</div>
      <h3>Erro ao carregar dados</h3>
      <p>{{ error }}</p>
      <button @click="carregarDados" class="retry-btn">Tentar Novamente</button>
    </div>

    <div class="relatorio-content" v-if="!loading && !error && dados">
      <div class="cards-grid">
        <div class="metric-card valor-final">
          <div class="card-icon">💰</div>
          <div class="card-content">
            <h3>Valor Final Médio</h3>
            <p class="metric-value">{{ formatarMoeda(dados.mediaValorFinal) }}</p>
            <span class="metric-label">Valor médio dos seguros</span>
          </div>
        </div>

        <div class="metric-card taxa-risco">
          <div class="card-icon">📊</div>
          <div class="card-content">
            <h3>Taxa de Risco Média</h3>
            <p class="metric-value">{{ formatarPercentual(dados.mediaTaxaRisco) }}</p>
            <span class="metric-label">Percentual médio de risco</span>
          </div>
        </div>

        <div class="metric-card premio-risco">
          <div class="card-icon">🎯</div>
          <div class="card-content">
            <h3>Prêmio de Risco Médio</h3>
            <p class="metric-value">{{ formatarMoeda(dados.mediaPremioRisco) }}</p>
            <span class="metric-label">Valor médio do prêmio de risco</span>
          </div>
        </div>

        <div class="metric-card premio-puro">
          <div class="card-icon">🛡️</div>
          <div class="card-content">
            <h3>Prêmio Puro Médio</h3>
            <p class="metric-value">{{ formatarMoeda(dados.mediaPremioPuro) }}</p>
            <span class="metric-label">Valor médio do prêmio puro</span>
          </div>
        </div>

        <div class="metric-card premio-comercial">
          <div class="card-icon">💼</div>
          <div class="card-content">
            <h3>Prêmio Comercial Médio</h3>
            <p class="metric-value">{{ formatarMoeda(dados.mediaPremioComercial) }}</p>
            <span class="metric-label">Valor médio do prêmio comercial</span>
          </div>
        </div>
      </div>

      <div class="actions">
        <button @click="carregarDados" class="refresh-btn">
          🔄 Atualizar Dados
        </button>
        <button @click="exportarDados" class="export-btn">
          📥 Exportar JSON
        </button>
      </div>

      <div class="info-section">
        <h3>Informações sobre o Cálculo</h3>
        <div class="info-grid">
          <div class="info-item">
            <strong>Taxa de Risco:</strong>
            <span>(Valor do Veículo × 5) ÷ (2 × Valor do Veículo)</span>
          </div>
          <div class="info-item">
            <strong>Prêmio de Risco:</strong>
            <span>Taxa de Risco × Valor do Veículo</span>
          </div>
          <div class="info-item">
            <strong>Prêmio Puro:</strong>
            <span>Prêmio de Risco × (1 + 3% Margem de Segurança)</span>
          </div>
          <div class="info-item">
            <strong>Prêmio Comercial:</strong>
            <span>5% × Prêmio Puro</span>
          </div>
        </div>
      </div>
    </div>

    <div class="empty-state" v-if="!loading && !error && dados && isDataEmpty">
      <div class="empty-icon">📋</div>
      <h3>Nenhum seguro cadastrado</h3>
      <p>Não há dados de seguros para exibir no relatório.</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import apiService, { type RelatorioMediasDto } from '@/services/apiService'

const dados = ref<RelatorioMediasDto | null>(null)
const loading = ref(false)
const error = ref<string | null>(null)

const isDataEmpty = computed(() => {
  if (!dados.value) return true
  return dados.value.mediaValorFinal === 0 && 
         dados.value.mediaTaxaRisco === 0 && 
         dados.value.mediaPremioRisco === 0 && 
         dados.value.mediaPremioPuro === 0 && 
         dados.value.mediaPremioComercial === 0
})

const carregarDados = async () => {
  loading.value = true
  error.value = null
  
  try {
    dados.value = await apiService.obterRelatorioMedias()
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Erro ao carregar dados do relatório'
    console.error('Erro ao carregar relatório:', err)
  } finally {
    loading.value = false
  }
}

const formatarMoeda = (valor: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).format(valor)
}

const formatarPercentual = (valor: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'percent',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  }).format(valor / 100)
}

const exportarDados = () => {
  if (!dados.value) return
  
  const dataStr = JSON.stringify(dados.value, null, 2)
  const dataBlob = new Blob([dataStr], { type: 'application/json' })
  const url = URL.createObjectURL(dataBlob)
  
  const link = document.createElement('a')
  link.href = url
  link.download = `relatorio-medias-seguros-${new Date().toISOString().split('T')[0]}.json`
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  
  URL.revokeObjectURL(url)
}

onMounted(() => {
  carregarDados()
})
</script>

<style scoped>
.relatorio-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.header {
  text-align: center;
  margin-bottom: 3rem;
}

.header h1 {
  color: #2c3e50;
  font-size: 2.5rem;
  font-weight: 700;
  margin-bottom: 0.5rem;
}

.subtitle {
  color: #7f8c8d;
  font-size: 1.1rem;
  margin: 0;
}

.loading {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 4rem 2rem;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #f3f3f3;
  border-top: 4px solid #3498db;
  border-radius: 50%;
  animation: spin 1s linear infinite;
  margin-bottom: 1rem;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.error {
  text-align: center;
  padding: 3rem 2rem;
  background: #fff5f5;
  border: 1px solid #fed7d7;
  border-radius: 12px;
  margin: 2rem 0;
}

.error-icon {
  font-size: 3rem;
  margin-bottom: 1rem;
}

.error h3 {
  color: #e53e3e;
  margin-bottom: 0.5rem;
}

.error p {
  color: #c53030;
  margin-bottom: 1.5rem;
}

.retry-btn {
  background: #e53e3e;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: background-color 0.2s;
}

.retry-btn:hover {
  background: #c53030;
}

.cards-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1.5rem;
  margin-bottom: 3rem;
}

.metric-card {
  background: white;
  border-radius: 16px;
  padding: 2rem;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  border: 1px solid #e2e8f0;
  transition: transform 0.2s, box-shadow 0.2s;
  display: flex;
  align-items: center;
  gap: 1.5rem;
}

.metric-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
}

.card-icon {
  font-size: 2.5rem;
  flex-shrink: 0;
}

.card-content {
  flex: 1;
}

.card-content h3 {
  color: #2d3748;
  font-size: 1.1rem;
  font-weight: 600;
  margin: 0 0 0.5rem 0;
}

.metric-value {
  font-size: 1.8rem;
  font-weight: 700;
  margin: 0.5rem 0;
  color: #1a202c;
}

.metric-label {
  color: #718096;
  font-size: 0.9rem;
}

.valor-final {
  border-left: 4px solid #48bb78;
}

.taxa-risco {
  border-left: 4px solid #4299e1;
}

.premio-risco {
  border-left: 4px solid #ed8936;
}

.premio-puro {
  border-left: 4px solid #9f7aea;
}

.premio-comercial {
  border-left: 4px solid #38b2ac;
}

.actions {
  display: flex;
  gap: 1rem;
  justify-content: center;
  margin-bottom: 3rem;
  flex-wrap: wrap;
}

.refresh-btn, .export-btn {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s;
  font-size: 1rem;
}

.refresh-btn {
  background: #4299e1;
  color: white;
}

.refresh-btn:hover {
  background: #3182ce;
}

.export-btn {
  background: #48bb78;
  color: white;
}

.export-btn:hover {
  background: #38a169;
}

.info-section {
  background: #f7fafc;
  border-radius: 12px;
  padding: 2rem;
  border: 1px solid #e2e8f0;
}

.info-section h3 {
  color: #2d3748;
  margin-bottom: 1.5rem;
  font-size: 1.3rem;
}

.info-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1rem;
}

.info-item {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.info-item strong {
  color: #4a5568;
  font-weight: 600;
}

.info-item span {
  color: #718096;
  font-size: 0.9rem;
}

.empty-state {
  text-align: center;
  padding: 4rem 2rem;
  background: #f7fafc;
  border-radius: 12px;
  border: 1px solid #e2e8f0;
}

.empty-icon {
  font-size: 4rem;
  margin-bottom: 1rem;
}

.empty-state h3 {
  color: #4a5568;
  margin-bottom: 0.5rem;
}

.empty-state p {
  color: #718096;
}

@media (max-width: 768px) {
  .relatorio-container {
    padding: 1rem;
  }
  
  .header h1 {
    font-size: 2rem;
  }
  
  .cards-grid {
    grid-template-columns: 1fr;
  }
  
  .metric-card {
    padding: 1.5rem;
  }
  
  .actions {
    flex-direction: column;
    align-items: center;
  }
  
  .refresh-btn, .export-btn {
    width: 100%;
    max-width: 300px;
  }
}
</style>

