<template>
  <v-container class="px-12">
    <v-row no-gutters>
      <v-col sm="2">
        <v-container class="px-12">
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
              <div @click="handleCategoryClick(item)">{{item.title}}</div>
            </template>
          </v-treeview>
        </v-container>
      </v-col>

      <v-col sm="10">
        <v-container grid-list>
          <v-skeleton-loader :loading="listLoading" v-if="listLoading"></v-skeleton-loader>
          <v-layout wrap row v-else>
            <v-flex v-for="p in products" :key="p.id" class="product-flex">
              <v-card class="product-card" hover>
                <v-img
                  :aspect-ratio="1"
                  v-if="p.fileId != null"
                  :src="getFileUrl(p.fileId)"
                  class="product-image"
                >
                  <template v-slot:placeholder>
                    <v-row class="fill-height ma-0" align="center" justify="center">
                      <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
                    </v-row>
                  </template>
                </v-img>
                <v-img :aspect-ratio="1" v-else src="@/assets/no-image.png" class="product-image">
                  <template v-slot:placeholder>
                    <v-row class="fill-height ma-0" align="center" justify="center">
                      <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
                    </v-row>
                  </template>
                </v-img>
                <v-card-title class="product-title">{{p.title}}</v-card-title>
                <v-card-text class="product-text">{{ p.description }}</v-card-text>

                <v-card-actions class="product-actions">
                  <v-spacer></v-spacer>
                  <v-btn icon>
                    <v-icon>mdi-cart</v-icon>
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-flex>
            <v-pagination
              v-if="pagination.total > pagination.limit"
              v-model="pagination.page"
              :per-page="pagination.limit"
              :length="totalPages"
            />
          </v-layout>          
        </v-container>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getAll } from "@/api/product";
import { getAll as categoryGetAll } from "@/api/category";
import { getFileUrl } from "@/api/file";

export default {
  name: "ProductList",
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
        limit: 40,
        total: 0
      }
    };
  },
  watch: {
    pagination: {
      handler(n, o) {
        this.getAll();
      },
      deep: true
    },
    $route: function(to, from) {
      this.getAll()
    }
  },
  created() {
    this.getAllCategory();
  },
  computed: {
    totalPages() {
      if (this.pagination.total > 0) {
        var res = Math.trunc(this.pagination.total / this.pagination.limit);
        if (this.pagination.total % this.pagination.limit) {
          res++;
        }
        return res;
      }
      return 0;
    }
  },
  methods: {
    async getAll() {
      this.listLoading = true
      var catID = this.$route.query.categoryId
      const res = await getAll(
        this.pagination.page,
        this.pagination.limit,
        catID
      );
      this.products = res.data;
      this.pagination.total = res.total;
      this.listLoading = false
    },
    async getAllCategory() {
      const res = await categoryGetAll();
      this.categories = res.data;

      if (this.$route.query.categoryId) {
        this.selectedCategory = this.categories.filter(
          x => x.id == this.$route.query.categoryId
        )[0];

        if (!this.selectedCategory) {
          this.selectedCategory = this.categories.forEach(x => {
            if (this.findCategory(x)) {
              return x;
            }
          });
        }
      }

      this.getAll();
    },
    getFileUrl(id) {
      return getFileUrl(id);
    },
    handleCategoryClick(val) {
      this.$router.push({path : '/catalog', query: {categoryId : val.id}})
    },
    findCategory(category) {
      var res = category.childs.filter(
        x => x.id == this.$route.query.categoryId
      );
      if (res.length === 0) {
        category.childs.forEach(x => {
          this.findCategory(x);
        });
      }
    }
  }
};
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

.product-image {
  height: 50%;
  padding: 1%;
}

.product-title {
  display: block;
  overflow: hidden;
  text-overflow: initial;
  font-weight: 600;
  font-size: 14px;
  height: 10%;
}

.product-text {
  font-size: 12px;
  height: 30%;
}

.product-actions {
  color: blue;
}

.product-flex {
  width: 20%;
  padding: 1px;
}
.product-card {
  width: 100%;
  height: 100%;
}
</style>
