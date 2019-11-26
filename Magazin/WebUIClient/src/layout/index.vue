<template>        
  <div>
    <Header/>
    <NavBar/>
    <AppMain />
    <Footer />
  </div>
</template>

<script>
import { NavBar, AppMain, Footer, Header } from './components'
export default {
    name: 'Layout',
    components: { NavBar, AppMain, Footer, Header}
}
</script>