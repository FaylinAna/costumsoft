<template>
    <v-card>
      <v-card-title>{{ package.customerName }}</v-card-title>
      <v-card-text>
        <p>Tracking Number: {{ package.trackingNumber }}</p>
        <p>Direccion: {{ package.deliveryAddress }}</p>
        <p>Peso: {{ package.weight }}</p>
        <ul>
          <StatusItem v-for="status in package.states" :key="status.id" :status="status" />
        </ul>
      </v-card-text>
    </v-card>
  </template>
  
  <script lang="ts">
  import { defineComponent, PropType } from 'vue';
  import { Package } from '../../ApiService/Interfaces/types';
  import StatusItem from './StatusItem.vue';
  
  export default defineComponent({
    props: {
      package: {
        type: Object as PropType<Package>,
        required: true,
      },
    },
    components: {
      StatusItem,
    },
  });
  </script>
  