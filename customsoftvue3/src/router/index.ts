
import { createRouter, createWebHistory } from 'vue-router';
import ApplicationConfiguration from '../components/Autentication/Login.vue'; 
import PaqueteHistory from '../components/Packages/PackageTable.vue';
import Directorio from '../components/Directorios/Directorio.vue';

const routes = [
  {
    path: '/application-configuration',
    name: 'ApplicationConfiguration',
    component: ApplicationConfiguration
  },
  {
    path: '/paqueteHistory',
    name: 'paqueteHistory',
    component: PaqueteHistory,
    meta: { requiresAuth: true }
  },
  {
    path: '/directorio',
    name: 'directorio',
    component: Directorio,
    meta: { requiresAuth: true }
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem('accessToken');
  
  if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated) {
    next({ name: 'ApplicationConfiguration' });
  } else {
    next();
  }
});

export default router;
