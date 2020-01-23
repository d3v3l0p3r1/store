<template>
  <v-simple-table light>
    <thead>
      <th colspan="2">Товар</th>
      <th>Цена</th>
      <th>Количество</th>
      <th>Итого</th>
      <th />
    </thead>
    <tbody>
      <tr v-for="item in Items" :key="item.product.id">
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
        <td>
          <div class="bascet-quntity-buttons">
            <v-text-field v-model="item.count" type="number" append="+" prepend="-" />
          </div>
        </td>
        <td>{{ item.count * item.product.price }} </td>
        <td>
          <v-btn icon>
            <v-icon>mdi-delete</v-icon>
          </v-btn>
        </td>
      </tr>
    </tbody>
  </v-simple-table>
</template>

<script>
import { getFileUrl } from '@/api/file'

export default {
    computed: {
        Items() {
            const products = this.$store.state.products
            console.log(products)
            return products
        }
    },
    methods: {
        getFileUrl(id) {
            return getFileUrl(id)
        }
    }
}
</script>

<style scoped>
.product-image {
  max-width: 100%;
  height: 50%;
  padding: 2px;
}

.bascet-quntity-buttons {
  display: inline-block;
  vertical-align: middle;
}
</style>
