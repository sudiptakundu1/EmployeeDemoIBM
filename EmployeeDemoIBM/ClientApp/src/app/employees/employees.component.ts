import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmationBoxComponent } from '../popup/confirmation-box/confirmation-box.component';
import { PopUpComponent } from '../popup/pop-up/pop-up.component';
import { Departments } from '../departments/departments.component';
import { EmployeesService } from '../services/employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent {
  public employees: EmployeesFullView[];
  public departments: Departments[];
  displayedColumns: string[] = ['Id', 'Name', 'DeptName','Action'];
  dataSource = new MatTableDataSource<EmployeesFullView>();
  errorMsg: string = '';
  successMsg: string = '';
  isLoading: boolean = true;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  //@ViewChild(MatSort, { static: true }) sort: MatSort;

  ngOnInit() {
    this.getEmployees();
  }

  constructor(private employeeService: EmployeesService, public dialog: MatDialog) {

  }

  resetMsg() {
    this.errorMsg = '';
    this.successMsg = '';
  }
  getEmployees() {
    this.isLoading = true;
    this.resetMsg();
    this.employeeService.getEmployees().subscribe(response => {

      this.dataSource = new MatTableDataSource(response);
      this.dataSource.paginator = this.paginator;
      //this.dataSource.sort = this.sort;
      this.isLoading = false;

    }), err => {
      this.errorMsg = err;
    }
  }
  editEmployees(employee) {

    this.employeeService.updateEmployee(employee).subscribe(() => {
      this.successMsg = 'Updated Successfully';
      this.getEmployees();
    }
    ), err => {
      this.errorMsg = 'Unable To Update';
    }
  }

  createEmployees(employee) {

    this.employeeService.createEmployee(employee).subscribe(() => {
      this.successMsg = 'Employee "'+employee.deptName+'" Created Successfully';
      this.getEmployees();
    }
    ), err => {
      this.errorMsg = 'Unable To Create Employee. Please check after some time.';
    }
  }

  deleteEmployees(deptId) {

    this.employeeService.deleteEmployee(deptId).subscribe(() => {
      this.successMsg = 'Employee Deleted Successfully';
      this.getEmployees();

    }
    ), err => {
      console.log('hiiii', err);
      this.errorMsg = 'Unable To Delete Employee';
    }
  }

  openDialog(element, action): void {
    this.resetMsg();
    var id = 0;
    var deptId = undefined;
    if (action == 'Update' || action == 'DELETE') {
      id = element.id;
      deptId = element.dept.deptId;
    }


    const dialogRef = this.dialog.open(PopUpComponent, {
      width: '400px',
      data: { id: id, name: element.name, deptId: deptId, pageName: 'Employee' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed', result);
      if (result != undefined && result != false) {

        let employeeObj: Employees = { id: result.id, deptId: result.deptId, name: result.name };
        console.log('employeeObj', employeeObj);
        if (action == 'Update')
          this.editEmployees(employeeObj);
        else if (action == 'Add')
          this.createEmployees(employeeObj);
      }
    });
  }

  openConfirmation(element, action) {
    this.resetMsg();
    const dialogRef = this.dialog.open(ConfirmationBoxComponent, {
      width: '400px',

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('The dialog was closed', result);
        this.deleteEmployees(element);
      }

    });
  }
}

export interface EmployeesFullView {
  id: number;
  name: string;
  dept: Departments;
}

export interface Employees {
  id: number;
  name: string;
  deptId: number;
}


