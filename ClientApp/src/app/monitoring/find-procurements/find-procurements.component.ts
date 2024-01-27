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

@Component({
  selector: 'app-find-procurements',
  templateUrl: './find-procurements.component.html',
  styleUrls: ['./find-procurements.component.sass']
})
export class FindProcurementsComponent implements OnInit {
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
  ProcurementsByParams:any[];
  selectedItemCategory:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedSparesCategory:SelectedModel[];
  baseSchoolNameList:SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedDemandNo: SelectedModel[];
  masterData = MasterData;
  departmentId:any;
  isShown: boolean = false ;
  isNoDataFound:boolean=false;
  disableSelect:boolean=false;
  departmentNameId:any;
  userRole = Role;
  unitId:any;
  traineeId:any;
  role:any;
  branchId:any;
  showHideDiv=false;


  displayedColumns: string[] = [ 'ser','itemName', 'demandNo', 'deno','qty', 'lpRqNo', 'status', 'supplier'];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private ItemDetailService: ItemDetailService,private monitoringService: MonitoringService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('itemDetailId'); 
    this.sparesCategoryId= this.route.snapshot.paramMap.get('sparesCategoryId');  
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    this.intitializeForm();
    this.getSelectedBaseSchoolName();

    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.ItemDetailService.findBaseSchoolNameId(this.branchId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.ItemDetailForm.get('unitId').setValue(this.unitId);
        console.log("UnitID");
      console.log(this.unitId);
      // this.getSelectedDemandNo();
      })
      this.getSelectedDepartmentName(this.branchId);
    }
    
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.ItemDetailForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    // this.getselectedItemType();
    // this.getselectedTrades();
    //this.GetDepartmentNameById(this.masterData.baseSchoolDept.submarine);
    this.getSelectedItemCategory();
    //this.getselectedSparesCategory();
  }
  intitializeForm() {
    this.ItemDetailForm = this.fb.group({
      unitId:[],
      departmentNameId:[],
      itemCategoryId:[],
      equipmentNameId:[],
      demandId:[],
      
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
  onItemCategorySelectionChange(){
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    var itemCategoryId = this.ItemDetailForm.value['itemCategoryId'];
    console.log(unitId,departmentNameId,itemCategoryId)
    this.ItemDetailService.getSelectedEquipmentByParameterRequest(unitId,departmentNameId,itemCategoryId).subscribe(res=>{
      this.selectedEquipment=res
    });
  }

  getSelectedDemandNo(){
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    console.log(unitId,departmentNameId)
    this.monitoringService.getSelectedDemandNo(unitId,departmentNameId).subscribe(res=>{
      this.selectedDemandNo=res
    });
  }

  onDemandSelectionChange(){
    var demandId = this.ItemDetailForm.value['demandId'];
    console.log(demandId)
    this.monitoringService.find(demandId).subscribe(res=>{
      console.log(res);
      this.ItemDetailForm.get('itemCategoryId').setValue(res.itemCategoryId);
      this.ItemDetailForm.get('equipmentNameId').setValue(res.equipmentNameId);
      this.disableSelect = true;
      this.monitoringService.getSelectedEquipmentName().subscribe(res=>{
        this.selectedEquipment=res
      });
    });
  }

  onEquipmentNameSelectionChange(){
    var unitId = this.ItemDetailForm.value['unitId'];
    var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    var itemCategoryId = this.ItemDetailForm.value['itemCategoryId'];
    var equipmentNameId = this.ItemDetailForm.value['equipmentNameId'];
    console.log(unitId,departmentNameId,itemCategoryId,equipmentNameId)
    this.monitoringService.getSelectedDemandNoByParams(unitId,departmentNameId,itemCategoryId,equipmentNameId).subscribe(res=>{
      this.selectedDemandNo=res
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
          <h3>List of Procurement</h3>
          
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
    this.monitoringService.findProcurements(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId == null ? 0 : findArr.equipmentNameId,findArr.demandId == null ? 0 : findArr.demandId).subscribe(res=>{
      this.ProcurementsByParams=res
      console.log(res)
    }); 

    
  
 
  }

}
