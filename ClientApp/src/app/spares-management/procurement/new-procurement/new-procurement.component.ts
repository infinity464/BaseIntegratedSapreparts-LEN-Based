import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../service/Procurement.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from '../../../basic-setup/service/DepartmentName.service';
import { DemandService } from '../../../spares-management/service/Demand.service';
import { Demand } from '../../models/Demand';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../../models/Procurement';
import { DatePipe } from '@angular/common';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ItemDetailService } from '../../service/itemDetail.service';
import { AcceptanceService } from '../../service/Acceptance.service';

@Component({
  selector: 'app-new-procurement',
  templateUrl: './new-procurement.component.html',
  styleUrls: ['./new-procurement.component.sass']
})
export class NewProcurementComponent implements OnInit {
  pageTitle: string;
  masterData = MasterData;
  destination:string;
  btnText:string;
  ProcurementForm: FormGroup;
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
  demandData: Demand[];
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

  departmentId:any;
  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedDepColumns: string[] = [ 'ser','itemName','demand', 'deno', 'qty','lpRequisitionNo','procurementStatusForStatusDropdown','supplier','procurementDocument',  'actions'];
  dataSource: MatTableDataSource<Procurement> = new MatTableDataSource();
  displayedColumns: string[] = ['sl', 'itemName','partNo', 'imcNumber', 'deno','demandQty'];

  constructor(private snackBar: MatSnackBar,private AcceptanceService: AcceptanceService,private authService: AuthService,private ItemDetailService: ItemDetailService,private datepipe: DatePipe,private DemandService:DemandService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
    const id = this.route.snapshot.paramMap.get('procurementId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Procurement';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementService.find(+id).subscribe(
        res => {
          this.ProcurementForm.patchValue({          
            procurementId: res.procurementId,
            itemDetailId: res.itemDetailId,
            itemCategoryId:res.itemCategoryId,
            equipmentNameId:res.equipmentNameId,
            demandId:res.demandId,
            supplierId: res.supplierId,
            lpRequisitionNo:res.lpRequisitionNo,
            partOfShipmentId: res.partOfShipmentId,
            departmentNameId: res.departmentNameId,
            tenderNumber: res.tenderNumber,
            dateOfTenderFloat: res.dateOfTenderFloat,
            tenderopeningDate: res.tenderopeningDate,
            workOrderDate:res.workOrderDate,
            tenderNotice: res.tenderNotice,
            tenderSpecification: res.tenderSpecification,
            financialApproval: res.financialApproval,
            workOrder: res.workOrder,
            dateOfDelivery: res.dateOfDelivery,
            unitPrice: res.unitPrice,
            procurementTypeId:res.procurementTypeId,
            procurementStatusId:res.procurementStatusId,
            qty: res.qty,
            remarks: res.remarks,
            procurementDocument: res.procurementDocument,
            isActive: res.isActive
          }); 
          this.demandId = res.demandId
          console.log("res");
          console.log(res);
          this.getDemandNoForUpdate();
          this.onDemandSelectionChange();
          this.getselectedDemandsOnUpdate(res.departmentNameId);
          this.getDemandData();
        }
      );
    } else {
      this.pageTitle = 'Create  Procurement';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ProcurementForm.get('departmentNameId').setValue(this.branchId);
     
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      console.log(this.branchId+" ggg")
      
      this.ProcurementForm.get('unitId').setValue(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
    }
  }
  intitializeForm() {
    this.ProcurementForm = this.fb.group({
      
      procurementId: [0],
      demandId: [''],
      itemDetailId: [''],
      itemCategoryId:[],
      equipmentNameId:[],
      unitId:[],
      procurementStatusId: [''],
      principalNameId: [''],
      manufactureId:[''],
      cstTecId: [''],
      supplierId:[''],
      procurementTypeId:[''],
      departmentNameId: [],
      sparesCategoryId: [''],
      tenderNumber: [''],
      workOrderDate:[],
      dateOfTenderFloat: [],
      procurementCompleteStatus:[0],
      tenderopeningDate: [],
      lpRequisitionNo:[''],
      tenderNotice: [''],
      notice:[''],
      tenderSpecification: [''],
      financialApproval: [''],
      approval:[''],
      workOrder: [''],
      order:[''],
      dateOfDelivery: [],
      unitPrice: [''],
      qty: [''],
      sftQty: [0],
      remarks: [''],
      procurementDocument: [''],
      doc:[''],
      isActive: [true]
    })
   // this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedItemDetails();
    this.getselectedPrincipalNames();
    this.getselectedManufacture();
    this.getselectedSupplier();
    this.getselectedSupplierA();
    this.getselectedPartOfShipments();
    this.getselectedSupplierM();
    this.getselectedCstTecs();
    this.getPartNoForProcurement();
    this.getSelectedItemCategory();
    this.getSelectedProcurementType();
    this.getSelectedProcurementStatus();
  }

  getDemandNoForUpdate(){
    var demandId = this.ProcurementForm.value['demandId'];
    this.ProcurementForm.get('demandId').setValue(demandId); 
    this.ProcurementService.getSelectedDemandById(demandId).subscribe(res => {
      this.selectedDemandNo = res;
    });
  }

  onDepartmentSelectionChange() {
    //if (dropdown.isUserInput) {
    //this.isShown=true;
    this.departmentId =  this.ProcurementForm.value['departmentNameId'];; 
    console.log("Department Id");
    console.log(this.departmentId);
    // this.departmentId = this.AcceptanceForm.value['departmentNameId']; 
    // this.getPartNoByDepartmentNameId(this.departmentId);
    // this.getDemandsList(this.departmentId);

    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;
      console.log("UnitID");
    console.log(this.unitId);


     this.ProcurementForm.get('unitId').setValue(this.unitId);

   
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
      this.ProcurementForm.patchValue({
        doc: file,
      });
    }
  }
  // onFileNChanged(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //    //console.log(file);
  //     this.ProcurementForm.patchValue({
  //       notice: file,
  //     });
  //   }
  // }
  // onFileFChanged(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //    //console.log(file);
  //     this.ProcurementForm.patchValue({
  //       approval: file,
  //     });
  //   }
  // }
  // onFileWChanged(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //    //console.log(file);
  //     this.ProcurementForm.patchValue({
  //       order: file,
  //     });
  //   }
  // }

 onSpareCategorySelectionChange(){
  //  if (dropdown.isUserInput) {
      this.isShown=true;
      // console.log("gggggggggggg");
        this.itemCategoryId = this.ProcurementForm.value['itemCategoryId'];
        this.ProcurementForm.get('itemCategoryId').setValue(this.itemCategoryId);
        this.departmentNameId = this.ProcurementForm.value['departmentNameId'];
        this.unitId = this.ProcurementForm.value['unitId'];
        // console.log("ggggggggggggggggggggppppppppppp");
        // console.log(this.unitId);

        this.ProcurementService.getSelectedDemandNoByStatus(this.unitId,this.departmentNameId,this.itemCategoryId,this.masterData.itemStatus.itemStatusProcurement,0).subscribe(res => {
          this.selectedDemandNo = res;
          console.log("Selected Demand");
          console.log(this.selectedDemandNo);
        });
       this.getProcurementList();
   //  }
  }

  getProcurementList(){
    this.ProcurementService.getProcurementListByDepartmentNameId(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.unitId,this.departmentNameId,this.itemCategoryId).subscribe(response => {
   //   console.log("dddddd");
      this.dataSource.data = response.items;
      
      console.log(this.dataSource.data )
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }
  // getSelectedDemandNo(){
   
  // }
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
  // onUploadClick(value){

  // }
  onDemandSelectionChange(){
    //if (dropdown.isUserInput) {
        this.demandId = this.ProcurementForm.value['demandId'];
        this.ProcurementService.getSelectedRefNo(this.demandId).subscribe(res => {
        this.selectedRefNo = res[0].text;
      });
      this.DemandService.find(this.demandId).subscribe(res=>{
        this.itemDetailId=res.itemDetailId;
        console.log("Item Detail Id");
        console.log(res.equipmentNameId);
        console.log(this.itemDetailId);
        this.ProcurementForm.get('itemDetailId').setValue(this.itemDetailId);   
        this.ProcurementForm.get('equipmentNameId').setValue(res.equipmentNameId);   
      });
        this.isShownList=true;
        this.DemandService.GetselectedDemandById(this.demandId).subscribe(res=>{
        this.demandData=res;
        var demandId = this.ProcurementForm.value['demandId'];
        this.getPartNoPassItemCategoryIdInProcurement(demandId)
      });


  // }
  }

  getSelectedItemCategory() {
    this.ProcurementService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  getselectedDemands(){
    this.departmentNameId = this.ProcurementForm.value['departmentNameId'];
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
      //console.log(this.filteredOptions);
    });
  }

  getDemandData(){
    var demandId = this.ProcurementForm.value['demandId'];
    console.log(demandId+"-----------------");
    this.isShownList=true;
    this.DemandService.GetselectedDemandById(demandId).subscribe(res=>{
      this.demandData=res;
      console.log("demand data equipmentNameId");
      console.log(this.demandData);
      var demandId = this.ProcurementForm.value['demandId'];
      this.getPartNoPassItemCategoryIdInProcurement(demandId)
      console.log(demandId)
      console.log("demand result "+this.demandData)
    });
  }
  getDateColor(dateFrom:any){
    //Date dateTime11 = Convert.ToDateTime(dateFrom);  
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
      //this.getProcurementsList(dropdown.source.value);
      this.cstTecYes=dropdown.source.value;
      console.log(this.cstTecYes)
    }
  }
  // getProcurementList()
  // {
    
  // }
  getProcurementsList(departmentId) {
    this.isLoading = true;
    
    this.ItemDetailService.findBaseSchoolNameId(this.departmentNameId).subscribe(res=>{
      console.log("ffffffffffffffffffffffffffff");
      console.log(res);
      this.unitId=res.thirdLevel;
     this.ProcurementForm.get('unitId').setValue(this.unitId);

     this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentNameId).subscribe(res => {
      this.selectedEquipment = res;
    });
     // console.log("value");
     // console.log(this.unitId);      
   });
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.ProcurementForm.get('departmentNameId').value;
    console.log(departmentId);
    this.getProcurementList();
  //  this.getProcurementsList(departmentId);
  }
  
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.ProcurementForm.get('procurementId').value;   
    this.ProcurementForm.get('dateOfTenderFloat').setValue((new Date(this.ProcurementForm.get('dateOfTenderFloat').value)).toUTCString()) ;
    this.ProcurementForm.get('tenderopeningDate').setValue((new Date(this.ProcurementForm.get('tenderopeningDate').value)).toUTCString()) ;
    this.ProcurementForm.get('workOrderDate').setValue((new Date(this.ProcurementForm.get('workOrderDate').value)).toUTCString()) ;
    this.ProcurementForm.get('dateOfDelivery').setValue((new Date(this.ProcurementForm.get('dateOfDelivery').value)).toUTCString()) ;
    
    console.log(this.ProcurementForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.ProcurementForm.value)) {
      const value = this.ProcurementForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementService.update(+id,formData).subscribe(response => {
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
      this.ProcurementService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/spares-management/procurement-list');
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
        this.ProcurementService.delete(id).subscribe(() => {
          this.getProcurementList();
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
