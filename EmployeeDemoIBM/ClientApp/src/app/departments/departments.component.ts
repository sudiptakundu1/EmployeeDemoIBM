import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmationBoxComponent } from '../popup/confirmation-box/confirmation-box.component';
import { PopUpComponent } from '../popup/pop-up/pop-up.component';
import { DepartmentsService } from '../services/departments.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html'
})
export class DepartmentsComponent implements OnInit {
  public departments: Departments[];
  displayedColumns: string[] = ['DeptId', 'DeptName','Action'];
  dataSource = new MatTableDataSource<Departments>();
  errorMsg: string = '';
  successMsg: string = '';
  isLoading: boolean=true;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  //@ViewChild(MatSort, { static: true }) sort: MatSort;

  ngOnInit() {
    this.getDepartments();
  }

  constructor(private departmentService: DepartmentsService, public dialog: MatDialog) {
      
  }
  
  getDepartments() {
    this.isLoading = true;

    this.departmentService.getDepartments().subscribe(response => {

      this.dataSource = new MatTableDataSource(response);
      this.dataSource.paginator = this.paginator;
      //this.dataSource.sort = this.sort;
      this.isLoading = false;

    }), err => {
      this.errorMsg = err;
    }
  }
  editDepartments(department) {

    this.departmentService.updateDepartment(department).subscribe(() => {
      this.successMsg = 'Department Updated Successfully' ;
      this.getDepartments();
    }
    , err => {
      this.errorMsg = 'Unable To Update Department. Please check after some time.';
    })
  }

  createDepartments(department) {

    this.departmentService.createDepartment(department).subscribe(() => {
      this.successMsg = 'Department "' + department.deptName + '" Created Successfully';
      this.getDepartments();
    }
      , err => {
        this.errorMsg = 'Unable To Create Department. Please check after some time.';
      });
  }

  deleteDepartments(deptId) {

    this.departmentService.deleteDepartment(deptId).subscribe(() => {
      this.successMsg = 'Deleted Successfully';
      this.getDepartments();

    }
      , err => {
        console.log('hiiii', err.error);
        if (err.error.includes('REFERENCE constraint'))
          this.errorMsg = 'Please delete employees associated with Department before deleting.'
        else
        this.errorMsg = 'Unable To Delete Department. Please try after some time';
      });
  }

  openDialog(element, action): void {
    this.resetMsg();
    var id = 0;
    if(action=='Update'||action=='DELETE')
      id = element.deptId;


    const dialogRef = this.dialog.open(PopUpComponent, {
      width: '400px',
      data: { id: id, name: element.deptName, pageName: 'Department'}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed', result);
      if (result != undefined && result != false) {
        let departmentObj: Departments = { deptId: result.id, deptName: result.name };
        console.log('departmentObj', departmentObj);
        if (action == 'Update')
          this.editDepartments(departmentObj);
        else if (action == 'Add')
          this.createDepartments(departmentObj);
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
        this.deleteDepartments(element);
      }
    });
  }
  resetMsg() {
    this.errorMsg = '';
    this.successMsg = '';
  }
}



export interface Departments {
  deptId: number;
  deptName: string;
}
