import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ElementUi from 'element-ui'
import locale from 'element-ui/lib/locale/lang/ru-RU'
import 'element-ui/lib/theme-chalk/index.css'

import '@/styles/index.scss' // global css

Vue.config.productionTip = false
Vue.use(ElementUi, { locale })

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
