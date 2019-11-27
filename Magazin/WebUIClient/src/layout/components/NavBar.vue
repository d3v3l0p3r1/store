<template>
<div>
  <v-app-bar app class="main-menu" extended>
    <v-spacer />
    <template v-slot:extension>
    <v-toolbar-items >
      <v-btn flat to="/">Главная</v-btn>
      <v-btn to="/products">Продукция</v-btn>
      <v-btn to="/news">Новости</v-btn>
      <v-btn to="/about">О нас</v-btn>
    </v-toolbar-items>
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

    color: #fff;
}
    :hover.main-menu > li {
        background-color: #344433;
    }

.main-nav {
    width: 100%;
    padding-left: 20%;
}
</style>
