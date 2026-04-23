import {Component, inject, OnInit, signal} from '@angular/core';
import {RouterLink} from '@angular/router';
import {LibrosService} from '../../services/libros.service';
import {Autor} from '../autores/autores.component';

export interface Libro {
  id: number;
  title: string;
  numberOfPages: number;
  genre: string;
  publishedDate: string;
  coverImagePath: string | null;
  isDeleted: boolean;
  author: Autor | null;
  createdAt: string;
  updatedAt: string;
}

export interface AutoresResponse {
  count: number;
  pageIndex: number;
  pageSize: number;
  data: Libro[];
  pageCount: number;
  resultByPage: number;
}


@Component({
  selector: 'app-libros',
  imports: [
    RouterLink
  ],
  templateUrl: './libros.component.html',
  styleUrl: './libros.component.scss',
})
export class LibrosComponent implements OnInit {
  private _librosService = inject(LibrosService);

  // Usamos un signal para la tabla de datos, así la UI reacciona automáticamente
  table = signal<AutoresResponse | null>(null);
  tableData = signal<Libro[]>([]);
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

    this._librosService.getTableData(criterio).subscribe({
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
