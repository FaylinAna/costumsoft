
<template>
    <div class="welcome-page">
      <img src="@/assets/delivery-icon.png" alt="Hola!" class="delivery-icon">
      <h1>Bienvenido</h1>
      <p>{{ currentDate }}</p>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        currentDate: ''
      };
    },
    mounted() {
      this.getCurrentDate();
    },
    methods: {
      getCurrentDate() {
        const currentDate = new Date();
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: false };
        this.currentDate = currentDate.toLocaleDateString('es-ES', options);
      }
    }
  };
  </script>
  
  <style scoped>
  .welcome-page {
    text-align: center;
  }
  .delivery-icon {
    width: 200px;
    height: 200px;
  }
  </style>
  