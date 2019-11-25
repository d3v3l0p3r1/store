import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import ElementUi from 'element-ui'
import locale from 'element-ui/lib/locale/lang/ru-RU'

Vue.config.productionTip = false
Vue.use(ElementUi, { locale })

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
