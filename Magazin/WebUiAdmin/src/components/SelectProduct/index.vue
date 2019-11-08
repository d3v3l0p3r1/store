<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    :before-close="handleClose"
    draggable
    modal
    width="80%"
    append-to-body
  >
    <ProductList v-on:selectedProductChange="onSelectedProductChange" />

    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">Выбрать</el-button>
      <el-button @click="handleClose">Отмена</el-button>
    </footer>
  </el-dialog>

</template>

<script>
import ProductList from '@/components/ProductList'

export default {
    name: 'SelectProduct',
    components: { ProductList },
    props: {
        dialogVisible: {
            type: Boolean,
            required: true
        }
    },
    data() {
        return {
            selectedProduct: null
        }
    },
    methods: {
      handleClose() {
        this.$emit('update:dialogVisible', false)
      },
      onSelectedProductChange(val) {
        this.selectedProduct = val
      },
      onSubmit() {
        if(this.selectedProduct == null) {
          this.$notify.error({
            title: 'Ошибка',
            message: 'Необходимо выбрать продукт'
          })
        } else {
          this.$emit('onProductSelect', this.selectedProduct)
        }
      }
    }

}
</script>
