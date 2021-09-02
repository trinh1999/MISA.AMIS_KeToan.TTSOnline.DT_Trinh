<template>
    <kendo-combobox :data-source="dataSourceArray"
                    :data-text-field="'text'"
                    :data-value-field="'value'"
                    :placeholder="'Select a country...'">
    </kendo-combobox>
</template>
<script>
import { ComboBox } from '@progress/kendo-dropdowns-vue-wrapper';
export default {
    components: {
        'kendo-combobox': ComboBox
    },
    data: function() {
        return {
            comboboxValue: "",
            dataSourceArray: [
                { text: 'Sửa', value: '1' },
                { text: 'Xóa', value: '2' },
                { text: 'Nhân bản', value: '3' },
               
            ]
        }
    }
}
</script>