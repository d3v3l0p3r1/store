<template>
  <el-dialog
    title="Документ"
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

          <el-form-item label="Название">
            <el-input v-model="entity.title" />
          </el-form-item>

          <el-form-item label="Описание">
            <el-input v-model="entity.description" />
          </el-form-item>

        </el-tab-pane>

        <el-tab-pane label="Позиции">
          <DocumentEntry :entries.sync="entity.entries" />
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
import DocumentEntry from '@/components/DocumentEntry'
import { create, get, update } from '@/api/outcomingDocument'

export default {
    name: 'Edit',
    components: { DocumentEntry },
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
        date: null,
        processDate: null,
        documentStatus: 0,
        title: '',
        description: '',
        author: null,
        authorId: 0,
        entries: []
      },
      entry: {
        product: null,
        count: 0
      }
    }
  },
   watch: {
    dialogVisible: function(newVisible, oldVisible) {
      if (newVisible === true) {
        if (this.entityId === 0) {
          this.reset()
        } else {
          this.loadDocument()
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
        date: null,
        processDate: null,
        documentStatus: 0,
        title: '',
        description: '',
        author: null,
        authorId: 0,
        recipient: null,
        recipientId: 0,
        entries: []
      }
      this.entry = {
        product: null,
        count: 0
      }
    },
    async onSubmit() {
      if (this.entity.id === 0) {
        var res = await create(this.entity)
        this.entityId = res.id
        await this.loadDocument()
      } else {
        await update(this.entity)
        await this.loadDocument()
      }
    },
    onCancel() {
      this.reset()
      this.$emit('onEditDialogClose')
    },
    async loadDocument() {
      var res = await get(this.entityId)
      this.entity = res
    }
  }
}
</script>
