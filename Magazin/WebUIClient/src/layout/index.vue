<template>
  <v-app>
    <NavBar />
    <AppMain />
    <Footer />
  </v-app>
</template>

<script>
import { NavBar, AppMain, Footer, Header } from './components'
export default {
    name: 'Layout',
    components: { NavBar, AppMain, Footer, Header }
}
</script>
