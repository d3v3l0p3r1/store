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

      <el-table-column label="Идентификатор">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Название">
        <template slot-scope="scope">
          <span>{{ scope.row.fullName }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Адрес">
        <template slot-scope="scope">
          <span>{{ scope.row.address }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Телефон">
        <template slot-scope="scope">
          <span>{{ scope.row.phoneNumber }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Email">
        <template slot-scope="scope">
          <span>{{ scope.row.email }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Операции">
        <template slot-scope="scope">
          <el-tooltip content="Редактировать" placement="top-start" :open-delay="500">
            <el-button type="primary" icon="el-icon-edit" circle @click="handleEditClick(scope.row)" />
          </el-tooltip>
        </template>
      </el-table-column>

    </el-table>
    <pagination v-show="pagination.total>0" :total="pagination.total" :page.sync="pagination.page" :limit.sync="pagination.limit" @pagination="loadEntities" />
    <EditDialog :dialog-visible.sync="dialogVisible" :entity-id.sync="selectedId" append-to-body @onEditDialogClose="onEditDialogClose" />
  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import { getUsers } from '@/api/user'
import EditDialog from '@/views/companies/edit'

export default {
    name: 'Users',
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
                limit: 10
            }
        }
    },
    created() {
      this.loadEntities()
    },
    methods: {
        async loadEntities() {
            this.listLoading = true
            const res = await getUsers(this.pagination.page, this.pagination.limit, 10)
            this.entities = res.data
            this.pagination.total = res.total
            this.listLoading = false
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
        },
        handleEditClick(row) {
          this.handleCurrentChange(row)
          this.dialogVisible = true
        },
        handleCreate() {
          this.dialogVisible = true
        }
    }
}
</script>
