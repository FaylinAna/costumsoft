<template>
    <v-list-item
      class="node"
      :class="{ expanded: node.expanded ,selected: node.isSelected}"
      @click="toggleNode(node)"
    >
      <template v-if="!node.editing">
        <span @dblclick="editItem(node)">{{ node.label }}</span>
      </template>
      <template v-else>
        <input
          type="text"
          v-model="node.label"
          @blur="saveItem(node)"
          @keyup.enter="saveItem(node)"
        />
      </template>
      <v-icon v-if="!node.file" @click.stop="addNewItem(node, 'directory')">mdi-folder-plus</v-icon>
      <file-uploader v-if="!node.file" :node="node" />
      <v-icon v-if="!node.file" @click.stop="openFileInput(node)">mdi-file-plus</v-icon>
      <v-icon @click.stop="deleteItem(node)">mdi-delete</v-icon>
    </v-list-item>
  </template>
  
  <script>
  import { ref } from "vue";
  
  export default {
    props: {
      node: Object,
    },
    methods: {
      toggleNode(node) {
        node.expanded = !node.expanded;
        node.isSelected = !node.isSelected;
      },
      editItem(node) {
        node.editing = true;
      },
      saveItem(node) {
        node.editing = false;
      },
      addNewItem(parent, type) {
        const newItemName = prompt("Ingrese el nombre del nuevo elemento:");
        if (newItemName) {
          if (!parent.children) {
            parent.children = [];
          }
          const newItem = {
            id: Date.now(),
            label: newItemName,
            icon:
              type === "directory" ? "mdi-folder-outline" : "mdi-file-outline",
            file: type === "file",
            children: type === "directory" ? [] : undefined,
          };
          parent.children.push(newItem);
        }
      },
      openFileInput(node) {
      this.$refs.fileInput.click();
    },
      deleteItem(node) {

      },
    },
  };
  </script>
  
  <style>
  .node {
    cursor: pointer;
  }
  .expanded {
    font-weight: bold;
  } 
  .selected {
  background-color: lightblue; 
}
  </style>
  