import {Component, inject, OnInit, signal} from '@angular/core';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {AutoresService} from '../../services/autores.service';
import {ToastrService} from 'ngx-toastr';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

@Component({
  selector: 'app-autores-crud',
  imports: [
    RouterLink,
    ReactiveFormsModule,
  ],
  templateUrl: './autores-crud.component.html',
  styleUrl: './autores-crud.component.scss',
})
export class AutoresCrudComponent implements OnInit {
  private _autoresService    = inject(AutoresService);
  private _route    = inject(ActivatedRoute);
  private _router    = inject(Router);
  private _toastr    = inject(ToastrService);
  private fb = inject(FormBuilder);

  private entityId = signal<any>(null);
  private isEdition = signal<boolean>(false);
  private returnUrl = signal<string>("");

  autorData = signal<any>(null);
  isPending = signal<boolean>(false);


  autorForm: FormGroup = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    lastName: ['', [Validators.required]],
    biography: [null],
    birthDate: [null],
    country: [null]
  });

  get f() {
    return this.autorForm.controls;
  }

  loadData() {
    console.log(this.entityId());
    this._autoresService.getAutorData(this.entityId()).subscribe({
      next: (data: any) => {
        this.autorData.set(data);
        this.autorForm.get('name')?.setValue(data.name, { emitEvent: false });
        this.autorForm.get('lastName')?.setValue(data.lastName, { emitEvent: false });
        this.autorForm.get('country')?.setValue(data.country, { emitEvent: false });
        this.autorForm.get('biography')?.setValue(data.biography, { emitEvent: false });
        this.autorForm.get('birthDate')?.setValue( new Date(data.birthDate).toISOString().split('T')[0], { emitEvent: false });

        console.log('Datos cargados:', data);
      },
      error: (error:any) => {
        console.error('Error al cargar autores:', error);
      }
    });
  }

  save() {
    if (this.autorForm.invalid) {
      this.autorForm.markAllAsTouched();
      return;
    }

    this.isPending.set(true);
    const formData = this.autorForm.value;

    const request$ = this.entityId()
      ? this._autoresService.updateData(this.entityId()!, formData)
      : this._autoresService.createData(formData);

    request$.subscribe({
      next: () => {
        this._toastr.success('Autor guardado correctamente');
        this._router.navigate(['/autores']);
      },
      error: (err) => {
        this._toastr.error('Error al guardar: ' + err.error.message);
        this.isPending.set(false);
      }
    });
  }

  ngOnInit(): void {
    const routeParams = this._route.snapshot.params;
    this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';

    // Acceso seguro a parámetros en Angular moderno
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
