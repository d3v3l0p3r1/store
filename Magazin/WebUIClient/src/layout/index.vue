<template>        
  <div>
    <NavBar class="site-header"/>
    <AppMain />    
  </div>
</template>

<script>
import { NavBar, AppMain } from './components'
export default {
    name: 'Layout',
    components: { NavBar, AppMain }
}
</script>