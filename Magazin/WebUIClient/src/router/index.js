import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/pages/Home'),
    hidden: true,
    meta: { title: 'Магазин', icon: '' }
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('@/pages/About'),
    meta: { title: 'О нас', icon: '' }
  },
  {
    path: '/products',
    name: 'products',
    component: () => import('@/pages/products/index'),
    meta: { title: 'Продукция', icon: '' }
  },
  {
    path: '/news',
    name: 'news',
    component: () => import('@/pages/news/index'),
    meta: { title: 'Новости', icon: '' }
  }

]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
