<template>
  <v-app-bar app extended class="main-menu" dark height="90px">
    <v-img src="/images/a.png" width="50px" max-height="50px" max-width="100px" />
    <v-spacer />
    <BascetIcon />
    <template slot="extension">
      <!-- Main toolbar -->
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn v-for="route in Routes" :key="route.link" text :to="route.link" class="main-navigation--button"> {{ route.title }}</v-btn>
      </v-toolbar-items>
      <!-- end main toolbar -->
      <div class="hidden-md-and-up">
        <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
        <span>Меню</span>
      </div>
      <v-navigation-drawer v-model="drawer" app right light absolute class="hidden-md-and-up">
        <v-list>
          <v-list-item v-for="route in Routes" :key="route.link" link :to="route.link">
            <v-list-item-content>
              <v-list-item-title>{{ route.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
    </template>
  </v-app-bar>
</template>

<script>
import Routes from '@/settings/routes'
import BascetIcon from '@/components/BascetIcon'

export default {
  components: { BascetIcon },
  data() {
    return {
      Routes: Routes,
      drawer: false
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
  background-color: #560082 !important;
}

.main-menu--tab {
  background-color: #002e4f !important;
}

.main-navigation--button {
  color: #fafafa;
}

</style>
