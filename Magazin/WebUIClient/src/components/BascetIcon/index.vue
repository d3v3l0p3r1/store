<template>
  <div>
    <v-btn icon to="/bascet">
      <v-badge :content="itemInBascet" overlap :value="itemInBascet != 0">
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

</style>
