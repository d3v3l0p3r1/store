<template>
  <div>
    <v-btn icon to="/bascet" light class="cart-button">
      <v-badge :content="itemInBascet" bottom :value="itemInBascet != 0" class="cart-badge" :bordered="false" dark color="#000000">
        <v-icon large>mdi-cart</v-icon>
      </v-badge>
    </v-btn>
  </div>
</template>

<script>
export default {
    name: 'BascetIcon',
    props: {},
    data() {
        return {
        }
    },
    computed: {
        itemInBascet() {
            const count = this.$store.state.products.length
            return count
        }
    }

}
</script>

<style scoped>
.cart-button {
color: #000000 !important;
}
.cart-badge {
  background-color: transparent !important;
  color: "#000000" !important;
}
</style>
