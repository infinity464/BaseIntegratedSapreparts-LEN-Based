import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { IssueRegister } from '../../models/IssueRegister';
import { IssueRegisterService } from '../../service/IssueRegister.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-equipementnonoplstate-list',
  templateUrl: './equipementnonoplstate-list.component.html',
  styleUrls: ['./equipementnonoplstate-list.component.sass']
})
export class EquipementnonoplStateListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: IssueRegister[] = [];
  IssueRegisterForm: FormGroup;
  selectedDepartmentNames: SelectedModel[];
  isLoading = false;
  unitId:any;
  departmentNameId:any;
  IssueList:any;
  traineeId:any;
  role:any;
  branchId:any;
  userRole = Role;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'itemName','toolsLocation','status','reason','actions'];
  dataSource: MatTableDataSource<IssueRegister> = new MatTableDataSource();

  selection = new SelectionModel<IssueRegister>(true, []);
  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private fb: FormBuilder,private IssueRegisterService: IssueRegisterService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    
    this.intitializeForm();
    
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.IssueRegisterForm.get('departmentNameId').setValue(this.branchId);
      this.IssueRegisterService.findBaseSchoolNameId(this.branchId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.IssueRegisterForm.get('unitId').setValue(this.unitId);
        console.log("UnitID");
        this.getEquipNonOPLList();
      console.log(this.unitId);
      })
      
      this.getSelectedDepartmentName(this.branchId);
      
    }
    if(this.role == this.userRole.Admin){
      console.log("admin block")
      this.IssueRegisterForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
      
    }
    
  }
  intitializeForm() {
    this.IssueRegisterForm = this.fb.group({
      unitId:[],
      departmentNameId:[],
    })
  }
  getSelectedDepartmentName(fourthLevel){
    this.IssueRegisterService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
 
  getEquipNonOPLList() {
    var unitId = this.IssueRegisterForm.value['unitId'];
    var departmentNameId = this.IssueRegisterForm.value['departmentNameId'];
    console.log(unitId,departmentNameId)
    this.isLoading = true;
    this.IssueRegisterService.getEquipNonOPLList(unitId,departmentNameId).subscribe(response => {
      
      this.IssueList = response.filter(x=>x.itemCategoryId==1); 
      console.log("issue list");
      console.log(this.IssueList)
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getEquipNonOPLList();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getEquipNonOPLList();
  } 

  deleteItem(row) {
    const id = row.fiscalYearId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.IssueRegisterService.delete(id).subscribe(() => {
          this.getEquipNonOPLList();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })    
  }
}
