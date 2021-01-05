import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError, retry } from 'rxjs/operators';
import {Claim} from '../models/claim.model';  
import {Constants} from 'src/app/shared/services/constants';
 
 
@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  constructor(private http:HttpClient) { }

 
  getAll(): Observable<Claim[]> {
    console.log("get all logdd");
    var lst = this.http.get<Claim[]>(Constants.apiRoot); 
    return lst;
  }

  get(id: any): Observable<any> {
    return this.http.get(`${Constants.apiRoot}/${id}`);
  }

  create(data: any): Observable<any> {
    return this.http.post(Constants.apiRoot, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put(`${Constants.apiRoot}/${id}`, data);
  }

  delete(id: any): Observable<any> {
    return this.http.delete(`${Constants.apiRoot}/${id}`);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(Constants.apiRoot);
  }

  search(keysearch: any): Observable<Claim[]> {
    return this.http.get<Claim[]>(`${Constants.apiRoot}?damaged_item=${keysearch}`);
  }
}
