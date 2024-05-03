<template>
  <div class="treeview">
    <v-card v-for="node in treeData" :key="node.id">
      <v-list-item
        class="node"
        :class="{ expanded: node.expanded }"
        @click="toggleNode(node)"
      >
        <span v-if="!node.editing" @dblclick="editItem(node)">
        {{ node.label}}</span>
        <input
          v-else
          type="text"
          v-model="node.label"
          @blur="saveItem(node)"
          @keyup.enter="saveItem(node)"
        />
        <v-icon v-if="!node.file" @click.stop="addNewItem(node, 'directory')"
          >mdi-folder-plus</v-icon
        >
        <input
          v-if="!node.file"
          type="file"
          style="display: none"
          :ref="`fileInput_${node.id}`"
          @change="($event) => handleFileChange(node, $event)"
        />
        <v-icon v-if="!node.file" @click.stop="openFileInput(node)"
          >mdi-file-plus</v-icon
        >
        <v-icon @click.stop="deleteItem(node)">mdi-delete</v-icon>
      </v-list-item>
      <v-list-item v-if="node.children && node.expanded">
        <CustomTreeView :treeData="node.children" />
      </v-list-item>
    </v-card>
  </div>
</template>

<script>
import { defineComponent, ref, toRefs } from "vue";

export default defineComponent({
  name: "CustomTreeView2",
  props: {
    treeData: {
      type: Array,
      default: () => [],
    },
  },

  setup(props) {
    const { treeData } = toRefs(props);
    const fileInputRefs = {};
    const deletedItems = ref([]);

    const toggleNode = (node) => {
      node.expanded = !node.expanded;
    };

    const addNewItem = (parent, type) => {
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
    };

    const openFileInput = (node) => {
      const refName = `fileInput_${node.id}`;
      const fileInputRef = fileInputRefs[refName];
      if (fileInputRef && fileInputRef.value) {
        fileInputRef.value.click();
      } else {
        console.error(`Input element ${refName} is undefined.`);
      }
    };

    const handleFileChange = (node, event) => {
      const file = event.target.files[0];
      if (file) {
        const fileName = file.name;
        const fileExtension = fileName.split(".").pop();
        const newItem = {
          id: Date.now(),
          label: fileName,
          icon: "mdi-file-outline",
          file: true,
          extension: fileExtension,
        };
        if (!node.children) {
          node.children = [];
        }
        node.children.push(newItem);
      }
    };

    const editItem = (node) => {
      node.editing = true;
    };

    const saveItem = (node) => {
      node.editing = false;
    };
    const deleteItem = (nodeToDelete) => {
      deletedItems.value.push(nodeToDelete);
      const parentNode = findParentNode(treeData.value, nodeToDelete.id);
      if (parentNode) {
        parentNode.children = parentNode.children.filter(
          (node) => node.id !== nodeToDelete.id
        );
      } else {
        treeData.value = treeData.value.filter(
          (node) => node.id !== nodeToDelete.id
        );
      }
    };
    const findParentNode = (nodes, id) => {
      for (const node of nodes) {
        if (node.children && node.children.some((child) => child.id === id)) {
          return node;
        } else if (node.children) {
          const parent = findParentNode(node.children, id);
          if (parent) {
            return parent;
          }
        }
      }
      return null;
    };

    return {
      toggleNode,
      addNewItem,
      openFileInput,
      handleFileChange,
      editItem,
      saveItem,
      fileInputRefs,
      treeData,
      deleteItem,
      findParentNode,
      deletedItems,
    };
  },
});
</script>

<style>
.node {
  cursor: pointer;
}

.expanded {
  font-weight: bold;
}
</style>
