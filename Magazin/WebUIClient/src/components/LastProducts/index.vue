<template>
  <div class="mt-10">
    <h1 class="slider_prepend_header">FIND YOUR BEAUTY MATCH</h1>
    <h5 class="slider_prepent_description">At vero eos et accusamus et iusto</h5>
    <div class="swiper-wrapper">
      <swiper :options="swiperOption">
        <swiper-slide v-for="product in data" :key="product.id">
          <v-card class="product-card">
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

            <v-card-actions class="product-actions">
              <v-spacer />
              <v-btn block dark tile large @click="addToBascetHandle(product)">
                <v-icon>mdi-cart</v-icon>
                <span>В корзину</span>
              </v-btn>
            </v-card-actions>
          </v-card>
        </swiper-slide>
      </swiper>
    </div>
  </div>
</template>

<script>
import { getLastProducts } from '@/api/public'
import { getFileUrl } from '@/api/file'

export default {
    name: 'LastProducts',
    data() {
        return {
            data: [],
            swiperOption: {
                slidesPerView: 4,
                spaceBetween: 10,
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true
                }
            }
        }
    },
    created() {
        this.loadProducts()
    },
    methods: {
        async loadProducts() {
            this.data = await getLastProducts(15)
        },
        getFileUrl(id) {
            return getFileUrl(id)
        }
    }
}
</script>

<style scoped>
.slider_prepend_header {
    text-align: center;
}

.slider_prepent_description{
    text-align: center;
    font-family: 'Cormorant';
    color: #5a5a5a;
    font-size: 22px;
    line-height: 1.136em;
    font-style: italic;
    letter-spacing: 0px;
    text-transform: none;
}

.swiper-wrapper {
    width: 80%;
    margin-left: 10%;
    margin-right: 10%;
}
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
