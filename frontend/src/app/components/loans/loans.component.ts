import {AfterViewInit, Component, inject, OnDestroy, OnInit, signal, ViewChild} from '@angular/core';
import {Subject} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {RouterLink} from '@angular/router';
import {AutoresService} from '../../services/autores.service';
import {Libro} from '../libros/libros.component';
import {LoansService} from '../../services/loans.service';
import {ToastrService} from 'ngx-toastr';

export interface Loan {
  id: number;
  bookId: number;
  borrowerName: string;
  loanDate: string;
  dueDate: string;
  returnDate: string | null;
  book: Libro;
  createdAt: string;
  updatedAt: string;
  status: number;
  statusLabel: string;
}

export interface AutoresResponse {
  count: number;
  pageIndex: number;
  pageSize: number;
  data: Loan[];
  pageCount: number;
  resultByPage: number;
}

@Component({
  selector: 'app-loans',
  imports: [
    RouterLink
  ],
  templateUrl: './loans.component.html',
  styleUrl: './loans.component.scss',
})
export class LoansComponent implements OnInit {
  private _loansService = inject(LoansService);
  private _toastr = inject(ToastrService);

  table = signal<AutoresResponse | null>(null);
  tableData = signal<Loan[]>([]);
  currentPage = signal<number>(1);

  retornar(id: string) {

    this._loansService.returnLoan(id, {}).subscribe({
      next: (data: any) => {
        this._toastr.success('Préstamo devuelto correctamente');
        this.loadData();
      },
      error: (err) => {
        console.error('Error al registrar revolucion:', err);
        this._toastr.error('Error al registrar revolucion: ' + err.error.message);
      }
    });
  };


  loadData() {
    var criterios: Array<string> = [];
    if (this.currentPage() > 1) {
      criterios.push('pageindex=' + this.currentPage());
    }

    var criterio = criterios.filter(item => item !== '').join('&')
    if (criterio.length > 0) {
      criterio = '?' + criterio;
    }

    this._loansService.getTableData(criterio).subscribe({
      next: (data: any) => {
        this.table.set(data);
        this.tableData.set(data.data);
        console.log('Datos cargados:', this.tableData());
      },
      error: (error) => {
        console.error('Error al cargar autores:', error);
      }
    });
  }


  changePage(page: number) {
    // Evitamos que se salga de los límites
    if (page < 1 || page > (this.table()?.pageCount || 1)) return;

    this.currentPage.set(page);
    this.loadData();
  }

  ngOnInit(): void {
    this.loadData();
  }

}
