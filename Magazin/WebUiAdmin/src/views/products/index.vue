<template>
  <div class="app-container">
    <div class="filter-container">
      <el-select v-model="selectedCategory" placeholder="Выбирите категорию" style="width: 190px" clearable class="filter-item" value-key="id" @change="handelSelectCategory">
        <el-option v-for="item in categories" :key="item.id" :label="item.title" :value="item" />
      </el-select>

      <el-button @click="handleCreateProduct">
        <span>Create</span>
      </el-button>

      <el-button @click="handleEditProduct">
        <span>Edit</span>
      </el-button>
    </div>

    <el-table
      v-loading="listLoading"
      :data="products"
      border
      fit
      highlight-current-row
      row-key="id"
      style="width: 100%;"
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
          <span>{{ scope.row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Изображение">
        <template slot-scope="scope">
          <img :src="scope.row.fileUrl">
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
    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getProducts" />
    <ProductEdit :dialog-visible.sync="dialog.visible" :entity-id.sync="selectedProductId" @onProductDialogClose="onProductDialogClose" />
  </div>
</template>

<script>
import { getProducts, getCategories } from '@/api/products'
import Pagination from '@/components/Pagination'
import ProductEdit from '@/views/products/productEdit'

export default {
    name: 'Products',
    components: { Pagination, ProductEdit },
    data() {
        return {
            products: null,
            categories: [],
            selectedCategory: null,
            selectedProduct: null,
            selectedProductId: 0,
            total: 0,
            listLoading: true,
            dialog: {
                status: '',
                visible: false
            },
            listQuery: {
                page: 1,
                limit: 20
            }
        }
    },
    computed: {

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
            const cat = this.selectedCategory == null ? 1 : this.selectedCategory.id
            const res = await getProducts(cat, this.listQuery.page, this.listQuery.limit)
            this.products = res.data
            this.total = res.total
            this.listLoading = false
        },
        handelSelectCategory() {
            this.getProducts()
        },
        handleSelectProduct(val) {
            this.selectedProduct = val
        },
        handleCreateProduct() {
            this.dialog.visible = true
        },
        handleEditProduct() {
            if (this.selectedProduct != null) {
                debugger
                this.selectedProductId = this.selectedProduct.id
                this.dialog.visible = true
            }
        },
        onProductDialogClose() {
            this.dialog.visible = false
        }
    }
}
</script>
