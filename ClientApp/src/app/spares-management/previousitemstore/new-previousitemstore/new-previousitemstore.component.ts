import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemStorService } from '../../service/ItemStor.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ItemStor } from '../../models/ItemStor';
import { MasterData } from 'src/assets/data/master-data';
import { AcceptanceService } from '../../service/Acceptance.service';
import { Acceptance } from '../../models/Acceptance';
import { DemandService } from '../../service/Demand.service';
import { MatTableDataSource } from '@angular/material/table';
import { ProcurementService } from '../../service/Procurement.service';
import { Procurement } from '../../models/Procurement';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { Demand } from '../../models/Demand';
import { ItemDetailService } from '../../service/itemDetail.service';
// import * as _ from 'lodash';
//import * as _cloneDeep from 'lodash/cloneDeep'

@Component({
  selector: 'app-new-previousitemstore',
  templateUrl: './new-previousitemstore.component.html',
  styleUrls: ['./new-previousitemstore.component.sass']
})
export class NewPreviousItemStoreComponent implements OnInit {
  pageTitle: String;
  destination: String;
  btnText:String;
  masterData = MasterData;
  ItemStorForm: FormGroup;
  validationErrors: string[] = [];
  selectedItemCategory:SelectedModel[]; 
  selectedDeno:SelectedModel[]; 
  selectedSparesCategory:SelectedModel[];
  selectedConditionofItem:SelectedModel[];
  selectedToolsLocation:SelectedModel[];
  selectedLifeLimitItem:SelectedModel[];
  selectedAcctStore:SelectedModel[]; 
  selectedServiceLifeType:SelectedModel[]; 
  selectedEndLifeType:SelectedModel[]; 
  selectedOverhaulingTypes:SelectedModel[]; 
  selectedDepartmentNames:SelectedModel[]; 
  selectedPartNo:SelectedModel[]; 
  selectedDemandNo:SelectedModel[];
  selectedPartNoForProcurement:SelectedModel[];
  // selectedConditionOfItem:SelectedModel[];
  acceptanceByDepartmentAndCategory:Acceptance;
  demandId:any;
  isShown: boolean = false ;
  isShownList: boolean = false ;
  procurementData: Procurement[];
  demandData: Demand[];
  itemDetailId:any;
  acceptanceData: Acceptance[];
  isLoading = false;
  unitId:any;
  departmentId:any;
  denoid:any;
  equipmentNameId:any;
  receiveDate:Date;
  
  userRole = Role;
  itemCategoryId:any;
  traineeId:any;
  role:any;
  branchId:any;
  showPattOrModelDropdown=false;
  showIMC=false;
  selectedImc:any;
  selectedItemDetails:any;
  options = [];
  filteredOptions;


  parameters: FormGroup;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser','equipmentName','itemDetail','deno','storeQty', 'toolsLocation','itemReceivedDate', 'dateOfExpiry','otherDoc','actions'];
  dataSource: MatTableDataSource<ItemStor> = new MatTableDataSource();
  displayedColumnsDemand: string[] = ['sl', 'nameOfItem','partNo', 'imcNumber','demandQty','procurementQty','sftQty'];
  sftColumns: string[] = ['sl','itemDetail', 'sftQty','storeQty','demandDate', 'deliveryDate', 'outerLatterNo'];
  procurementColumns: string[] = ['sl','tenderNumber','dateOfDelivery', 'dateOfTenderFloat', 'cstTec', 'qty'];


  constructor(private snackBar: MatSnackBar,private itemDetailsService: ItemDetailService,private ProcurementService:ProcurementService,private DemandService: DemandService, private authService: AuthService,private procurementService: ProcurementService,private demandService: DemandService,private acceptanceService:AcceptanceService,private confirmService: ConfirmService,private ItemStorService: ItemStorService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('itemStorId');
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Previous Item Store';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ItemStorService.find(+id).subscribe(
        res => {
          this.ItemStorForm.patchValue({          
            itemStorId: res.itemStorId,
            acceptanceId: res.acceptanceId,
            procurementId: res.procurementId,
            demandId:res.demandId,
            denoId: res.denoId,
            storeQty:res.storeQty,
            plrNo: res.plrNo,
            countryOfOrigin:res. countryOfOrigin,
            manufacture:res.manufacture,
            dateOfexpiry:res.dateOfExpiry,
            departmentNameId: res.departmentNameId,
            itemCategoryId: res.itemCategoryId,
            serviceLifeTypeId: res.serviceLifeTypeId,
            sparesCategoryId: res.sparesCategoryId,
            conditionOfItemId:res.conditionOfItemId,
            toolsLocationId:res.toolsLocationId,
            endLifeTypeId: res.endLifeTypeId,
            acctStoreId: res.acctStoreId,
            overhaulingTypeId: res.overhaulingTypeId,
            retirementTypeId: res.retirementTypeId,
            itemDetailId: res.itemDetailId,
            itemSerNo: res.itemSerNo,
            icmNo: res.icmNo,
            shelfLife: res.shelfLife,
            endShalfLife: res.endShalfLife,
            warrantyStartDate: res.warrantyStartDate,
            warrantyEndDate: res.warrantyEndDate,
            itemReceivedDate: res.itemReceivedDate,
            totalReceivedQty: res.totalReceivedQty,
            issuedQty: res.issuedQty,
            demandQty: res.demandQty,
            demandDate: res.demandDate,
            letterOuterNo: res.letterOuterNo,
            refPoNo: res.refPoNo,
            tenderNumber: res.tenderNumber,
            dateOfTenderFloat: res.dateOfTenderFloat,
            tenderopeningDate: res.tenderopeningDate,
            tenderPublishDate: res.tenderPublishDate,
            tenderNotice: res.tenderNotice,
            location: res.location,
            serviceLife: res.serviceLife,
            endLifeTime: res.endLifeTime,
            accessories: res.accessories,
            stockRegisterPageNo: res.stockRegisterPageNo,
            retirmentLife: res.retirmentLife,
            remarks: res.remarks,
            arcDoc: res.arcDoc,
            cofcDoc: res.cofcDoc,
            otherDoc: res.otherDoc,
            oemDoc: res.oemDoc,
            status: res.status,
            isActive: res.isActive,
            permanentQty:res.permanentQty,
            tyQty:res.tyQty,
            surveyQty :res.surveyQty,
            repairQty:res.repairQty,
            shipFittedQty:res.shipFittedQty
          });  

          this.getDemandNoForUpdate();
          this.getselectedAcceptenceOnUpdate(res.departmentNameId);
          this.getAcceptanceData();   
          this.onSpareCategorySelectionChange(res); 
          this.onDemandSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Create Previous Item Store';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ItemStorForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.ItemStorForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    this.getselectedItemCategory();
    this.getselectedDeno();
    this.getselectedAcctStore();
    this.getselectedServiceLifeType();
    this.getselectedEndLifeType();
    this.getselectedOverhaulingTypes();
    this.getselectedSparesCategory();
    this.getselectedConditionOfItem();
    this.getselectedToolsLocations();
    this.getselectedLifeLimitItem();
    this.getSelectedConditionOfItem();
    this.getPartNoForProcurement();
  }
  intitializeForm() {
    this.ItemStorForm = this.fb.group({
      itemStorId: [0],
      demandId:[""],
      denoId: [],
      unitId:[''],
      departmentNameId: [],
      equipmentNameId:[''],
      itemCategoryId:[],
      conditionOfItemId:[''],
      lifeLimitItemId:[''],
      toolsLocationId:[''],
      manufacture:[''],
      countryOfOrigin:[''],
      plrNo:[''],
      itemDetailId:[],
      itemSerNo:[],
      icmNo:[],
      dateOfexpiry:[''],
      storeQty:[''],
      endShalfLife:[],
      receiveDate:[''],
      shelfLife:[''],
      itemReceivedDate:[],
      demandQty:[],
      letterOuterNo:[],
      refPoNo:[],
      tenderNumber:[],
      tenderNotice:[],
      location:[],
      qtyEntryType:[''],
      serviceLife:[],
      endLifeTime:[],
      accessories:[],
      stockRegisterPageNo:[],
      retirmentLife:[],
      remarks:[],
      otherDoc:[''],
      doc:[''],
      pattorimc:[''],
      part:[''],
      isActive: [true],  
      permanentQty:[0],
      tyQty:[0],
      surveyQty :[0],
      repairQty:[0],
      shipFittedQty:[0],
      //part:[''],
      imc:[''],
     // denoId:['']
    })

        //autocomplete
        this.ItemStorForm.get('part').valueChanges
        .subscribe(value => {
          this.getSelectedTraineeByPno(value);
        })
  
         //autocomplete
      this.ItemStorForm.get('imc').valueChanges
      .subscribe(value => {
        this.getSelectedImc(value);
      })
  }

  onPattOrModelSelectionChange(){
    var pattorimc = this.ItemStorForm.value['pattorimc'];
    if(pattorimc ==1){
       this.showPattOrModelDropdown=true;
       this.showIMC=false;
    }
    else if(pattorimc ==2){
      this.showIMC =true;
      this.showPattOrModelDropdown=false;
    }
    console.log(pattorimc);
  }

  getSelectedTraineeByPno(pno) {

    var itemCategoryId = this.ItemStorForm.value['itemCategoryId'];
    var departmentNameId = this.ItemStorForm.value['departmentNameId'];
    var unitId = this.ItemStorForm.value['unitId'];
    this.DemandService.getSelectedPartNoByNameForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getSelectedImc(pno){
    var itemCategoryId = this.ItemStorForm.value['itemCategoryId'];
    var departmentNameId = this.ItemStorForm.value['departmentNameId'];
    var unitId = this.ItemStorForm.value['unitId'];
    this.DemandService.getSelectedImcForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
   //autocomplete
   onTraineeSelectionChanged(item) {
    // console.log(item);
    this.itemDetailId = item.value
    this.itemCategoryId=item.value
    this.ItemStorForm.get('itemDetailId').setValue(item.value);
    this.ItemStorForm.get('part').setValue(item.text);
    this.getItemNameById(this.itemDetailId);

    console.log("itemdetailid");
    console.log(this.itemDetailId);


    this.getPartNoPassItemCategoryIdInDemand(this.itemCategoryId);
    console.log(item.value)
    console.log('V');

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }

  onImcSelectionChange(item) {
    // console.log(item);
    this.itemDetailId = item.value
    this.itemCategoryId=item.value
    this.ItemStorForm.get('itemDetailId').setValue(item.value);
    this.ItemStorForm.get('imc').setValue(item.text);
    this.getItemNameById(this.itemDetailId);

    console.log("itemdetailid");
    console.log(this.itemDetailId);


    this.getPartNoPassItemCategoryIdInDemand(this.itemCategoryId);
    console.log(item.value)
    console.log('V');

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }
  getPartNoPassItemCategoryIdInDemand(itemDetailId: number) {
    this.DemandService.getPartNoPassItemCategoryIdInDemand(itemDetailId).subscribe(res => {
      this.selectedItemDetails = res;
     // console.log(this.filteredOptions);
    });
  }


  getItemNameById(id: number) {
    console.log(id);
    // this.itemDetailsService.getItemNameById(id).subscribe(res => {
    //   this.selectedItemName = res;
    //   console.log("selected Item Name");
    //   console.log(this.selectedItemName);
    //   this.itemValue = this.selectedItemName[0].value
    //   console.log(this.itemValue);
    // });
    this.itemDetailsService.find(id).subscribe(res => {
      this.equipmentNameId = res.equipmentNameId;
      this.ItemStorForm.get('equipmentNameId').setValue(this.equipmentNameId);
    });
    // this.itemDetailsService.find(id).subscribe(res => {
    //   console.log(res);
    //   this.equipmentName = res.equipmentName;
    // });

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }

  getDemandData(){
    var demandId = this.ItemStorForm.value['demandId'];
    console.log(demandId+"-------------");
    this.isShownList=true;
    this.demandService.GetselectedDemandById(demandId).subscribe(res=>{
      this.demandData=res;
    });
  }

  getDemandNoForUpdate(){
    var demandId = this.ItemStorForm.value['demandId'];
    this.ItemStorForm.get('demandId').setValue(demandId); 
    this.ProcurementService.getSelectedDemandById(demandId).subscribe(res => {
      this.selectedDemandNo = res;
    });
  }

  public onDateChange() {
    const _ = require('lodash');

      var itemReceiveDate = _.cloneDeep(
       this.ItemStorForm.get('itemReceivedDate').value
      );

      var shelfLife = parseInt(this.ItemStorForm.get('shelfLife').value);

      var dateOfExpiry: any;

      //console.log(now.add(3, 'months').format()); 

     // d.setMonth(d.getMonth() - 3);

      itemReceiveDate.setMonth(itemReceiveDate.getMonth() + shelfLife);
      dateOfExpiry = itemReceiveDate;

      this.ItemStorForm.get('dateOfexpiry').setValue(new Date(dateOfExpiry))
  }

  // OnTextCheck(value){

  //  var itemReceiveDate = this.ItemStorForm.get('itemReceivedDate').value;
  //  console.log(value);
  //  var textBoxValue = Number(value)
  //   itemReceiveDate.setDate(itemReceiveDate.getDate() + textBoxValue);
  //   console.log(itemReceiveDate);

  //   this.ItemStorForm.get('dateOfexpiry').setValue(itemReceiveDate);
  // }
  getPartNoForProcurement(){
    this.procurementService.getPartNoForProcurement(this.masterData.procurement.itemStatusStore,this.masterData.procurement.procurementStatus).subscribe(res=>{
      this.selectedPartNoForProcurement=res
      console.log("procurementnnnnnnnnnnn");
      console.log(this.selectedPartNoForProcurement)
    }); 
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.ItemStorForm.patchValue({
        doc: file,
      });
    }
  }
  getSelectedLocation(){
    this.ItemStorService.getSelectedLocation().subscribe(res=>{
      this.selectedToolsLocation=res
    }); 
  }
  getSelectedConditionOfItem(){
    this.ItemStorService.getSelectedConditionOfItem().subscribe(res=>{
      this.selectedConditionofItem=res
    }); 
  }
  getSelectedDepartmentName(fourthLevel){
    this.ItemStorService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
    }); 
  }

  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
      this.isShown=true;
      this.itemCategoryId=dropdown.source.value;
      this.ItemStorService.getSelectedDemandNoByStatus(this.unitId,this.departmentId,this.itemCategoryId,this.masterData.itemStatus.itemStatusStore,0).subscribe(res => {
        this.selectedDemandNo = res;
        console.log("Selected Demand");
        console.log(this.selectedDemandNo);
      });

      this.isLoading = true;
      this.ItemStorService.getItemStorsList(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.departmentId,this.unitId,this.itemCategoryId).subscribe(response => {
        
        this.dataSource.data = response.items;
        console.log(this.dataSource.data )
        this.paging.length = response.totalItemsCount    
        this.isLoading = false;
      })  
     }
  }
  onDepartmentSelectionChange() {
    this.departmentId =  this.ItemStorForm.value['departmentNameId'];; 

    this.ItemStorService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.ItemStorForm.get('unitId').setValue(this.unitId);  
   });
  }
  onDemandSelectionChange(){
      this.isShownList=true;
      this.demandId =  this.ItemStorForm.value['demandId'];;
    this.isShownList=true;
    this.DemandService.find(this.demandId).subscribe(res=>{
      this.itemDetailId=res.itemDetailId;
      this.ItemStorForm.get('itemDetailId').setValue(this.itemDetailId);
    });

    this.DemandService.find(this.demandId).subscribe(res=>{
      this.denoid=res.denoId;
      this.ItemStorForm.get('denoId').setValue(this.denoid);
    });

    this.DemandService.find(this.demandId).subscribe(res=>{
      this.equipmentNameId=res.equipmentNameId;
      this.ItemStorForm.get('equipmentNameId').setValue(this.equipmentNameId);
    });

    this.DemandService.GetselectedDemandByParameterSpRequest(this.demandId).subscribe(res=>{
      this.demandData=res;
      console.log("Demand Data");
      console.log(this.demandData);
    });
}
  getselectedAcceptence(){
    var departmentNameId = this.ItemStorForm.value['departmentNameId'];
    this.isShown=true;
    this.ItemStorService.partnoFromAcceptanceByDepartmentName(departmentNameId, this.masterData.sparescategory.spares).subscribe(res=>{
      this.selectedPartNo=res   
    });
    this.getItemStorsList(departmentNameId);
  }
  getselectedAcceptenceOnUpdate(id){
    this.ItemStorService.partnoFromAcceptanceForUpdateByDepartmentName(id, this.masterData.sparescategory.spares).subscribe(res=>{
      this.selectedPartNo=res;
    });
  }
  getAcceptanceData(){    
    var acceptanceId = this.ItemStorForm.value['acceptanceId'];
    this.ItemStorService.getacceptanceById(acceptanceId).subscribe(res=>{
      this.acceptanceData=res;        
    });
    this.acceptanceService.find(acceptanceId).subscribe(res=>{
      this.acceptanceByDepartmentAndCategory=res
      this.ItemStorForm.get('demandId').setValue(res.demandId);
      this.ItemStorForm.get('itemDetailId').setValue(res.itemDetailId);
      this.ItemStorForm.get('procurementId').setValue(res.procurementId);
      this.ItemStorForm.get('sparesCategoryId').setValue(res.sparesCategoryId);    
            
      this.procurementService.GetselectedProcurementById(res.procurementId).subscribe(res=>{
        this.procurementData=res;        
      });
      this.demandService.find(res.demandId).subscribe(res=>{
        this.ItemStorForm.get('denoId').setValue(res.denoId);
        this.ItemStorForm.get('demandQty').setValue(res.demandQty);
        this.ItemStorForm.get('demandDate').setValue(res.demandDate);
        this.ItemStorForm.get('letterOuterNo').setValue(res.letterOuterNo);
        this.ItemStorForm.get('refPoNo').setValue(res.refPoNo);
      });
      this.procurementService.find(res.procurementId).subscribe(res=>{
        this.ItemStorForm.get('tenderNumber').setValue(res.tenderNumber);
        this.ItemStorForm.get('dateOfTenderFloat').setValue(res.dateOfTenderFloat);
        this.ItemStorForm.get('tenderopeningDate').setValue(res.tenderopeningDate);
        this.ItemStorForm.get('tenderPublishDate').setValue(res.tenderPublishDate);
      });   
      this.isShown=true;
    });

  }

  GetDepartmentNameById(baseNameId){    
    this.ItemStorService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getselectedItemCategory(){
    this.ItemStorService.getselectedItemCategory().subscribe(res=>{
      this.selectedItemCategory=res  
    });
  }
  getselectedLifeLimitItem(){
    this.ItemStorService.getselectedLifeLimitItem().subscribe(res=>{
      this.selectedLifeLimitItem=res  
    });
  }
  getselectedDeno(){
    this.ItemStorService.getselectedDeno().subscribe(res=>{
      this.selectedDeno=res   
    });
  }
  getselectedSparesCategory(){
    this.ItemStorService.getselectedSparesCategory().subscribe(res=>{
      this.selectedSparesCategory=res    
    });
  }
  getselectedConditionOfItem(){
    this.ItemStorService.getselectedConditionOfItem().subscribe(res=>{
      this.selectedConditionofItem=res    
    });
  }
  getselectedToolsLocations(){
    this.ItemStorService.getselectedToolsLocations().subscribe(res=>{
      this.selectedToolsLocation=res     
    });
  }
  getselectedAcctStore(){
    this.ItemStorService.getselectedAcctStore().subscribe(res=>{
      this.selectedAcctStore=res     
    });
  }
  getselectedServiceLifeType(){
    this.ItemStorService.getselectedServiceLifeType().subscribe(res=>{
      this.selectedServiceLifeType=res    
    });
  }
  getselectedEndLifeType(){
    this.ItemStorService.getselectedEndLifeType().subscribe(res=>{
      this.selectedEndLifeType=res     
    });
  }
  getselectedOverhaulingTypes(){
    this.ItemStorService.getselectedOverhaulingTypes().subscribe(res=>{
      this.selectedOverhaulingTypes=res     
    });
  }

  
  getItemStorsList(departmentId) {
  }
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.ItemStorForm.get('departmentNameId').value;
    this.getItemStorsList(departmentId);
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }


  deleteItem(row) {
    const id = row.itemStorId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      if (result) {
        this.ItemStorService.delete(id).subscribe(() => {
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

  onSubmit() {
    const id = this.ItemStorForm.get('itemStorId').value;  
    this.ItemStorForm.get('itemReceivedDate').setValue((new Date(this.ItemStorForm.get('itemReceivedDate').value)).toUTCString());
    this.ItemStorForm.get('dateOfexpiry').setValue((new Date(this.ItemStorForm.get('dateOfexpiry').value)).toUTCString());
    console.log(this.ItemStorForm.value) 
    const formData = new FormData();
    for (const key of Object.keys(this.ItemStorForm.value)) {
      const value = this.ItemStorForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.ItemStorService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/spares-management/add-itemstor');
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
      this.ItemStorService.previousItemsubmit(formData).subscribe(response => {
        this.reloadCurrentRoute();
        this.intitializeForm();
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

}
