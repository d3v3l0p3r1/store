<template>
  <div class="home h-100 w-100">
    <Carousel />
    <LastProducts />
    <BrandCarousel />
    <ProductList />
  </div>
</template>

<script>
import ProductList from '@/components/ProductList'
import Carousel from '@/components/Carousel'
import LastProducts from '@/components/LastProducts'
import BrandCarousel from '@/components/BrandCarousel'
export default {
  name: 'Home',
  components: {
    ProductList,
    Carousel,
    LastProducts,
    BrandCarousel
  }
}
</script>
