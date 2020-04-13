<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="entity" :rules="rules">
      <el-form-item label="Название" prop="title">
        <el-input v-model="entity.title" :minlength="1" />
      </el-form-item>
      <el-form-item label="Описание" prop="description">
        <el-input v-model="entity.description" type="textarea" />
      </el-form-item>
      <el-form-item label="Изображение">
        <el-upload
          action=""
          :http-request="uploadImage"
          :multiple="false"
          :limit="1"
          list-type="picture-card"
          :file-list="fileList"
        >
          <i class="el-icon-plus" />
        </el-upload>
      </el-form-item>

    </el-form>
    <footer slot="footer" class="dialog-footer">
      <el-button type="primary" @click="onSubmit">Сохранить</el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>
  </div>

</template>

<script>
import { create, get, update } from '@/api/brands'
import { uploadImage, getFileUrl } from '@/api/upload'
import { MessageBox } from 'element-ui'

export default {
    name: 'BrandEdit',

    data() {
        return {
            entity: {
                id: this.$route.params.id,
                header: null,
                description: null,
                href: null,
                fileId: null,
                show: false
            },
            imageUrl: null,
            fileList: [],
            rules: {
              header: [
                { title: true, message: 'Введите название бренда', trigger: 'blur' }
              ],
              description: [
                { required: true, message: 'Введите описание', trigger: 'blur' }
              ]
            }
        }
    },
    computed: {
    },
    beforeMount: async function() {
      if (this.$route.params.id) {
        this.entity = await get(this.$route.params.id)

        this.fileList.slice()
        this.fileList.push({
          name: this.entity.file.fileName,
          url: getFileUrl(this.entity.fileId)
        })
      }
    },
    methods: {
        async onSubmit() {
          var valid = await this.$refs['ruleForm'].validate()
          if (!valid) {
            return
          }

          if (this.entity.fileId == null) {
            MessageBox('Файл не загружен. Загрузите файл', 'Ошибка', 'error')
            return
          }

          var res = null
          if (this.entity.id === 0) {
            res = await create(this.entity)
          } else {
            res = await update(this.entity)
          }

          if (res != null) {
            this.$router.push('/brands/index')
          }
        },
        onCancel() {
          this.$router.push('/brands/index')
        },
        async uploadImage(o) {
          const formData = new FormData()
          formData.append('formFile', o.file)
          var fileData = await uploadImage(formData)
          this.entity.fileId = fileData.id
        }
    }
}
</script>

<style scoped>
</style>
