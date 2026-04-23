import { Component, inject, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import { LoansService } from '../../services/loans.service';

@Component({
  selector: 'app-loanscrud',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './loanscrud.component.html',
  styleUrl: './loanscrud.component.scss',
})
export class LoanscrudComponent {
  private _loansService    = inject(LoansService);
  private _route    = inject(ActivatedRoute);
  private _router    = inject(Router);
  private _toastr    = inject(ToastrService);
  private fb = inject(FormBuilder);

  private entityId = signal<any>(null);
  private isEdition = signal<boolean>(false);
  private returnUrl = signal<string>("");

  autorData = signal<any>(null);
  isPending = signal<boolean>(false);

  private dateNotBeforeToday(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) return null;

      const today = new Date();
      today.setHours(0, 0, 0, 0);
      const inputDate = new Date(control.value.replace(/-/g, '/'));
      inputDate.setHours(0, 0, 0, 0);
      console.log(inputDate);

      return inputDate < today ? { beforeToday: true } : null;
    };
  }
  private dateRangeValidator: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    const loanDate = group.get('loanDate')?.value;
    const dueDate = group.get('dueDate')?.value;

    if (!loanDate || !dueDate) return null;

    return new Date(dueDate) < new Date(loanDate)
      ? { rangeInvalid: true }
      : null;
  };

  loanForm: FormGroup = this.fb.group({
    borrowerName: ['', [Validators.required, Validators.minLength(3)]],
    bookId: ['', [Validators.required]],
    loanDate:  ['', [Validators.required, this.dateNotBeforeToday()]],
    dueDate:  ['', [Validators.required]],
  }, { validators: this.dateRangeValidator });

  get f() {
    return this.loanForm.controls;
  }

  save() {
    if (this.loanForm.invalid) {
      this.loanForm.markAllAsTouched();
      return;
    }

    this.isPending.set(true);
    const formData = this.loanForm.value;

    const request$ = this._loansService.createData(formData);

    request$.subscribe({
      next: () => {
        this._toastr.success('Préstamo guardado correctamente');
        this._router.navigate(['/loans']);
      },
      error: (err) => {
        this._toastr.error('Error al guardar: ' + err.error.message);
        this.isPending.set(false);
      }
    });
  }

}
