<template>
  <div class="app-container">
    <el-header class="filter-container">
      <div>
        <el-button class="filter-item el-button el-button--success" @click="handleCreate">
          <span>Создать</span>
        </el-button>
        <el-button class="filter-item el-button el-button--warning" @click="handleEdit">
          <span>Изменить</span>
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

      <el-table-column label="Заголовок">
        <template slot-scope="scope">
          <span>{{ scope.row.header }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Показывать на слайдере">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.show" disabled />
        </template>
      </el-table-column>

      <el-table-column label="Описание">
        <template slot-scope="scope">
          <span>{{ scope.row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Ссылка">
        <template slot-scope="scope">
          <span>{{ scope.row.href }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Картинка">
        <template slot-scope="scope">
          <img :src="getFileUrl(scope.row.fileId)" width="60px">
        </template>
      </el-table-column>

    </el-table>
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="loadEntities" />
  </div>
</template>

<script>
import { getFileUrl } from '@/api/upload'
import { fetch } from '@/api/carousel'
import Pagination from '@/components/Pagination'

export default {
  name: 'Carousel',
  components: { Pagination },
  data() {
        return {
            entities: null,
            listLoading: true,
            dialogVisible: false,
            selectedId: 0,
            pagination: {
                total: 0,
                page: 1,
                limit: 10
            },
            selectedRow: null
        }
    },
    created() {
      this.loadEntities()
    },
    methods: {
        async loadEntities() {
            this.listLoading = true
            const res = await fetch(this.pagination.page, this.pagination.limit)

            this.entities = res.data
            this.pagination.total = res.total
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
