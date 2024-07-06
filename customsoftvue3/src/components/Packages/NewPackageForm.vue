<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card light>
          <v-card-title>Detalle del paquete</v-card-title>
          <v-card-text>
            <v-text-field
              v-model="newPackage.customerName"
              label="Nombre"
              required
            ></v-text-field>
            <v-text-field
              v-model="newPackage.deliveryAddress"
              label="Direccion"
              required
            ></v-text-field>
            <v-text-field
              v-model="newPackage.weight"
              label="Peso"
              required
              type="number"
            ></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-btn @click="createPackage" color="primary">Crear Paquete</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { Package } from "../../ApiService/Interfaces/types";
import { addPackage } from "../../ApiService/Services/ApiService";

export default defineComponent({
  setup(_, { emit }) {
    const newPackage = ref<Package>({
      customerName: "",
      deliveryAddress: "",
      weight: 0,
    });
    const error = ref<string>("");
    const messagesInfo = ref<string>("");
    const createPackage = () => {
      emit("loading");
      return new Promise(async (resolve, reject) => {
        try {
          const newPackageResponse = await addPackage(newPackage.value);
          messagesInfo.value =
            "Se agrego el paquete con trackinNumber :" +
            newPackageResponse.trackingNumber;
          emit("added");
          newPackage.value = {
            customerName: "",
            deliveryAddress: "",
            weight: 0,
          };
          emit("message-succes", newPackageResponse, messagesInfo);
          resolve(newPackageResponse);
        } catch (err) {
          
          error.value = err.message || "Error al agregar el paquete";
          emit("message-error", error.value);
          reject(err);
        }
      });
    };

    return {
      newPackage,
      error,
      createPackage,
      messagesInfo,
    };
  },
});
</script>
