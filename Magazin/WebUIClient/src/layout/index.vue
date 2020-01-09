<template>
  <v-app>
    <SystemBar />
    <NavBar />
    <AppMain />
    <Footer />
  </v-app>
</template>

<script>
import { NavBar, AppMain, Footer, Header, SystemBar } from "./components";
export default {
  name: "Layout",
  components: { NavBar, AppMain, Footer, Header, SystemBar }
};
</script>
