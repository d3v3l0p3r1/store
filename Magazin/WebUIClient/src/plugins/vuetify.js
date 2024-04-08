import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import '@fortawesome/fontawesome-free/css/all.css' // Ensure you are using css-loader

Vue.use(Vuetify)

const opts = {
    icons: {
        iconfont: 'fa'
    },
    theme: {
        light: true,
        themes: {
            light: {
                background: '#ffffff'
            }
        }
    }
}

export default new Vuetify(opts)
