import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ToastrService } from 'ngx-toastr';
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
  displayedColumns: string[] = ['Id', 'Name', 'DeptName', 'Action'];
  dataSource = new MatTableDataSource<EmployeesFullView>();
  isLoading: boolean = true;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  //@ViewChild(MatSort, { static: true }) sort: MatSort;

  ngOnInit() {
    this.getEmployees();
  }

  constructor(private employeeService: EmployeesService, public dialog: MatDialog, private toaster: ToastrService) {

  }

  getEmployees() {
    this.isLoading = true;
    this.employeeService.getEmployees().subscribe(response => {

      this.dataSource = new MatTableDataSource(response);
      this.dataSource.paginator = this.paginator;
      //this.dataSource.sort = this.sort;
      this.isLoading = false;

    }), err => {
      this.toaster.error('There is some problem while loading data. Please check after some time.', 'Failure');
    }
  }
  editEmployees(employee) {

    this.employeeService.updateEmployee(employee).subscribe(() => {
      this.toaster.success('Employee updated successfully', 'Success');
      this.getEmployees();
    }
    ), err => {
      this.toaster.error('Unable to update employee. Please check after some time.', 'Failure');
    }
  }

  createEmployees(employee) {

    this.employeeService.createEmployee(employee).subscribe(() => {
      this.toaster.success('Employee "' + employee.name+ '" created successfully', 'Success');
      this.getEmployees();
    }
    ), err => {
      this.toaster.error('Unable to create employee. Please check after some time.', 'Failure');
    }
  }

  deleteEmployees(deptId) {

    this.employeeService.deleteEmployee(deptId).subscribe(() => {
      this.toaster.success('Deleted successfully', 'Success');
      this.getEmployees();

    }
    ), err => {
      this.toaster.error('Unable To Delete Employee. Please check after some time.', 'Failure');
    }
  }

  openDialog(element, action): void {
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
      if (result != undefined && result != false) {

        let employeeObj: Employees = { id: result.id, deptId: result.deptId, name: result.name };
        if (action == 'Update')
          this.editEmployees(employeeObj);
        else if (action == 'Add')
          this.createEmployees(employeeObj);
      }
    });
  }

  openConfirmation(element, action) {
    const dialogRef = this.dialog.open(ConfirmationBoxComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
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


