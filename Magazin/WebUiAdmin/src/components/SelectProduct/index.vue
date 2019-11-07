<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    :before-close="handleClose"
    draggable
    modal
    width="80%"
  >
    <ProductList />
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

    }

}
</script>
