<template>
  <div class="home h-100 w-100">
    <Carousel />
    <ProductList />
  </div>
</template>

<script>
import ProductList from '@/components/ProductList'
import Carousel from '@/components/Carousel'
export default {
  name: 'Home',
  components: {
    ProductList,
    Carousel
  }
}
</script>
