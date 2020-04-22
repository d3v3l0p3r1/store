import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import '@/styles/index.scss' // global css
import vuetify from './plugins/vuetify'
import VueAwesomeSwiper from 'vue-awesome-swiper'
import 'swiper/css/swiper.min.css'
import VueNumberInput from '@chenfengyuan/vue-number-input'

Vue.use(VueAwesomeSwiper)
Vue.config.productionTip = false

Vue.use(VueNumberInput)

store.subscribe((muration, state) => {
  localStorage.setItem('store', JSON.stringify(state))
})

new Vue({
  router,
  store,
  beforeCreate() {
		this.$store.commit('initialiseStore')
	},
  vuetify,
  render: h => h(App)
}).$mount('#app')
