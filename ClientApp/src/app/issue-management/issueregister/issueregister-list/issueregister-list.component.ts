import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { IssueRegister } from '../../models/IssueRegister';
import { IssueRegisterService } from '../../service/IssueRegister.service';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role'; 
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
 

@Component({
  selector: 'app-issueregister',
  templateUrl: './issueregister-list.component.html',
  styleUrls: ['./issueregister-list.component.sass']
})
export class IssueRegisterListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: IssueRegister[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  role:any;
  branchId:any;
  traineeId:any;
  departmentNameId:any;
  unitId:any;
  userRole = Role;

  displayedColumns: string[] = [ 'ser', 'itemName', 'partNO','imc','issueQty','toolsLocation', 'issueDate','actions'];
  dataSource: MatTableDataSource<IssueRegister> = new MatTableDataSource();
  
  constructor(private snackBar: MatSnackBar,private ItemDetailService:ItemDetailService, private IssueRegisterService: IssueRegisterService,private authService: AuthService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.departmentNameId =this.branchId;

      this.ItemDetailService.findBaseSchoolNameId(this.departmentNameId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.getIssueRegisters();
     });
    }
  }
 
  getUnitId() {
    this.ItemDetailService.findBaseSchoolNameId(this.departmentNameId).subscribe(res=>{
      this.unitId=res.thirdLevel;
      console.log("Unit Id");
      console.log(this.unitId);
   });
  }

  getIssueRegisters() {
    console.log("issue regste");
    this.isLoading = true;
    this.IssueRegisterService.getIssueRegisters(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.unitId,this.departmentNameId).subscribe(response => {
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getIssueRegisters();
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getIssueRegisters();
  }

  deleteItem(row) {
    const id = row.issueRegisterId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.IssueRegisterService.delete(id).subscribe(() => {
          this.getIssueRegisters();
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
