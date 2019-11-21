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
        <el-input v-model="entity.fullName" />
      </el-form-item>

      <el-form-item label="Email">
        <el-input v-model="entity.email" type="email" />
      </el-form-item>

      <el-form-item label="Адрес">
        <el-input v-model="entity.address" />
      </el-form-item>

      <el-form-item label="Телефон">
        <el-input v-model="entity.phoneNumber" type="tel" />
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
import { create, get, update, getRoles } from '@/api/user'

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
        userName: '',
        fullName: '',
        address: '',
        type: 10,
        phoneNumber: '',
        email: '',
        password: ''
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
  created() {
    this.loadRoles()
  },
  methods: {
    reset() {
      this.entity = {
        id: 0,
        userName: '',
        fullName: '',
        address: '',
        type: 10,
        phoneNumber: '',
        email: '',
        password: '',
        role: ''
      }
    },
    async onSubmit() {
      this.entity.userName = this.entity.email
      this.entity.password = 'mHL)B]*C5YQEy6}f'
      this.entity.type = 10
      this.entity.role = 'company'

      if (this.entity.id === 0) {
        var res = await create(this.entity)
        this.entityId = res.id
        await this.loadEntity()
      } else {
        await update(this.entity)
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
    },
    async loadRoles() {
      var res = await getRoles()
      this.roles = res
    }
  }
}
</script>
