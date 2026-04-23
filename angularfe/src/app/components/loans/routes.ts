import { Routes } from '@angular/router';
import {LoansComponent} from './loans.component';
import {LoanscrudComponent} from './loanscrud.component';

export const routes: Routes = [
  {
    path: '',
    component: LoansComponent,
    data: {
      title: `Prestamos`
    },
    children: [
      {
        path: 'new',
        component: LoanscrudComponent,
        data: {
          title: 'Nuevo registro'
        }
      },
      {
        path: ':id',
        component: LoanscrudComponent,
        data: {
          title: 'Editar registro'
        }

      },

    ]

  }
];

