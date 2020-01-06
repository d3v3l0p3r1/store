<template>
<div>
    <span>{{itemInBascet}}</span>
    <v-icon color="red">mdi-cart</v-icon>
</div>
</template>

<script>
export default {
    name: 'bascetIcon',
    props: {},
    data() {
        return {}
    },
    computed: {
        itemInBascet() {
            const count = this.$store.state.products.length;
            return count;
        }
    }

}
</script>