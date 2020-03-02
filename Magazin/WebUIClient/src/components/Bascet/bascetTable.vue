<template>
  <div>
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
              <v-text-field v-model="item.count" dense solo flat class="inputCount" hide-details single-line type="number">
                <v-btn slot="prepend-inner" icon @click="handleMinusClick(item)">
                  <v-icon>mdi-minus</v-icon>
                </v-btn>
                <v-btn slot="append" icon @click="handlePlusClick(item)">
                  <v-icon>mdi-plus</v-icon>
                </v-btn>
              </v-text-field>
            </div>
          </td>
          <td>{{ item.count * item.product.price }} </td>
          <td>
            <v-btn icon @click="handleDeleteClick(item)">
              <v-icon>mdi-delete</v-icon>
            </v-btn>
          </td>
        </tr>
      </tbody>
    </v-simple-table>

    <div>
      <v-simple-table>
        <tbody>
          <tr>
            <td>Итог</td>
            <td>{{ Summ }}</td>
          </tr>
        </tbody>
      </v-simple-table>
      <v-btn block>Перейти к оформлению ></v-btn>
    </div>
  </div>
</template>

<script>
import { getFileUrl } from '@/api/file'
import { mapMutations } from 'vuex'

export default {
    computed: {
        Items() {
            const products = this.$store.state.products
            return products
        },
        Summ() {
          let summ = 0
          for (var i = 0; i < this.$store.state.products.length; i++) {
            summ += this.$store.state.products[i].product.price * this.$store.state.products[i].count
          }
          return summ
        }
    },
    methods: {
        getFileUrl(id) {
            return getFileUrl(id)
        },
        handleMinusClick(item) {
          this.decrementBascetItem(item)
        },
        handlePlusClick(item) {
          this.incrementBascetItem(item)
        },
        handleDeleteClick(item) {
          this.removeFromBascet(item)
        },
        ...mapMutations(['removeFromBascet', 'incrementBascetItem', 'decrementBascetItem'])
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

.inputCount input[type='number'] {
    -moz-appearance:textfield;
}
.inputCount input::-webkit-outer-spin-button,
.inputCount input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}

</style>
