import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from "@angular/router";
import { AutoresService } from '../../services/autores.service';

export interface Autor {
  id: number;
  name: string;
  lastName: string;
  biography: string | null;
  birthDate: string | null;
  country: string | null;
  isDeleted: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface AutoresResponse {
  count: number;
  pageIndex: number;
  pageSize: number;
  data: Autor[];
  pageCount: number;
  resultByPage: number;
}

@Component({
  selector: 'app-autores',
  imports: [
    RouterLink
  ],
  templateUrl: './autores.component.html',
  styleUrl: './autores.component.scss',
})
export class AutoresComponent implements OnInit {
  private _autoresService = inject(AutoresService);

  // Usamos un signal para la tabla de datos, así la UI reacciona automáticamente
  table = signal<AutoresResponse | null>(null);
  tableData = signal<Autor[]>([]);
  currentPage = signal<number>(1);


  loadData() {
    var criterios: Array<string> = [];
    if (this.currentPage() > 1) {
      criterios.push('pageindex=' + this.currentPage());
    }

    var criterio = criterios.filter(item => item !== '').join('&')
    if (criterio.length > 0) {
      criterio = '?' + criterio;
    }

    this._autoresService.getTableData(criterio).subscribe({
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
