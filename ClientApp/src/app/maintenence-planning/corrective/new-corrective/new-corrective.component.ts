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

@Component({
  selector: 'app-new-corrective',
  templateUrl: './new-corrective.component.html',
  styleUrls: ['./new-corrective.component.sass']
})
export class NewCorrectiveComponent implements OnInit {
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
  selectedSpareRequisition:SelectedModel[];
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
  selectedPriority:SelectedModel[];
  selectedMaintenenceAuthority:SelectedModel[];
  selectedbyWhoms:SelectedModel[];
  selectedStatusOfJob:SelectedModel[];
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

  displayedDepColumns: string[] = [ 'ser','itemName','dateOfDefect','descriptionOfDefect', 'workOrderNo','workStartDate','statusOfJob','actions'];
  dataSource: MatTableDataSource<CorrectiveMaintenence> = new MatTableDataSource();
  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];

  constructor(public dialog: MatDialog,private snackBar: MatSnackBar,private IssueRegisterService:IssueRegisterService,private AcceptanceService:AcceptanceService,private CorrectiveMaintenenceService: CorrectiveMaintenenceService,private authService: AuthService,private ItemDetailService: ItemDetailService,private datepipe: DatePipe,private DemandService:DemandService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    const id = this.route.snapshot.paramMap.get('correctiveMaintenenceId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Corrective';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.CorrectiveMaintenenceService.find(+id).subscribe(
        res => {
          this.CorrectiveForm.patchValue({    
            correctiveMaintenenceId: res.correctiveMaintenenceId,
            unitId: res.unitId,
            departmentNameId: res.departmentNameId,
            equipmentNameId: res.equipmentNameId,
            itemCategoryId:res.itemCategoryId,
            priorityId: res.priorityId,
            maintenanceAuthorityId:res.maintenanceAuthorityId,
            statusOfJobId: res.statusOfJobId,
            byWhomId: res.byWhomId,
            dateOfDefect: res.dateOfDefect,
            descriptionOfDefect: res.descriptionOfDefect,
            spareRequisitionId:res.spareRequisitionId,
            workOrderNo: res.workOrderNo,
            workOrderDate: res.workOrderDate,
            workStartDate: res.workStartDate,
            refDocument: res.refDocument,
            spareRequisition: res.spareRequisition,
            completedStatus: res.completedStatus,
         //   workCompletedDate: res.workCompletedDate,
            isActive: res.isActive,
            partNo:res.partNo,
            issueRegisterId: res.issueRegisterId,
            itemDetailId: res.itemDetailId,
          }); 
          console.log("response");
          console.log(res);
          this.onDepartmentSelectionChange();
          this.onEquipmentSelectionChange(res.issueRegisterId);
          this.getPartNoOnUpdate();
        }
      );
    } else {
      this.pageTitle = 'Create  Corrective';
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
      
      correctiveMaintenenceId: [0],
      demandId: [''],
      itemDetailId: [''],
      itemCategoryId:[],
      unitId:[],
      procurementStatusId: [''],
      principalNameId: [''],
      issueRegisterId:[''],
      equipmentNameId:[''],
      priorityId:[''],
      refDocument:[''],
      doc:[''],
      byWhomId:[''],
      workStartDate:[''],
      statusOfJobId:[''],
      maintenanceAuthorityId:[''],
      descriptionOfDefect:[''],
      workOrderNo:[''],
      dateOfDefect:[''],
      manufactureId:[''],
      cstTecId: [''],
      supplierId:[''],
      procurementTypeId:[''],
      departmentNameId: [],
      sparesCategoryId: [''],
      tenderNumber: [''],
      workOrderDate:[],
      completedStatus:[''],
      spareRequisitionId:[''],
      partNo:[''],
    //  dateOfTenderFloat: [],
      procurementCompleteStatus:[0],
    //  tenderopeningDate: [],
      lpRequisitionNo:[''],
      tenderNotice: [''],
      notice:[''],
      tenderSpecification: [''],
      workCompletedDate:[''],
      financialApproval: [''],
      approval:[''],
      workOrder: [''],
      order:[''],
      //dateOfDelivery: [],
      unitPrice: [''],
      qty: [''],
      sftQty: [0],
      remarks: [''],
      // partNo:[''],
      procurementDocument: [''],
      spareRequisition:[''],
     // spareRequisitionId:[''],
     // equipmentNameId:[''],
      //status: [],
     // menuPosition: [],
      isActive: [true],
      ItemStoreList: this.fb.array([
        this.createIssueRegisterData()
      ]),
    })
    this.getSelectedPriority();
    this.getSelectedMaintenenceAuthority();
    this.getSelectedByWhoms();
    this.getSelectedStatusOfJob();
    this.getSelectedSpareRequisition();
  }
  
  getControlLabel(index: number, type: string) {
    return (this.CorrectiveForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  }

  private createIssueRegisterData() {

    return this.fb.group({
      deno: [''],
      itemDetail: [''],
      dateOfDefect:[''],
      descriptionOfDefect:[''],
      workOrderNo:[''],
      workStartDate:[''],
      statusOfJob:[''],
      workCompletedDate:[''],
      partNo: [''],
      returnStatusId:[''],
      itemCategoryId:[''],
      refDocument:[''],
      part:[''],
      storeQty:[''],
      itemSerNo: [''],
      issuedQty: [''],
      toolsLocationId:[''],
      status: [''],
      itemDetailId: [''],
      itemStoreId: [''],
      departmentNameId: [''],
      sparesCategoryId: [''],
      isRefundable: [false],
      isChecked: [false],
      totalReceivedQty: [''],
      availableQty: [''],
      returnQty: [''],
      returnQtyUpdate: [''],
      itemReceivedDate: [''],
      issueTypeId:[''],
      nameOfItem:[''],
      warrantyEndDate: [],
      dateOfExpiry:[''],
      acctStore: [''],
      spareRequisitionId:[''],
      returnBy:[''],
      issueQty:[''],
      issueDate:[''],
      itemName:[''],
      returnDate:[''],
      completedStatus:[''],
     // workCompletedDate:[''],
      issueRegisterId:[''],
      correctiveMaintenenceId:[''],
      spareRequisition:[''],
      unitId:[''],
      statusOfJobId:[''],
      equipmentNameId:['']
    });
  }

  getSelectedPriority(){
    this.CorrectiveMaintenenceService.getSelectedPriority().subscribe(res => {
      this.selectedPriority = res;
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
      this.selectedStatusOfJob = res.filter(x=>x.value !=9);
    });
  }

  getSelectedSpareRequisition(){
    this.CorrectiveMaintenenceService.getSelectedSpareRequisition().subscribe(res => {
      this.selectedSpareRequisition = res;
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

   // this.id = row.id;
   
   
    console.log("datatatatattatata");
    console.log(data);
   
       this.isShownStockList=true;
       var unitId=data.value.unitId;
       var departmentNameId=data.value.departmentNameId;
       var itemCategoryId =data.value.itemCategoryId;
       var equipmentNameId =data.value.equipmentNameId;
       console.log(unitId+'-'+departmentNameId+'-'+equipmentNameId)

      this.CorrectiveMaintenenceService.getSpareStockByParameterSpRequest(unitId,departmentNameId,equipmentNameId).subscribe(res => {
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
    data.value.statusOfJobId=9;
// console.log(data.value)
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


     this.CorrectiveForm.get('unitId').setValue(this.unitId);

     this.CorrectiveMaintenenceService.getSelectedEquipmentNameByParameterRequest(this.unitId,this.departmentId).subscribe(res => {
      this.selectedEquipment = res;
      console.log("Department Name Id----------");
      console.log(this.selectedEquipment);
    });

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


  getPartNoOnUpdate(){
    var issueRegisterId=this.CorrectiveForm.value['issueRegisterId'];
    console.log("issuee--------------");
    console.log(issueRegisterId);
    this.IssueRegisterService.find(issueRegisterId).subscribe(res => {
      this.partNo = res.partNO;
    });
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

     this.CorrectiveMaintenenceService.getCorrectiveMaintenences(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.unitId,this.departmentId,issueRegisterId).subscribe(response => {
      console.log("dddddd");
      this.dataSource.data = response.items;
      this.clearList();
      this.getItemStoreListonClick();
      console.log(this.dataSource.data )
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
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
    }
    console.log('12');
    this.CorrectiveForm.patchValue({ ItemStoreList: this.dataSource.data });
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
        this.demandId = dropdown.source.value;
        this.ProcurementService.getSelectedRefNo(this.demandId).subscribe(res => {
        this.selectedRefNo = res[0].text;
      });
      this.DemandService.find(this.demandId).subscribe(res=>{
        this.itemDetailId=res.itemDetailId;
        console.log("Item Detail Id");
        console.log(this.itemDetailId);
        this.CorrectiveForm.get('itemDetailId').setValue(this.itemDetailId);   
      });
        this.isShownList=true;
        this.DemandService.GetselectedDemandById(this.demandId).subscribe(res=>{
        this.demandData=res;
        var demandId = this.CorrectiveForm.value['demandId'];
        this.getPartNoPassItemCategoryIdInProcurement(demandId)
      });


   }
  }

  getSelectedItemCategory() {
    this.ProcurementService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  getselectedDemands(){
    this.departmentNameId = this.CorrectiveForm.value['departmentNameId'];
    this.isShown=true;
    //console.log("ffffffffffffffffffffffffffvvvfff");

    this.ProcurementService.getselectedDemands(this.departmentNameId, this.masterData.sparescategory.spares).subscribe(res=>{
    
      this.selectedPartNo=res;
    });
    this.getProcurementsList(this.departmentNameId);
    
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
  // getselectedLocalAgents(){
  //   this.ProcurementService.getselectedLocalAgents().subscribe(res=>{
  //     this.localAgent=res;
  //   }); 
  // }
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



  getselectedDemandsOnUpdate(id){
    this.ProcurementService.getselectedDemandsOnUpdate(id, this.masterData.sparescategory.spares).subscribe(res=>{
      this.selectedPartNo=res;
      //console.log(this.selectedPartNo)
    });
  }

  getselectedCstTecs(){
    this.ProcurementService.getselectedCstTecs().subscribe(res=>{
      this.cstTec=res;
    }); 
  }
  getPartNoPassItemCategoryIdInProcurement(itemDetailId: number) {
    this.ProcurementService.getPartNoPassItemCategoryIdInProcurement(itemDetailId).subscribe(res => {
      this.selectedPartNo = res;
    });
  }

  getDemandData(){
    var demandId = this.CorrectiveForm.value['demandId'];
    console.log(demandId+"-----------------");
    this.isShownList=true;
    this.DemandService.GetselectedDemandById(demandId).subscribe(res=>{
      this.demandData=res;
      console.log("demand data");
      console.log(this.demandData);
      var demandId = this.CorrectiveForm.value['demandId'];
      this.getPartNoPassItemCategoryIdInProcurement(demandId)
      console.log(demandId)
      console.log("demand result "+this.demandData)
    });
  }
  getDateColor(dateFrom:any){
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    var date1 = new Date(dateFrom); 
	  var date2 =  new Date(this.currentDateTime);
    if(date2 >= date1){
      return 'red';
    }else{
      return 'black';
    }
    
  }
  oncstTecYes(dropdown){

    if(dropdown.isUserInput) {
      this.cstTecYes=dropdown.source.value;
      console.log(this.cstTecYes)
    }
  }

  getProcurementsList(departmentId) {
    this.isLoading = true;
    
    this.ItemDetailService.findBaseSchoolNameId(this.departmentNameId).subscribe(res=>{
      console.log("ffffffffffffffffffffffffffff");
      console.log(res);
      this.unitId=res.thirdLevel;
     this.CorrectiveForm.get('unitId').setValue(this.unitId);

     this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentNameId).subscribe(res => {
      this.selectedEquipment = res;
    });     
   });
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.CorrectiveForm.get('departmentNameId').value;
    console.log(departmentId);
  }
  

  onSubmit() {
    const id = this.CorrectiveForm.get('correctiveMaintenenceId').value;   
    console.log(id);

    this.CorrectiveForm.get('workOrderDate').setValue((new Date(this.CorrectiveForm.get('workOrderDate').value)).toUTCString()) ;
    this.CorrectiveForm.get('dateOfDefect').setValue((new Date(this.CorrectiveForm.get('dateOfDefect').value)).toUTCString()) ;
    this.CorrectiveForm.get('workStartDate').setValue((new Date(this.CorrectiveForm.get('workStartDate').value)).toUTCString()) ;
   // this.CorrectiveForm.get('workCompletedDate').setValue((new Date("")).toUTCString()) ;
   
    console.log(this.CorrectiveForm.value)
    // var datestr = (new Date(this.CorrectiveForm.get('workCompletedDate')).toUTCString());
    // formdata.append("start", datestr);

    const formData = new FormData();
    for (const key of Object.keys(this.CorrectiveForm.value)) {
      const value = this.CorrectiveForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
    // this.CorrectiveForm.get('workCompletedDate').setValue((new Date(this.CorrectiveForm.get('workCompletedDate').value)).toUTCString()) ;
        if (result) {
          this.CorrectiveMaintenenceService.update(+id,formData).subscribe(response => {
             this.router.navigateByUrl('/maintenence-planning/add-corrective');
         //   this.reloadCurrentRoute();
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
      this.CorrectiveMaintenenceService.submit(formData).subscribe(response => {
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
  deleteItem(correctiveMaintenenceId) {
    const id = correctiveMaintenenceId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.CorrectiveMaintenenceService.delete(id).subscribe(() => {
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
