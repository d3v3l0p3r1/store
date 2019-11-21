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

      <el-table-column type="expand">
        <template slot-scope="scope">
          <el-table :data="scope.row.balanceEntries" border>

            <el-table-column label="Документ">
              <template slot-scope="childScope">
                <span v-if="childScope.row.incomingDocument!=null">Приход: {{ childScope.row.incomingDocument.title }}</span>
                <span v-else>Расход: {{ childScope.row.outComingDocument.title }}</span>
              </template>
            </el-table-column>

            <el-table-column label="Количество">
              <template slot-scope="childScope">
                <span>{{ childScope.row.count }}</span>
              </template>
            </el-table-column>

          </el-table>
        </template>
      </el-table-column>

      <el-table-column label="Продукт">
        <template slot-scope="scope">
          <span>{{ scope.row.product.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Изображение">
        <template slot-scope="scope">
          <img :src="getFileUrl(scope.row.product.fileID)" width="60px">
        </template>
      </el-table-column>

      <el-table-column label="Остаток">
        <template slot-scope="scope">
          <span>{{ getSumm(scope.row) }}</span>
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
import { getFileUrl } from '@/api/upload'
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
        },
        getFileUrl(id) {
          return getFileUrl(id)
        },
        getSumm(row) {
          const reducer = (acc, val) => acc + val.count
          const val = row.balanceEntries.reduce(reducer, 0)
          console.log('acc value:', val)
          return val
        }
    }
}
</script>
