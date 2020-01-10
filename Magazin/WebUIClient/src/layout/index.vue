<template>
  <v-app>
    <NavBar />
    <AppMain />
    <Footer />
  </v-app>
</template>

<script>
import { NavBar, AppMain, Footer } from './components';
export default {
  name: 'Layout',
  components: { NavBar, AppMain, Footer }
};
</script>
