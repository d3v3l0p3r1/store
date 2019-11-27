<template>
<div>
  <v-app-bar extended class="main-menu" app color="#560082" height="240px">
    <v-spacer />
    <template v-slot:extension>
    <v-tabs centered dark class="main-menu--tab" background-color="#002e4f">
      <v-tab to="/">Главная</v-tab>
      <v-tab to="/products">Продукция</v-tab>
      <v-tab to="/news">Новости</v-tab>
      <v-tab to="/about">О нас</v-tab>
    </v-tabs>
    </template>
  </v-app-bar>
</div>
</template>

<script>
export default {
    computed: {
        styling() {

        }
    }
}
</script>

<style lang="scss" scoped>
.main-menu {
  background-color: #560082;
}

.main-menu--tab {
  background-color: #002e4f !important;
}
</style>
