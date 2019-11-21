<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    :before-close="handleClose"
    draggable
    modal
    width="80%"
  >
    <el-form v-loading="loading" :model="product" label-position="top">
      <el-tabs>
        <el-tab-pane label="Основное">
          <el-form-item label="Название">
            <el-input v-model="product.title" />
          </el-form-item>

          <el-form-item label="Категория">
            <el-select v-model="product.categoryId" placeholder="Выберите категорию" value-key="id">
              <el-option
                v-for="item in categories"
                :key="item.id"
                :label="item.title"
                :value="item.id"
              />
            </el-select>
          </el-form-item>

          <el-form-item label="Вид">
            <el-select v-model="product.kindId" placeholder="Выберите вид" value-key="id">
              <el-option
                v-for="item in kinds"
                :key="item.id"
                :label="item.title"
                :value="item.id"
              />
            </el-select>
          </el-form-item>

          <el-form-item label="Описание">
            <el-input v-model="product.description" type="textarea" />
          </el-form-item>

          <el-form-item label="Главное изображение">
            <el-upload
              action
              accept=".jpg, .jpeg, .png, .jfif"
              :limit="1"
              :auto-upload="false"
              :multiple="false"
              :show-file-list="true"
              :file-list="mainImage"
              drag
              :on-change="handleMainImageChange"
              :on-remove="handleMainImageRemove"
              list-type="picture"
            >
              <i class="el-icon-upload" />
            </el-upload>
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="Изображения">
          <el-form-item label="Изображения">
            <el-upload
              action
              accept=".jpg, .jpeg, .png, .jfif"
              :auto-upload="false"
              :multiple="true"
              :show-file-list="true"
              :file-list="images"
              drag
              list-type="picture"
              :on-change="handleImagesChange"
              :on-remove="handleImagesRemove"
            >
              <i class="el-icon-upload" />
            </el-upload>
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
    </el-form>
    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">
        <span v-if="product.id===0">Создать</span>
        <span v-else>Обновить</span>
      </el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>
  </el-dialog>
</template>

<script>
import {
  getProduct,
  createProduct,
  updateProduct,
  getCategories,
  getKinds
} from '@/api/products'
import { getFileUrl } from '@/api/upload'

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
        id: 0,
        title: '',
        categoryId: 0,
        desctiption: '',
        price: 0,
        file: null,
        fileID: 0,
        kindId: 0,
        images: []
      },
      mainImage: [],
      images: [],
      categories: [],
      kinds: [],
      loading: true
    }
  },
  watch: {
    dialogVisible: function(newVisible, oldVisible) {
      if (newVisible === true) {
        this.loadProduct()
      } else {
        this.clear()
      }
    }
  },
  methods: {
    clear() {
       this.product = {
          id: 0,
          title: '',
          categoryId: 0,
          desctiption: '',
          price: 0,
          file: null,
          fileID: 0,
          kindId: 0,
          images: []
        }

        this.categories = []
        this.kinds = []

        this.images = this.images.splice()
        this.mainImage = this.mainImage.splice()
    },
    async loadProduct() {
      this.clear()
      this.loading = true
      this.mainImage = []
      this.images = []

      if (this.entityId !== 0) {
        this.product = await getProduct(this.entityId)
        if (this.product.file != null) {
          this.mainImage = this.mainImage.splice()
          this.mainImage.push({
            id: this.product.file.id,
            name: this.product.file.fileName,
            url: getFileUrl(this.product.file.id)
          })
        }

        if (this.product.images != null) {
          this.images = this.images.splice()
          for (var i = 0; i < this.product.images.length; i++) {
            this.images.push({
              id: this.product.images[i].id,
              name: this.product.images[i].file.fileName,
              url: getFileUrl(this.product.images[i].file.id)
            })
          }
        }
      } else {
        this.clear()
      }

      await this.loadCategories()
      await this.loadKinds()

      this.loading = false
    },
    async loadCategories() {
      const res = await getCategories()
      this.categories = res.data
      if (this.product.id === 0) {
        this.product.categoryId = this.categories[0].id
      }
    },
    async loadKinds() {
      const res = await getKinds()
      this.kinds = res.data
      if (this.product.id === 0) {
        this.product.kindId = this.kinds[0].id
      }
    },
    handleClose() {
      this.$emit('onProductDialogClose')
    },
    onSubmit() {
      this.loading = true
      const formData = new FormData()

      this.product.kind = null
      this.product.category = null

      formData.append('Model', JSON.stringify(this.product))

      if (this.mainImage.length === 0) {
        this.$notify('Ошибка, укажите файл')
        return
      }

      formData.append('mainImage', this.mainImage[0].raw)
      this.images.forEach(image => {
        formData.append('images', image.raw)
      })

      if (this.product.id === 0) {
        createProduct(formData).then(response => {
          this.loading = false
          this.$emit('onProductDialogClose')
        })
      } else {
        updateProduct(formData).then(response => {
          this.loading = false
          this.$emit('onProductDialogClose')
        })
      }
    },
    onCancel() {
      this.$emit('onProductDialogClose')
    },
    handleMainImageChange(file, fileList) {
      this.mainImage = this.mainImage.splice()
      this.mainImage.push(file)
    },
    handleMainImageRemove(file, fileList) {
      this.product.file = null
      this.product.fileID = 0
    },
    handleImagesChange(file, fileList) {
      this.images.push(file)
    },
    handleImagesRemove(file, fileList) {
      if (file.id !== 0) {
        this.product.images = this.product.images.filter(
          (value, index, array) => {
            return value.id !== file.id
          }
        )
      }
    }
  }
}
</script>
