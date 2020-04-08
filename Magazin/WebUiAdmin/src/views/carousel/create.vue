<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="entity" :rules="rules">
      <el-form-item label="Заголовок" prop="header">
        <el-input v-model="entity.header" :minlength="1" />
      </el-form-item>
      <el-form-item label="Описание" prop="description">
        <el-input v-model="entity.description" type="textarea" />
      </el-form-item>
      <el-form-item label="Ссылка" prop="href">
        <el-input v-model="entity.href" :minlength="1" />
      </el-form-item>
      <el-form-item label="Показывать?">
        <el-switch v-model="entity.show" />
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
      <el-button type="primary" @click="onSubmit">
        <span v-if="entity.id===0">Создать</span>
        <span v-else>Обновить</span>
      </el-button>
      <el-button @click="onCancel">Отмена</el-button>
    </footer>
  </div>

</template>

<script>
import { create, get, update } from '@/api/carousel'
import { uploadImage, getFileUrl } from '@/api/upload'
import { MessageBox } from 'element-ui'

export default {
    name: 'CarouselEdit',

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
                { required: true, message: 'Введите заголовок', trigger: 'blur' }
              ],
              description: [
                { required: true, message: 'Введите описание', trigger: 'blur' }
              ],
              href: [
                { required: true, message: 'Введите ссылку', trigger: 'blur' }
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
            this.$router.push('/carousel/index')
          }
        },
        onCancel() {
          this.$router.push('/carousel/index')
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
