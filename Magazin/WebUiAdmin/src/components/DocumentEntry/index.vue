<template>
  <div>
    <div class="filter-container">
      <el-button class="filter-item el-button el-button--success" @click="handleCreate">
        <span>Добавить</span>
      </el-button>
      <el-button class="filter-item el-button el-button--warning" @click="handleEdit">
        <span>Изменить</span>
      </el-button>
    </div>

    <el-table
      :data="entries"
      border
      fit
      highlight-current-row
      row-key="id"
      style="width: 100%;"
      empty-text="Нет данных"
      @current-change="handleSelectEntry"
    >

      <el-table-column>
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Название">
        <template slot-scope="scope">
          <span>{{ scope.row.product.title }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Цена">
        <template slot-scope="scope">
          <span>{{ scope.row.product.price }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Количество">
        <template slot-scope="scope">
          <span>{{ scope.row.count }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Операции">
        <template slot-scope="scope">

           <el-tooltip content="Редактировать" placement="top-start" :open-delay=500 >
            <el-button type="primary" icon="el-icon-edit" circle @click="handleEditClick(scope.row)" />
          </el-tooltip>

          <el-tooltip content="Удалить" placement="top-start" :open-delay=500 >
            <el-button type="danger" icon="el-icon-delete" circle @click="handleRemoveClick(scope.row)" />
          </el-tooltip>

        </template>
      </el-table-column>
    </el-table>
    <EntryEdit :entry="entry" :dialog-visible.sync="editDialogVisible" @onSubmit="onEntryEditComplete" />
  </div>
</template>

<script>
import EntryEdit from '@/components/DocumentEntry/edit'

export default {
  name: 'DocumentEntry',
  components: { EntryEdit },
  props: {
    entries: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      entry: {
        id: 0,
        documentId: 0,
        productId: 0,
        product: null,
        count: 0
      },
      editDialogVisible: false,
      selectedEntry: null
    }
  },
  methods: {
      handleCreate() {
        this.entry = {
          id: 0,
          documentId: 0,
          productId: 0,
          product: null,
          count: 0
        }
        this.editDialogVisible = true
      },
      handleEdit() {
        if (this.selectedEntry != null) {
          this.entry = this.selectedEntry
          this.editDialogVisible = true
        }
      },
      handleSelectEntry(val) {
        this.selectedEntry = val
      },
      onEntryEditComplete(val) {
        var contains = this.entries.indexOf(val)
        if (contains === -1) {
          this.entries.push(this.entry)
        }

        this.editDialogVisible = false
      },
      handleEditClick(row) {
        var e = this.entries.filter(x => row.id === x.id)
        this.selectedEntry = e[0]
        this.handleEdit()
      },
      handleRemoveClick(row) {
        var entry = this.entries.find(x => x.id === row.id)
        var index = this.entries.indexOf(entry)
        if (index !== -1) {
          this.entries.splice(index, 1)
        }
      }
  }
}
</script>
