<template>
  <v-app>
    <v-layout>
      <v-app-bar color="primary" dense>
        <template v-slot:prepend> </template>

        <v-app-bar-title> Historial de Paquetes </v-app-bar-title>

        <template v-slot:append>
          <v-tooltip bottom>
            <template v-slot:activator="{ on }">
              <v-btn icon v-on="on" to="/PaqueteHistory">
                <v-icon>mdi-package</v-icon>
              </v-btn>
            </template>
            <span>Packetes</span>
          </v-tooltip>
          <v-tooltip bottom>
          <template v-slot:activator="{ on }">
            <v-btn icon v-on="on" to="/directorio">
              <v-icon>mdi-folder</v-icon>
            </v-btn>
          </template>
          <span slot="tooltip">Directorio</span>
        </v-tooltip>
          <v-tooltip bottom>
            <template v-slot:activator="{ on }">
              <v-btn icon v-on="on" @click="DeleteLocalStorage">
                <v-icon>mdi-login</v-icon>
              </v-btn>
            </template>
            <span>Cerrar sesion</span>
          </v-tooltip>
         
        </template>
      </v-app-bar>

      <v-main>
        <v-container fluid>
          <router-view />
        </v-container>
      </v-main>
    </v-layout>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  setup() {
    const DeleteLocalStorage = () => {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("key");
      localStorage.removeItem("nombre");
      window.location.href = "/application-configuration";
    };
    return {
      DeleteLocalStorage,
      $vuetify: {
        theme: {
          dark: false, // Cambiar a false para usar el tema claro
        },
      },
    };
  },
});
</script>
