<template>
  <div class="app-container">
    <div class="filter-container" />

    <el-table />
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="getDocuments" />
  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import { getDocuments } from '@/api/incmoingDocuments'
export default {
    name: 'IncomingDocuments',
    components: { Pagination },
    data() {
        return {
            documents: null,
            listLoading: true,
            pagination: {
                total: 0,
                page: 1,
                limit: 20
            }
        }
    },
    methods: {
        async getDocuments() {
            const res = await getDocuments()
            this.documents = res
        }
    }
}
</script>
