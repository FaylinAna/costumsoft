<template>
  <v-menu offset-y v-model="menuOpen" :offset-y-transition="false">
    <template v-slot:activator="{ on }">
      <v-btn icon v-on="on" @click="toggleMenu">
        <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
    </template>
    <v-list>
      <v-list-item @click="showPackageDetails">
        <v-icon>mdi-eye</v-icon> 
      </v-list-item>
      <v-list-item @click="updatePackage">
        <v-icon>mdi-update</v-icon> 
      </v-list-item>
      <v-list-item>
        <UploadFile :itemId="item.id"/>
      </v-list-item>
    </v-list>
  </v-menu>
  
</template>

<script>
import { ref } from 'vue';

export default {
  props: {
    item: {
      type: Object, 
      required: true 
    },
  },
  setup(props) {
    const menuOpen = ref(false);

    const toggleMenu = () => {
      menuOpen.value = !menuOpen.value;
    };

    const showPackageDetails = () => {
      console.log(item,"show")

    };

    const updatePackage = () => {

    };

    const uploadFile = () => {

    };

    return {
      menuOpen,
      toggleMenu,
      showPackageDetails,
      updatePackage,
      uploadFile
    };
  }
};
</script>
