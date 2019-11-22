<template>
  <div>
    <el-dialog
      title="Компания"
      :visible.sync="dialogVisible"
      :before-close="handleClose"
      draggable
      modal
      width="80%"
      append-to-body
    >
      <ContractorList :enable-edit="false" @current-change="handleCurrentChange" />
      <footer slot="footer" class="dialog-footer">
        <el-button type="primary" @click="onSubmit">Выбрать</el-button>
        <el-button @click="handleClose">Отмена</el-button>
      </footer>
    </el-dialog>
  </div>

</template>

<script>
import ContractorList from '@/components/ContractorList/index'
export default {
    name: 'SelectCompany',
    components: { ContractorList },
    props: {
        dialogVisible: {
            type: Boolean,
            required: true
        }
    },
    data() {
        return {
            selected: null
        }
    },
    methods: {
      handleCurrentChange(val) {
          this.selected = val
      },
      handleClose() {
          this.$emit('update:dialogVisible', false)
      },
      onSubmit() {
        if (this.selected == null) {
          this.$notify.error({
            title: 'Ошибка',
            message: 'Необходимо выбрать'
          })
        } else {
          this.$emit('on-select', this.selected)
        }
      }
    }

}
</script>
