import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Class } from '../Models/class';

@Injectable({
  providedIn: 'root'
})
export class ClassServiceService {
private apiUrl = 'https://localhost:7148/api/Class';

  constructor(private http: HttpClient) {}

  getClassById(id: number): Observable<Class> {
    return this.http.get<Class>(`${this.apiUrl}/${id}`);
  }

}
