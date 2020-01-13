<template>
  <div>
    <v-badge :content="itemInBascet" :value="itemInBascet" color="#fafafa" overlap>
      <v-icon large>mdi-cart</v-icon>
    </v-badge>
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
