<template>
  <div class="home h-100 w-100">
    <ProductList />
  </div>
</template>

<script>
import ProductList from '@/components/ProductList'
export default {
  name: 'home',
  components: {
    ProductList
  }
}
</script>
