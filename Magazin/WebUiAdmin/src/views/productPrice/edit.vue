<template>
  <el-dialog
    title="Пользователь"
    :visible.sync="dialogVisible"
    :before-close="onCancel"
    draggable
    modal
    append-to-body
    width="80%"
  >

    <el-form :model="entity" label-position="top">

      <el-form-item label="Продукт">
        <el-input v-model="productTitle" placeholder="Выбирете продукт" readonly>
          <el-button slot="append" icon="el-icon-search" @click="searchProduct" />
        </el-input>
      </el-form-item>

      <el-form-item label="Цена">
        <el-input-number v-model="entity.price" />
      </el-form-item>

    </el-form>

    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">
        <span v-if="entity.id===0">Создать</span>
        <span v-else>Обновить</span>
      </el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>

    <SelectProduct :dialog-visible.sync="selectProductVisible" @onProductSelect="onProductSelect" />

  </el-dialog>
</template>
<script>
import { create, get, update } from '@/api/productPrice'
import SelectProduct from '@/components/SelectProduct/index'

export default {
    name: 'Edit',
    components: { SelectProduct },
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
      selectProductVisible: false,
      entity: {
        id: 0,
        price: 0,
        productId: 0,
        product: null,
        date: null
      },
      productTitle: ''
    }
  },
  computed: {
    },
   watch: {
    dialogVisible: function(newVisible, oldVisible) {
      if (newVisible === true) {
        if (this.entityId === 0) {
          this.reset()
        } else {
          this.loadEntity()
        }
      } else {
        this.reset()
      }
    }
  },
  methods: {
    reset() {
      this.entity = {
        id: 0,
        title: ''
      }
    },
    async onSubmit() {
      if (this.entity.id === 0) {
        this.entity.product = null
        const res = await create(this.entity)
        this.entity = res
      } else {
        this.entity.product = null
        const res = await update(this.entity)
        this.entity = res
      }
    },
    onCancel() {
      this.reset()
      this.$emit('onEditDialogClose')
    },
    async loadEntity() {
      var res = await get(this.entityId)
      this.productTitle = res.product.title
      this.entity = res
    },
    searchProduct() {
      this.selectProductVisible = true
    },
    onProductSelect(val) {
      this.productTitle = val.title
      this.entity.productId = val.id
      this.entity.product = val
      this.selectProductVisible = false
    }
  }
}
</script>
