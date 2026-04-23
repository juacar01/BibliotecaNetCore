import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Prestamos',
    url: '/loans',
    iconComponent: { name: 'cil-home' },
  },
  {
    title: true,
    name: 'Gestión'
  },
  {
    name: 'Autores',
    url: '/autores',
    iconComponent: { name: 'cil-child' }
  },
  {
    name: 'Libros',
    url: '/libros',
    iconComponent: { name: 'cil-grid' }
  },

];
