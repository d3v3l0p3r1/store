import Vue from 'vue'
import Vuetify from 'vuetify/lib'

Vue.use(Vuetify)

const opts = {
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
