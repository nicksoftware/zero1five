import LoadableComponent from '../Loadable/index';

export const userRouter = [
    {
        path: '/user',
        name: 'user',
        title: 'User',
        component: LoadableComponent(() => import('../../components/Layout/UserLayout')),
        isLayout: true,
        showInMenu: false,
    },
    {
        path: '/user/login',
        name: 'login',
        title: 'LogIn',
        component: LoadableComponent(() => import('../../scenes/Login')),
        showInMenu: false,
    },
];

export const frontRouters = [
    {
        path: '/',
        exact: true,
        name: 'home',
        permission: '',
        title: 'Home',
        component: LoadableComponent(() => import('../../components/Layout/MainLayout')),
        isLayout: true,
        showInMenu: false,
    },
    {
        path: '/front',
        name: 'front',
        permission: '',
        title: 'Front',
        showInMenu: true,
        component: LoadableComponent(() => import('../../scenes/Home')),
    },
    {
        path: '/about',
        permission: '',
        title: 'About',
        name: 'about',
        showInMenu: true,
        component: LoadableComponent(() => import('../../scenes/About')),
    }
];

export const routers = [...userRouter, ...frontRouters];
