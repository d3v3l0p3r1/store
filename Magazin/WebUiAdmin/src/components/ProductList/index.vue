<template>
  <el-container class="app-container">
    <el-aside class="product-categories--tree">
      <el-tree
        v-loading="listLoading"
        :data="categories"
        lazy
        highlight-current-row
        empty-text="Нет данных"
        node-key="id"
        :props="treeOptions"
        @current-change="handelSelectCategory"
        @node-expand="onNodeExpand"
      >
        <span slot-scope="{node}">
          <span>{{ node.label }}</span>
        </span>
      </el-tree>
    </el-aside>
    <el-container>
      <el-header class="filter-container">
        <el-button class="filter-item el-button el-button--success" @click="handleCreateProduct">
          <span>Создать</span>
        </el-button>
        <el-button class="filter-item el-button el-button--warning" @click="handleEditProduct">
          <span>Изменить</span>
        </el-button>
      </el-header>
      <el-main>
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
              <img :src="getFileUrl(scope.row.fileId)" width="60px">
            </template>
          </el-table-column>

          <el-table-column label="Вид">
            <template slot-scope="scope">
              <span>{{ scope.row.kind }}</span>
            </template>
          </el-table-column>

          <el-table-column label="Операции">
            <template slot-scope="scope">

              <el-tooltip content="Редактировать" placement="top-start" :open-delay="500">
                <el-button type="primary" icon="el-icon-edit" circle @click="handleEditClick(scope.row)" />
              </el-tooltip>

              <el-tooltip content="Удалить" placement="top-start" :open-delay="500">
                <el-button type="danger" icon="el-icon-delete" circle @click="handleRemoveClick(scope.row)" />
              </el-tooltip>

            </template>
          </el-table-column>

        </el-table>

      </el-main>
      <el-footer>
        <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getProducts" />
      </el-footer>
      <ProductEdit :dialog-visible.sync="dialog.visible" :entity-id.sync="selectedProductId" @onProductDialogClose="onProductDialogClose" />
    </el-container>
  </el-container>
</template>

<script>
import { getFileUrl } from '@/api/upload'
import { getProducts, getCategories, remove } from '@/api/products'
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
            },
            treeOptions: {
              label: 'title',
              children: 'childs'
            }
        }
    },
    computed: {

    },
    created() {
      this.onLoad()
      this.getProducts()
    },
    methods: {
        async getProducts() {
            this.listLoading = true
            const cat = this.selectedCategory == null ? null : this.selectedCategory.id
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
                this.selectedProductId = this.selectedProduct.id
                this.dialog.visible = true
            }
        },
        onProductDialogClose() {
            this.dialog.visible = false
            this.selectedProductId = 0
            this.getProducts()
        },
        handleEditClick(row) {
          this.selectedProduct = row
          this.handleEditProduct()
        },
        async handleRemoveClick(row) {
          await remove(row.id)
          this.getProducts()
        },
        getFileUrl(id) {
          return getFileUrl(id)
        },
        async onLoad() {
          var res = await getCategories()
          this.categories = res.data
        },
        onNodeExpand(data, node) {
          if (node.childNodes) { node.childNodes.splice(0, node.childNodes.length) }
          this.getChild(data)
        },
        async getChild(node) {
          if (node.childs) {
            node.childs.splice(0, node.childs.length)
          }
          const res = await getCategories(node.id)
          node.childs = res.data
        }
    }
}
</script>

<style scoped>
.product-categories--tree {
  background-color: transparent;
  padding: 0;
  height: 100%;
}
</style>
