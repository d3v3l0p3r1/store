<template>
  <v-container fluid>
    <v-row>
      <v-col md="6">
        <swiper :options="swiperOption" class="product-image-carousel">
          <swiper-slide v-for="image in images" :key="image">
            <v-img
              :aspect-ratio="1"
              :src="getFileUrl(image)"
              class="product-image"
            />
          </swiper-slide>
        </swiper>
      </v-col>
      <v-col md="6">
        <div v-if="entity">
          <h1>{{ entity.title }}</h1>
          <h3>{{ entity.price }} <v-icon class="">fa-ruble-sign</v-icon></h3>
          <br>
          <p>{{ entity.description }}</p>
          <br>
          <number-input v-model="count" center controls inline :min="1" @change="onCountChange" />
          <v-btn tile dark class="order-button" :height="40" @click="addToBascetHandle">Заказать</v-btn>
          <div>
            <span><b>Упаковка: </b></span>
            <span>{{ entity.package }}</span>
          </div>
          <v-spacer />
          <div>
            <span><b>Артикул: </b></span>
            <span>{{ entity.venderCode }}</span>
          </div>
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { getDetail } from '@/api/product'
import { getFileUrl } from '@/api/file'
import { mapMutations } from 'vuex'

export default {
  components: { },
    data() {
        return {
            entity: null,
            count: 0,
            images: [],
            swiperOption: {
                slidesPerView: 1,
                spaceBetween: 1,
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true
                }
            }
        }
    },
    beforeCreate() {
    },
    created() {
      this.loadEntity()
    },
    mounted() {
    },
    methods: {
        async loadEntity() {
            this.entity = await getDetail(this.$route.params.id)
            this.images.push(this.entity.fileId)
            for (var i = 0; i < this.entity.files.length; i++) {
              this.images.push(this.entity.files[i])
            }
            console.log(this.images)
        },
        getFileUrl(id) {
            return getFileUrl(id)
        },
        onCountChange(newValue, oldValue) {
          if (newValue < 0) {
            this.count = 0
          } else {
            this.count = newValue
          }
        },
        addToBascetHandle() {
          this.addToBascet({ product: this.entity, count: this.count })
        },
        ...mapMutations(['addToBascet'])
    }
}
</script>

<style scoped>
.product-image {
  max-height: 400px;
  max-width: 500px;
}

.product-image-carousel {
  max-height: 400px;
  max-width: 500px;
}

.order-button {
  margin-bottom: 30px;
  margin-left: 2%;
}
</style>>
