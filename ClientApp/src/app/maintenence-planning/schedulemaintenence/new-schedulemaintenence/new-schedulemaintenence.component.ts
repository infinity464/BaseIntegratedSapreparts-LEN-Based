import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../../spares-management/service/Procurement.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from '../../../basic-setup/service/DepartmentName.service';
import { DemandService } from '../../../spares-management/service/Demand.service';
import {Demand} from '../../../spares-management/models/Demand';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import {Procurement} from '../../../spares-management/models/Procurement';
import { DatePipe } from '@angular/common';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { AcceptanceService } from 'src/app/spares-management/service/Acceptance.service';
import {CorrectiveMaintenenceService} from '../../service/CorrectiveMaintenence.service';
import { IssueRegisterService } from 'src/app/issue-management/service/IssueRegister.service';
import { IssueRegister } from 'src/app/issue-management/models/IssueRegister';
import { CorrectiveMaintenence } from '../../models/CorrectiveMaintenence';
import { MatDialog } from '@angular/material/dialog';
import {SpareStocklistComponent} from '../../../../app/maintenence-planning/corrective/spare-stocklist/spare-stocklist.component'
import {ScheduleMaintenenceService} from '../../service/ScheduleMaintenence.service';
import { ScheduleMaintenence } from '../../models/ScheduleMaintenence';
import { JobDescriptionScheduleMaintenence } from '../../models/JobDescriptionScheduleMaintenence';

@Component({
  selector: 'app-new-schedulemaintenence',
  templateUrl: './new-schedulemaintenence.component.html',
  styleUrls: ['./new-schedulemaintenence.component.sass']
})
export class NewScheduleMaintenenceComponent implements OnInit {
  pageTitle: string;
  masterData = MasterData;
  destination:string;
  btnText:string;
  CorrectiveForm: FormGroup;
  validationErrors: string[] = [];
  itemDetail: SelectedModel[];
  procurementStatus: SelectedModel[];
  SupplierM: SelectedModel[];
  Supplier: SelectedModel[];
  SupplierA: SelectedModel[];
  partOfShipment: SelectedModel[];
  selectedItemDetails: SelectedModel[];
  departmentName: SelectedModel[];
  selectedPartNoForProcurement:SelectedModel[];
  principalName: SelectedModel[];
  selectManufacture: SelectedModel[];
  cstTec: SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedPartNo: SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedItemCategory:SelectedModel[];
  selectedDemandNo:SelectedModel[];
  isShown: boolean = false ;
  isShownList: boolean = false ;
  isShownStockList:boolean=false;
  demandData: Demand[];
  issueRegister:IssueRegister;
  itemDetailId: number;
  itemCategoryId: number;
  selectedProcurementType:SelectedModel[];
  selectedProgress:SelectedModel[];
  selectedProcurementStatus:SelectedModel[];
  sparesCategoryId: number;
  isLoading = false;
  searchText="";
  cstTecYes:any;
  currentDateTime:any;
  unitId:any;
  departmentNameId:any;
  selectedRefNo:any;
  demandId:any;
  partNo:any;
  toolsLocation:any;
  itemName:any;
  // itemDetailId:any;
  selectedPriority:SelectedModel[];
  selectedMaintenenceAuthority:SelectedModel[];
  selectedMaintenenceType:SelectedModel[];
  selectedbyWhoms:SelectedModel[];
  selectedStatusOfJob:SelectedModel[];
  selectedDescription:SelectedModel[];
  selectedFiscalYear:SelectedModel[];
  spareStockBySpRequest:any[];
  departmentId:any;
  equipmentNameId:any;
 // itemCategoryId:any;
  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  progressCount:number;
  totalProgress:any;
  completedProgress:number;
  

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedDepColumns: string[] = [ 'ser','itemName','dateOfDefect','descriptionOfDefect', 'workOrderNo','workStartDate','statusOfJob','actions'];
  dataSource: MatTableDataSource<JobDescriptionScheduleMaintenence> = new MatTableDataSource();
  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];

  constructor(public dialog: MatDialog,private IssueRegisterService:IssueRegisterService,private ScheduleMaintenenceService:ScheduleMaintenenceService,private snackBar: MatSnackBar,private AcceptanceService:AcceptanceService,private CorrectiveMaintenenceService: CorrectiveMaintenenceService,private authService: AuthService,private ItemDetailService: ItemDetailService,private datepipe: DatePipe,private DemandService:DemandService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    const id = this.route.snapshot.paramMap.get('scheduleMaintenanceId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Schedule Maintenence';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ScheduleMaintenenceService.find(+id).subscribe(
        res => {
          this.CorrectiveForm.patchValue({    
            scheduleMaintenanceId:res.scheduleMaintenanceId,
            unitId: res.unitId,
            departmentNameId: res.departmentNameId,
            issueRegisterId: res.issueRegisterId,
            itemDetailId: res.itemDetailId,
            scheduleMaintenenceTypeId: res.scheduleMaintenenceTypeId,
            fiscalYearId: res.fiscalYearId,
            jobDescriptionScheduleMaintenenceId:res.jobDescriptionScheduleMaintenenceId,
            byWhomId:res.byWhomId,
            drNo: res.drNo,
            startDate: res.startDate,
            completedDate: res.completedDate,
            menuPosition: res.menuPosition,
            isActive: res.isActive
          }); 
          // this.getselectedDemandsOnUpdate(res.departmentNameId);
          // this.getDemandData();
          //this.getPartNoPassItemCategoryIdInProcurement(this.itemDetailId);
          //this.getselectedDemands();
          console.log("response");
          console.log(res);
          this.onDepartmentSelectionChange();
        //  this.onEquipmentSelectionChange(res.partNo)
        }
      );
    } else {
      this.pageTitle = 'Create  Schedule Maintenence';
      this.destination = "Add";
      this.btnText = 'Save';
    }
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
      
      scheduleMaintenanceId: [0],
      unitId: [''],
      departmentNameId: [''],
      issueRegisterId: [''],
      itemDetailId: [''],
      partNo:[''],
      scheduleMaintenenceTypeId: [''],
      fiscalYearId: [''],
      jobDescriptionScheduleMaintenenceId: [''],
      byWhomId: [''],
      drNo: [''],
      startDate: [''],
      completedDate:[''],
      menuPosition: [''],
      //spareId:[''],
      isActive: [true],
      ItemStoreList: this.fb.array([
        this.createIssueRegisterData()
      ]),
    })
    this.getSelectedPriority();
    this.getSelectedMaintenenceAuthority();
    this.getSelectedByWhoms();
    this.getSelectedStatusOfJob();
    this.getSelectedScheduleMaintenenceType();
    this.getSelectedFiscalYear();
    this.getSelectedProgress();
  }
  
  getControlLabel(index: number, type: string) {
    return (this.CorrectiveForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  }

// getLevel(i,type){
//    var value= (this.CorrectiveForm.get('ItemStoreList') as FormArray).at(i).get(type).value
//    console.log(value);
// }
  // getControlLabelValue(index: number, type: string) {
  //    (this.CorrectiveForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  // }

  private createIssueRegisterData() {

    return this.fb.group({
      scheduleMaintenanceId:  [0],
      unitId:  [''],
      departmentNameId:  [''],
      issueRegisterId:  [''],
      itemDetailId:  [''],
      scheduleMaintenenceTypeId:  [''],
      fiscalYearId:  [''],
      jobDescriptionScheduleMaintenenceId:  [''],
      byWhomId:  [''],
      progresId:  [''],
     // fiscalYearId:[''],
      drNo: [''] ,
      startDate:  [''],
      completedDate:  [''],
      menuPosition:  [''],
      completedStatus:[''],
      jobDescription:[''],
      progress:['']
     // isActive: true,
    });
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

  // onMaintananceTypeSelection(){
  //   var unitId = this.CorrectiveForm.value['unitId'];
  //   var departmentNameId = this.CorrectiveForm.value['departmentNameId'];
  //   var issueRegisterId = this.CorrectiveForm.value['issueRegisterId'];
  //   var scheduleMaintenenceTypeId = this.CorrectiveForm.value['scheduleMaintenenceTypeId'];

  //   console.log(unitId,departmentNameId,issueRegisterId,scheduleMaintenenceTypeId)
  // }

  getSelectedPriority(){
    this.CorrectiveMaintenenceService.getSelectedPriority().subscribe(res => {
      this.selectedPriority = res;
    });
  }

  getSelectedProgress(){
    this.ScheduleMaintenenceService.getSelectedProgress().subscribe(res => {
      this.selectedProgress = res;
    });
  }
  getSelectedMaintenenceAuthority(){
    this.CorrectiveMaintenenceService.getSelectedMaintenenceAuthority().subscribe(res => {
      this.selectedMaintenenceAuthority = res;
    });
  }
  getSelectedByWhoms(){
    this.CorrectiveMaintenenceService.getSelectedByWhoms().subscribe(res => {
      this.selectedbyWhoms = res;
    });
  }
  getSelectedStatusOfJob(){
    this.CorrectiveMaintenenceService.getSelectedStatusOfJob().subscribe(res => {
      this.selectedStatusOfJob = res;
    });
  }

  getSelectedEquipmentNameByParameterRequest(unitId,departmentNameId){
    this.CorrectiveMaintenenceService.getSelectedEquipmentNameByParameterRequest(unitId,departmentNameId).subscribe(res => {
      this.selectedStatusOfJob = res;
    });
  }

  onCompletedButtonClick(event, data){
    console.log("event");
       console.log(event)

       console.log("data");
       console.log(data)
  }

  onMaintananceTypeSelection(){
    var unitId = this.CorrectiveForm.value['unitId'];
    var departmentNameId = this.CorrectiveForm.value['departmentNameId'];
    var issueRegisterId = this.CorrectiveForm.value['issueRegisterId'];
    var scheduleMaintenenceTypeId = this.CorrectiveForm.value['scheduleMaintenenceTypeId'];
   


    this.isShown=true;
     
      this.ScheduleMaintenenceService.getJobDescriptionScheduleMaintenences(this.paging.pageIndex, this.paging.pageSize,this.searchText,unitId,departmentNameId,issueRegisterId,scheduleMaintenenceTypeId).subscribe(response => {
        this.dataSource.data = response.items;
        console.log(response.items);
        this.clearList();
        this.getItemStoreListonClick();
        this.paging.length = response.totalItemsCount    
        this.isLoading = false;
        var count;
        var totalProgress=0;
        // for (let i = 0; i < this.dataSource.data.length; i++) {
        //   this.totalProgress +=this.dataSource.data[i].progress
        //   count=this.dataSource.data[i].progress.split("%",1);
        //   var newCount= Number(count[0])
        //   totalProgress +=newCount;
        // }
         this.progressCount=this.dataSource.data.length*100;
         this.completedProgress=(totalProgress*100)/this.progressCount
         console.log(this.completedProgress);      
      })
  }
  onSpareStockButtonClick(event, data){

   // this.id = row.id;
   
    console.log("datatatatattatata");
    console.log(data);

       this.isShownStockList=true;
       var unitId=data.value.unitId;
       var departmentNameId=data.value.departmentNameId;
       var itemCategoryId =data.value.itemCategoryId;
       console.log(unitId+'-'+departmentNameId+'-'+itemCategoryId)

      this.CorrectiveMaintenenceService.getSpareStockByParameterSpRequest(unitId,departmentNameId,itemCategoryId).subscribe(res => {
        this.spareStockBySpRequest = res;
        let tempDirection;
        if (localStorage.getItem('isRtl') === 'true') {
          tempDirection = 'rtl';
        } else {
          tempDirection = 'ltr';
        }
        const dialogRef = this.dialog.open(SpareStocklistComponent, {
          data: this.spareStockBySpRequest,
          direction: tempDirection,
        });
        console.log(this.spareStockBySpRequest);
      });


      // console.log(this.spareStockBySpRequest   +"-");
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  onNotCompletedButtonClick(event, data){
   console.log(data.value)
    this.confirmService.confirm('Confirm Return message', 'Are You Sure Completed This  Item?').subscribe(result => {
      if (result) {
        this.ScheduleMaintenenceService.submit(data.value).subscribe(response => {
          this.router.navigateByUrl('/maintenence-planning/schedulemaintenence-list');
          //maintenence-planning/schedulemaintenence-list
          //this.reloadCurrentRoute();
          this.snackBar.open('Information Saved Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-success'
          });
        }

        , error => {
          this.validationErrors = error;
        })
      }
    })
  }
  onDepartmentSelectionChange() {
    //if (dropdown.isUserInput) {
    //this.isShown=true;
    this.departmentId =  this.CorrectiveForm.value['departmentNameId'];; 
    console.log("Department Id");
    console.log(this.departmentId);

             
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;
      console.log("UnitID");
    console.log(this.unitId);


     this.CorrectiveForm.get('unitId').setValue(this.unitId);

     this.ScheduleMaintenenceService.getSelectedEquipmentNameFromIssueRegisterByParameter(this.unitId,this.departmentId,1).subscribe(res => {
      this.selectedEquipment = res;
      console.log("Department Name Id----------");
      console.log(this.selectedEquipment);
    });
  
   });
 // }
  }

  onYearSelectionChange(dropdown){
    if (dropdown.isUserInput) {
      var fiscalYearId = dropdown.source.value;
      var issueRegisterId =this.CorrectiveForm.value['issueRegisterId'];
      var departmentameId =this.CorrectiveForm.value['departmentNameId'];
      var scheduleMaintenenceTypeId =this.CorrectiveForm.value['scheduleMaintenenceTypeId'];
      //var fiscalYearId =this.CorrectiveForm.value['fiscalYearId']
      console.log("unit id");
      console.log(this.unitId);
      console.log("department id");
      console.log(departmentameId);
      console.log("Issue Register Id");
      console.log(issueRegisterId);
      console.log("Fiscal year Id");
      console.log(fiscalYearId);

      console.log("Schedule maintenence type Id");
      console.log(scheduleMaintenenceTypeId);
      
      this.ScheduleMaintenenceService.getSelectedEquipmentForScheduleByParameterRequest(this.unitId,departmentameId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId).subscribe(res=>{
        this.selectedDescription=res
        console.log(res)
        console.log(res)
      }); 
    }
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
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     //console.log(file);
      this.CorrectiveForm.patchValue({
        doc: file,
      });
    }
  }
  onFileNChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     //console.log(file);
      this.CorrectiveForm.patchValue({
        notice: file,
      });
    }
  }
  onFileFChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     //console.log(file);
      this.CorrectiveForm.patchValue({
        approval: file,
      });
    }
  }
  onFileWChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     //console.log(file);
      this.CorrectiveForm.patchValue({
        order: file,
      });
    }
  }

  clearList() {
    const control = <FormArray>this.CorrectiveForm.controls["ItemStoreList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getItemStoreListonClick() {
    const control = <FormArray>this.CorrectiveForm.controls["ItemStoreList"];
    for (let i = 0; i < this.dataSource.data.length; i++) {
      control.push(this.createIssueRegisterData());
     // console.log(this.dataSource.data);
    }
    //console.log(control);
    this.CorrectiveForm.patchValue({ 
      ItemStoreList: this.dataSource.data,
    // console.log()
    });
    // console.log('12');
    // console.log(<FormArray>this.CorrectiveForm.controls["ItemStoreList"].value);
  }

  onUploadClick(value){

  }


  GetDepartmentNameById(baseNameId){    
    this.DepartmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log(res)
    }); 
  }

  oncstTecYes(dropdown){

    if(dropdown.isUserInput) {
      //this.getProcurementsList(dropdown.source.value);
      this.cstTecYes=dropdown.source.value;
      console.log(this.cstTecYes)
    }
  }

  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.CorrectiveForm.get('departmentNameId').value;
    console.log(departmentId);
   // this.getProcurementList();
  }
  
  // reloadCurrentRoute() {
  //   let currentUrl = this.router.url;
  //   this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
  //       this.router.navigate([currentUrl]);
  //   });
  // }
  onSubmit() {
    const id = this.CorrectiveForm.get('scheduleMaintenanceId').value;   
    // this.CorrectiveForm.get('workOrderDate').setValue((new Date(this.CorrectiveForm.get('workOrderDate').value)).toUTCString()) ;
    // this.CorrectiveForm.get('dateOfDefect').setValue((new Date(this.CorrectiveForm.get('dateOfDefect').value)).toUTCString()) ;
    // this.CorrectiveForm.get('workStartDate').setValue((new Date(this.CorrectiveForm.get('workStartDate').value)).toUTCString()) ;
    
    console.log(this.CorrectiveForm.value)

    // const formData = new FormData();
    // for (const key of Object.keys(this.CorrectiveForm.value)) {
    //   const value = this.CorrectiveForm.value[key];
    //   formData.append(key, value);
    // }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ScheduleMaintenenceService.update(+id,this.CorrectiveForm.value).subscribe(response => {
            this.router.navigateByUrl('/spares-management/add-procurement');
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    } else {
      this.ScheduleMaintenenceService.submit(this.CorrectiveForm.value).subscribe(response => {
        this.reloadCurrentRoute();
        this.snackBar.open('Information Inserted Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }
  deleteItem(row) {
    const id = row.procurementId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ScheduleMaintenenceService.delete(id).subscribe(() => {
         // this.getProcurementList();
          //this.getProcurements();
          this.reloadCurrentRoute();
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
