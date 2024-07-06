<template>
  <v-alert :type="alertType" v-if="visible">{{ message }}</v-alert>
</template>

<script lang="ts">
import { defineComponent, PropType, ref,computed } from 'vue';

export default defineComponent({
  props: {
    error: {
      type: Object as PropType<{ errors: Record<string, string[]> }>,
      default: null
    },
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
    const errorMessage = computed(() => {
      if (props.error && props.error.errors) {
        const errorKeys = Object.keys(props.error.errors);
        const errorMessages = errorKeys.map(key => props.error.errors[key].join('; '));
        return errorMessages.join('; ');
      } else {
        return '';
      }
    });
    return {
      visible,
      errorMessage
    };
  }
});
</script>
