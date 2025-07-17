import axios from 'axios'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
})

export interface SeguradorDto {
  id: number
  nome: string
  cpf: string
  idade: number
}

export interface SeguroDto {
  id: number
  seguradorId: number
  veiculoValor: number
  veiculoMarcaModelo: string
  taxaRisco: number
  premioRisco: number
  premioPuro: number
  premioComercial: number
  valorFinal: number
  dataCriacao: string
  segurado?: SeguradorDto
}

export interface RelatorioMediasDto {
  mediaValorFinal: number
  mediaTaxaRisco: number
  mediaPremioRisco: number
  mediaPremioPuro: number
  mediaPremioComercial: number
}

export interface ApiResponse<T> {
  success: boolean
  status: number
  message: string
  data: T
  error?: Array<{ code: string; message: string }>
}

export interface CriarSeguroCommand {
  cpf: string
  veiculoValor: number
  veiculoMarcaModelo: string
}

class ApiService {
  async criarSeguro(command: CriarSeguroCommand): Promise<SeguroDto> {
    const response = await apiClient.post<ApiResponse<SeguroDto>>('/seguros', command)
    return response.data.data
  }

  async obterSeguro(id: number): Promise<SeguroDto> {
    const response = await apiClient.get<ApiResponse<SeguroDto>>(`/seguros/${id}`)
    return response.data.data
  }

  async obterRelatorioMedias(): Promise<RelatorioMediasDto> {
    const response = await apiClient.get<ApiResponse<RelatorioMediasDto>>('/seguros/relatorio')
    return response.data.data
  }
}

export default new ApiService()

