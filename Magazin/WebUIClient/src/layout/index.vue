<template>
  <v-app>
    <v-navigation-drawer app>
      <Header />
    </v-navigation-drawer>

    <v-app-bar app>
      <NavBar />
    </v-app-bar>

    <v-content>
      <AppMain />
    </v-content>

    <v-footer app>
      <Footer />
    </v-footer>
  </v-app>
</template>

<script>
import { NavBar, AppMain, Footer, Header } from './components'
export default {
    name: 'Layout',
    components: { NavBar, AppMain, Footer, Header }
}
</script>
