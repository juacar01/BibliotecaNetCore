import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LoansService {

  strcontroller = 'Loan';

  constructor(private http: HttpClient) {
  }


  getListData() {
    const reqHeader = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8;', 'No-Auth': 'False'});
    return this.http.get(environment.restApiUrl + '/' + this.strcontroller + '/pagination', {headers: reqHeader});
  }





}
