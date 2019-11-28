<template>
  <v-container class="px-12">
    <v-row no-gutters>
      <v-col sm="2">

        <v-container class="px-12">
          <v-list-item-group>
            <v-list-item v-for="c in categories" :key="c.id" v-on:click="handleCategoryClick(c)">
              <v-list-item-content>{{c.title}}</v-list-item-content>
            </v-list-item>
          </v-list-item-group>
        </v-container>
      </v-col>

      <v-col sm="10">

        <v-container fluid grid-list-xl>
          <v-layout wrap>
          <v-flex v-for="p in products" :key="p.id" xl4>
            <v-card class="px-2">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>{{p.title}}</v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-img :aspect-ratio="4/4" :src="getFileUrl(p.fileId)" :width="140" :height="140" :left="true" class="px-12">
                 <template v-slot:placeholder>
                    <v-row
                      class="fill-height ma-0"
                      align="center"
                      justify="center" >
                      <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
                    </v-row>
                </template>
              </v-img>
              <v-card-text>{{ p.description }}</v-card-text>
            </v-card>
          </v-flex>
          <v-pagination v-if="pagination.total > pagination.limit" v-model="pagination.page" :per-page="pagination.limit" :length="pagination.total" />
          </v-layout>
        </v-container>

      </v-col>
    </v-row>
  </v-container>
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
                limit: 10,
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
