<template>
  <div class="mt-10">
    <h1 class="slider_prepend_header">BRANDS</h1>
    <div class="swiper-wrapper">
      <swiper :options="swiperOption">
        <swiper-slide v-for="brand in data" :key="brand.id">
          <h2>{{ brand.title }}</h2>
          <v-img
            v-if="brand.fileId != null"
            :aspect-ratio="1"
            :src="getFileUrl(brand.fileId)"
            class="product-image"
          />
          <v-img v-else :aspect-ratio="1" src="/images/no-image.png" class="product-image">
            <template v-slot:placeholder>
              <v-row class="fill-height ma-0" align="center" justify="center">
                <v-progress-circular indeterminate color="grey lighten-5" />
              </v-row>
            </template>
          </v-img>
        </swiper-slide>
      </swiper>
    </div>
  </div>
</template>

<script>
import { getBrands } from '@/api/public'
import { getFileUrl } from '@/api/file'

export default {
    name: 'BrandCarousel',
    data() {
        return {
            data: [],
            swiperOption: {
                slidesPerView: 10,
                spaceBetween: 10,
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true
                }
            }
        }
    },
    created() {
        this.loadBrands()
    },
    methods: {
        async loadBrands() {
            this.data = await getBrands()
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
</style>
