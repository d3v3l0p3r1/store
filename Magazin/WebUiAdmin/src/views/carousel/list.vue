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
    >

      <el-table-column label="Идентификатор">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Продукт">
        <template slot-scope="scope">
          <span>{{ scope.row.title }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Цена">
        <template slot-scope="scope">
          <span>{{ scope.row.price }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Вид">
        <template slot-scope="scope">
          <span>{{ scope.row.kindTitle }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Количество">
        <template slot-scope="scope">
          <span>{{ scope.row.count }}</span>
        </template>
      </el-table-column>

    </el-table>
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="loadEntities" />
  </div>
</template>

<script>
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
            }
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
        }
    }

}
</script>
