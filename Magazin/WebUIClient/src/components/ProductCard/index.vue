<template>
  <v-card hover class="product-card">
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
    <v-img :aspect-ratio="1" v-else src="@/assets/no-image.png" class="product-image">
      <template v-slot:placeholder>
        <v-row class="fill-height ma-0" align="center" justify="center">
          <v-progress-circular indeterminate color="grey lighten-5"></v-progress-circular>
        </v-row>
      </template>
    </v-img>
    <v-card-title class="product-title">{{product.title}}</v-card-title>
    <v-card-text class="product-text">{{ product.description }}</v-card-text>

    <v-card-actions class="product-actions">
      <v-spacer />
      <v-btn icon @click="addToBascetHandle(product)">
        <v-icon>mdi-cart</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapMutations } from "vuex";

export default {
  name: "ProductCard",
  props: {
    product: {
      type: Object,
      required: true
    }
  },
  data() {
    return {};
  },
  methods: {
    addToBascetHandle(product) {
      this.addToBascet({ product: product, count: 1 });
    },
    ...mapMutations(["addToBascet"])
  }
};
</script>

<style scoped>
.product-card {
  width: 245px;
}
.product-image {
  max-width: 100%;
  height: 50%;
  padding: 2px;
}

.product-title {
  display: block;
  overflow: hidden;
  text-overflow: initial;
  font-weight: 600;
  font-size: 14px;
  height: 10%;
}

.product-text {
  font-size: 12px;
  height: 30%;
}

.product-actions {
  color: blue;
}
</style>