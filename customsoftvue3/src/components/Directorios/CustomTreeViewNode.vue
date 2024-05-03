<template>
    <v-card v-for="node in treeData" :key="node.id">
      <CustomTreeNode :node="node" />
      <v-list-item v-if="node.children && node.expanded">
        <CustomTreeViewNode :treeData="node.children" />
      </v-list-item>
    </v-card>
  </template>
  
  <script>
  import CustomTreeNode from "./CustomTreeNode.vue";
  
  export default {
    components: {
      CustomTreeNode,
    },
    props: {
      treeData: {
        type: Array,
        default: () => [],
      },
    },
  };
  </script>
  