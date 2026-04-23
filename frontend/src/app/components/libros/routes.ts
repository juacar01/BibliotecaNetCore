import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./libros.component').then(m => m.LibrosComponent),
    data: {
      title: `Libros`
    }
  },
  {
    path: 'new',
    loadComponent: () => import('./libros-crud.component').then(m => m.LibrosCrudComponent),
    data: {
      title: `Nuevo Libro`
    }
  },
  {
    path: ':id',
    loadComponent: () => import('./libros-crud.component').then(m => m.LibrosCrudComponent),
    data: {
      title: `Editar Libro`
    }
  },
  {
    path: '**',
    redirectTo: '',
  }
];

