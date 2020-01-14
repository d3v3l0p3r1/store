import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products: [] // { product, count }
  },
  mutations: {
    addToBascet(state, bascetItem) {
      console.log(bascetItem)
      var index = state.products.find(x => x.product.id === bascetItem.product.id)
      if (index != null) {
        index.count++
      } else {
        state.products.push(bascetItem)
      }
    }
  },
  actions: {
  },
  modules: {
  }
})
