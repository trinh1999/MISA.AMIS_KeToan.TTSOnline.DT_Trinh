<template>
<div>

    <div class="radio-item">
      <input type="radio" id="ritem0" name="ritem" value="0" v-model="picked">
      <label for="ritem0">Nữ</label>
    </div>

    <div class="radio-item">
      <input type="radio" id="ritem1" name="ritem" value="1" v-model="picked">
      <label for="ritem1">Nam</label>
    </div>

    <div class="radio-item">
      <input type="radio" id="ritem2" name="ritem" value="2" v-model="picked">
      <label for="ritem2">Khác</label>
    </div>
</div>
</template>

<style>
@import "../../css/base/radio.css";
</style>

<script>
import "../../main.js";
  export default {
    data() {
      return {
        selected: 'first',
        options: [
          { text: 'Nữ', value: '0' },
          { text: 'Nam', value: '1' },
          { text: 'Khác', value: '2' }
        ]
      }
    },
  }
</script>