import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LibrosService {
  private http = inject(HttpClient);

  private strcontroller = 'book';

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

  getAutorData(id:any): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/${id}`;
    return this.http.get(url, { headers: this.getHeaders() });
  }


  createData(formData: any): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/create`;
    return this.http.post(url, formData);
  }

  updateData(id: string | number, formData: any): Observable<any> {
    const url = `${environment.restUrl}${environment.restApiUrl}/${this.strcontroller}/${id}`;
    return this.http.put(url, formData);
  }

}
