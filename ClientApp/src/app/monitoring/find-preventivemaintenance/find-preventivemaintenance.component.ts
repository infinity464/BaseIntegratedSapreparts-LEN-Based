import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from '../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { MonitoringService } from '../service/monitoring.service';
import { ItemDetailService } from '../../spares-management/service/itemDetail.service';
import { PreventiveMaintenanceService } from 'src/app/maintenence-planning/service/PreventiveMaintenance.service';
import { ScheduleMaintenenceService } from 'src/app/maintenence-planning/service/ScheduleMaintenence.service';
import { IssueRegisterService } from 'src/app/issue-management/service/IssueRegister.service';
import {MaintenanceCategoryService} from 'src/app/maintenence-planning/service/MaintenanceCategory.service';
import { MaintenanceCategory } from 'src/app/basic-setup/models/MaintenanceCategory';


@Component({
  selector: 'app-find-preventivemaintenance',
  templateUrl: './find-preventivemaintenance.component.html',
  styleUrls: ['./find-preventivemaintenance.component.sass']
})
export class FindPreventiveMaintenanceComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  sparesCategoryId:string;
  ItemDetailForm: FormGroup;
  validationErrors: string[] = [];
  selectedCategoryTypes:SelectedModel[]; 
  selectedItemType:SelectedModel[]; 
  selectedTrades:SelectedModel[]; 
  selectedDepartmentName:SelectedModel[];
  itemDetailByDepartmentId:any[];
  PreventiveList:any[];
  selectedItemCategory:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedSparesCategory:SelectedModel[];
  baseSchoolNameList:SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedItemName: SelectedModel[];
  selectedCategory: SelectedModel[];
  selectedSubCategory: SelectedModel[];
  selectedMaintenenceCategory:MaintenanceCategory[];
  masterData = MasterData;
  departmentId:any;
  isShown: boolean = false ;
  isNoDataFound:boolean=false;
  departmentNameId:any;
  userRole = Role;
  pmsNo:any;
  unitId:any;
  itemDetailId:any;
  traineeId:any;
  role:any;
  branchId:any;
  showHideDiv=false;


  displayedColumns: string[] = [ 'ser', 'jic','jobDescription', 'byWhomName','completedDate'];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar,private MaintenenceCategoryService:MaintenanceCategoryService,private IssueRegisterService:IssueRegisterService,private ScheduleMaintenenceService:ScheduleMaintenenceService,private PreventiveMaintenanceService:PreventiveMaintenanceService,private authService: AuthService,private confirmService: ConfirmService,private ItemDetailService: ItemDetailService,private monitoringService: MonitoringService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('preventiveMaintenanceId'); 
    //this.sparesCategoryId= this.route.snapshot.paramMap.get('sparesCategoryId');  
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    this.intitializeForm();
    this.getSelectedBaseSchoolName();

    if(this.role == this.userRole.SuperAdmin){
    }
    
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.ItemDetailForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }

    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.ItemDetailService.findBaseSchoolNameId(this.branchId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.ItemDetailForm.get('unitId').setValue(this.unitId);
        console.log("UnitID");
        this.onDepartmentSelectionChange();
      console.log(this.unitId);
      })
      
    }
    this.getSelectedItemCategory();
    // this.getPmsNoByItemDetailId();
  }
  intitializeForm() {
    this.ItemDetailForm = this.fb.group({
      unitId:[],
      departmentNameId:[],
      itemDetailId:[],
      issueRegisterId:[],
      maintenanceTypeId:[],
      maintenanceCategoryId:[],
      pmsno:[''],
      
    })
  }
  getSelectedItemCategory(){
    this.ItemDetailService.getSelectedItemCategory().subscribe(res=>{
      this.selectedItemCategory=res 
    });
  }
  getSelectedBaseSchoolName(){
    this.isShown=true;
    this.monitoringService.getSelectedBaseSchool().subscribe(res=>{
      this.baseSchoolNameList=res;
    });
  }
  getSelectedDepartmentNameByUnitId(){
    var unitId = this.ItemDetailForm.value['unitId'];
    this.getSelectedDepartmentName(unitId);
  }
  
  getSelectedDepartmentName(fourthLevel){
    this.monitoringService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getPmsNoByItemDetailId(){
    // this.ItemDetailForm.get('itemDetailId').setValue(this.itemDetailId);
    var itemDetailId = this.ItemDetailForm.value['itemDetailId'];
    console.log("itemDetailIdss")
    console.log(itemDetailId)
    this.monitoringService.getPmsNoByItemDetailId(itemDetailId).subscribe(res=>{
      this.pmsNo=res[0].text;
     console.log("response");
     console.log(this.pmsNo)
     this.ItemDetailForm.get('pmsno').setValue(this.pmsNo);
   });
  }
  onDepartmentSelectionChange(){
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    //var itemCategoryId = this.preventiveMaintenanceForm.value['itemCategoryId'];
    console.log(unitId,departmentNameId)
    this.monitoringService.getItemNameByItemDetailByPreventive(unitId,departmentNameId).subscribe(res=>{
      
      this.selectedItemName=res
      console.log("ff");
      console.log(this.selectedItemName);
    });
    // this.ScheduleMaintenenceService.getSelectedEquipmentNameFromIssueRegisterByParameter(unitId,departmentNameId,1).subscribe(res => {
    //   this.selectedItemName = res;
    //   console.log("Department Name Id----------");
    //   console.log(this.selectedItemName);
    // });
  }
  onEquipmentNameSelectionChange(){
   // var itemDetailId;
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    var itemDetailId = this.ItemDetailForm.value['itemDetailId'];
  //  var issueRegisterId = this.ItemDetailForm.value['issueRegisterId'];
    //console.log(unitId,departmentNameId,itemDetailId)
      console.log(itemDetailId+"00");

    // this.IssueRegisterService.find(issueRegisterId).subscribe(res=>{
    // this.itemDetailId =res.itemDetailId;
    // console.log("itemdetail");
    // console.log(this.itemDetailId);
    // this.ItemDetailForm.get("itemDetailId").setValue(this.itemDetailId);
    // //   this.ItemDetailForm.get('unitId').setValue(this.unitId);
    // //   console.log("UnitID");
    // //   this.onDepartmentSelectionChange();
    // // console.log(this.unitId);

    // })
    this.ItemDetailForm.get('itemDetailId').setValue(itemDetailId);
    console.log("item detail");
    console.log(itemDetailId);
    this.getPmsNoByItemDetailId();
    //MaintenenceCategoryService
    // this.monitoringService.getCategoryByIdRequest(unitId,departmentNameId,itemDetailId).subscribe(res=>{
    //  
    //   this.selectedCategory=res
    // });

      this.MaintenenceCategoryService.getselectedMaintenanceType().subscribe(res=>{
     // this.getPmsNoByItemDetailId();
      this.selectedCategory=res
      console.log(this.selectedCategory);
    });


   // var itemDetailId = this.ItemDetailForm.value['itemDetailId'];
   // console.log("Item Detail"+itemDetailId)

  }
  onCategorySelectionChange(){
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    var itemDetailId = this.ItemDetailForm.value['itemDetailId'];
    var maintenanceTypeId = this.ItemDetailForm.value['maintenanceTypeId'];
    console.log(unitId,departmentNameId,itemDetailId,maintenanceTypeId)
    
        this.MaintenenceCategoryService.getMaintenenceCategoryByType(maintenanceTypeId).subscribe(res=>{
       this.selectedSubCategory=res
      console.log(this.selectedMaintenenceCategory);
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
          <h3>Preventive Maintenance List</h3>
          
          </div>
          <br>
          <hr>
          ${printContents}
          
        </body>
      </html>`
    );
    popupWin.document.close();
}

  
  
  

  onSubmit() {
    //const id = this.ItemDetailForm.get('itemDetailId').value;   
    var findArr = this.ItemDetailForm.value;
    console.log(findArr)
    this.monitoringService.findPreventiveMaintenance(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemDetailId == null ? 0 : findArr.itemDetailId,findArr.maintenanceCategoryId == null ? 0 : findArr.maintenanceCategoryId,findArr.maintenanceTypeId == null ? 0 : findArr.maintenanceTypeId).subscribe(res=>{
      this.PreventiveList=res;
      console.log(this.PreventiveList)
    }); 

    
  
 
  }

}
