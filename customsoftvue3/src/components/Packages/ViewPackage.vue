<template>
  <v-card>
    <v-card-title class="green--text">Número de Seguimiento:</v-card-title>
    <v-card-text>
      <v-text-field
        v-model="trackingNumber"
        label="Numero de Seguimiento"
      ></v-text-field>
      <v-btn @click="searchByTrackingNumber" color="primary">Buscar</v-btn>
      <package v-if="packageDetails" :package="packageDetails" />
      <custom-alert
        v-if="error"
        :message="'Error al consultar el paquete: ' + error"
        alertType="error"
      />
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { State, Package } from "../../ApiService/Services/types";
import { getPackageByTrackingNumber } from "../../ApiService/Services/ApiService";

export default defineComponent({
  setup() {
    const trackingNumber = ref("");
    const packageDetails = ref<Package | null>(null);
    const error = ref<string>("");

    const searchByTrackingNumber = async () => {
      try {
        console.log(trackingNumber, "trackingNumber");
        const response = await getPackageByTrackingNumber(trackingNumber.value);
        packageDetails.value = response;
      } catch (err) {
        error.value = err.message || "Error no se encontro el paquete";
      }
    };

    return {
      trackingNumber,
      packageDetails,
      error,
      searchByTrackingNumber,
    };
  },
});
</script>
