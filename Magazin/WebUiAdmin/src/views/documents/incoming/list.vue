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
          <span>{{ scope.row.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Описание">
        <template slot-scope="scope">
          <span style="overflow: hidden; white-space: nowrap;">{{ scope.row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Дата создания">
        <template slot-scope="scope">
          <span>{{ scope.row.date }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Дата проведения">
        <template slot-scope="scope">
          <span>{{ scope.row.processDate }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Автор">
        <template slot-scope="scope">
          <span>{{ scope.row.author.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Сумма">
        <template slot-scope="scope">
          <span>{{scope.row.total}}</span>
        </template>
      </el-table-column>

      <el-table-column label="Статус">
        <template slot-scope="scope">
          <span>
            {{ statuses[scope.row.documentStatus] }}
          </span>
        </template>
      </el-table-column>

      <el-table-column label="Операции">
        <template slot-scope="scope">
          <el-tooltip v-if="scope.row.documentStatus==0 || scope.row.documentStatus==20" content="Провести документ" placement="top-start" :open-delay="500">
            <el-button type="success" icon="el-icon-check" circle @click="handleApplyClick(scope.row)" />
          </el-tooltip>

          <el-tooltip v-if="scope.row.documentStatus== 10" content="Отменить проводку" placement="top-start" :open-delay="500">
            <el-button type="danger" icon="el-icon-back" circle @click="handleDiscardClick(scope.row)" />
          </el-tooltip>

          <el-tooltip content="Редактировать" placement="top-start" :open-delay="500">
            <el-button type="primary" icon="el-icon-edit" circle @click="handleEditClick(scope.row)" />
          </el-tooltip>

          <el-tooltip content="Удалить" placement="top-start" :open-delay="500">
            <el-button type="danger" icon="el-icon-delete" circle @click="handleRemoveClick(scope.row)" />
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
import { getDocuments, remove, apply, discard } from '@/api/incmoingDocuments'
import { getDocumentStatusEnum } from '@/api/enums'
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
                limit: 10
            },
            statuses: []
        }
    },
    created() {
      this.loadStatuses()
      this.loadEntities()
    },
    methods: {
        async loadStatuses() {
          this.statuses = await getDocumentStatusEnum()
        },
        async loadEntities() {
            this.listLoading = true
            const res = await getDocuments(this.pagination.page, this.pagination.limit)
            this.entities = res.data
            this.pagination.total = res.total
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
        },
        handleEditClick(row) {
          this.handleCurrentChange(row)
          this.dialogVisible = true
        },
        async handleRemoveClick(row) {
          this.listLoading = true
          await remove(row.id)
          this.loadEntities()
        },
        async handleApplyClick(row) {
          await apply(row.id)
          this.loadEntities()
        },
        async handleDiscardClick(row) {
          await discard(row.id)
          this.loadEntities()
        }
    }
}
</script>
