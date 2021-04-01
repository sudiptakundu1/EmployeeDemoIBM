import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Departments } from '../departments/departments.component';
import { env } from 'process';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<any> {
    return this.http.get<Departments[]>(environment.DEPARTMENT_API);
  }
  createDepartment(department: Departments) {
    return this.http.post(environment.DEPARTMENT_API, department);
  }

  updateDepartment(department: Departments) {
    return this.http.put(environment.DEPARTMENT_API, department);

  }

  deleteDepartment(departmentID) {
    return this.http.delete(environment.DEPARTMENT_API +"?DeptId="+departmentID);

  }
}
