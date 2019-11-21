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
      <el-tabs>
        <el-tab-pane label="Основное">
          <el-form-item label="Имя">
            <el-input v-model="entity.name" />
          </el-form-item>

          <el-form-item label="Описание">
            <el-input v-model="entity.description" />
          </el-form-item>

          <el-form-item label="Телефон">
            <el-input v-model="entity.phone" type="cel" />
          </el-form-item>

          <el-form-item label="Email">
            <el-input v-model="entity.email" type="email" />
          </el-form-item>

          <el-form-item label="Адрес">
            <el-input v-model="entity.address" />
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
      </el-tabs>
    </el-form>

    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">
        <span v-if="entity.id===0">Создать</span>
        <span v-else>Обновить</span>
      </el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>

  </el-dialog>
</template>
<script>
import { create, get, update } from '@/api/contractor'
import { getFileUrl } from '@/api/upload'

export default {
    name: 'Edit',
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
      entity: {
        id: 0,        
        name: '',
        imageId: 0,
        image: null,
        address: '',
        description: '',
        phone: '',
        email: ''
      },
      mainImage: []
    }
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
        name: '',
        imageId: 0,
        image: null,
        address: '',
        description: '',
        phone: '',
        email: ''
      }
      this.mainImage = this.mainImage.splice()
    },
    async onSubmit() {
      const formData = new FormData()
      formData.append('Model', JSON.stringify(this.entity))
      formData.append('image', this.mainImage[0].raw)

      if (this.entity.id === 0) {
        var res = await create(formData)
        this.entityId = res.id
        await this.loadEntity()
      } else {
        await update(formData)
        await this.loadEntity()
      }
    },
    onCancel() {
      this.reset()
      this.$emit('onEditDialogClose')
    },
    async loadEntity() {
      var res = await get(this.entityId)      
      this.entity = res
      if (this.entity.image != null) {
          this.mainImage = this.mainImage.splice()
          this.mainImage.push({
            id: this.entity.image.id,
            name: this.entity.image.fileName,
            url: getFileUrl(this.entity.imageId)
          })
        }
    },
    handleMainImageChange(file, fileList) {
      this.mainImage = this.mainImage.splice()
      this.mainImage.push(file)
    },
    handleMainImageRemove(file, fileList) {
      this.product.file = null
      this.product.fileID = 0
    }
  }
}
</script>
