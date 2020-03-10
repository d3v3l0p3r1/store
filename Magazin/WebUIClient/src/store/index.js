import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products: [], // { product, count }
    orders: [] // orders
  },
  mutations: {
    initialiseStore(state) {
      if (localStorage.getItem('store')) {
        this.replaceState(Object.assign(state, JSON.parse(localStorage.getItem('store'))))
      }
    },
    addToBascet(state, bascetItem) {
      var index = state.products.find(x => x.product.id === bascetItem.product.id)
      if (index != null) {
        index.count++
      } else {
        state.products.push(bascetItem)
      }
    },    
    incrementBascetItem(state, bascetItem) {
      bascetItem.count++
    },
    decrementBascetItem(Store, bascetItem) {
      if (bascetItem.count >= 0) { bascetItem.count-- }
    },
    removeFromBascet(state, item) {
      state.products = state.products.filter(x => x.product !== item.product)
    },
    addOrder(state, order) {
      state.orders.push(order)
    }
  },
  actions: {
  },
  modules: {
  }
})
