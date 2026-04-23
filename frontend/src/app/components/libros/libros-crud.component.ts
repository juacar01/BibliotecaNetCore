import {Component, inject, OnInit, signal} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {AutoresService} from '../../services/autores.service';
import {ToastrService} from 'ngx-toastr';
import {LibrosService} from '../../services/libros.service';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-libros-crud',
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './libros-crud.component.html',
  styleUrl: './libros-crud.component.scss',
})
export class LibrosCrudComponent implements OnInit {
  private _librosService    = inject(LibrosService);
  private _route    = inject(ActivatedRoute);
  private _router    = inject(Router);
  private _toastr    = inject(ToastrService);
  private fb = inject(FormBuilder);

  private entityId = signal<any>(null);
  private isEdition = signal<boolean>(false);
  private returnUrl = signal<string>("");

  libroData = signal<any>(null);
  isPending = signal<boolean>(false);

  private selectedFile: File | null = null;
  imagePreview = signal<string | null>(null);


  libroForm: FormGroup = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(3)]],
    genre: ['', [Validators.required]],
    numberOfPages: ['', [Validators.required]],
    publishedDate: ['', [Validators.required]],
    author: [null],
    authorId: ['', [Validators.required]],
    image: [null]

  });

  // Método para capturar el archivo cuando el usuario lo selecciona
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;

      // Crear previsualización
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview.set(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }
  loadData() {
    console.log(this.entityId());
    this._librosService.getAutorData(this.entityId()).subscribe({
      next: (data: any) => {
        this.libroData.set(data);
        this.libroForm.get('title')?.setValue(data.title, { emitEvent: false });
        this.libroForm.get('lastName')?.setValue(data.lastName, { emitEvent: false });
        this.libroForm.get('genre')?.setValue(data.genre, { emitEvent: false });
        this.libroForm.get('numberOfPages')?.setValue(data.numberOfPages, { emitEvent: false });
        this.libroForm.get('authorId')?.setValue(data.authorId, { emitEvent: false });
        this.libroForm.get('publishedDate')?.setValue( new Date(data.publishedDate).toISOString().split('T')[0], { emitEvent: false });
        this.imagePreview.set(environment.restUrl+'/'+data.coverImagePath);
        console.log('Datos cargados:', data);
      },
      error: (error:any) => {
        console.error('Error al cargar autores:', error);
      }
    });
  }

  save() {
    if (this.libroForm.invalid) {
      this.libroForm.markAllAsTouched();
      return;
    }
    this.isPending.set(true);
    const formData = new FormData();
    Object.keys(this.libroForm.value).forEach(key => {
      const value = this.libroForm.get(key)?.value;
      formData.append(key, value);
    });
    if (this.selectedFile) {
      formData.append('imagen', this.selectedFile, this.selectedFile.name);
    }

    const request$ = this.entityId()
      ? this._librosService.updateData(this.entityId()!, formData)
      : this._librosService.createData(formData);

    request$.subscribe({
      next: () => {
        this._toastr.success('Libro guardado correctamente');
        this._router.navigate(['/libros']);
      },
      error: (err) => {
        console.log(err);
        this._toastr.error('Error al guardar: ' + err.error.message);
        this.isPending.set(false);
      }
    });
  }

  ngOnInit(): void {
    const routeParams = this._route.snapshot.params;
    this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';

    const paramId = routeParams['id'];

    if (paramId === undefined || paramId === 'new') {
      this.entityId.set(null);
      this.isEdition.set(false);
    } else {
      this.entityId.set(paramId);
      this.isEdition.set(true);
      this.loadData();
    }

  }


}
