<template>
    <div>
      <v-btn 
        v-if="showTransitButton"
        @click="confirmStatusChange('Transito')" 
        class="btn-green mr-2"
      >
        Paquete en Tránsito
      </v-btn>
      <v-btn 
        v-if="showRejectButton"
        @click="confirmStatusChange('Rechazado')" 
        class="btn-red mr-2"
      >
        Rechazar Paquete
      </v-btn>
      <v-btn
        v-if="showDeliveredButton"
        @click="confirmStatusChange('Entregado')" 
        class="btn-blue mr-2"
      >
        Paquete Entregado
      </v-btn>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, PropType, ref, computed } from 'vue';
  import { State } from '../../ApiService/Interfaces/types';
  
  export default defineComponent({
    props: {
      packageStates: {
        type: Array as PropType<State[]>,
        required: true
      }
    },
    setup(props, { emit }) {
      const showTransitButton = computed(() => {
        return props.packageStates.length === 1 && props.packageStates[0].state === 'Nuevo';
      });
  
      const showRejectButton = computed(() => {
        return (props.packageStates.length === 1 && props.packageStates[0].state === 'Nuevo') 
                || (props.packageStates.some(status => status.state === 'Transito')
                && !props.packageStates.some(status => status.state === 'Entregado')) ;
      });
  
      const showDeliveredButton = computed(() => {
        return props.packageStates.some(status => status.state === 'Transito')
                && !props.packageStates.some(status => status.state === 'Entregado');
      });
  
      const confirmStatusChange = (status: string) => {
        
        emit('status-change', status);
      };
  
      return {
        showTransitButton,
        showRejectButton,
        showDeliveredButton,
        confirmStatusChange
      };
    }
  });
  </script>
  
  <style scoped>
  .btn-green {
    background-color: green;
    color: white;
  }
  .btn-red {
    background-color: red;
    color: white;
  }
  .btn-blue {
    background-color: blue;
    color: white;
  }
  </style>
  