<template>
    <input
      type="file"
      style="display: none"
      :ref="`fileInput_${node.id}`"
      @change="($event) => handleFileChange(node, $event)"
    />
  </template>
  
  <script>
  export default {
    props: {
      node: Object,
    },
    methods: {
      handleFileChange(node, event) {
        // Lógica para manejar la carga de archivos
      },
    },
  };
  </script>
  