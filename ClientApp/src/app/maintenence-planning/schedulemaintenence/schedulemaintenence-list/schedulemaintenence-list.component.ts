import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ScheduleMaintenence } from '../../models/ScheduleMaintenence';
import { SelectionModel } from '@angular/cdk/collections';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import {ScheduleMaintenenceService} from '../../service/ScheduleMaintenence.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { MatDialog } from '@angular/material/dialog';
import {ProgressDialogMessageComponent} from '../progress-dialog-message/progress-dialog-message.component'

@Component({
  selector: 'app-schedulemaintenence-list',
  templateUrl: './schedulemaintenence-list.component.html',
  styleUrls: ['./schedulemaintenence-list.component.sass']
})
export class ScheduleMaintenenceListComponent implements OnInit {

  masterData = MasterData;
  userRole = Role;
  ELEMENT_DATA: ScheduleMaintenence[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  traineeId:any;
  role:any;
  branchId:any;

  unitId:any;
  progressCount:number;
  totalProgress:any;
  completedProgress:number;

  displayedColumns: string[] = [ 'ser', 'jobDescriptionScheduleMaintenence', 'drNo','fiscalYear', 'byWhom','startDate', 'progress', 'actions'];
  dataSource: MatTableDataSource<ScheduleMaintenence> = new MatTableDataSource();

  selection = new SelectionModel<ScheduleMaintenence>(true, []);
  
  constructor(public dialog: MatDialog,private snackBar: MatSnackBar,private authService: AuthService,private ScheduleMaintenenceService: ScheduleMaintenenceService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    

    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      // this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.ScheduleMaintenenceService.findBaseSchoolNameId(this.branchId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.getScheduleMaintenences(this.unitId,this.branchId);
      })
      
    }
  }
 
  getScheduleMaintenences(unitId,departmentameId) {
    this.isLoading = true;
    this.ScheduleMaintenenceService.getScheduleMaintenencesForDept(this.paging.pageIndex, this.paging.pageSize,this.searchText,unitId,departmentameId).subscribe(response => {
      
      this.dataSource.data = response.items
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
      console.log("");
      console.log(this.dataSource.data)
      var count;
      var totalProgress=0;
      for (let i = 0; i < this.dataSource.data.length; i++) {
        this.totalProgress +=this.dataSource.data[i].progress
        console.log("Total Progress");
        if(this.dataSource.data[i].progress !=null)
        {
          console.log(this.dataSource.data[i].progress);
          count=this.dataSource.data[i].progress.split("%",1);
          var newCount= Number(count[0])
          totalProgress +=newCount;
        }
      }
       this.progressCount=this.dataSource.data.length*100;
       this.completedProgress=(totalProgress*100)/this.progressCount
      //  console.log("8888");
      //  console.log(this.completedProgress);      
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getScheduleMaintenences(this.unitId,this.branchId);
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getScheduleMaintenences(this.unitId,this.branchId);
  } 
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onCompletedButtonClick(row){
     console.log(row)
     this.ScheduleMaintenenceService.find(row).subscribe(res=>{
      console.log(res);

      if(Number(res.progresId) !=1 ){
        let tempDirection;
         if (localStorage.getItem('isRtl') === 'true') {
           tempDirection = 'rtl';
         } else {
           tempDirection = 'ltr';
         }
         const dialogRef = this.dialog.open(ProgressDialogMessageComponent, {
          // data: this.spareStockBySpRequest,
           direction: tempDirection,
         });
      }
      else{
        this.confirmService.confirm('Confirm Complete message', 'Are You Sure Completed This Item').subscribe(result => {
          console.log(result);
          if (result) {
            this.ScheduleMaintenenceService.completeSchedule(row.scheduleMaintenanceId,res).subscribe(response => {
              this.reloadCurrentRoute();
              this.snackBar.open('Information Updated Successfully ', '', {
                duration: 2000,
                verticalPosition: 'bottom',
                horizontalPosition: 'right',
                panelClass: 'snackbar-success'
              });
            })
          }
        })  
      }
    })
   }
  deleteItem(row) {
    const id = row.scheduleMaintenanceId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe(result => {
      console.log(result);
      if (result) {
        this.ScheduleMaintenenceService.delete(id).subscribe(() => {
          this.getScheduleMaintenences(this.unitId,this.branchId);
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
