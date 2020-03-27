<template>
  <div class="app-container">
    <el-form>
      <el-form-item label="Изображение">
        <el-upload
          :action="getUploadUrl"
          :show-file-list="false"
        >
          <img v-if="imageUrl" :src="imageUrl" class="avatar">
          <i v-else class="el-icon-plus avatar-upload-icon" />
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
import { getUploadUrl } from '@/api/upload'
export default {
    name: 'CarouselEdit',
    data() {
        return {
            entity: {
                id: 0
            },
            imageUrl: null
        }
    },
    computed: {
        getUploadUrl() {
            return getUploadUrl()
        }
    },
    methods: {
        onSubmit() {

        },
        onCancel() {

        }
    }
}
</script>

<style scoped>
  .avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  .avatar-uploader .el-upload:hover {
    border-color: #409EFF;
  }
  .avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    line-height: 178px;
    text-align: center;
  }
  .avatar {
    width: 178px;
    height: 178px;
    display: block;
  }
</style>
