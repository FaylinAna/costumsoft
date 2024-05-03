<template>
    <div>
      <CustomTreeViewNode :treeData="treeData" />
    </div>
  </template>
  
  <script>
  import { defineComponent } from 'vue';
  import CustomTreeViewNode from './CustomTreeViewNode.vue';
  export default defineComponent({
    components: {
      CustomTreeViewNode
    },
    data() {
      return {
        treeData: [
          {
            id: 1,
            label: 'Directorio Principal nuevo',
            icon: 'mdi-folder',
            expanded: false,
            children: [
              {
                id: 2,
                label: 'Carpeta 1',
                icon: 'mdi-folder',
                expanded: false,
                children: [
                  { id: 3, label: 'Archivo1.txt', icon: 'mdi-file-outline', file: true },
                  { id: 4, label: 'Archivo2.js', icon: 'mdi-file-outline', file: true },
                  { id: 5, label: 'Archivo3.png', icon: 'mdi-file-image-outline', file: true }
                ]
              },
              {
                id: 6,
                label: 'Carpeta 2',
                icon: 'mdi-folder',
                expanded: false,
                children: [
                  { id: 7, label: 'Archivo4.json', icon: 'mdi-file-outline', file: true },
                  { id: 8, label: 'Archivo5.css', icon: 'mdi-file-outline', file: true }
                ]
              }
            ]
          }
        ]
      };
    }
  });
  </script>
  