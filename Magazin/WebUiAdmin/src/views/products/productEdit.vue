<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    width="80%"
    :before-close="handleClose"
  >
    <el-form />
  </el-dialog>
</template>

<script>
import { getProduct } from '@/api/products'

export default {
    name: 'ProductEdit',
    props: {
        entityId: {
            required: false,
            type: Number,
            default: 0
        },
        dialogVisible: {
            required: true,
            type: Boolean
        }
    },
    data() {
        return {
            product: {
                title: '',
                desctiption: '',
                price: 0,
                fileId: 0,
                kindId: 0
            }
        }
    },
    created() {
        this.loadProduct()
    },
    methods: {
        async loadProduct() {
            debugger
            if (this.entityId !== 0) {
                this.product = await getProduct(this.entityId)
            }
        },
        handleClose() {
            this.$emit('onProductDialogClose')
        }

    }
}
</script>
