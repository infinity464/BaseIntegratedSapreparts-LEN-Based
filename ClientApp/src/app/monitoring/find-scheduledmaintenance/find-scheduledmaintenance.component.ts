import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from '../../basic-setup/service/DepartmentName.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { AcceptanceService } from 'src/app/spares-management/service/Acceptance.service';
import { MatDialog } from '@angular/material/dialog';
import {ScheduleMaintenenceService} from '../../maintenence-planning/service/ScheduleMaintenence.service'
import { ScheduleMaintenence } from '../../maintenence-planning/models/ScheduleMaintenence';
import { MonitoringService } from '../service/monitoring.service';

@Component({
  selector: 'app-find-scheduledmaintenance',
  templateUrl: './find-scheduledmaintenance.component.html',
  styleUrls: ['./find-scheduledmaintenance.component.sass']
})
export class FindScheduledMaintenanceComponent implements OnInit {
  pageTitle: string;
  userRole = Role;
  masterData = MasterData;
  destination:string;
  btnText:string;
  CorrectiveForm: FormGroup;
  validationErrors: string[] = [];
  selectedMaintenenceType:SelectedModel[];
  selectedFiscalYear:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  isShown: boolean = false ;
  isNoDataFound: boolean = false ;
  currentDateTime:any;
  unitId:any;
  departmentNameId:any;
  departmentName:any;
  scheduledMaintananceList:any;
  traineeId:any;
  role:any;
  branchId:any;
  showHideDiv=false;
  baseSchoolNameList:SelectedModel[];
  progressCount:number;
  totalProgress:any;
  completedProgress:number;

  displayedColumns: string[] = [ 'ser', 'jobDesc','drNo', 'byWhom', 'startDate','progress', 'completeDate'];
 
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  constructor(public dialog: MatDialog,private monitoringService: MonitoringService,private ScheduleMaintenenceService:ScheduleMaintenenceService,private snackBar: MatSnackBar,private AcceptanceService:AcceptanceService,private authService: AuthService,private ItemDetailService: ItemDetailService,private datepipe: DatePipe,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    // const id = this.route.snapshot.paramMap.get('scheduleMaintenanceId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.CorrectiveForm.get('departmentNameId').setValue(this.branchId);
     
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      console.log(this.branchId+" ggg")
      
      this.CorrectiveForm.get('unitId').setValue(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
    }
  }
  intitializeForm() {
    this.CorrectiveForm = this.fb.group({      
      unitId: [],
      departmentNameId: [],
      issueRegisterId: [],      
      scheduleMaintenenceTypeId: [],
      fiscalYearId: [],      
    });
    this.getSelectedScheduleMaintenenceType();
    this.getSelectedFiscalYear();
    this.getSelectedBaseSchoolName();
  }
  getSelectedDepartmentNameByUnitId(){
    var unitId = this.CorrectiveForm.value['unitId'];
    this.getSelectedDepartmentName(unitId);
  }
  getSelectedScheduleMaintenenceType(){
    this.ScheduleMaintenenceService.getSelectedScheduleMaintenenceType().subscribe(res => {
      this.selectedMaintenenceType = res;
    });
  }

  getSelectedFiscalYear(){
    this.ScheduleMaintenenceService.getSelectedFiscalYear().subscribe(res => {
      this.selectedFiscalYear = res;
    });
  }

  
  onDepartmentSelectionChange() {    
    var departmentId =  this.CorrectiveForm.value['departmentNameId'];; 
    console.log("Department Id");
    console.log(departmentId);

    this.ItemDetailService.findBaseSchoolNameId(departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;
      console.log("UnitID");
      console.log(this.unitId);
      this.CorrectiveForm.get('unitId').setValue(this.unitId);
      this.ScheduleMaintenenceService.getSelectedEquipmentNameFromIssueRegisterByParameter(this.unitId,departmentId,1).subscribe(res => {
        this.selectedEquipment = res;
        console.log("Department Name Id----------");
        console.log(this.selectedEquipment);
      });
    });
  }

  
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
      console.log(res)
    }); 
  }


  GetDepartmentNameById(baseNameId){    
    this.DepartmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log(res)
    }); 
  }
  getSelectedBaseSchoolName(){
    this.isShown=true;
    this.monitoringService.getSelectedBaseSchool().subscribe(res=>{
      this.baseSchoolNameList=res;
    });
  }
  onSubmit() {
    this.isShown = true;
    var findArr = this.CorrectiveForm.value;
    console.log(findArr)
    this.monitoringService.findScheduledMaintanences(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
      this.scheduledMaintananceList=res

      var count;
      var totalProgress=0;
      for (let i = 0; i < this.scheduledMaintananceList.length; i++) {
        this.totalProgress +=this.scheduledMaintananceList[i].progress
        console.log("Total Progress");
        if(this.scheduledMaintananceList[i].progress !=null)
        {
          console.log(this.scheduledMaintananceList[i].progress);
          count=this.scheduledMaintananceList[i].progress.split("%",1);
          var newCount= Number(count[0])
          totalProgress +=newCount;
        }
      }
       this.progressCount=this.scheduledMaintananceList.length*100;
       this.completedProgress=(totalProgress*100)/this.progressCount
   
      console.log(res)
    }); 


  }
  
  toggle(){
    this.showHideDiv = !this.showHideDiv;
  }
  printSingle(){
    this.showHideDiv= false;
    this.print();
  }
  print(){ 
     
    let printContents, popupWin;
    printContents = document.getElementById('print-routine').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <style>
          body{  width: 99%;}
            label { font-weight: 400;
                    font-size: 13px;
                    padding: 2px;
                    margin-bottom: 5px;
                  }
            table, td, th {
                  border: 1px solid silver;
                    }
                    table td {
                  font-size: 13px;
                    }

                    table th {
                  font-size: 13px;
                    }
              table {
                    border-collapse: collapse;
                    width: 98%;
                    }
                th {
                    height: 26px;
                    }
                .header-text{
                  text-align:center;
                }
                .header-text h3{
                  margin:0;
                }
          </style>
        </head>
        <body onload="window.print();window.close()">
          <div class="header-text">
          <h3>Schedule Maintanance List</h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`
    );
    popupWin.document.close();
}

}
