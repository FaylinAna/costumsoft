<template>
  <v-list-item
    class="node"
    :class="{ expanded: node.expanded, selected: node.isSelected}"
    @click="toggleNode"
  >
    <template v-if="!node.editing">
      <span @dblclick="editItem">{{ node.label }}</span>
    </template>
    <template v-else>
      <input
        type="text"
        v-model="node.label"
        @blur="saveItem"
        @keyup.enter="saveItem"
      />
    </template>
    <v-icon v-if="!node.file" @click.stop="addNewItem('directory')">mdi-folder-plus</v-icon>
    <file-uploader v-if="!node.file" :node="node" />
    <v-icon v-if="!node.file" @click.stop="openFileInput">mdi-file-plus</v-icon>
    <v-icon @click.stop="deleteItem">mdi-delete</v-icon>
  </v-list-item>
</template>

<script>
import { ref } from "vue";

export default {
  props: {
    node: Object,
  },
  setup(props) {
    const toggleNode = () => {
      props.node.expanded = !props.node.expanded;
      props.node.isSelected = !props.node.isSelected;
    };

    const editItem = () => {
      props.node.editing = true;
    };

    const saveItem = () => {
      props.node.editing = false;
    };

    const addNewItem = (type) => {
      const newItemName = prompt("Ingrese el nombre del nuevo elemento:");
      if (newItemName) {
        if (!props.node.children) {
          props.node.children = [];
        }
        const newItem = {
          id: Date.now(),
          label: newItemName,
          icon:
            type === "directory" ? "mdi-folder-outline" : "mdi-file-outline",
          file: type === "file",
          children: type === "directory" ? [] : undefined,
        };
        props.node.children.push(newItem);
      }
    };

    const openFileInput = () => {

    };

    const deleteItem = () => {
  
    };

    return { toggleNode, editItem, saveItem, addNewItem, openFileInput, deleteItem };
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
