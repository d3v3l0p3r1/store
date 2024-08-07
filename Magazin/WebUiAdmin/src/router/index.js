import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    noCache: true                if set true, the page will no be cached(default is false)
    affix: true                  if set true, the tag will affix in the tags-view
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path*',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: 'Dashboard',
        meta: { title: 'Dashboard', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [
      {
        path: 'index',
        component: () => import('@/views/profile/index'),
        name: 'Profile',
        meta: { title: 'Profile', icon: 'user', noCache: true }
      }
    ]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  {
    path: '/documents',
    component: Layout,
    roles: ['admin'],
    meta: {
      title: 'Документы',
      icon: 'documentation'
    },
    children: [
      {
        path: 'incomingDocuments',
        component: () => import('@/views/documents/incoming/list'),
        name: 'IncomingDocuments',
        meta: { title: 'Документы прихода', icon: 'documents-plus-symbol-for-interface-with-rounded-square-shape' }
      },
      {
        path: 'outcomingDocuments',
        component: () => import('@/views/documents/outcoming/list'),
        name: 'OutcomingDocuments',
        meta: { title: 'Документы расхода', icon: 'document-with-minus-sign' }
      }
    ]
  },
  {
    path: '/categories',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/categories/list'),
        name: 'CategoryGrid',
        meta: { title: 'Категории продуктов', icon: 'theme' }
      }
    ]
  },
  {
    path: '/brands',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/brands/list'),
        name: 'BrandsGrid',
        meta: { title: 'Список брендов', icon: 'planet-earth' }
      },
      {
        path: 'edit/:id',
        component: () => import('@/views/brands/edit'),
        name: 'EditBrand',
        meta: { title: 'Бренд' },
        hidden: true
      }
    ]
  },
  {
    path: '/kinds',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/kinds/list'),
        name: 'KindGrid',
        meta: { title: 'Виды продуктов', icon: 'theme' }
      }
    ]
  },
  {
    path: '/products',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/products/index'),
        name: 'ProductGrid',
        meta: { title: 'Продукты', icon: 'theme' }
      }
    ]
  },
  {
    path: '/balance',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/balanceRegister/list'),
        name: 'BalanceGrid',
        meta: { title: 'Остатки', icon: 'calculator' }
      }
    ]
  },
  {
    path: '/moves',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/balanceRegister/moves'),
        name: 'MovesGrid',
        meta: { title: 'Движения', icon: 'money-eyes-on-square-face' }
      }
    ]
  },
  {
    path: '/productPrice',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/productPrice/list'),
        name: 'ProductPriceGrid',
        meta: { title: 'Цены', icon: 'money-eyes-on-square-face' }
      }
    ]
  },
  {
    path: '/users',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/users/list'),
        name: 'UsersGrid',
        meta: { title: 'Пользователи', icon: 'black-male-user-symbol' }
      }
    ]
  },
  {
    path: '/companies',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/contractors/list'),
        name: 'CompaniesGrid',
        meta: { title: 'Контрагенты', icon: 'planet-earth' }
      }
    ]
  },
  {
    path: '/carousel',
    roles: ['admin'],
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/carousel/list'),
        name: 'CarouselGrid',
        meta: { title: 'Карусель', icon: 'planet-earth' }
      },
      {
        path: 'create',
        component: () => import('@/views/carousel/create'),
        name: 'CreateCarousel',
        meta: { title: 'Элемент карусели' },
        hidden: true
      },
      {
        path: 'edit/:id',
        component: () => import('@/views/carousel/create'),
        name: 'EditCarousel',
        meta: { title: 'Элемент карусели' },
        hidden: true
      }
    ]
  },
  {
    path: '/icon',
    component: Layout,
    children: [
      {
        path: 'index',
        component: () => import('@/views/icons/index'),
        name: 'Icons',
        meta: { title: 'Icons', icon: 'icon', noCache: true }
      }
    ]
  },

  // 404 page must be placed at the end !!!
  { path: '*', redirect: '/404', hidden: true }
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
