import { Routes } from '@angular/router';
import {LoansComponent} from './loans.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./loans.component').then(m => m.LoansComponent),
    data: {
      title: `Prestamos`
    }
  },
  {
    path: 'new',
    loadComponent: () => import('./loanscrud.component').then(m => m.LoanscrudComponent),
    data: {
      title: `nuevo préstamo`
    }
  },
  {
    path: '**',
    redirectTo: '',
  }
];

