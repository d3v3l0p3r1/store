<template>
  <v-app-bar app>
    <!-- Main toolbar -->
    <v-img src="@/assets/logo.png" width="200" />
    <v-toolbar-title>Наш магазин</v-toolbar-title>
    <v-toolbar-items class="hidden-sm-and-down">
      <v-btn v-for="route in Routes" :key="route.link" text :to="route.link"> {{ route.title }}</v-btn>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
import BascetIcon from '@/components/BascetIcon/index'
import Routes from '@/settings/routes'

export default {
  components: { BascetIcon },
  data() {
    return {
      Routes: Routes
    }
  },
  computed: {
  }
}
</script>

<style lang="scss" scoped>
.header-main {
  font-family: 'Open Sans', sans-serif;
  font-size: 14px;
  line-height: 1.2;
  font-weight: 400;
  background-color: #fafafa;
  color: #2e2e2e;
}
.main-menu {
  background-color: #560082;
}

.main-menu--tab {
  background-color: #002e4f !important;
}
</style>
