<template>
  <div>
    <v-system-bar app dark class="system-bar">
      <v-spacer />
      <v-btn to="/login">Login</v-btn>
    </v-system-bar>
    <v-app-bar app class="main-menu" light height="90px">      
      <a href="/" class="main-link">Indigo</a>
      <v-spacer />
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn v-for="route in Routes" :key="route.link" text :to="route.link" class="main-navigation--button">
          <span class="main-toolbar--link">
            {{ route.title }}
          </span>
        </v-btn>
      </v-toolbar-items>
      <!--Mobile toolbar-->
      <div class="hidden-md-and-up">
        <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
        <span>Меню</span>
      </div>
      <v-navigation-drawer v-model="drawer" app right light absolute class="hidden-md-and-up">
        <v-list>
          <v-list-item v-for="route in Routes" :key="route.link" link :to="route.link">
            <v-list-item-content>
              <v-list-item-title>
                <span class="main-link">
                  {{ route.title }}
                </span>
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
      <!--END Mobile toolbar-->
      <v-spacer />
      <BascetIcon />
    </v-app-bar>
  </div>
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
  background-color: #ffffff !important;
}

.main-menu--tab {
  background-color: #ffffff !important;
}

.main-navigation--button {
  color: #000000;
}

.system-bar {
  background-color: #000000;
}

.main-link {
  font-family: "TuesdayNight" !important;
  font-size: 52px;;
  font-weight: 400;
  text-decoration: none;
  color: indigo;
  padding-left: 40px;
}

.main-toolbar--link {
  font-family: "Lato,sans-serif"
}

</style>
