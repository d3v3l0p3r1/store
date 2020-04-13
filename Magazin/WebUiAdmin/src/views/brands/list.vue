<template>
  <div class="app-container">
    <el-header class="filter-container">
      <div>
        <el-button class="filter-item el-button el-button--warning" @click="handleEdit">
          <span>Редактировать</span>
        </el-button>
      </div>
    </el-header>
    <el-table
      v-loading="listLoading"
      :data="entities"
      border
      fit
      highlight-current-row
      row-key="id"
      style="width: 100%;"
      empty-text="Нет данных"
      @current-change="handleSelectRow"
    >
      <el-table-column label="Идентификатор">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Название">
        <template slot-scope="scope">
          <span>{{ scope.row.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Картинка">
        <template slot-scope="scope">
          <img :src="getFileUrl(scope.row.fileId)" width="60px">
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getFileUrl } from '@/api/upload'
import { fetch } from '@/api/brands'

export default {
  name: 'Carousel',
  components: { },
  data() {
        return {
            entities: null,
            listLoading: true,
            dialogVisible: false,
            selectedId: 0,
            selectedRow: null
        }
    },
    created() {
      this.loadEntities()
    },
    methods: {
        async loadEntities() {
            this.listLoading = true
            const res = await fetch()

            this.entities = res
            this.listLoading = false
        },
        handelSelectCategory() {
          this.loadEntities()
        },
        handleCreate() {
          this.$router.push('create')
        },
        handleEdit() {
          if (this.selectedRow != null) {
            var str = 'edit/' + this.selectedRow.id
            this.$router.push(str)
          }
        },
        handleSelectRow(row) {
          this.selectedRow = row
        },
        getFileUrl(id) {
          return getFileUrl(id)
        }
    }

}
</script>
