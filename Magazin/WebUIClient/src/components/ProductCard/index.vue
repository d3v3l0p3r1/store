<template>
  <v-card
    class="product-card"
  >
    <a :href="'/product/' + product.id">
      <v-img
        v-if="product.fileId != null"
        :aspect-ratio="1"
        :src="getFileUrl(product.fileId)"
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
      <v-card-title class="product-title">
        <span class>{{ product.title }}</span>
      </v-card-title>
      <v-card-text class="product-text">{{ product.price }} руб</v-card-text>
    </a>
    <v-card-actions class="product-actions">
      <v-spacer />
      <v-btn block dark tile large @click="addToBascetHandle(product)">
        <v-icon>mdi-cart</v-icon>
        <span>В корзину</span>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapMutations } from 'vuex'
import { getFileUrl } from '@/api/file'
export default {
  name: 'ProductCard',
  props: {
    product: {
      type: Object,
      required: true
    }
  },
  data() {
    return {}
  },
  methods: {
    getFileUrl(id) {
      return getFileUrl(id)
    },
    addToBascetHandle(product) {
      this.addToBascet({ product: product, count: 1 })
    },
    ...mapMutations(['addToBascet'])
  }
}
</script>

<style scoped>
.product-card {
  height: 100%;
}
.product-image {
  max-width: 100%;
  height: 50%;
  padding: 2px;
}

.product-title {
  display: block;
  font-weight: 400;
  font-size: 14px;
  width: 100%;
  height: 55px;
  word-wrap: break-word !important;
  line-height: 1rem;
  word-break: break-word;
}

.product-text {
  color: #2e2e2e !important;
  font-weight: 800 !important;
  margin-top: 20px;
  font-size: 16px;
}

.product-actions {
  color: blue;
}

.bascet-button {
  width: 100%;
  border-radius: 0%;
  background-color: #4c1e8b !important;
  color: #fff;
}
.bascet-button:hover {
  background-color: #fac62c !important;
}
</style>
