<template>
  <div>
    <input
      type="file"
      ref="fileInput"
      style="display: none"
      @change="handleFileChange"
    />
    <v-icon @click="openFileInput">mdi-file-plus</v-icon>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { addFile } from "../../ApiService/Services/ApiService";

export default defineComponent({
  props: {
    itemId: {
      type: Number,
      required: true,
    },
  },
  setup(props, { emit })  {
    const fileInput = ref(null);
    const pathFile = ref<string>("");
    const messagesInfo = ref<string>("");
    
    const openFileInput = () => {
      fileInput.value.click();
    };

    const handleFileChange = async (event) => {
      emit("loading");
      const selectedFile = event.target.files[0];
      if (selectedFile) {
        const formData = new FormData();
        formData.append("fieldInfo", selectedFile);

        try {
          const fileRequest = await addFile(formData, props.itemId, selectedFile.type);
          pathFile.value = fileRequest;
          messagesInfo.value = "se agrego el archivo en la ruta" + pathFile.value;

          emit("message-succes",fileRequest,messagesInfo);
          
        } catch (error) {
          console.error("Error:", error || "Error al subir el archivo");
          emit("message-error", error);
        } finally {
          emit("added");
        }
      }
    };

    return {
      fileInput,
      openFileInput,
      handleFileChange,
      pathFile,
      messagesInfo
    };
  },
});
</script>
