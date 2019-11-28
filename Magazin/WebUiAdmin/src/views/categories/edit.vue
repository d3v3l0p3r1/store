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
      <el-form-item label="Имя">
        <el-input v-model="entity.title" />
      </el-form-item>

      <el-form-item hidden>
        <el-input v-model="entity.parentId" />
      </el-form-item>
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
import { create, get, update } from '@/api/categories'

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
      },
      entityParentId: {
        required: false,
        type: Number,
        default: null
      }
  },
  data() {
    return {
      entity: {
        id: 0,
        title: '',
        parentId: this.entityParentId
      }
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
        title: '',
        parentId: this.entityParentId
      }
    },
    async onSubmit() {
      if (this.entity.id === 0) {
        const res = await create(this.entity)
        this.entityId = res.id
        this.entity = res
      } else {
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
      this.entity = res
    }
  }
}
</script>
