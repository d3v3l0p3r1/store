<template>
  <v-app-bar app extended class="main-menu" dark height="90px">
    <v-img src="/images/a.png" width="50px" max-height="50px" max-width="100px" />
    <v-spacer />
    <BascetIcon />
    <template slot="extension">      
      <v-spacer />
      <!-- Main toolbar -->
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn v-for="route in Routes" :key="route.link" text :to="route.link" class="main-navigation--button"> {{ route.title }}</v-btn>
      </v-toolbar-items>
      <!-- end main toolbar -->
      <v-menu class="hidden-sm-and-up">
        <template v-slot:activator="{ on }">
          <v-app-bar-nav-icon v-on="on" />
        </template>
        
        <v-list>
          <v-list-item v-for="route in Routes" :key="route.link" :to="route.link"> {{ route.title }} </v-list-item>
        </v-list>
      </v-menu>
      <v-spacer />
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
  background-color: #560082 !important;
}

.main-menu--tab {
  background-color: #002e4f !important;
}

.main-navigation--button {
  color: #fafafa;
}

</style>
