<template>
  <v-alert :type="alertType" v-if="visible">{{ message }}</v-alert>
</template>

<script lang="ts">
import { defineComponent, PropType, ref } from 'vue';

export default defineComponent({
  props: {
    message: {
      type: String,
      required: true
    },
    alertType: {
      type: String as PropType<'success' | 'error'>,
      required: true
    },
    timeout: {
      type: Number,
      default: 7000 
    }
  },
  setup(props) {
    const visible = ref(true);
    setTimeout(() => {
      visible.value = false;
    }, props.timeout);

    return {
      visible
    };
  }
});
</script>
