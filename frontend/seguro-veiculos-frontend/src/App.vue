<template>
  <div id="app">
    <header class="app-header">
      <div class="container">
        <h1 class="app-title">
          <span class="icon">🚗</span>
          Sistema de Seguros de Veículos
        </h1>
        <p class="app-subtitle">Gestão e relatórios de seguros automotivos</p>
      </div>
    </header>
    
    <main class="app-main">
      <RouterView />
    </main>
    
    <footer class="app-footer">
      <div class="container">
        <p>&copy; 2025 Sistema de Seguros de Veículos. Desenvolvido com Vue.js e .NET 8.0</p>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  min-height: 100vh;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
}

.app-header {
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(10px);
  border-bottom: 1px solid rgba(255, 255, 255, 0.2);
  padding: 2rem 0;
  text-align: center;
  box-shadow: 0 2px 20px rgba(0, 0, 0, 0.1);
}

.app-title {
  color: #2c3e50;
  font-size: 2.5rem;
  font-weight: 700;
  margin-bottom: 0.5rem;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
}

.icon {
  font-size: 2rem;
}

.app-subtitle {
  color: #7f8c8d;
  font-size: 1.1rem;
  font-weight: 400;
}

.app-main {
  flex: 1;
  background: rgba(255, 255, 255, 0.9);
  backdrop-filter: blur(10px);
  margin: 2rem;
  border-radius: 20px;
  box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.app-footer {
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(10px);
  border-top: 1px solid rgba(255, 255, 255, 0.2);
  padding: 1.5rem 0;
  text-align: center;
  color: #7f8c8d;
  font-size: 0.9rem;
}

@media (max-width: 768px) {
  .app-title {
    font-size: 2rem;
    flex-direction: column;
    gap: 0.25rem;
  }
  
  .app-subtitle {
    font-size: 1rem;
  }
  
  .app-main {
    margin: 1rem;
    border-radius: 15px;
  }
  
  .app-header {
    padding: 1.5rem 0;
  }
}
</style>
