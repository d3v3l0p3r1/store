<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item el-button el-button--success" @click="handleCreate">
        <span>Создать</span>
      </el-button>
      <el-button class="filter-item el-button el-button--warning" @click="handleEdit">
        <span>Изменить</span>
      </el-button>
    </div>
    <el-table
      v-loading="listLoading"
      :data="entities"
      border
      fit
      highlight-current-row
      row-key="id"
      style="width: 100%;"
      empty-text="Нет данных"
      @current-change="handleCurrentChange"
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
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="loadEntities" />
    <EditDialog :dialog-visible.sync="dialogVisible" :entity-id.sync="selectedId" append-to-body @onEditDialogClose="onEditDialogClose" />
  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import { getDocuments } from '@/api/incmoingDocuments'
import EditDialog from '@/views/documents/incoming/edit'

export default {
    name: 'IncomingDocuments',
    components: { Pagination, EditDialog },
    data() {
        return {
            entities: null,
            listLoading: true,
            dialogVisible: false,
            selectedId: 0,
            pagination: {
                total: 0,
                page: 1,
                limit: 20
            }
        }
    },
    created() {
        this.loadEntities()
    },
    methods: {
        async loadEntities() {
            this.listLoading = true
            const res = await getDocuments(this.pagination.page, this.pagination.limit)
            this.entities = res.data
            this.total = res.total
            this.listLoading = false
        },
        handleCreate() {
          this.dialogVisible = true
        },
        handleEdit() {
          if (this.selectedId !== 0) {
            this.dialogVisible = true
          }
        },
        handleCurrentChange(val) {
          if (val != null) {
            this.selectedId = val.id
          } else {
            this.selectedId = 0
          }
        },
        onEditDialogClose() {
          this.dialogVisible = false
          this.selectedId = 0
          this.loadEntities()
        }
    }
}
</script>
