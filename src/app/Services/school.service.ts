import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { School } from '../Models/school';
import { Observable } from 'rxjs';
import { Grade } from '../Models/grade';
import { Class } from '../Models/class';
import { Student } from '../Models/student';
import { SchoolHeadrs } from '../Models/school-headrs';
import { Year } from '../Models/year';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {

  private apiUrl = 'https://localhost:7148/api/School';

  constructor(private http: HttpClient) {}

    getAll() {
      return this.http.get<School[]>(this.apiUrl);
    }
    getYears(schoolId: number): Observable<Year[]> {
      return this.http.get<Year[]>(`${this.apiUrl}/years?schoolId=${schoolId}`);
    }

    getGrades(schoolId: number , Year:number): Observable<Grade[]> {
      return this.http.get<Grade[]>(`${this.apiUrl}/grades?schoolId=${schoolId}&year=${Year}`);
    }

    getClasses(schoolId: number , Year:number , Grade:number): Observable<Class[]> {
      return this.http.get<Class[]>(`${this.apiUrl}/classes?schoolId=${schoolId}&year=${Year}&gradeId=${Grade}`);
    }

    getReport(schoolID: number, year: number, gradeID: number, classID: number): Observable<Student[]> {
      return this.http.get<Student[]>(`${this.apiUrl}/detailed-reports?schoolId=${schoolID}&year=${year}&gradeId=${gradeID}&classID=${classID}`, {
        params: {
          schoolID: schoolID.toString(),
          year: year.toString(),
          gradeID: gradeID.toString(),
          classID: classID.toString()
        }
      });

  }

  getSchoolHeadersById(id: number): Observable<SchoolHeadrs> {
    return this.http.get<SchoolHeadrs>(`${this.apiUrl}/${id}`);
  }








}
