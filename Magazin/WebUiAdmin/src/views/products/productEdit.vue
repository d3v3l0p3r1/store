<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    :before-close="handleClose"
    draggable
    modal
    width="80%"
  >
    <el-form :model="product" label-position="top">
      <el-tabs>
        <el-tab-pane label="Основное">
          <el-form-item label="Название">
            <el-input v-model="product.title" />
          </el-form-item>

          <el-form-item label="Категория">
            <el-select v-model="product.categoryId" placeholder="Выбирите категорию" value-key="id">
              <el-option v-for="item in categories" :key="item.id" :label="item.title" :value="item.id" />
            </el-select>
          </el-form-item>

          <el-form-item label="Вид">
            <el-select v-model="product.kindId" placeholder="Выбирите вид" value-key="id">
              <el-option v-for="item in kinds" :key="item.id" :label="item.title" :value="item.id" />
            </el-select>
          </el-form-item>

          <el-form-item label="Описание">
            <el-input v-model="product.description" type="textarea" />
          </el-form-item>

          <el-form-item label="Цена">
            <el-input v-model="product.price" type="number" />
          </el-form-item>

          <el-form-item label="Главное изображение">
            <el-upload
              action=""
              accept=".jpg, .jpeg, .png, .jfif"
              :limit="1"
              :auto-upload="false"
              :multiple="false"
              :show-file-list="true"
              :file-list="fileList"
              :on-change="handleMainImageChange"
              list-type="picture"
            >
              <i class="el-icon-upload" />
            </el-upload>
          </el-form-item>
        </el-tab-pane>
        <el-tab-pane label="Изображения">
          <el-form-item label="Изображения">
            <el-upload
              action=""
              accept=".jpg, .jpeg, .png, .jfif"
              :auto-upload="false"
              :multiple="true"
              :show-file-list="true"
              :file-list="otherFiles"
              list-type="picture"
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
      <el-button>Отмена</el-button>
    </footer>
  </el-dialog>
</template>

<script>
import { getProduct, createProduct, updateProduct, getCategories, getKinds } from '@/api/products'
import { uploadImage } from '@/api/upload'

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
                fileId: 0,
                kindId: 0,
                images: []
            },
            fileList: [],
            otherFiles: [],
            categories: [],
            kinds: []
        }
    },
    watch: {
        dialogVisible: function(newVisible, oldVisible) {
            if (newVisible === true) {
                this.loadProduct()
            }
        }
    },
    created() {
        this.loadCategories()
        this.loadKinds()
    },
    methods: {
        async loadProduct() {
            if (this.entityId !== 0) {
                this.product = await getProduct(this.entityId)
            } else {
                this.product = {
                    id: 0,
                    title: '',
                    categoryId: 0,
                    desctiption: '',
                    price: 0,
                    fileId: 0,
                    kindId: 0,
                    images: []
                }
                this.fileList = []
                this.otherFiles = []
            }
        },
        async loadCategories() {
            const res = await getCategories()
            this.categories = res.data
            this.product.categoryId = this.categories[0].id
        },
        async loadKinds() {
            const res = await getKinds()
            this.kinds = res.data
            this.product.kindId = this.kinds[0].id
        },
        handleClose() {
            this.$emit('onProductDialogClose')
        },
        onSubmit() {
            const fileData = new FormData()
            fileData.append('file', this.fileList[0].raw)
            fileData.append('name', 'temp')
            uploadImage(fileData).then((resposne) => {
                this.product.fileId = resposne.id
                if (this.product.id === 0) {
                    createProduct(this.product).then((response) => {
                        this.$emit('onProductDialogClose')
                    })
                } else {
                    updateProduct(this.product).then((resposne) => {
                        this.$emit('onProductDialogClose')
                    })
                }
            })
        },
        handleMainImageChange(file, fileList) {
            this.fileList = this.fileList.slice()
            this.fileList.push(file)
        }
    }
}
</script>
