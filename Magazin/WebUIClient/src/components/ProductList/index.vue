<template>
  <div fluid>
    <div class="row h-100">
      <div class="col-md-2">
        <b-nav vertical>
          <b-nav-item v-for="c in categories" :key="c.id" @click="handleCategoryClick(c)">
            {{ c.title }}
          </b-nav-item>
        </b-nav>
      </div>
      <div class="col-md-10">
        <b-card-group deck class="product-grid h-100">
          <b-card v-for="p in products" :key="p.id">
            <b-card-title :title="p.title" />
            <b-card-img :src="getFileUrl(p.fileId)" :width="140" :height="140" :left="true" />
            <b-card-text>{{ p.description }}</b-card-text>
          </b-card>
        </b-card-group>
        <b-pagination v-if="pagination.total > pagination.limit" v-model="pagination.page" :per-page="pagination.limit" :total-rows="pagination.total" />
      </div>
    </div>
  </div>
</template>

<script>
import { getAll } from '@/api/product'
import { getAll as categoryGetAll } from '@/api/category'
import { getFileUrl } from '@/api/file'

export default {
    name: 'ProductList',
    props: {
    },
    data() {
        return {
            products: [],
            categories: [],
            selectedProduct: null,
            selectedCategory: null,
            pagination: {
                page: 1,
                limit: 20,
                total: 0
            }
        }
    },
    watch: {
        pagination: {
            handler(n, o) {
                this.getAll()
            },
            deep: true
        }
    },
    created() {
        this.getAllCategory()
        this.getAll()
    },
    methods: {
        async getAll() {
            var catID = this.selectedCategory != null ? this.selectedCategory.id : null
            const res = await getAll(this.pagination.page, this.pagination.limit, catID)
            this.products = res.data
            this.pagination.total = res.total
        },
        async getAllCategory() {
            const res = await categoryGetAll()
            this.categories = res.data
        },
        getFileUrl(id) {
            return getFileUrl(id)
        },
        handleCategoryClick(val) {
            this.selectedCategory = val
            this.getAll()
        }
    }

}
</script>

<style scoped>
 .card {
     max-width: 224px;
     min-width: 224px;
 }

 .product-grid {
     height: 100%;
 }
 .product-grid > div {
     margin: 1%;
 }
</style>
