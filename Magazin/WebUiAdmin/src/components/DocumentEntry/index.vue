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
      }
  }
}
</script>
