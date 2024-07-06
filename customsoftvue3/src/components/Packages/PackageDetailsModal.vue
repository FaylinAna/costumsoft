<template>
  <v-container>
    <v-card>
      <v-card-title class="font-weight-bold"
        >TR - {{ editedPackageItem.trackingNumber }}
      </v-card-title>
      <v-card-text>
        <v-row>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="editedPackageItem.customerName"
              label="Nombre Del cliente"
              outlined
              :disabled="!editableFields.customerName"
              :class="{ 'text--black': !editableFields.customerName }"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="editedPackageItem.deliveryAddress"
              label="Dirección"
              outlined
              :disabled="!editableFields.deliveryAddress"
              :class="{ 'text--black': !editableFields.deliveryAddress }"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6">
            <v-text-field
              v-model="editedPackageItem.weight"
              label="Peso"
              outlined
              :disabled="!editableFields.weight"
              :class="{ 'text--black': !editableFields.weight }"
            ></v-text-field>
          </v-col>
        </v-row>
        <StatusItem
          v-for="status in editedPackageItem.states"
          :key="status.id"
          :status="status"
        />
      </v-card-text>
      <v-card-actions  >
        <template v-if="isEditMode">
          <PackageStatusButtons
            :packageStates="editedPackageItem.states"
            @status-change="handleStatusChange"
          />
        </template>
        <v-btn @click="toggleEditMode" color="primary">{{
          isEditMode ? "Guardar" : "Editar"
        }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, computed } from "vue";
import { State, Package } from "../../ApiService/Interfaces/types";
import StatusItem from "../Shared/StatusItem.vue";
import PackageStatusButtons from "../Utils/PackageStatusButtons.vue";
import { UpdatePackage } from "../../ApiService/Services/ApiService";
export default defineComponent({
  components: {
    StatusItem,
    PackageStatusButtons,
  },
  props: {
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
    const formattedDate = ref("");
    const isEditMode = ref(false);
    const editedPackageItem = ref({ ...props.packageItem });
    const error = ref<string>("");
    const messagesInfo = ref<string>("");

    const editableFields = computed(() => ({
      customerName: isEditMode.value,
      deliveryAddress: isEditMode.value,
      weight: isEditMode.value,
    }));

    const formatDate = (dateString: string) => {
      const date = new Date(dateString);
      const formattedDate = date.toISOString().slice(0, 19).replace("T", " ");
      return formattedDate;
    };

    const handleStatusChange = (newStatus: string) => {
      editedPackageItem.value.StateDescription = newStatus;
      emit("loading");
      return new Promise(async (resolve, reject) => {
        try {
          const newPackageResponse = await UpdatePackage(
            editedPackageItem.value
          );
          emit("added");
          
          messagesInfo.value =
            "Se Actualizo el paquete con el estatus => " + newStatus;
          emit("message-succes", newPackageResponse, messagesInfo);
          resolve(newPackageResponse);
        } catch (err) {
          emit("message-error", error.value);
          error.value = err.message || "Error al Actualizar el paquete";
          reject(err);
        }
      });
    };

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
      formatDate,
      handleStatusChange,
    };
  },
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
