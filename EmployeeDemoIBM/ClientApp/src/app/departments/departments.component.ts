import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationBoxComponent } from '../popup/confirmation-box/confirmation-box.component';
import { PopUpComponent } from '../popup/pop-up/pop-up.component';
import { DepartmentsService } from '../services/departments.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html'
})
export class DepartmentsComponent implements OnInit {
  public departments: Departments[];
  displayedColumns: string[] = ['DeptId', 'DeptName', 'Action'];
  dataSource = new MatTableDataSource<Departments>();
  isLoading: boolean = true;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  //@ViewChild(MatSort, { static: true }) sort: MatSort;

  ngOnInit() {
    this.getDepartments();
  }

  constructor(private departmentService: DepartmentsService, public dialog: MatDialog, private toaster: ToastrService) {

  }

  getDepartments() {
    this.isLoading = true;

    this.departmentService.getDepartments().subscribe(response => {
      this.dataSource = new MatTableDataSource(response);
      this.dataSource.paginator = this.paginator;
      //this.dataSource.sort = this.sort;
      this.isLoading = false;

    }), err => {
      this.toaster.error('There is some problem while loading data. Please check after some time.', 'Failure');
    }
  }
  editDepartments(department) {

    this.departmentService.updateDepartment(department).subscribe(() => {
      this.toaster.success('Department updated successfully', 'Success');
      this.getDepartments();
    }
      , err => {
        this.toaster.error('Unable To Update Department. Please check after some time.', 'Failure');
      })
  }

  createDepartments(department) {
    this.departmentService.createDepartment(department).subscribe(() => {
      this.toaster.success('Department "' + department.deptName + '" is created successfully', 'Success');
      this.getDepartments();
    }
      , err => {
        this.toaster.error('Unable to create department. Please check after some time.', 'Failure');
      });
  }

  deleteDepartments(deptId) {
    this.departmentService.deleteDepartment(deptId).subscribe(() => {
      this.toaster.success('Deleted successfully', 'Success');
      this.getDepartments();
    }
      , err => {
        if (err.error.includes('REFERENCE constraint'))
          this.toaster.warning('Please delete employees associated with this Department before deleting.', 'Warning');
        else
          this.toaster.error('Unable to delete department. Please try after some time', 'Failure');
      });
  }

  openDialog(element, action): void {
    var id = 0;
    if (action == 'Update' || action == 'DELETE')
      id = element.deptId;

    const dialogRef = this.dialog.open(PopUpComponent, {
      width: '400px',
      data: { id: id, name: element.deptName, pageName: 'Department' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined && result != false) {
        let departmentObj: Departments = { deptId: result.id, deptName: result.name };
        if (action == 'Update')
          this.editDepartments(departmentObj);
        else if (action == 'Add')
          this.createDepartments(departmentObj);
      }
    });
  }

  openConfirmation(element, action) {
    const dialogRef = this.dialog.open(ConfirmationBoxComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteDepartments(element);
      }
    });
  }
}

export interface Departments {
  deptId: number;
  deptName: string;
}
