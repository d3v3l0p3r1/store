<template>
  <div>
    <v-badge :content="itemInBascet" overlap :value="itemInBascet != 0">
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
            console.log('Count:', count)
            return count
        }
    }

}
</script>

<style scoped>

</style>
