<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
  <v-card>
    <v-card-title class="font-weight-bold">TR - {{ editedPackageItem.trackingNumber }} / Fecha {{ formattedDate }}</v-card-title>
    <v-card-text>
      <v-row>
        <v-col cols="12" sm="6">
          <v-text-field v-model="editedPackageItem.customerName" label="Nombre Del cliente" outlined :disabled="!editableFields.customerName" :class="{ 'text--black': !editableFields.customerName }"></v-text-field>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field v-model="editedPackageItem.deliveryAddress" label="Dirección" outlined :disabled="!editableFields.deliveryAddress" :class="{ 'text--black': !editableFields.deliveryAddress }"></v-text-field>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field v-model="editedPackageItem.weight" label="Peso" outlined :disabled="!editableFields.weight" :class="{ 'text--black': !editableFields.weight }"></v-text-field>
        </v-col>
      </v-row>
      <v-row v-for="state in packageStates" :key="state.id" align="center">
        <v-col cols="auto">
          <v-icon color="primary">mdi-truck-delivery</v-icon>
        </v-col>
        <v-col>
          <div>{{ state.state }} - {{ state.state_date_packages }}</div>
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-actions>
      <v-btn @click="toggleEditMode" color="primary">{{ isEditMode ? 'Guardar' : 'Editar' }}</v-btn>
    </v-card-actions>
  </v-card>
</v-col>
</v-row >

</v-container>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, computed } from 'vue';
import { State, Package } from '../ApiService/Interfaces/types';

export default defineComponent({
  props: {
    packageItem: {
      type: Object as PropType<Package>,
      required: true,
    },
    packageStates: {
      type: Array as PropType<State[]>,
      required: true
    }
  },
  setup(props) {
    const formattedDate = ref("");
    const isEditMode = ref(false);
    const editedPackageItem = ref({ ...props.packageItem });

    const editableFields = computed(() => ({
      customerName: isEditMode.value,
      deliveryAddress: isEditMode.value,
      weight: isEditMode.value,

    }));

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      const formattedDate = date.toISOString().slice(0, 19).replace('T', ' ');
      return formattedDate;
    };
    
    const updateFormattedDate = () => {
      formattedDate.value = formatDate(editedPackageItem.value.stateDate);
    };

    // Inicializar la fecha formateada al montar el componente
    updateFormattedDate();

    // Observar cambios en editedPackageItem.stateDate
    // y actualizar la fecha formateada en consecuencia
    watch(() => editedPackageItem.value.stateDate, () => {
      updateFormattedDate();
    });

    const toggleEditMode = () => {
      if (isEditMode.value) {
      }
      isEditMode.value = !isEditMode.value;
    };

    return {
      isEditMode,
      editedPackageItem,
      editableFields,
      toggleEditMode,
      formatDate
    };
  }
});
</script>

<style>
.font-weight-bold {
  font-weight: bold;
}
.text--black {
  color: #000000; 
}
</style>