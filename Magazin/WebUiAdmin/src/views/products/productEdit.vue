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
              :file-list="mainImage"
              drag
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
              :file-list="images"
              drag
              list-type="picture"
              :on-change="handleImagesChange"
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
                categoryId: 1,
                desctiption: '',
                price: 0,
                file: null,
                fileId: 0,
                kindId: 1,
                images: []
            },
            mainImage: [],
            images: [],
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
                if (this.product.file != null) {
                  this.mainImage = this.mainImage.splice()
                  this.mainImage.push(
                    {
                      name: this.product.file.fileName,
                      url: getFileUrl(this.product.file.id)
                    })
                }

                if (this.product.images != null) {
                  this.images = this.images.splice()
                  for (var i = 0; i < this.product.images.length; i++) {
                    debugger
                    this.images.push(
                      {
                        name: this.product.images[i].file.fileName,
                        url: getFileUrl(this.product.images[i].file.id)
                      })
                  }
                }
            } else {
                this.product = {
                    id: 0,
                    title: '',
                    categoryId: 1,
                    desctiption: '',
                    price: 0,
                    file: null,
                    fileId: 0,
                    kindId: 1,
                    images: []
                }
                this.mainImage = []
                this.images = []
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
            const formData = new FormData()

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
              createProduct(formData).then((response) => {
                this.$emit('onProductDialogClose')
            })
            } else {
              updateProduct(formData).then((response) => {
                this.$emit('onProductDialogClose')
              })
            }
        },
        handleMainImageChange(file, fileList) {
            this.mainImage = this.mainImage.splice()
            this.mainImage.push(file)
        },
        handleImagesChange(file, fileList) {
          this.images.push(file)
        }
    }
}
</script>
