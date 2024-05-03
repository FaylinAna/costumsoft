<template>

    <v-layout>
      <v-app-bar
        color="primary"
        density="compact"
      >
        <template v-slot:prepend>
          <v-app-bar-nav-icon></v-app-bar-nav-icon>
        </template>

        <v-app-bar-title>Historial Paquetes</v-app-bar-title>

        <template v-slot:append>
          <v-btn icon="mdi-dots-vertical"></v-btn>
        </template>
      </v-app-bar>

      <v-main>
        <v-container fluid>
          <Login/>
        </v-container>
      </v-main>
    </v-layout>

</template>

<script setup lang="ts">
import PackageTable from './PackageTable.vue';
import Login from './Autentication/Login.vue';

  //
</script>
