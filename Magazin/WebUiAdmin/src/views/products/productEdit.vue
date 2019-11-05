<template>
  <el-dialog
    title="Продукт"
    :visible.sync="dialogVisible"
    :before-close="handleClose"
    draggable    
    modal
    width="80%" >
        <el-form :model="product" label-position="top" >

            <el-form-item label="Название">
                <el-input v-model="product.title"></el-input>
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
                <el-input type="textarea" v-model="product.description"></el-input>
            </el-form-item>

            <el-form-item label="Цена">
                <el-input type="number" v-model="product.price"></el-input>
            </el-form-item>

            <el-form-item label="Главное изображение">
                <el-upload
                    action=""
                    accept=".jpg, .jpeg, .png"
                    :limit="1"
                    :auto-upload="false"
                    :multiple="false"
                    :show-file-list="true"
                    :file-list="fileList"
                    :on-change="handleMainImageChange"
                    list-type="picture" >
                    <i class="el-icon-upload"/>
                </el-upload>
            </el-form-item>
        </el-form>
        <footer slot="footer" class="dialog-footer">
                <el-button type="primary" @click="onSubmit">Создать</el-button>
                <el-button>Отмена</el-button>            
        </footer>
  </el-dialog>
</template>

<script>
import { getProduct, createProduct, getCategories, getKinds } from '@/api/products'
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
                title: '',
                categoryId: 0,
                desctiption: '',
                price: 0,
                fileId: 0,
                kindId: 0,
                images: []
            },
            fileList: [],
            categories: [],
            kinds: []
        }
    },
    created() {
        this.loadProduct()
        this.loadCategories()
        this.loadKinds()
    },
    methods: {
        async loadProduct() {
            if (this.entityId !== 0) {
                debugger
                this.product = await getProduct(this.entityId)
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
        handleBeforeUpload(file) {
        },
        onSubmit() {
            const fileData = new FormData()
            fileData.append('file', this.fileList[0].raw)
            fileData.append('name', 'temp')
            uploadImage(fileData).then((resposne) => {
                this.product.fileId = resposne.id
                createProduct(this.product)
            })
        },
        handleMainImageChange(file, fileList) {
            debugger
            this.fileList = this.fileList.slice()
            this.fileList.push(file)
        }
    }
}
</script>
