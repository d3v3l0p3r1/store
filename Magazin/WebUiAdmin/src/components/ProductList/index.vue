<template>
  <div>
    <div class="filter-container">
      <el-select v-model="selectedCategory" placeholder="Выбирите категорию" style="width: 190px" clearable class="filter-item" value-key="id" @change="handelSelectCategory">
        <el-option v-for="item in categories" :key="item.id" :label="item.title" :value="item" />
      </el-select>
    </div>
    <el-table
      v-loading="listLoading"
      :data="products"
      border
      fit
      highlight-current-row
      row-key="id"
      style="width: 100%;"
      empty-text="Нет данных"
      @current-change="handleSelectProduct"
    >
      <el-table-column>
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Название">
        <template slot-scope="scope">
          <span>{{ scope.row.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Описание">
        <template slot-scope="scope">
          <el-popover
            placement="top-start"
            title="Title"
            width="200"
            trigger="hover"
            content="scope.row.description"
          />
          <span style="overflow: hidden; white-space: nowrap;">{{ scope.row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Изображение" width="120px">
        <template slot-scope="scope">
          <img :src="scope.row.fileUrl" width="60px">
        </template>
      </el-table-column>

      <el-table-column label="Цена">
        <template slot-scope="scope">
          <span>{{ scope.row.price }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Вид">
        <template slot-scope="scope">
          <span>{{ scope.row.kind }}</span>
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="getProducts" />

  </div>
</template>

<script>
import { getProducts, getCategories } from '@/api/products'
import Pagination from '@/components/Pagination'

export default {
    name: 'Products',
    components: { Pagination },
    props: {
    },
    data() {
        return {
            selectedCategory: null,
            selectedProduct: null,
            products: [],
            categories: [],
            listLoading: true,
            dialogVisible: false,
            pagination: {
                page: 1,
                limit: 20,
                total: 0
            }
        }
    },
    created() {
      this.getCategories()
      this.getProducts()
    },
    methods: {
        async getCategories() {
            const res = await getCategories()
            this.categories = res.data
        },
        async getProducts() {
            this.listLoading = true
            const cat = this.selectedCategory == null ? null : this.selectedCategory.id
            const res = await getProducts(cat, this.pagination.page, this.pagination.limit)
            this.products = res.data
            this.pagination.total = res.total
            this.listLoading = false
        },
        handelSelectCategory(val) {
            this.$emit('update:selectedCategory', val)
            this.getProducts()
        },
        handleSelectProduct(val) {
          this.$emit('selectedProductChange', val)
          this.selectedProduct = val
        }
    }

}
</script>
