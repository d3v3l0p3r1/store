import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import '@/styles/index.scss' // global css
import Vuetify from 'vuetify/lib'
import vuetify from './plugins/vuetify'

Vue.config.productionTip = false
Vue.use(Vuetify)

new Vue({
  el: '#app',
  router,
  store,
  vuetify,
  render: h => h(App)
})
