import { createRouter, createWebHistory } from 'vue-router'
import RelatorioMedias from '@/components/RelatorioMedias.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'relatorio',
      component: RelatorioMedias,
      meta: {
        title: 'Relatório de Médias - Seguros de Veículos'
      }
    }
  ]
})

router.beforeEach((to) => {
  document.title = to.meta?.title as string || 'Seguros de Veículos'
})

export default router
