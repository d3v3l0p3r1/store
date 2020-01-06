<template>
<div>
  <v-app-bar extended class="main-menu" app color="#560082" height="90px" hide-on-scroll>
    <template v-slot:extension>
    <v-tabs centered dark class="main-menu--tab" background-color="#002e4f" height="42px;" elevate-on-scroll>
      <v-tab to="/">Главная</v-tab>
      <v-tab to="/catalog">Продукция</v-tab>
      <v-tab to="/brands">Бренды</v-tab>
      <v-tab to="/catalog?categoryId=4">Уход</v-tab>
      <v-tab to="/news">Новости</v-tab>
      <v-tab to="/about">О нас</v-tab>
    </v-tabs>
    <BascetIcon/>
    </template>    
  </v-app-bar>
</div>
</template>

<script>

import BascetIcon from '@/components/BascetIcon/index'

export default {
  components: { BascetIcon },
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
