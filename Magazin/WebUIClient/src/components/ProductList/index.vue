<template>
    <div>
        <div>
            <el-row>
                <el-col v-for="o in products" :key="o" :offset="index > 0 ? 2 : 0" >
                    <el-card :body-style="{ padding: '0px' }">
                        <img src=""/>
                    </el-card>
                    <div>
                        <span>{{o.title}}</span>
                    </div>
                </el-col>
            </el-row>
        </div>

        <el-pagination :total=pagination.total :page=pagination.page :page-size=pagination.limit />
    </div>
</template>

<script>
import { getAll } from '@/api/product'

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
                limit: 20,
                total: 0
            }
        }
    },
    created() {
        this.getAll()
    },
    methods: {
        async getAll() {
            const res = await getAll(this.pagination.page, this.pagination.limit)
            this.products = res.data
        }
    }
    
}
</script>