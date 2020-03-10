<template>
  <v-container>
    <v-simple-table>
      <thead>
        <th>Номер заказа</th>
        <th>Статус</th>
        <th>Сумма</th>
      </thead>
      <tbody>
        <tr v-for="order in orders" :key="order.id">
          <td>{{ order.id }}</td>
          <td>{{ order.state }} </td>
          <td>{{ order.totalAmount }}</td>
        </tr>
      </tbody>
    </v-simple-table>
  </v-container>
</template>

<script>

export default {
    name: 'OrderList',
    computed: {
        orders() {
            return this.$store.state.orders
        }
    }
}
</script>
