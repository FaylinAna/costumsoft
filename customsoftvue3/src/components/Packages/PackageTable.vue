<template>
  <v-card title="Paquetes" flat>
    <custom-alert
      v-if="messagesView"
      :message="messagesView"
      alertType="success"
    />
    <custom-alert
      v-if="messageserrorView"
      :message="messageserrorView"
      alertType="error"
    />
    <input
      type="file"
      ref="fileInput"
      style="display: none"
      @change="handleFileChange"
    />
    <v-card-subtitle>
      <v-row>
        <v-col cols="6">
          <div class="float-right">
            <v-btn @click="openDialog_Details()" icon color="primary">
              <v-icon>mdi-plus</v-icon>
            </v-btn>
          </div>
        </v-col>
        <v-col cols="6">
          <div class="float-left">
            <v-btn @click="openDialog_Package()" icon color="primary">
              <v-icon>mdi-magnify</v-icon>
            </v-btn>
          </div>
        </v-col>
      </v-row>
    </v-card-subtitle>
    <v-card-text>
      <v-row>
        <v-col>
          <v-data-table
            :headers="headers"
            :items="packages"
            :search="search"
            outlined
            dense
            :items-per-page="5"
            color="blue"
            dark
            loading-text="Cargando... Por favor espera"
          >
            <template v-slot:item.status="{ item }">
              <v-chip :color="getChipColor(item)" text-color="white">
                {{ getLastStatus(item) }}
              </v-chip>
            </template>
            <template v-slot:item.actions="{ item }">
              <v-icon @click="showPackageDetails(item)">mdi-eye</v-icon>
              <UploadFile
                :itemId="item.id"
                @loading="showLoadingAlert"
                @message-error="showErrorAlert"
                @message-succes="showSuccessAlert"
              />
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-card-text>
    <DialogShared
      :isVisible="isModalVisible"
      :modalContent="ModalContent"
      :modalTitle="ModalTitle"
      :packageStates="selectedPackageStates"
      :packageItem="selectPackage"
      @package-added="handlePackageAdded"
      v-if="isModalVisible"
      @close="closeModal"
    >
    <template v-if="ModalContent === 'packageDetails'">
          <div>
            <PackageDetailsModal 
            :packageStates="selectedPackageStates"
            :packageItem = "selectPackage"
            @loading="showLoadingAlert" 
            @added="handlePackageAdded" 
            @message-error="showErrorAlert"
            @message-succes ="showSuccessAlert"
            />
          </div>
        </template>
       
        <template v-else-if="ModalContent === 'NewPackageForm'">
          <div>
            <NewPackageForm 
            @loading="showLoadingAlert" 
            @added="handlePackageAdded" 
            @message-error="showErrorAlert"
            @message-succes ="showSuccessAlert"
             />
          </div>
        </template>
    </DialogShared>
  </v-card>
</template>
<script lang="ts">
import { defineComponent, ref, onMounted, reactive } from "vue";
import NewPackageForm from "./NewPackageForm.vue";
import { Package, State } from "../../ApiService/Interfaces/types";
import { getPackages } from "../../ApiService/Services/ApiService";

import UploadFile from "../Files/UploadFiles.vue";
export default defineComponent({
  setup() {
    const isVisible = ref(false);
    const ModalContent = ref("");
    const ModalTitle = ref("");
    const packages = ref<Package[]>([]);
    const selectedPackageStates = ref<State[]>([]);
    const selectPackage = ref<Package>();
    const isModalVisible = ref(false);
    const search = reactive({
      value: "",
    });
    const loading = ref(true);
    const messagesView = ref<string>("");
    const messageserrorView = ref<string>("");

    const updatePackage = (item: Package) => {
      selectedPackageStates.value = item?.states;
      ModalContent.value = "updatePackage";
      ModalTitle.value = "Detalle del paquete";
      isModalVisible.value = true;
    };
    const openDialog_Details = () => {
      isVisible.value = true;
      ModalContent.value = "NewPackageForm";
      ModalTitle.value = "Crear Nuevo Paquete";
      isModalVisible.value = true;
    };
    const fetchPackages = async () => {
      try {
        const response = await getPackages();
        packages.value = response;
      } catch (error) {
        console.error("Error al obtener los paquetes:", error);
      }
    };
    const handlePackageAdded = () => {
      fetchPackages();
    };

    const headers = [
      { title: "# Seguimiento ", key: "trackingNumber" },
      { title: "Nombre", key: "customerName" },
      { title: "Direccion", key: "deliveryAddress" },
      { title: "Peso", value: "weight" },
      { title: "Status", value: "status" },
      { title: "Actions", value: "actions", sortable: false },
    ];
    const showPackageDetails = (item: Package) => {
      selectPackage.value = item;
      selectedPackageStates.value = item?.states;
      ModalContent.value = "packageDetails";
      ModalTitle.value = "Detalle del paquete";
      isModalVisible.value = true;
    };
    const openDialog_Package = (item: Package) => {
      selectPackage.value = item;
      ModalContent.value = "ViewPackage";
      ModalTitle.value = "Buscar un paquete";
      isModalVisible.value = true;
    };
    const closeModal = () => {
      isModalVisible.value = false;
    };

    const showSuccessAlert = (req1, msg) => {
      console.log(msg, "msg msg msg");
      loading.value = false;
      messagesView.value = msg;
    };

    const showErrorAlert = (messageserror) => {
      loading.value = false;
      messageserrorView.value = messageserror;
    };

    const showLoadingAlert = () => {
      loading.value = true;
      console.log("showLoadingAlert");
    };

    const getLastStatus = (item: Package): string => {
      if (item.states && item.states.length > 0) {
        return item.states[item.states.length - 1].state;
      }
      return "";
    };
    const getChipColor = (item) => {
      const status = getLastStatus(item);
      if (status === "Nuevo") {
        return "primary";
      } else if (status === "Rechazado") {
        return "red";
      } else if (status === "Entregado") {
        return "green";
      } else if (status === "Transito") {
        return "yellow";
      } else {
        return "grey";
      }
    };
    onMounted(fetchPackages);
    return {
      packages,
      headers,
      showPackageDetails,
      ModalContent,
      selectedPackageStates,
      selectPackage,
      isModalVisible,
      handlePackageAdded,
      openDialog_Package,
      ModalTitle,
      closeModal,
      openDialog_Details,
      fetchPackages,
      getLastStatus,
      updatePackage,
      loading,
      showLoadingAlert,
      showErrorAlert,
      showSuccessAlert,
      messageserrorView,
      messagesView,
      search,
      getChipColor,
    };
  },
  components: {
    NewPackageForm,
    UploadFile,
  },
});
</script>
<style>
.float-right {
  float: right;
  margin-right: 10px;
}

.float-left {
  float: left;
  margin-left: 10px;
}
</style>
