<template>
  <el-dialog
    title="Позиция документа"
    :visible.sync="dialogVisible"
    :before-close="onCancel"
    draggable
    modal
    append-to-body
    width="40%"
  >

    <el-form v-model="entry" label-position="top">
      <el-form-item label="Продукт">
        <el-input v-model="productTitle" placeholder="Выбирете продукт" readonly>
          <el-button slot="append" icon="el-icon-search" @click="searchProduct" />
        </el-input>
      </el-form-item>

      <el-form-item v-model="entry.count" label="Количество">
        <el-input-number v-model="entry.count" />
      </el-form-item>

      <el-form-item label="Цена">
        <el-input-number v-model="entry.price" />
      </el-form-item>
    </el-form>

    <SelectProduct :dialog-visible.sync="selectProductVisible" @onProductSelect="onProductSelect" />

    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">Выбрать</el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>

  </el-dialog>
</template>

<script>
import SelectProduct from '@/components/SelectProduct/index'

export default {
    name: 'DocumentEntryEdit',
    components: { SelectProduct },
    props: {
        entry: {
            type: Object,
            required: true
        },
        dialogVisible: {
            type: Boolean,
            required: true,
            default: false
        }
    },
    data() {
        return {
            selectProductVisible: false
        }
    },
    computed: {
        productTitle: function() {
            if (this.entry.product != null) {
                return this.entry.product.title
            } else {
                return ''
            }
        }
    },
    watch: {
    },
    methods: {
        onSubmit() {
            if (this.entry == null) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Необходимо выбрать продукт'
                })
                return
            }

            if (this.entry.product == null) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Необходимо выбрать продукт'
                })
                return
            }

            if (this.entry.count <= 0) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Количество должно быть больше 0'
                })
                return
            }

            if (this.entry.price <= 0) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Цена не можеть быть меньше 1'
                })
                return
            }

            this.$emit('onSubmit', this.entry)
        },
        searchProduct() {
            this.selectProductVisible = true
        },
        onCancel() {
            this.$emit('update:dialogVisible', false)
        },
        onProductSelect(val) {
            this.entry.productId = val.id
            this.entry.product = val
            this.selectProductVisible = false
        }
    }
}
</script>
