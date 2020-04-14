<template>
  <v-container fluid>
    <v-row>
      <v-col md="6">
        <v-row>
          <v-col sm="6" offset-sm="6">
            <v-img :src="getFileUrl(entity.fileId)" />
          </v-col>
          <v-col>
            <swiper :options="swiperOption">
              <swiper-slide v-for="image in images" :key="image">
                <v-img
                  :aspect-ratio="1"
                  :src="getFileUrl(image)"
                  class="product-image"
                />
              </swiper-slide>
            </swiper>
          </v-col>
        </v-row>
      </v-col>
      <v-col md="6">
        <h1>{{ entity.title }}</h1>
        <h3>{{ entity.price }} <v-icon class="">fa-ruble-sign</v-icon></h3>
        <br>
        <p>{{ entity.description }}</p>
        <br>
        <number-input v-model="amount" center controls inline large />
        <v-btn v-btn contained tile dark>Заказать</v-btn>
        <div>
          <span><b>Упаковка: </b></span>
          <span>{{ entity.package }}</span>
        </div>
        <v-spacer />
        <div>
          <span><b>Артикул: </b></span>
          <span>{{ entity.venderCode }}</span>
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getDetail } from '@/api/product'
import { getFileUrl } from '@/api/file'

export default {
  components: { },
    data() {
        return {
            entity: null,
            amount: 0,
            images: []
        }
    },
    created() {
        this.loadEntity()
    },
    methods: {
        async loadEntity() {
            this.entity = await getDetail(this.$route.params.id)
            this.images.push(this.entity.fileId)
            this.images.push(this.entity.images)
        },
        getFileUrl(id) {
            return getFileUrl(id)
        }
    }
}
</script>

<style>

</style>
