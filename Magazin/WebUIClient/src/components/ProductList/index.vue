<template>
  <v-container class="pa-10">
    <v-row no-gutters>
      <v-col class="col-md-9 col-xs-12 col-right">
        <div class="product-wrap">
          <ProductCard
            v-for="p in products"
            :key="p.id"
            :product="p"
            class="col-lg-3 ma-4"
          />
          <v-pagination
            v-if="pagination.total > pagination.limit"
            v-model="pagination.page"
            :per-page="pagination.limit"
            :length="totalPages"
          />
        </div>
      </v-col>

      <v-col class="col-md-3 col-xs-12 col-left">
        <v-container>
          <v-treeview
            :items="categories"
            item-key="id"
            item-text="title"
            item-children="childs"
            activatable
            open-on-click
            :hoverable="true"
          >
            <template slot="label" slot-scope="{ item }">
              <div @click="handleCategoryClick(item)">{{ item.title }}</div>
            </template>
          </v-treeview>
        </v-container>
      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import { getAll } from '@/api/product'
import { getAll as categoryGetAll } from '@/api/category'
import { getFileUrl } from '@/api/file'
import ProductCard from '@/components/ProductCard/index'

export default {
  name: 'ProductList',
  components: { ProductCard },
  props: {},
  data() {
    return {
      products: [],
      categories: [],
      selectedProduct: null,
      selectedCategory: null,
      listLoading: false,
      pagination: {
        page: 1,
        limit: 42,
        total: 0
      }
    }
  },
  computed: {
    totalPages() {
      if (this.pagination.total > 0) {
        var res = Math.trunc(this.pagination.total / this.pagination.limit)
        if (this.pagination.total % this.pagination.limit) {
          res++
        }
        return res
      }
      return 0
    }
  },
  watch: {
    pagination: {
      handler(n, o) {
        this.getAll()
      },
      deep: true
    },
    $route: function(to, from) {
      this.getAll()
    }
  },
  created() {
    this.getAllCategory()
  },
  methods: {
    async getAll() {
      this.listLoading = true
      var catID = this.$route.query.categoryId
      const res = await getAll(
        this.pagination.page,
        this.pagination.limit,
        catID
      )
      this.products = res.data
      this.pagination.total = res.total
      this.listLoading = false
    },
    async getAllCategory() {
      const res = await categoryGetAll()
      this.categories = res.data

      if (this.$route.query.categoryId) {
        this.selectedCategory = this.categories.filter(
          x => x.id === this.$route.query.categoryId
        )[0]

        if (!this.selectedCategory) {
          this.selectedCategory = this.categories.forEach(x => {
            if (this.findCategory(x)) {
              return x
            }
          })
        }
      }

      this.getAll()
    },
    getFileUrl(id) {
      return getFileUrl(id)
    },
    handleCategoryClick(val) {
      this.$router.push({ path: '/catalog', query: { categoryId: val.id }})
    },
    findCategory(category) {
      var res = category.childs.filter(
        x => x.id === this.$route.query.categoryId
      )
      if (res.length === 0) {
        category.childs.forEach(x => {
          this.findCategory(x)
        })
      }
    }
  }
}
</script>

<style scoped>
.product-wrap {
  display: flex;
  flex-wrap: wrap;
  flex-direction: row-reverse;
}
</style>
