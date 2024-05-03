<template>
  <v-dialog v-model="isVisible">
    <v-card>
      <v-card-title>{{ modalTitle }}</v-card-title>
      <v-card-text>
        <template v-if="modalContent === 'packageDetails'">
          <div>
            <PackageDetailsModal 
            :packageStates="packageStates"
            :packageItem = "packageItem"
            />

          </div>
        </template>
        <template v-else-if="modalContent === 'NewPackageForm'">
          <div>
            <NewPackageForm 
            @loading="showLoadingAlert" 
            @added="handleFileChange" 
            @message-error="showErrorAlert"
            @message-succes ="showSuccessAlert"
             />
          </div>
        </template>
        <template v-else-if="modalContent === 'ViewPackage'">
          <div>
          </div>
        </template>
        <template v-else-if="modalContent === 'updatePackage'">
          <div></div>
        </template>
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
import { State } from "../ApiService/Interfaces/types";
import {PackageDetailsModal} from "../Packages/PackageDetailsModal.vue"
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
    const handleFileChange = (newPackageResponse,messagesInfo) => {
      buttonsDisabled.value = false;
      loading.value = false;
      
      emit("added"); 

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
