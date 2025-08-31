import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CbcTest } from '../models/cbc-test.model';

@Injectable({
  providedIn: 'root'
})
export class CbcService {
  private apiUrl = 'https://localhost:5001/api/cbc'; // change to your API

  constructor(private http: HttpClient) {}

  getAll(): Observable<CbcTest[]> {
    return this.http.get<CbcTest[]>(this.apiUrl);
  }

  getById(id: number): Observable<CbcTest> {
    return this.http.get<CbcTest>(`${this.apiUrl}/${id}`);
  }

  create(cbc: CbcTest): Observable<CbcTest> {
    return this.http.post<CbcTest>(this.apiUrl, cbc);
  }

  update(id: number, cbc: CbcTest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, cbc);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
