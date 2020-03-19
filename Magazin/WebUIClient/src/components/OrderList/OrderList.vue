<template>
  <v-container>
    <v-expansion-panel v-for="order in orders" :key="order.id">
      <v-expansion-panel-header>Заказ №{{ order.id }}</v-expansion-panel-header>
      <v-expansion-panel-content>
        <v-simple-table light>
          <thead>
            <th colspan="2">Товар</th>
            <th>Цена</th>
            <th>Количество</th>
            <th>Итого</th>
            <th />
          </thead>
          <tbody>
            <tr v-for="item in order.products" :key="item.product.id">
              <td>
                <v-img
                  v-if="item.product.fileId != null"
                  :aspect-ratio="1"
                  :src="getFileUrl(item.product.fileId)"
                  class="product-image"
                >
                  <template v-slot:placeholder>
                    <v-row class="fill-height ma-0" align="center" justify="center">
                      <v-progress-circular indeterminate color="grey lighten-5" />
                    </v-row>
                  </template>
                </v-img>
                <v-img v-else :aspect-ratio="1" src="/images/no-image.png" class="product-image">
                  <template v-slot:placeholder>
                    <v-row class="fill-height ma-0" align="center" justify="center">
                      <v-progress-circular indeterminate color="grey lighten-5" />
                    </v-row>
                  </template>
                </v-img>
              </td>
              <td>{{ item.product.title }}</td>
              <td>{{ item.product.price }}</td>
              <td>{{ item.count * item.product.price }} </td>
            </tr>
          </tbody>
        </v-simple-table>
      </v-expansion-panel-content>
    </v-expansion-panel>
  </v-container>
</template>

<script>
import { getFileUrl } from '@/api/file'

export default {
    name: 'OrderList',
    computed: {
        orders() {
            return this.$store.state.orders
        }
    },
    methods: {
      getFileUrl(id) {
        return getFileUrl(id)
      }
    }
}
</script>
