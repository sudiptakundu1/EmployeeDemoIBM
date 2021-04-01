import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Departments } from '../../departments/departments.component';
import { DepartmentsService } from '../../services/departments.service';

@Component({
  selector: 'app-pop-up',
  templateUrl: './pop-up.component.html',
  styleUrls: ['./pop-up.component.css']
})
export class PopUpComponent implements OnInit {
  name = new FormControl('', [Validators.required]);
  allowSave:boolean= false;
  deptList: Departments[]=[];
  selectedDept = '';
  constructor(
    public dialogRef: MatDialogRef<PopUpComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private departmentService: DepartmentsService) {
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }
  toggleSave() {
    if (this.data.name.trim() != '' && this.data.name != undefined) {
      this.data.name = this.data.name.trim();
      this.allowSave = true;
      if (this.data.pageName == 'Employee' && (this.data.deptId == '' || this.data.deptId == undefined))
        this.allowSave = false;

    }
  }
  onYesClick(): void {
    
    if (this.allowSave)
      this.dialogRef.close(this.data);
  }
  ngOnInit() {
    if (this.data.pageName == 'Employee') {
      this.getdepartmentDropdown();
      this.selectedDept = this.data.deptId != undefined ? this.data.deptId:'';

    }
  }

  getdepartmentDropdown() {
    
      this.departmentService.getDepartments().subscribe(response => {

        this.deptList = response;
        console.log('  this.deptList', this.deptList)

      }), err => {
      }
    }
  }


