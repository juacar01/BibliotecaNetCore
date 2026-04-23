import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoansService {
  private http = inject(HttpClient);

  private strcontroller = 'loan';

  private getHeaders() {
    return new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8;',
      'No-Auth': 'False'
    });
  }

  getTableData(criteria: string = ''): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/pagination` + criteria;
    return this.http.get(url, { headers: this.getHeaders() });
  }

  createData(formData: any): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/create`;
    return this.http.post(url, formData, { headers: this.getHeaders() });
  }

  returnLoan(id: string | number,formData: any): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/${id}/registerreturn`;
    return this.http.put(url, formData, { headers: this.getHeaders() });
  }
}
