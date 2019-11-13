<template>
  <div class="app-container">
    <div class="filter-container">
      <el-select v-model="selectedCategory" placeholder="Выбирите категорию" style="width: 190px" clearable class="filter-item" value-key="id" @change="handelSelectCategory">
        <el-option v-for="item in categories" :key="item.id" :label="item.title" :value="item" />
      </el-select>
    </div>
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
import Pagination from '@/components/Pagination'
import { getMoves } from '@/api/balance'
import { getCategories } from '@/api/products'

export default {
    name: 'Users',
    components: { Pagination },
    data() {
        return {
            entities: null,
            listLoading: true,
            dialogVisible: false,
            selectedId: 0,
            selectedCategory: null,
            pagination: {
                total: 0,
                page: 1,
                limit: 10
            },
            categories: []
        }
    },
    created() {
      this.loadEntities()
      this.getCategories()
    },
    methods: {
        async loadEntities() {
            this.listLoading = true
            const cat = this.selectedCategory == null ? null : this.selectedCategory.id
            const res = await getMoves(this.pagination.page, this.pagination.limit, cat)
            this.entities = res.data
            this.pagination.total = res.total
            this.listLoading = false
        },
        async getCategories() {
            const res = await getCategories()
            this.categories = res.data
        },
        handelSelectCategory() {
          this.loadEntities()
        }
    }
}
</script>
