<template>
    <div>
      <v-list-item @click="toggleExpanded">
        <v-list-item-content>
          <v-list-item-title v-text="item.name"></v-list-item-title>
        </v-list-item-content>
        <v-list-item-action v-if="item.type === 'directory'">
          <v-btn @click.stop="addFolderClick">Agregar Carpeta</v-btn>
          <input type="file" ref="fileInput" @change="addFileClick" style="display: none;" />
          <v-btn @click.stop="openFileInput" v-if="item.type === 'directory'">Agregar Archivo</v-btn>
        </v-list-item-action>
      </v-list-item>
      <v-list v-if="item.type === 'directory' && item.expanded">
        <DirectoryItem v-for="child in item.children" :key="child.name" :item="child" @addFolder="addFolder" @addFile="addFile" @selectFolder="selectFolder" />
      </v-list>
    </div>
  </template>
  
  <script lang="ts">
  import { ref } from 'vue';
  
  export default {
    props: {
      item: {
        type: Object,
        required: true,
      },
    },
    setup(props) {
      const openFileInput = () => {
        props.$refs.fileInput.click();
      };
  
      const addFolderClick = () => {
        const folderName = prompt('Ingrese el nombre de la nueva carpetaf:');
        if (folderName) {
          props.$emit('addFolder', folderName);
        }
      };
  
      const addFileClick = (event) => {
        const file = event.target.files[0];
        if (file) {
          props.$emit('addFile', file.name);
        }
      };
  
      const toggleExpanded = () => {
        if (props.item.type === 'directory') {
          props.item.expanded = !props.item.expanded;
        }
      };
  
      return {
        openFileInput,
        addFolderClick,
        addFileClick,
        toggleExpanded,
      };
    },
  };
  </script>
  