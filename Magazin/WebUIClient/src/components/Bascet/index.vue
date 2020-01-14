<template>
  <div>
    <v-tabs centered>
      <v-tab key="bascet" href="#bascet">Корзина</v-tab>
      <v-tab key="order" href="#order">Заказ</v-tab>

      <v-tab-item value="bascet">
        <BascetTable />
      </v-tab-item>
      <v-tab-item value="order">
        <div>Заказ</div>
      </v-tab-item>
    </v-tabs>
  </div>
</template>

<script>
import BascetTable from './bascetTable'

export default {
    components: { BascetTable },
    data() {
        return {
            tabs: null
        }
    },
    computed: {        
    }

}
</script>
