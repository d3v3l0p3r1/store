<template>            
    <div fluid>
        <b-card-group deck>
            <b-card v-for="p in products" :key="p.id" >
                <b-card-title :title="p.title"></b-card-title>
                <b-card-img :src="getFileUrl(p.fileId)" :width=140 :height=140 :left=true />
                <b-card-text>{{p.description}}</b-card-text>
            </b-card>
        </b-card-group>
        <b-pagination :per-page=pagination.limit :total-rows=pagination.total v-model="pagination.page"/>
    </div>
</template>

<script>
import { getAll } from '@/api/product'
import { getFileUrl } from '@/api/file'
 
export default {
    name: 'ProductList',
    props: {
    },
    data() {
        return {
            products: [],
            selectedProduct: null,
            pagination: {
                page: 1,
                limit: 4,
                total: 0,
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
        this.getAll()
    },
    methods: {
        async getAll() {            
            const res = await getAll(this.pagination.page, this.pagination.limit)            
            this.products = res.data
            this.pagination.total = res.total
        },
        getFileUrl(id) {
            return getFileUrl(id)
        }
    }
    
}
</script>

<style scoped>
 .card {
     max-width: 25%;
 }
</style>