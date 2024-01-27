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
import {JobDescriptionEntryScheduleMaintService} from '../../service/JobDescriptionEntryScheduleMaint'
import { JobDescriptionScheduleMaintenence } from '../../models/JobDescriptionScheduleMaintenence';

@Component({
  selector: 'app-new-jobdescriptionschedulemaintenence',
  templateUrl: './new-jobdescriptionschedulemaintenence.component.html',
  styleUrls: ['./new-jobdescriptionschedulemaintenence.component.sass']
})
export class NewJobDescriptionScheduleMaintenenceComponent implements OnInit {
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
  selectedFiscalYear:SelectedModel[];
  selectedProcurementStatus:SelectedModel[];
  selectedJobDescriptionEntry:SelectedModel[];
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
  selectedPriority:SelectedModel[];
  selectedMaintenenceAuthority:SelectedModel[];
  selectedbyWhoms:SelectedModel[];
  selectedStatusOfJob:SelectedModel[];
  selectedJobCategoryMaintenence:SelectedModel[];
  spareStockBySpRequest:any[];
  departmentId:any;
  equipmentNameId:any;
 // itemCategoryId:any;
  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedDepColumns: string[] = [ 'ser','itemName','jobDescription','scheduleMaintenenceType','jobCategoryMaintenence','actions'];
  dataSource: MatTableDataSource<JobDescriptionScheduleMaintenence> = new MatTableDataSource();
  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];

  constructor(public dialog: MatDialog,private JobDescriptionEntryScheduleMaintService:JobDescriptionEntryScheduleMaintService,private snackBar: MatSnackBar,private IssueRegisterService:IssueRegisterService,private AcceptanceService:AcceptanceService,private CorrectiveMaintenenceService: CorrectiveMaintenenceService,private authService: AuthService,private ItemDetailService: ItemDetailService,private datepipe: DatePipe,private DemandService:DemandService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    const id = this.route.snapshot.paramMap.get('jobDescriptionScheduleMaintenenceId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Schedule Maintenence';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.JobDescriptionEntryScheduleMaintService.find(+id).subscribe(
        res => {
          this.CorrectiveForm.patchValue({    
            jobDescriptionScheduleMaintenenceId: res.jobDescriptionScheduleMaintenenceId,
            unitId: res.unitId,
            departmentNameId: res.departmentNameId,
            itemDetailId: res.itemDetailId,
            scheduleMaintenenceTypeId: res.scheduleMaintenenceTypeId,
            jobCategoryMaintenenceId: res.jobCategoryMaintenenceId,
            fiscalYearId: res.fiscalYearId,
            issueRegisterId: res.issueRegisterId,
            equipmentNameId: res.equipmentNameId,
            menuPosition: res.menuPosition,
            jobDescription:res.jobDescription,
            isActive: true
          }); 
          // this.getselectedDemandsOnUpdate(res.departmentNameId);
          // this.getDemandData();
          //this.getPartNoPassItemCategoryIdInProcurement(this.itemDetailId);
          //this.getselectedDemands();
          console.log("response");
          console.log(res);
          this.onDepartmentSelectionChange();
          // this.onEquipmentSelectionChange(res.partNo)
        }
      );
    } else {
      this.pageTitle = 'Create Schedule Maintenence';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.CorrectiveForm.get('departmentNameId').setValue(this.branchId);
     
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();


      // this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      //  var unitId=res.thirdLevel; 
    //   getPartNoForProcurement()
    //    console.log(this.departmentId+" department name id");
    //    console.log(unitId+"unit id");
    //  });



    //  console.log("Unit Id");
    //  console.log(unitId);
     // this.onDepartmentSelectionChange(this.branchId)
      // this.getselectedProcurements();
      // console.log("depat");
      // console.log(this.branchId);
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
      
      // procurementId: [0],
      jobDescriptionScheduleMaintenenceId: [0],
      unitId: [''],
      departmentNameId: [''],
      itemDetailId: [''],
      scheduleMaintenenceTypeId: [''],
      jobCategoryMaintenenceId: [''],
      //fiscalYearId: [''],
      issueRegisterId: [''],
      equipmentNameId: [''],
      menuPosition: [''],
      jobDescription: [''], 
      itemCategoryId:[''],     
      isActive: [true]
      // ItemStoreList: this.fb.array([
      //   this.createIssueRegisterData()
      // ]),
    })
    this.getSelectedPriority();
    this.getSelectedMaintenenceAuthority();
    this.getSelectedByWhoms();
    this.getSelectedStatusOfJob();
    this.getSelectedScheduleMaintenenceType();
    this.getSelectedJobCategoryMaintenence();
    this.getSelectedFiscalYear();
  }
  

  getSelectedPriority(){
    this.CorrectiveMaintenenceService.getSelectedPriority().subscribe(res => {
      this.selectedPriority = res;
    });
  }

  getSelectedScheduleMaintenenceType(){
    this.JobDescriptionEntryScheduleMaintService.getSelectedScheduleMaintenenceType().subscribe(res => {
      this.selectedJobDescriptionEntry = res;
    });
  }
  getSelectedJobCategoryMaintenence(){
    this.JobDescriptionEntryScheduleMaintService.getSelectedJobCategoryMaintenence().subscribe(res => {
      this.selectedJobCategoryMaintenence = res;
    });
  }
  getSelectedFiscalYear(){
    this.JobDescriptionEntryScheduleMaintService.getSelectedFiscalYear().subscribe(res => {
      this.selectedFiscalYear = res;
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

  onSpareStockButtonClick(event, data){
   
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
        this.CorrectiveMaintenenceService.completedCorrectiveMaintenence(data.value.correctiveMaintenenceId,data.value).subscribe(response => {
          this.reloadCurrentRoute();
          this.snackBar.open('Information Updated Successfully ', '', {
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

   // getSelectedEquipmentNameFromIssueRegisterByParameter
     this.CorrectiveForm.get('unitId').setValue(this.unitId);

     this.JobDescriptionEntryScheduleMaintService.getSelectedEquipmentNameFromIssueRegisterByParameter(this.unitId,this.departmentId,1).subscribe(res => {
      this.selectedEquipment = res;
      console.log("Department Name Id----------");
      console.log(this.selectedEquipment);
    });
    // getSelectedEquipmentNameByParameterRequest
    //  this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentId).subscribe(res => {
    //   this.selectedEquipment = res;
    // });
     // console.log("value");
     // console.log(this.unitId);      
   });
 // }
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

  onEquipmentSelectionChange(dropdown){
    if (dropdown.isUserInput) {
      this.isShown=true;
      console.log("Equipment Name Id");
      console.log(dropdown.source.value);
      var issueRegisterId=dropdown.source.value;
      this.IssueRegisterService.find(issueRegisterId).subscribe(res => {
        this.partNo = res.partNO;
        this.itemName =res.itemName;
        this.toolsLocation =res.toolsLocation;
        this.itemDetailId = res.itemDetailId;
        this.itemCategoryId =res.itemCategoryId;
        this.equipmentNameId=res.equipmentNameId;
       console.log("euuuuuuuuu");
        console.log(res);
      //  this.itemCategoryId =res.itemCategoryId;
       this.CorrectiveForm.get('itemCategoryId').setValue(this.itemCategoryId);  
        this.CorrectiveForm.get('itemDetailId').setValue(this.itemDetailId);  
        this.CorrectiveForm.get('equipmentNameId').setValue(this.equipmentNameId);  
        console.log("Issue register");
        console.log(this.partNo);
        console.log("DepartmentName id");
        console.log(this.departmentId);
        console.log("unit id");
        console.log(this.unitId);
        console.log("IssueRegister");
        console.log(issueRegisterId);
        console.log("EqipmentNameId");
        console.log(issueRegisterId);

     this.JobDescriptionEntryScheduleMaintService.getJobDescriptionScheduleMaintenences(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.unitId,this.departmentId,this.itemDetailId).subscribe(response => {
      console.log("dddddd");
      this.dataSource.data = response.items;
      // this.clearList();
      // this.getItemStoreListonClick();
      console.log(this.dataSource.data )
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
      });
     }
  }

  getSelectedProcurementType(){
    this.ProcurementService.getSelectedProcurementType().subscribe(res => {
      this.selectedProcurementType = res;
    });
  }

  getSelectedProcurementStatus(){
    this.ProcurementService.getSelectedProcurementStatus().subscribe(res => {
      this.selectedProcurementStatus = res;
    });
  }
  onUploadClick(value){

  }
  onDemandSelectionChange(dropdown){
    if (dropdown.isUserInput) {

      }
  }

  getSelectedItemCategory() {
    this.ProcurementService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  getPartNoForProcurement(){
    this.ProcurementService.getPartNoForProcurement(this.masterData.procurement.itemStatusProcurement,this.masterData.procurement.procurementStatus).subscribe(res=>{
      this.selectedPartNoForProcurement=res
      console.log("procurementnnnnnnnnnnn");
      console.log(res)
    }); 
  }

  GetDepartmentNameById(baseNameId){    
    this.DepartmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log(res)
    }); 
  }

  getselectedItemDetails(){
    this.ProcurementService.getselectedItemDetails().subscribe(res=>{
      
      this.itemDetail=res;

    });    
  }

  getselectedPrincipalNames(){
    this.ProcurementService.getselectedPrincipalNames().subscribe(res=>{
      this.principalName=res;
    });  
  }
  getselectedManufacture(){
    this.ProcurementService.getselectedManufacture().subscribe(res=>{
      this.selectManufacture=res;
    }); 
  }

  getselectedSupplier(){
    this.ProcurementService.getselectedSupplier().subscribe(res=>{
      this.Supplier=res;
    }); 
  }
  getselectedSupplierA(){
    this.ProcurementService.getselectedSupplierA().subscribe(res=>{
      this.SupplierA=res;
    }); 
  }
  getselectedSupplierM(){
    this.ProcurementService.getselectedSupplierM().subscribe(res=>{
      this.SupplierM=res;
    }); 
  }
  getselectedPartOfShipments(){
    this.ProcurementService.getselectedPartOfShipments().subscribe(res=>{
      this.partOfShipment=res;
    }); 
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.CorrectiveForm.get('departmentNameId').value;
    console.log(departmentId);
  }

  onSubmit() {
    const id = this.CorrectiveForm.get('jobDescriptionScheduleMaintenenceId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.JobDescriptionEntryScheduleMaintService.update(+id,this.CorrectiveForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/add-jobdescriptionschedulemaintenence');
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
      this.JobDescriptionEntryScheduleMaintService.submit(this.CorrectiveForm.value).subscribe(response => {
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
    const id = row.jobDescriptionScheduleMaintenenceId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.JobDescriptionEntryScheduleMaintService.delete(id).subscribe(() => {
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
