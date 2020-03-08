<template>
  <div>
    <v-container class="pa-12">
      <h2>Корзина</h2>
      <BascetTable />
    </v-container>
  </div>
</template>

<script>
import BascetTable from './bascetTable'

export default {
    components: { BascetTable },
    data() {
        return {            
        }
    },
    computed: {        
    }

}
</script>
