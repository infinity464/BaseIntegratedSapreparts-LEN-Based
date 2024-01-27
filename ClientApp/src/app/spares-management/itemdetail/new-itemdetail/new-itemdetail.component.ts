import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemDetailService } from '../../service/itemDetail.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DemandService } from '../../../spares-management/service/Demand.service';
import { ItemDetail } from '../../models/itemDetail';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import {BaseSchoolNameService} from '../../../security/service/BaseSchoolName.service';
import { BaseSchoolName } from 'src/app/security/models/BaseSchoolName';
import { AcceptanceService } from '../../service/Acceptance.service';
import { MatTableDataSource } from '@angular/material/table';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-itemdetail',
  templateUrl: './new-itemdetail.component.html',
  styleUrls: ['./new-itemdetail.component.sass']
})
export class NewItemDetailComponent implements OnInit {
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
  selectedItemCategory:SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedSparesCategory:SelectedModel[];
  baseSchoolNameList:SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  masterData = MasterData;
  departmentId:any;
  isShown: boolean = false ;
  isNoDataFound:boolean=false;
  departmentNameId:any;
  userRole = Role;
  unitId:any;
  traineeId:any;
  role:any;
  branchId:any;
  groupArrays:{ equipmentName: string; courses: any; }[];
  itemCategoryId:any;
  selectedEquipmentPriority:SelectedModel[];
  itemDetailId:any;
  selectedItemName:SelectedModel[];
  options = [];
  filteredOptions;
  partNo:any;
  isLoading = false;
  // unitId:any;
  // departmentId:any;
  // itemCategoryId:any;

  displayedColumns: string[] = [ 'ser','photoUp', 'equipmentName','nameOfItem', 'partNo',  'imcNumber','equipmentPriority','spec', 'minimumStock','actions'];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  constructor(private snackBar: MatSnackBar,private AcceptanceService: AcceptanceService,private baseSchoolNameService: BaseSchoolNameService,private authService: AuthService,private confirmService: ConfirmService,private demandService:DemandService,private ItemDetailService: ItemDetailService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }
  dataSource: MatTableDataSource<ItemDetail> = new MatTableDataSource();
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('itemDetailId'); 
    this.sparesCategoryId= this.route.snapshot.paramMap.get('sparesCategoryId');  
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId);
    this.onSpareCategorySelectionChange("");

    if (id) {
      this.pageTitle = 'Edit Item Creation';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ItemDetailService.find(+id).subscribe(
        res => {
          this.ItemDetailForm.patchValue({          

            itemDetailId: res.itemDetailId,
            partNo: res.partNo,
            imcNumber: res.imcNumber,
            serialNo: res.serialNo,
            model: res.model,
            brand: res.brand,
            nameOfItem: res.nameOfItem,
            itemCategoryId:res.itemCategoryId,
            equipmentNameId:res.equipmentNameId,
            //itemCategoryTypeId:res.itemCategoryTypeId,
            //sparesCategoryId:res.sparesCategoryId,
            equipmentNameIds:res.equipmentNameId,
            departmentNameId:res.departmentNameId,
            //itemTypeId:res.itemTypeId,
            minimumStock: res.minimumStock,
            alternatiovePrartNo: res.alternatiovePrartNo,
            //tradeId: res.tradeId,
            spec: res.spec,
            photoUp: res.photoUp,
            remarks: res.remarks,
            equipmentPriorityId:res.equipmentPriorityId
          });   
          console.log("item detail response");
          console.log(res);
          //  if(res.equipmentPriority ===1 ){
            
          //  }
          this.getSelectedDepartmentNameByUnitId();
          this.onDepartmentSelectionChange();
          this.onSparesCategorySelectionChangeGetEquipmentName(res.sparesCategoryId)    
        }
      );
    } else {
      this.pageTitle = 'Item Creation';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getSelectedBaseSchoolName();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.ItemDetailService.findBaseSchoolNameId(this.branchId).subscribe(res=>{
        this.unitId=res.thirdLevel;
        this.ItemDetailForm.get('unitId').setValue(this.unitId);
        console.log("UnitID");
      console.log(this.unitId);
      this.onDepartmentSelectionChange();
      })
      this.getSelectedDepartmentName(this.branchId);
    }
    if(this.role == this.userRole.Admin){
      console.log("admin block")
      
      this.ItemDetailForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
      // console.log("admin block")
      
      // this.ItemDetailForm.get('unitId').setValue(this.branchId);
      // console.log(this.branchId)
      // this.getSelectedDepartmentName(this.branchId);
    }
    this.getselectedItemCategoryTypes();
    this.getselectedItemType();
    this.getselectedTrades();
    this.getSelectedItemCategory();
    this.getselectedSparesCategory();
    this.getselectedEquipmentName();
    this.getSelectedEquipmentPriority();
  }
  intitializeForm() {
    this.ItemDetailForm = this.fb.group({
      itemDetailId: [0],
      unitId:[''],
      partNo: [''],
      imcNumber: [''],
      serialNo: [''],
      model: [''],
      brand: [''],
      nameOfItem: [''],
      departmentNameId:[''],
      itemCategoryId:[''],
      itemCategoryTypeId:[''],
      itemCategorysId:[''],
      equipmentNameId:[''],
      sparesCategoryId:[''],
      equipmentPriority:[''],
      equipmentNameIds:[''],
      //sparesCategoryId:[this.masterData.sparescategory.spares],
      itemTypeId:[''],
      alternatiovePrartNo: [''],
      minimumStock:[''],
      tradeId:[''],
      spec:[''],
      photo: [''],
      photoUp: [''],
      remarks: [''],
      isActive: [true],
      equipmentPriorityId:[''],
      part:['']
    })

            //autocomplete
      this.ItemDetailForm.get('part').valueChanges
      .subscribe(value => {
        this.getSelectedTraineeByPno(value);
      })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.ItemDetailForm.patchValue({
        photo: file,
      });
    }
  }
  getSelectedTraineeByPno(pno) {
   // var itemCategoryId = this.ItemDetailForm.value['itemCategoryId'];
    //var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    //var unitId = this.IssueRegisterForm.value['unitId'];
    this.ItemDetailService.getSelectedEquipmentNameForAutocomplete(pno).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
      this.partNo = this.ItemDetailForm.value['part'].value;
       console.log("dddd");
    })
  }
  onTraineeSelectionChanged(item) {
    this.itemDetailId = item.value
    this.ItemDetailForm.get('equipmentNameId').setValue(item.value);
    this.ItemDetailForm.get('part').setValue(item.text);

    this.getItemNameById(this.itemDetailId);
    // console.log("itemDetail id");
    // console.log(this.itemDetailId);
    // console.log("unit id");
    // console.log(this.unitId);
    // console.log("department id");
    // console.log(this.departmentId);
    // console.log("item category");
    // console.log(this.itemCategoryId);
    // if(this.itemCategoryId ===1){
    //   console.log("kkkkkkkkkkkkk");
    //   this.isShownForEquipment=true;
    //   this.IssueRegisterService.getItemStorListForEquipmentIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
    //     this.selectedItemStoreList=res; 
        
    //     // for (var index in this.selectedItemStoreList) {
    //     //  index.
    //     // }
    //     // this.selectedItemStoreList.filter((userMenu) => {
    //     //   return userMenu.availableQty.filter((role) => role === '2');
    //     // });

    //    // this.selectedItemStoreList.filter(x=>x.availableQty !=0)
    //     //Sum available qty from list
    //     this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
    //                     .reduce((sum, current) => sum + current.availableQty, 0);

    //                     console.log("available qty");
    //                     console.log(this.totalAvailableQty);
      
    //     console.log(this.selectedItemStoreList);
    //     this.clearList();
    //     this.getItemStoreListonClick();
    //   });
    // }
    // else{
    //   this.isShown=true;
    //   this.IssueRegisterService.getSelectedItemStorListForIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
    //     this.selectedItemStoreList=res 

    //     this.selectedItemStoreList.find(x=>x.availableQty !==0)


    //     //Sum available qty from list
    //     this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
    //     .reduce((sum, current) => sum + current.availableQty, 0);

    //     console.log("available qty");
    //     console.log(this.totalAvailableQty);
    //    // console.log("kkkkkkkkkkkkk");
    //     console.log(this.selectedItemStoreList);
    //     this.clearList();
    //     this.getItemStoreListonClick();
    //   });
    // }
  }

  getItemNameById(id: number) {
    console.log(id);
    this.ItemDetailService.getItemNameById(id).subscribe(res => {
      this.selectedItemName = res;
      console.log(this.selectedItemName);
     // this.itemValue = this.selectedItemName[0].value
     // console.log(this.itemValue);
    });

    // this.ItemDetailService.find(id).subscribe(res => {
    //   this.equipmentNameId = res.equipmentNameId;
    //   this.IssueRegisterForm.get('equipmentNameId').setValue(this.equipmentNameId);
    // });
    // this.ItemDetailService.find(id).subscribe(res => {
    //   console.log(res);
    //   this.equipmentName = res.equipmentName;
    // //  this.DemandForm.get('equipmentNameId').setValue(this.equipmentName);
    // });
  }

  getSelectedBaseSchoolName(){
    this.isShown=true;
    this.baseSchoolNameService.getSelectedBaseSchool().subscribe(res=>{
      this.baseSchoolNameList=res;
    });
  }
  getSelectedEquipmentPriority(){
    this.ItemDetailService.getSelectedEquipmentPriority().subscribe(res=>{
      this.selectedEquipmentPriority=res;
    });
  }
  getSelectedDepartmentNameByUnitId(){
    var unitId = this.ItemDetailForm.value['unitId'];
    this.getSelectedDepartmentName(unitId);
  }

  getselectedEquipmentName(){
    this.ItemDetailService.getselectedEquipmentName().subscribe(res=>{
      this.selectedEquipment=res;
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
  onDepartmentSelectionChange(){
    this.isShown=true;
    this.departmentNameId = this.ItemDetailForm.value['departmentNameId'];
    this.getselectedPresentStocks(this.departmentNameId);
     
    this.ItemDetailService.findBaseSchoolNameId(this.departmentNameId).subscribe(res=>{
       this.unitId=res.thirdLevel;

      this.ItemDetailForm.get('unitId').setValue(this.unitId);

      this.ItemDetailService.getEquipmentNameBySparesCategoryIdAndDepartmentNameId(this.masterData.sparescategory.spares,this.departmentNameId).subscribe(res=>{
        this.selectedEquipmentName=res     
      }); 
       
    });
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  getselectedItemCategoryTypes(){
      this.ItemDetailService.getselectedItemCategoryTypes().subscribe(res=>{
      this.selectedCategoryTypes=res
    });
   }

  getselectedPresentStocks(departmentId){
    
  }
  onEquipmentSelectionChange(dropdown){
    if (dropdown.isUserInput) {
      console.log("Dropdown");
      console.log(dropdown);
      var equipmentNameId = dropdown.source.value.value;
      console.log("rrrrr"+ equipmentNameId)
      this.ItemDetailForm.get('equipmentNameId').setValue(equipmentNameId);
      //var itemCategoryId = this.ItemDetailForm.value['itemCategoryId'];
      console.log(this.itemCategoryId)

      if(this.itemCategoryId === 1){
      console.log("dropdown");
     /// this.ItemDetailForm.get('nameOfItem').disabled;
    var itemName= this.ItemDetailForm.value['equipmentNameIds'];
    console.log("itemName");
    console.log(itemName);
      this.ItemDetailForm.get('nameOfItem').setValue(dropdown.source.value.text);
     }

     else{
      this.ItemDetailForm.get('nameOfItem').setValue("");
     // this.ItemDetailForm.get('nameOfItem').enable();
     }
    }
 }
  

  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
      this.itemCategoryId=dropdown.source.value;

      //var itemCategoryId = this.ItemDetailForm.value['itemCategoryId'];

      // if(dropdown.source.value ===1){
      //   this.ItemDetailForm.get('nameOfItem').disable();
      // }

      this.ItemDetailService.getselectedItemDetailByDepartmentNameIdUnitIdItemCategoryId(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.departmentNameId,this.unitId,this.itemCategoryId).subscribe(res=>{
        //this.itemDetailByDepartmentId=res
        this.dataSource.data = res.items;
        console.log(this.dataSource.data )
        this.paging.length = res.totalItemsCount    
        //this.isLoading = false;
      });

      // this.ItemDetailService.getSelectedEquipmentByParameterRequest(this.unitId,this.departmentNameId,itemCategoryId).subscribe(res=>{
      //   this.selectedEquipment=res
      // });
     }
  }
  pageChanged(event: PageEvent) {
    this.ItemDetailService.getselectedItemDetailByDepartmentNameIdUnitIdItemCategoryId(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.departmentNameId,this.unitId,this.itemCategoryId).subscribe(res=>{
      //this.itemDetailByDepartmentId=res
      this.dataSource.data = res.items;
      console.log(this.dataSource.data )
      this.paging.length = res.totalItemsCount    
      //this.isLoading = false;
    });
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.onSpareCategorySelectionChange(PageEvent);
  }
  
  applyFilter(searchText: any){ 
    this.ItemDetailService.getselectedItemDetailByDepartmentNameIdUnitIdItemCategoryId(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.departmentNameId,this.unitId,this.itemCategoryId).subscribe(res=>{
      //this.itemDetailByDepartmentId=res
      this.dataSource.data = res.items;
      console.log(this.dataSource.data )
      this.paging.length = res.totalItemsCount    
      //this.isLoading = false;
    });
    this.searchText = searchText;
    //this.getItemDetails();
    this.onSpareCategorySelectionChange("");
  }

  GetDepartmentNameById(baseNameId){    
    this.demandService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentName=res
    }); 
  }
  getSelectedItemCategory(){
    this.ItemDetailService.getSelectedItemCategory().subscribe(res=>{
      this.selectedItemCategory=res
      console.log('Category ID')
      console.log(this.selectedItemCategory);      
    });
  }
  getselectedItemType(){
    this.ItemDetailService.getselectedItemType().subscribe(res=>{
      this.selectedItemType=res
      //console.log(this.selectedItemType);      
    });
  }
  getselectedTrades(){
    this.ItemDetailService.getselectedTrades().subscribe(res=>{
      this.selectedTrades=res
      //console.log(this.selectedTrades);      
    });
  }
  onSparesCategorySelectionChangeGetEquipmentName( sparesCategoryId){
    if(sparesCategoryId !=null){
      this.ItemDetailService.getEquipmentNameBySparesCategoryId(sparesCategoryId).subscribe(res=>{
        this.selectedEquipmentName=res
      });
    }
   }

  //  onSpareCategorySelectionChange(dropdown){
  //   if (dropdown.isUserInput) {
  //     // this.ItemDetailForm.value['unitId'];
  //   var departmentNameId = this.ItemDetailForm.value['departmentNameId'];
  //   var itemCategoryId = dropdown.source.value.value;

  //   this.ItemDetailForm.get('itemCategoryId').setValue(itemCategoryId);

  //    console.log(departmentNameId+"----"+itemCategoryId);

    // this.ItemDetailService.getEquipmentNameBySparesCategoryIdAndDepartmentNameId(itemCategoryId,departmentNameId).subscribe(res=>{
    //   this.selectedEquipmentName=res
    //   console.log(this.selectedEquipmentName);      
    // });
      //this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      // this.budgetCodeId = dropdown.source.value;
      //  this.BudgetAllocationService.getSelectedBudgetCodeNameByBudgetCodeId(dropdown.source.value).subscribe(res=>{
      //    this.selectedBudgetCodeName=res
      //    this.budgetCodeName = res[0].text;
      //    this.BudgetAllocationForm.get('budgetCodeName').setValue(this.budgetCodeName);
      //    console.log(this.budgetCodeName);
      //  });
  //    }
  //  }

  // getselectedEquipmentName(){
  //   var sparesCategoryId = this.ItemDetailForm['sparesCategoryId'].value;

  //   this.ItemDetailService.getEquipmentNameBySparesCategoryIdAndDepartmentNameId(sparesCategoryId,departmentNameId).subscribe(res=>{
  //     this.selectedEquipmentName=res
  //     console.log(this.selectedEquipmentName);      
  //   });
  // }
  getselectedSparesCategory(){
    this.ItemDetailService.getselectedSparesCategory().subscribe(res=>{
      this.selectedSparesCategory=res
      //console.log(this.selectedSparesCategory);      
    });
  }

  deleteItem(row) {
    const id = row.itemDetailId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ItemDetailService.delete(id).subscribe(() => {
          //this.getItemDetails();
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
    const id = this.ItemDetailForm.get('itemDetailId').value;   
    console.log(this.ItemDetailForm.value);
    const formData = new FormData();
    for (const key of Object.keys(this.ItemDetailForm.value)) {
      const value = this.ItemDetailForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.ItemDetailService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/add-itemdetail');
            ///this.reloadCurrentRoute();
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
      this.ItemDetailService.submit(formData).subscribe(response => {
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

}
