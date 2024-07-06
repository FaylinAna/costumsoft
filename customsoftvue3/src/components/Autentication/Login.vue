<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6"> 
        <v-card > 
          <v-card-title class="blue lighten-4">Configuración de la Aplicación</v-card-title>
          <v-card-text>
            <v-text-field v-model="newApplicationModel.Nombre" label="Nombre de la Aplicación" required></v-text-field>
            <v-text-field v-model="newApplicationModel.ApiKey" label="API Key" required type="password"></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-btn @click="handleGetToken" color="primary">Obtener Token</v-btn>
          </v-card-actions>
          <custom-alert v-if="error" :message="'Error al actualizar Token: ' + error" alertType="error" />
          <custom-alert v-if="token" :message="'Se actualizo Correctamente el token: '" alertType="success"  />
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { getToken } from  '../../ApiService/Services/ApiService';
import { ApplicationModel } from '../../ApiService/Interfaces/types';
import { useRouter } from 'vue-router'; 

export default defineComponent({
  setup() {
    const router = useRouter(); 
    const error = ref<string>('');
    const token = ref<string>('');
    const newApplicationModel = ref<ApplicationModel>({
      ApiKey: '',
      Nombre: '',
      ClientSecret :''
    });

    const handleGetToken = async () => {
      try {
        const token = await getToken(newApplicationModel.value);
        console.log('Token guardado en localStorage:', token);
        router.push('/');
      } catch (err) {
        console.error('Error al actualizar el token:', err);
        error.value = err.message || 'Error desconocido al agregar el paquete';
      }
    };

    return { error, token, newApplicationModel, handleGetToken };
  }
});
</script>

<style scoped>
.blue.lighten-4 {
  background-color: #e3f2fd; /* Ajusta el color de fondo azul tenue según tus preferencias */
}
</style>
