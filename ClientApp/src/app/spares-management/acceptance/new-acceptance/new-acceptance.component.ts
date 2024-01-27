import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AcceptanceService } from '../../service/Acceptance.service';
import { ProcurementService } from '../../service/Procurement.service';
import { DemandService } from '../../service/Demand.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Procurement } from '../../models/Procurement';
import { MasterData } from 'src/assets/data/master-data';
import { Demand } from '../../models/Demand';
import { MatTableDataSource } from '@angular/material/table';
import { Acceptance } from '../../models/Acceptance';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ItemDetailService } from '../../service/itemDetail.service';


@Component({
  selector: 'app-new-acceptance',
  templateUrl: './new-acceptance.component.html',
  styleUrls: ['./new-acceptance.component.sass']
})
export class NewAcceptanceComponent implements OnInit {
  pageTitle: string;
  destination: string;
  masterData = MasterData;
  btnText: string;
  demandId: number;
  itemDetailId: number;
  sparesCategoryId: number;
  demandAuthorityId: number;
  AcceptanceForm: FormGroup;
  validationErrors: string[] = [];
  selectedItemDetails: SelectedModel[];
  selectedSourceOfSupplys: SelectedModel[];
  selectedManufactures: SelectedModel[];
  selectedPrincipalNames: SelectedModel[];
  selectedPlaceOfDeliverys: SelectedModel[];
  selectedConditionOfItem: SelectedModel[];
  selectedItemInspections: SelectedModel[];
  selectedOpinion:SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedDemandNo:SelectedModel[];
  selectedPartNo: SelectedModel[];
  selectedItemCategory:SelectedModel[];
  selectedProcurementQty:SelectedModel[];
  isShown: boolean = false;
  isReason: boolean = false;
  itemCategoryId: number;
  procurementData: Procurement[];
  selectedDate:any;
  selectedDemandQty:any;
  procurementId:any;

  isLoading = false;
  searchText = "";
  departmentId:any;
  unitId:any;

  demandData: Demand[];

  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedColumns: string[] = ['ser', 'demand','itemInspection', 'opinion', 'acceptanceDate','sftQty','acceptanceDocument', 'actions'];
  dataSource: MatTableDataSource<Acceptance> = new MatTableDataSource();
  demandColumns: string[] = ['sl', 'demandAuthority', 'deno', 'demandQty', 'demandDate', 'fiscalYear'];
  procurementColumns: string[] = ['sl','itemDetail', 'itemName', 'qty', 'remarks'];

  constructor(private snackBar: MatSnackBar,private ItemDetailService: ItemDetailService,private authService: AuthService, private ProcurementService: ProcurementService, private DemandService: DemandService, private confirmService: ConfirmService, private AcceptanceService: AcceptanceService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('acceptanceId');
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit SFT';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.AcceptanceService.find(+id).subscribe(
        res => {
          this.AcceptanceForm.patchValue({

            acceptanceId: res.acceptanceId,
            //procurementId: res.procurementId,
            demandId: res.demandId,
            //itemDetailId: res.itemDetailId,
            nsdLetterNo:res.nsdLetterNo,
            opinionId:res.opinionId,
            reason:res.reason,
            itemCategoryId:res.itemCategoryId,
            departmentNameId: res.departmentNameId,
            acceptanceDate:res.acceptanceDate,
            //procurementStatusId:res.procurementStatusId,
            sourceOfSupplyId: res.sourceOfSupplyId,
            //sparesCategoryId: res.sparesCategoryId,
            manufactureId: res.manufactureId,
            principalNameId: res.principalNameId,
            placeOfDeliveryId: res.placeOfDeliveryId,
            demandAuthorityId: res.demandAuthorityId,
            //conditionOfItemId: res.conditionOfItemId,
            ItemInspectionId: res.itemInspectionId,
            sftDate: res.sftDate,
            sftLetterNo: res.sftLetterNo,
            workOrderNo: res.workOrderNo,
            workOrderDate: res.workOrderDate,
            sftQty: res.sftQty,
            //qty:res.qty,
            //procurementQty:res.procurementQty,
            itemSerNo: res.itemSerNo,
            model: res.model,
            brand: res.brand,
            warranty: res.warranty,
            warrantyFrom:res.warrantyFrom,
            warrantyTo:res.warrantyTo,
            deliveryDate: res.deliveryDate,
            inspectionDate: res.inspectionDate,
            purchasePrice: res.purchasePrice,
            dateOfManufacture: res.dateOfManufacture,
            acDocument: res.acDocument,
            arcDocument: res.arcDocument,
            cofcDocument: res.cofcDocument,
            othersDocument: res.othersDocument,
            sftRegPage: res.sftRegPage,
            acceptanceDocument: res.acceptanceDocument,
            docVerification: res.docVerification,
           // itemInspectionId:res.itemInspectionId,
            sftStatus: res.sftStatus,
            remarks:res.remarks,

          });
          this.getDemandNoForUpdate();
          this.onDemandSelectionChange();
          this.getselectedProcurementsOnUpdate(res.departmentNameId);
          this.getProcurementData();

          console.log("response");
          console.log(res);
        }
      );
    } else {
      this.pageTitle = 'Create SFT';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.AcceptanceForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
     // this.onDepartmentSelectionChange(this.branchId)
      // this.getselectedProcurements();
      // console.log("depat");
      // console.log(this.branchId);
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.AcceptanceForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }

    // if(this.role == this.userRole.SuperAdmin){

    //   console.log("admin block superadmin")
      
    //   this.AcceptanceForm.get('unitId').setValue(this.branchId);
    //   console.log(this.branchId)
    //   this.getSelectedDepartmentName(this.branchId);
    // }
    this.getselectedItemDetails();
    this.getselectedSourceOfSupplys();
    this.getselectedManufactures();
    this.getselectedPrincipalNames();
    this.getselectedPlaceOfDeliverys();
    this.getselectedConditionOfItem();
    this.getSelectedItemInspection();
    //this.GetDepartmentNameById(this.masterData.baseSchoolDept.submarine);
    
    this.getSelectedItemCategory();
    this.getSelectedItemInspection();
    this.getSelectedOpinion();
    this.onDepartmentSelectionChange();
    // this.getselectedDemandAuthority();
  }
  intitializeForm() {
    this.AcceptanceForm = this.fb.group({
      acceptanceId: [0],
      procurementId: [''],
      demandId: [],
      doc: [''],
      itemDetailId: [''],
      nsdLetterNo:[''],
      opinionId:[''],
      acceptanceDate:[''],
      itemCategoryId:[''],
      departmentNameId: [],
      unitId:[''],
      procurementStatusId: [''],
      ItemInspectionId:[''],
      reason:[''],
      warrenty:[''],
      //sourceOfSupplyId: [],
      sparesCategoryId: [''],
      //manufactureId: [],
      //principalNameId: [],
      //placeOfDeliveryId: [],
      //demandAuthorityId: [],
      conditionOfItemId: [''],
      // sftDate: [],
      sftLetterNo: [''],
      workOrderNo: [''],
      //workOrderDate: [''],
      sftQty: [''],
      qty:[''],
      storeQtyStatus: [0],
      storeQty: [0],
      ProcurementQty: [''],
      itemSerNo: [''],
      model: [''],
      brand: [''],
      warranty: [''],
      // warrantyFrom:[],
      // warrantyTo:[],
      //deliveryDate: [''],
      //inspectionDate: [''],
      purchasePrice: [''],
      //dateOfManufacture: [''],
      acDocument: [''],
      arcDocument: [''],
      cofcDocument: [''],
      othersDocument: [''],
      sftRegPage: [''],
      acceptanceDocument: [''],
      docVerification: [''],
      sftStatus: [0],
      remarks:[''],
      isActive: [true],

    })
  }
  getSelectedDepartmentName(fourthLevel){
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
    }); 
  }
  onDepartmentSelectionChange() {
    //if (dropdown.isUserInput) {
    //this.isShown=true;
    this.departmentId =  this.AcceptanceForm.value['departmentNameId'];; 

    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.AcceptanceForm.get('unitId').setValue(this.unitId);  
   });
  }
  getSelectedOpinion(){
    this.AcceptanceService.getSelectedOpinion().subscribe(res => {
      this.selectedOpinion = res;
    });
  }
  getSelectedItemInspection(){
    this.AcceptanceService.getSelectedItemInspection().subscribe(res => {
      this.selectedItemInspections = res;
    });
  }
  onOpinionSelectionChange(dropdown){
    if (dropdown.isUserInput) {
      var value =dropdown.source.value;
      if(value ===4){
        this.isReason=true;
      }
     else if(value ===1){
        this.isReason=false;
      }
    }
  }
  onDemandSelectionChange(){
   // if (dropdown.isUserInput) {
      //this.isShown=true;
     // this.unitId = 
      var demandId=this.AcceptanceForm.value['demandId'];
      this.unitId=this.AcceptanceForm.value['unitId'];
      this.departmentId=this.AcceptanceForm.value['departmentNameId'];
      this.itemCategoryId=this.AcceptanceForm.value['itemCategoryId'];
        console.log("demand Id--------------");
        console.log(demandId);

        // this.DemandService.find(this.demandId).subscribe(res=>{
        //   this.itemDetailId=res.itemDetailId;
        //   this.ItemStorForm.get('itemDetailId').setValue(this.itemDetailId);
        // });

      // this.ProcurementService.find(demandId).subscribe(res=>{
      //   this.procurementId=res.procurementId;
      //   this.AcceptanceForm.get('procurementId').setValue(this.procurementId);
      // });

      console.log("Department Name Id");
      console.log(this.departmentId);
      console.log("Unit Id");
      console.log(this.unitId);

      console.log("Item Category Id");
      console.log(this.itemCategoryId);

      console.log("demandId");
     // console.log(this.selectedDate);

      this.AcceptanceService.getSelectedDemandDate(this.unitId,this.departmentId,this.itemCategoryId,demandId).subscribe(res => {
        this.selectedDate = res[0].text;
        // var date=this.selectedDate;
         console.log(this.selectedDate);
        console.log("0000000");
      });

      this.AcceptanceService.getSelectedDemandQty(this.unitId,this.departmentId,this.itemCategoryId,demandId).subscribe(res => {
        this.selectedDemandQty = res[0].text;
      //  console.log(this.selectedDate);
      });

      this.AcceptanceService.getSelectedProcurementQty(this.unitId,this.departmentId,this.itemCategoryId,demandId).subscribe(res => {
        this.selectedProcurementQty = res[0].text;
        console.log(this.selectedDate);
      });

    //  getAcceptanceList(departmentId) {
        // this.isLoading = true;
        // this.AcceptanceService.getAcceptanceList(this.paging.pageIndex, this.paging.pageSize, this.searchText,this.departmentId, this.itemCategoryId, this.unitId).subscribe(response => {
    
        //   this.dataSource.data = response.items;
        //   //console.log("dddddd");
        //   console.log(this.dataSource.data)
        //   this.paging.length = response.totalItemsCount
        //   this.isLoading = false;
        // })
     // }
      // console.log("demandId");
      // console.log(this.selectedDate);
      // this.demandId = dropdown.source.value;
      // this.ProcurementService.getSelectedRefNo(this.demandId).subscribe(res => {
      //   this.selectedRefNo = res[0].text;
      //   console.log("selected");
      //   console.log(this.selectedRefNo);
      // });
 //  }
  }
  getDemandNoForUpdate(){
    var demandId = this.AcceptanceForm.value['demandId'];
    this.AcceptanceForm.get('demandId').setValue(demandId); 
    this.ProcurementService.getSelectedDemandById(demandId).subscribe(res => {
      this.selectedDemandNo = res;
    });
  }

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.AcceptanceForm.patchValue({
        doc: file,
      });
    }
  }
  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
      this.itemCategoryId=dropdown.source.value;
      this.AcceptanceService.getSelectedDemandNoByStatus(this.unitId,this.departmentId,this.itemCategoryId,this.masterData.itemStatus.itemStatusSFT,0).subscribe(res => {
        this.selectedDemandNo = res;
        console.log("Selected Demand");
        console.log(this.selectedDemandNo);
      });

      console.log("item Category Id");
      console.log(this.itemCategoryId);
      this.isShown=true;
      this.isLoading = true;
      this.AcceptanceService.getAcceptanceList(this.paging.pageIndex, this.paging.pageSize, this.searchText,this.departmentId, this.itemCategoryId, this.unitId).subscribe(response => {
  
        this.dataSource.data = response.items;
        console.log("dddddd");
        console.log(this.dataSource.data)
        this.paging.length = response.totalItemsCount
        this.isLoading = false;
      })

      // console.log("gggggggggggg");
      //   this.itemCategoryId = dropdown.source.value;
      //   this.AcceptanceForm.get('itemCategoryId').setValue(this.itemCategoryId);
      //   this.departmentNameId = this.AcceptanceForm.value['departmentNameId'];
      //   this.unitId = this.AcceptanceForm.value['unitId'];
      //   console.log("ggggggggggggggggggggppppppppppp");
      //   console.log(this.unitId);

      //   this.ProcurementService.getSelectedDemandNo(this.unitId,this.departmentNameId,this.itemCategoryId).subscribe(res => {
      //     this.selectedDemandNo = res;
      //     console.log("Selected Demand");
      //     console.log(this.selectedDemandNo);
      //   });
      //  this.getProcurementList(this.itemCategoryId);
      
     }
  }

  getSelectedItemCategory() {
    this.DemandService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  getPartNoPassItemCategoryIdInAcceptance(itemDetailId: number) {
    this.AcceptanceService.getPartNoPassItemCategoryIdInAcceptance(itemDetailId).subscribe(res => {
      this.selectedPartNo = res;
      //console.log(this.filteredOptions);
    });
  }

  getProcurementData() {
    var procurementId = this.AcceptanceForm.value['procurementId'];
    this.ProcurementService.GetselectedProcurementById(procurementId).subscribe(res => {
      this.procurementData = res;
      var procurementId = this.AcceptanceForm.value['procurementId'];
      this.getPartNoPassItemCategoryIdInAcceptance(procurementId)
      console.log(procurementId)
      console.log("res")
      console.log(res)

    });
    this.ProcurementService.find(procurementId).subscribe(res => {
      this.demandId = res.demandId;
      this.itemDetailId = res.itemDetailId;
      this.sparesCategoryId = res.sparesCategoryId;
      var procurementQty = res.qty;
      console.log("procurement qty" + procurementQty)
      this.AcceptanceForm.get('sparesCategoryId').setValue(this.sparesCategoryId);
      this.AcceptanceForm.get('demandId').setValue(this.demandId);
      this.AcceptanceForm.get('itemDetailId').setValue(this.itemDetailId);
      this.AcceptanceForm.get('ProcurementQty').setValue(procurementQty);
      //this.AcceptanceForm.get('itemCategoryId').setValue(res.itemCategoryId);
      this.DemandService.GetselectedDemandById(this.demandId).subscribe(res => {
        this.demandData = res;

        console.log("demand result " + this.demandData)
      });
      this.DemandService.find(this.demandId).subscribe(res => {
        this.demandAuthorityId = res.demandAuthorityId;
        //this.AcceptanceForm.get('demandAuthorityId').setValue(this.demandAuthorityId);
        
        this.isShown = true;
      });
    });




  }

  onDocChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      // this.labelImport.nativeElement.value = file.name;
      console.log('acceptance doc');
      console.log(file);
      // this.BIODataGeneralInfoForm.controls["picture"].setValue(event.target.files[0]);
      this.AcceptanceForm.patchValue({
        doc: file,
      });
    }
  }

  getselectedProcurementsOnUpdate(id) {
    this.AcceptanceService.getselectedProcurementsOnUpdate(id, this.masterData.sparescategory.spares).subscribe(res => {
      this.selectedPartNo = res;
      console.log(this.selectedPartNo)
    });
  }
  getselectedProcurements() {
    this.isShown=true;
    var departmentNameId = this.AcceptanceForm.value['departmentNameId'];
    console.log("yyy");
    console.log(departmentNameId);
    this.AcceptanceService.getselectedProcurements(departmentNameId, this.masterData.sparescategory.spares).subscribe(res => {
      this.selectedPartNo = res;
    });
    this.getAcceptancesList(departmentNameId);
  }
  getselectedItemDetails() {
    this.AcceptanceService.getselectedItemDetails().subscribe(res => {
      this.selectedItemDetails = res
      console.log(this.selectedItemDetails);
    });
  }
  // getselectedDepartmentNames() {
  //   this.AcceptanceService.getselectedDepartmentNames().subscribe(res => {
  //     this.selectedDepartmentNames = res
  //     console.log(this.selectedDepartmentNames);
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.AcceptanceService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getselectedSourceOfSupplys() {
    this.AcceptanceService.getselectedSourceOfSupplys().subscribe(res => {
      this.selectedSourceOfSupplys = res
      console.log(this.selectedSourceOfSupplys);
    });
  }
  getselectedManufactures() {
    this.AcceptanceService.getselectedManufactures().subscribe(res => {
      this.selectedManufactures = res
      console.log(this.selectedManufactures);
    });
  }
  getselectedPrincipalNames() {
    this.AcceptanceService.getselectedPrincipalNames().subscribe(res => {
      this.selectedPrincipalNames = res
      console.log(this.selectedPrincipalNames);
    });
  }
  getselectedPlaceOfDeliverys() {
    this.AcceptanceService.getselectedPlaceOfDeliverys().subscribe(res => {
      this.selectedPlaceOfDeliverys = res
      console.log(this.selectedPlaceOfDeliverys);
    });
  }
  // getselectedDemandAuthority(){
  //   this.AcceptanceService.getselectedDemandAuthority().subscribe(res=>{
  //     this.selectedDemandAuthority=res
  //     console.log(this.selectedDemandAuthority);      
  //   });
  // }
  getselectedConditionOfItem() {
    this.AcceptanceService.getselectedConditionOfItem().subscribe(res => {
      this.selectedConditionOfItem = res
      console.log(this.selectedConditionOfItem);
    });
  }
  // getselectedItemInspections() {
  //   this.AcceptanceService.getselectedItemInspections().subscribe(res => {
  //     this.selectedItemInspections = res
  //     console.log(this.selectedItemInspections);
  //   });
  // }
  
  getAcceptancesList(departmentId) {
    this.isLoading = true;
    this.AcceptanceService.getAcceptanceListByDepartmentNameId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.masterData.sparescategory.spares, departmentId).subscribe(response => {

      this.dataSource.data = response.items;
      //console.log("dddddd");
      console.log(this.dataSource.data)
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
    })
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    var departmentId = this.AcceptanceForm.get('departmentNameId').value;
    console.log(departmentId);
    this.getAcceptancesList(departmentId);
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  onSubmit() {
    const id = this.AcceptanceForm.get('acceptanceId').value;

    //this.AcceptanceForm.get('dateOfDelivery').setValue((new Date(this.AcceptanceForm.get('dateOfDelivery').value)).toUTCString()) ;
    // this.AcceptanceForm.get('sftDate').setValue((new Date(this.AcceptanceForm.get('sftDate').value)).toUTCString());
    // this.AcceptanceForm.get('warrantyFrom').setValue((new Date(this.AcceptanceForm.get('warrantyFrom').value)).toUTCString());
    // this.AcceptanceForm.get('warrantyTo').setValue((new Date(this.AcceptanceForm.get('warrantyTo').value)).toUTCString());
    this.AcceptanceForm.get('acceptanceDate').setValue((new Date(this.AcceptanceForm.get('acceptanceDate').value)).toUTCString());
    //this.AcceptanceForm.get('inspectionDate').setValue((new Date(this.AcceptanceForm.get('inspectionDate').value)).toUTCString());
    //this.AcceptanceForm.get('dateOfManufacture').setValue((new Date(this.AcceptanceForm.get('dateOfManufacture').value)).toUTCString());


    console.log(this.AcceptanceForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.AcceptanceForm.value)) {
      const value = this.AcceptanceForm.value[key];
      formData.append(key, value);
    }

    console.log(this.AcceptanceForm.value)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

        if (result) {
          this.AcceptanceService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/spares-management/add-acceptance');
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
      this.AcceptanceService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/spares-management/acceptance-list');
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
    const id = row.acceptanceId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.AcceptanceService.delete(id).subscribe(() => {
          //this.getAcceptances();
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
