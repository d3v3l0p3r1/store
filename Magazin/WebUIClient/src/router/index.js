import Vue from 'vue'
import VueRouter from 'vue-router'
import Layout from '@/layout'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/',
        component: () => import('@/pages/Home'),
        meta: { title: 'Магазин', icon: '' }
      }
    ]

  },
  {
    path: '/about',
    name: 'about',
    component: Layout,
    children: [
      {
        path: '/about',
        component: () => import('@/pages/About'),
        meta: { title: 'О нас', icon: '' }
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
