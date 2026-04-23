import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./autores.component').then(m => m.AutoresComponent),
    data: {
      title: `Autores`
    }
  },
  {
    path: 'new',
    loadComponent: () => import('./autores-crud.component').then(m => m.AutoresCrudComponent),
    data: {
      title: `Nuevo Autor`
    }
  },
  {
    path: ':id',
    loadComponent: () => import('./autores-crud.component').then(m => m.AutoresCrudComponent),
    data: {
      title: `Editar Autor`
    }
  },
  {
    path: '**',
    redirectTo: '',
  }
];

