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
    <el-tree
      v-loading="listLoading"
      highlight-current-row
      lazy
      :data="entities"
      style="width: 100%;"
      empty-text="Нет данных"
      node-key="id"
      :props="treeProps"
      @current-change="handleCurrentChange"
      @node-expand="onNodeExpand"
    >
      <span slot-scope="{ node, data }" class="custom-tree-node">
        <span>{{ node.label }}</span>
        <span>
          <el-tooltip content="Добавить" placement="top-start" :open-delay="500">
            <el-button type="success" size="mini" icon="el-icon-circle-plus-outline" circle @click="handleCreate(data, node)" />
          </el-tooltip>

          <el-tooltip content="Редактировать" placement="top-start" :open-delay="500">
            <el-button type="primary" size="mini" icon="el-icon-edit" circle @click="handleEditClick(data, node)" />
          </el-tooltip>

          <el-tooltip content="Удалить" placement="top-start" :open-delay="500">
            <el-button type="danger" size="mini" icon="el-icon-delete" circle @click="handleRemoveClick(data, node)" />
          </el-tooltip>
        </span>
      </span>

    </el-tree>
    <EditDialog :dialog-visible.sync="dialogVisible" :entity-id.sync="selectedId" append-to-body :entity-parent-id.sync="parentId" @onEditDialogClose="onEditDialogClose" />
  </div>
</template>

<script>
import { getAll, remove } from '@/api/categories'
import EditDialog from '@/views/categories/edit'

export default {
    name: 'Users',
    components: { EditDialog },
    data() {
        return {
            entities: null,
            listLoading: true,
            dialogVisible: false,
            selectedId: 0,
            parentId: 0,
            treeProps: {
              children: 'childs',
              label: 'title'
            }
        }
    },
    created() {
      this.loadEntites()
    },
    methods: {
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
          this.loadEntites()
        },
        handleEditClick(row, node) {
          this.handleCurrentChange(row)
          this.dialogVisible = true
          this.tree.selectedNode = node
        },
        handleCreate(data) {
          if (data != null) {
            this.parentId = data.id
          } else {
            this.parentId = null
          }

          this.selectedId = 0
          this.dialogVisible = true
        },
        async handleRemoveClick(row) {
          await remove(row.id)
          this.loadEntites()
        },
        async loadEntites() {
          this.listLoading = true
          const res = await getAll()
          this.entities = res.data
          this.listLoading = false
        },
        onNodeExpand(data, node) {
          this.getChild(data)
        },
        async getChild(node) {
          this.listLoading = true
          const res = await getAll(node.id)
          node.childs = res.data
          this.listLoading = false
        }

    }
}
</script>

<style scoped>
.custom-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 24px;

}
</style>
