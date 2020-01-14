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
    path: '/catalog/',
    name: 'catalog',
    component: () => import('@/pages/products/index'),
    props: { name: 'categoryId' },
    meta: { title: 'Каталог', icon: '' }
  },
  {
    path: '/brands',
    name: 'brands',
    component: () => import('@/pages/brands/index'),
    meta: { title: 'Бренды' }
  },
  {
    path: '/news',
    name: 'news',
    component: () => import('@/pages/news/index'),
    meta: { title: 'Новости', icon: '' }
  },
  {
    path: '/bascet',
    name: 'bascet',
    component: () => import('@/pages/bascet/index'),
    meta: { title: 'Корзина' }
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
