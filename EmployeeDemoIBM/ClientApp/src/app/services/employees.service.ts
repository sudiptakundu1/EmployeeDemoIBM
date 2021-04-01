import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeesFullView, Employees } from '../Employees/Employees.component';
import { env } from 'process';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<any> {
    return this.http.get<EmployeesFullView[]>(environment.EMPLOYEE_API);
  }
  createEmployee(employee: Employees) {
    return this.http.post(environment.EMPLOYEE_API, employee);
  }

  updateEmployee(employee: Employees) {
    return this.http.put(environment.EMPLOYEE_API, employee);

  }

  deleteEmployee(employeeID) {

    return this.http.delete(environment.EMPLOYEE_API + "?empId=" + employeeID);

  }}
