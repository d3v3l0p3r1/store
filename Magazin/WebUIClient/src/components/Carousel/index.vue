<template>
  <v-carousel cycle light hide-delimiter-background hide-delimiters>
    <v-carousel-item v-for="item in items" :key="item.id">
      <div class="slide_info">
        <h3 class="slide_info__header">{{ item.header }}</h3>
        <h4>{{ item.description }}</h4>
        <v-btn>Подробнее</v-btn>
      </div>
      <v-img :src="getFileUrl(item.fileId)" :aspect-ratio="1" height="600" />
    </v-carousel-item>
  </v-carousel>
</template>

<script>
import { getCarousel } from '@/api/public'
import { getFileUrl } from '@/api/file'
export default {
    name: 'Carousel',
    data() {
        return {
            items: []
        }
    },
    created() {
        this.loadSlides()
    },
    methods: {
        async loadSlides() {
            this.items = await getCarousel()
        },
          getFileUrl(id) {
      return getFileUrl(id)
    }
    }
}
</script>

<style scoped>
.slide_info{
    position: absolute;
    z-index: 1;
}
.slide_info__header{
    top: 12em;
}
</style>
