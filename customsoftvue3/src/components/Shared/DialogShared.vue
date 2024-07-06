<template>
  <v-dialog v-model="isVisible" max-width="800px">
    <v-card>
      <v-card-title>{{ modalTitle }}</v-card-title>
      <v-card-text>
       
        <slot></slot>
      </v-card-text>
      <v-card-actions>
        <v-btn @click="closeModal" color="primary" :disabled="buttonsDisabled"
          >Cerrar</v-btn
        >
      </v-card-actions>
      <progressLoading v-if="loading" />
  
    <custom-alert
    v-if="messagesView"
      :message=messagesView
      alertType="success"
    />
    <custom-alert
    v-if="messageserrorView"
      :message=messageserrorView
      alertType="error"
    />
    </v-card>
  </v-dialog>
</template>
<script lang="ts">
import { defineComponent, ref, PropType } from "vue";
import { State,Package } from "../../ApiService/Interfaces/types";

export default defineComponent({
  props: {
    isVisible: Boolean,
    modalContent: String,
    modalTitle: String,
    packageItem: {
      type: Object as PropType<Package>, 
      required: true,
    },
    packageStates: {
      type: Array as PropType<State[]>,
      required: true,
    },
  },
  setup(props, { emit }) {
    const isVisible = ref(props.isVisible);
    const buttonsDisabled = ref(false);
    const loading = ref(false);
    const messagesView   = ref<string>("");
    const messageserrorView  = ref<string>("");

    const closeModal = () => {
      emit("close");
    };

    const showSuccessAlert = (newPackageResponse,messagesInfo) => {
      buttonsDisabled.value = false;
      loading.value = false;
      
      buttonsDisabled.value = false;
      messagesView.value = messagesInfo;
    };
    
    const showErrorAlert = (messageserror) => {
      buttonsDisabled.value = false;
      loading.value = false;
      messageserrorView.value = messageserror
    };

    const showLoadingAlert = () => {
      loading.value = true
      buttonsDisabled.value = true;
      console.log("showLoadingAlert");
 
    };
    const handleFileChange = () => {
      buttonsDisabled.value = false;
      loading.value = false;
      emit("package-added"); 

    };

    return {
      isVisible,
      closeModal,
      handleFileChange,
      buttonsDisabled,
      showErrorAlert,
      showLoadingAlert,
      showSuccessAlert,
      loading,
      messagesView,
      messageserrorView
      
    };
  },
});
</script>
