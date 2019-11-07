<template>
  <div>
    <div class="filter-container">
      <el-button class="filter-item el-button el-button--success" @click="handleCreate">
        <span>Создать</span>
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
  </div>
</template>

<script>
export default {
  name: 'DocumentEntry',
  props: {
    entries: {
      type: Array,
      required: true
    }
  }

}
</script>
