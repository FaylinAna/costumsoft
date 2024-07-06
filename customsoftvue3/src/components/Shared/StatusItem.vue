<template>
  <v-list-item>
    <v-list-item-icon>
      <v-icon>mdi-truck-delivery</v-icon>
    </v-list-item-icon>
    <v-list-item-content>
      <v-list-item-title
        >{{ status.state }} - {{ status.state_Date_Packages }}</v-list-item-title
      >
    </v-list-item-content>
  </v-list-item>
</template>

<script lang="ts">
import { defineComponent, PropType } from "vue";
import { State } from "../../ApiService/Interfaces/types";

export default defineComponent({
  props: {
    status: {
      type: Object as PropType<State>,
      required: true,
    },
  },
});
</script>
