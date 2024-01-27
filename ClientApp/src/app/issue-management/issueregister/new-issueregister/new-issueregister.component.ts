import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { IssueRegisterService } from '../../service/IssueRegister.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from 'src/app/basic-setup/service/DepartmentName.service';
import { ItemStor } from 'src/app/spares-management/models/ItemStor';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { style } from '@angular/animations';
import { MasterData } from 'src/assets/data/master-data';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role'; 
import { AcceptanceService } from 'src/app/spares-management/service/Acceptance.service';
import { DemandService } from 'src/app/spares-management/service/Demand.service';
import { MatDialog } from '@angular/material/dialog';
import {SpareStocklistComponent} from '../../../../app/maintenence-planning/corrective/spare-stocklist/spare-stocklist.component'
import {CorrectiveMaintenenceService} from '../../../../app/maintenence-planning/service/CorrectiveMaintenence.service';
import {NegativeDialogMessageComponent} from '../../../issue-management/negative-dialog-message/negative-dialog-message.component';

@Component({
  selector: 'app-new-issueregister',
  templateUrl: './new-issueregister.component.html',
  styleUrls: ['./new-issueregister.component.sass']
})
export class NewIssueRegisterComponent implements OnInit {
  pageTitle: string;
  destination: string;
  btnText: string;
  IssueRegisterForm: FormGroup;
  validationErrors: string[] = [];
  selectedItemDetails: SelectedModel[];
  selectedIssueStatuses: SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedSparesCategory: SelectedModel[];
  selectedItemNameValue:SelectedModel[];
  selectedToolsLocation:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedItemName:SelectedModel[];
  selectedItemCategory:SelectedModel[];
  selectedIssueType:SelectedModel[];
  getitemdetailid: number;
  getitemname: string;
  itemValue:any;
  equipmentNameId:any;
  equipmentName:any;
  selectedItemStoreList: ItemStor[];
  isShown: boolean = false;
  isDynamicFormShown: boolean = false;
  isShownForEquipment:boolean =false;
  checked: boolean = false;
  message: string;
  titleColor: '#FF0000';
  itemName: string;
  trainingCrewId:number;
  itemDetailId:number;
  totalAvailableQty:any;
  masterData = MasterData;
  userRole = Role;
  departmentId:any;
  unitId:any;
  partNo:any;
  itemCategoryId:any;

  options = [];
  filteredOptions;

  traineeId:any;
  role:any;
  branchId:any;
  calculateData:any;
  showPattOrModelDropdown=false;
  showIMC=false;

  spareStockBySpRequest:any[];

  constructor(public dialog: MatDialog,private CorrectiveMaintenenceService: CorrectiveMaintenenceService,private snackBar: MatSnackBar,private ItemDetailService:ItemDetailService,private DemandService:DemandService,private AcceptanceService: AcceptanceService, private authService: AuthService, private itemDetailService: ItemDetailService, private departmentNameService: DepartmentNameService, private confirmService: ConfirmService, private IssueRegisterService: IssueRegisterService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('issueRegisterId');

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Issue Register';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.IssueRegisterService.find(+id).subscribe(
        res => {
          this.IssueRegisterForm.patchValue({

            issueRegisterId: res.issueRegisterId,
            itemDetailId: res.itemDetailId,
            issueStatusId: res.issueStatusId,
            trainingCrewId: res.trainingCrewId,
            totalReceivedQty: res.totalReceivedQty,
            issueQty: res.issueQty,
            issueDate: res.issueDate,
            issuedTo: res.issuedTo,
            reason: res.reason,
            status: res.status,
            demandCompleteStatus: res.demandCompleteStatus,
            isRefundable: res.isRefundable,
            availableQtyBeforeIssue: res.availableQtyBeforeIssue,
            availableQtyAfterIssue: res.issueDate,
            receivedPerson: res.receivedPerson,
            remarks: res.remarks,
          });
        }
      );
    } else {
      this.pageTitle = 'Create Issue Register';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.IssueRegisterForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.IssueRegisterForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    this.getselectedItemDetails();
    this.getselectedIssueStatuses();
    this.getselectedSparesCategory();
    this.getselectedItemDetails();
    this.getselectedToolsLocation();
    this.getSelectedItemCategory();
    this.getSelectedIssueType();
  }
  intitializeForm() {
    this.IssueRegisterForm = this.fb.group({
      issueRegisterId: [0],
      sparesCategoryId: [],
      departmentNameId: [''],
      itemCategoryId:[''],
      equipmentNameId:[''],
      itemDetailId: [''],
      partNo:[''],
      part:[''],
      imc:[''],
      pattorimc:[''],
      unitId:[''],
      issueStatusId: [],
      trainingCrewId: [],
      pno:[''],
      totalReceivedQty: [''],
      issueDate: [''],
      issuedTo: [''],
      issuedBy:[''],
      surveyStatus:[0],
      demandCompleteStatus:[0],
      status:[0],
      surveyNumber:[''],
      surveyDate:[''],
      reason: [''],
      remarks: [''],
      dateOfExpiry:[''],
      availableQtyBeforeIssue: [''],
      availableQtyAfterIssue: [''],
      receivedPerson: [''],
      isActive: [true],
      ItemStoreList: this.fb.array([
        this.createIssueRegisterData()
      ]),

    })
        //autocomplete
        this.IssueRegisterForm.get('part').valueChanges
        .subscribe(value => {
          this.getSelectedTraineeByPno(value);
        })

        this.IssueRegisterForm.get('imc').valueChanges
        .subscribe(value => {
          this.getSelectedImc(value);
        })
  }

  getControlLabel(index: number, type: string) {
    return (this.IssueRegisterForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  }

  private createIssueRegisterData() {

    return this.fb.group({
      deno: [''],
      itemDetail: [''],
      partNo: [''],
      part:[''],
      storeQty:[''],
      itemSerNo: [''],
      issuedQty: [''],
      toolsLocationId:[''],
      toolsLocation:[''],
      status: [''],
      itemDetailId: [''],
      itemStorId: [''],
      departmentNameId: [''],
      sparesCategoryId: [''],
      isRefundable: [false],
      isChecked: [false],
      totalReceivedQty: [''],
      availableQty: [''],
      returnQty: [''],
      itemReceivedDate: [''],
      issueTypeId:[''],
      nameOfItem:[''],
      warrantyEndDate: [],
      dateOfExpiry:[''],
      acctStore: ['']
    });
  }

  // OnTextCheck(i,j,k){

  // }
  onImcSelectionChange(item) {
    this.itemDetailId = item.value
    this.IssueRegisterForm.get('itemDetailId').setValue(item.value);
    this.IssueRegisterForm.get('imc').setValue(item.text);

    this.getItemNameById(this.itemDetailId);
    // console.log("itemDetail id");
    // console.log(this.itemDetailId);
    // console.log("unit id");
    // console.log(this.unitId);
    // console.log("department id");
    // console.log(this.departmentId);
    // console.log("item category");
    // console.log(this.itemCategoryId);
    if(this.itemCategoryId ===1){
      console.log("kkkkkkkkkkkkk");
      this.isShownForEquipment=true;
      this.IssueRegisterService.getItemStorListForEquipmentIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
        this.selectedItemStoreList=res; 
        
        // for (var index in this.selectedItemStoreList) {
        //  index.
        // }
        // this.selectedItemStoreList.filter((userMenu) => {
        //   return userMenu.availableQty.filter((role) => role === '2');
        // });

       // this.selectedItemStoreList.filter(x=>x.availableQty !=0)
        //Sum available qty from list
        this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
                        .reduce((sum, current) => sum + current.availableQty, 0);

                        console.log("available qty");
                        console.log(this.totalAvailableQty);
      
        console.log(this.selectedItemStoreList);
        this.clearList();
        this.getItemStoreListonClick();
      });
    }
    else{
      this.isShown=true;
      this.IssueRegisterService.getSelectedItemStorListForIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
        this.selectedItemStoreList=res 

        this.selectedItemStoreList.find(x=>x.availableQty !==0)


        //Sum available qty from list
        this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
        .reduce((sum, current) => sum + current.availableQty, 0);

        console.log("available qty");
        console.log(this.totalAvailableQty);
       // console.log("kkkkkkkkkkkkk");
        console.log(this.selectedItemStoreList);
        this.clearList();
        this.getItemStoreListonClick();
      });
    }
  }

  getSelectedImc(pno){
    var itemCategoryId = this.IssueRegisterForm.value['itemCategoryId'];
    var departmentNameId = this.IssueRegisterForm.value['departmentNameId'];
    var unitId = this.IssueRegisterForm.value['unitId'];
    this.DemandService.getSelectedImcForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log("rrrrrrrrr");
      console.log(this.selectedDepartmentNames)
     // console.log(res)
    }); 
  }

  onPattOrModelSelectionChange(){
    var pattorimc = this.IssueRegisterForm.value['pattorimc'];
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

  getSelectedIssueType(){
    this.IssueRegisterService.getSelectedIssueType().subscribe(res=>{
      this.selectedIssueType=res 
    });
  }
  getSelectedTraineeByPno(pno) {
    var itemCategoryId = this.IssueRegisterForm.value['itemCategoryId'];
    var departmentNameId = this.IssueRegisterForm.value['departmentNameId'];
    var unitId = this.IssueRegisterForm.value['unitId'];
    this.DemandService.getSelectedPartNoByNameForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
      this.partNo = this.IssueRegisterForm.value['part'].value;
       console.log("dddd");
    })
  }

  onDepartmentSelectionChange() {
    this.departmentId = this.IssueRegisterForm.value['departmentNameId']; 
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.IssueRegisterForm.get('unitId').setValue(this.unitId);

     this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentId).subscribe(res => {
      this.selectedEquipment = res;
    });   
   });
  }
  getSelectedItemCategory() {
    this.DemandService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
    this.departmentId = this.IssueRegisterForm.value['departmentNameId'];
    this.itemCategoryId = dropdown.source.value;
   console.log("unit id");
   console.log(this.unitId);
   console.log("department id");
   console.log(this.departmentId);
   console.log("item category");
   console.log(this.itemCategoryId);
    this.IssueRegisterForm.get('itemCategoryId').setValue(this.itemCategoryId);
     }
   }
  onCheckboxChange(event, data) {
    console.log("event");
    console.log(event);
    if (data.value.availableQty < data.value.issuedQty) {
      // if(this.calculateData < 0){
        this.btnText="";
        let tempDirection;
        if (localStorage.getItem('isRtl') === 'true') {
          tempDirection = 'rtl';
        } else {
          tempDirection = 'ltr';
        }
        const dialogRef = this.dialog.open(NegativeDialogMessageComponent, {
          data: this.spareStockBySpRequest,
          direction: tempDirection,
        });
     // }
    }
    this.isDynamicFormShown = true;
  }

  clearList() {
    const control = <FormArray>this.IssueRegisterForm.controls["ItemStoreList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getItemStoreListonClick() {
    const control = <FormArray>this.IssueRegisterForm.controls["ItemStoreList"];
    for (let i = 0; i < this.selectedItemStoreList.length; i++) {
      control.push(this.createIssueRegisterData());
    }
    console.log('12');
    this.IssueRegisterForm.patchValue({ ItemStoreList: this.selectedItemStoreList });
  }
  //autocomplete for pno
  //autocomplete
  onTraineeSelectionChanged(item) {
    this.itemDetailId = item.value
    this.IssueRegisterForm.get('itemDetailId').setValue(item.value);
    this.IssueRegisterForm.get('part').setValue(item.text);

    this.getItemNameById(this.itemDetailId);
    // console.log("itemDetail id");
    // console.log(this.itemDetailId);
    // console.log("unit id");
    // console.log(this.unitId);
    // console.log("department id");
    // console.log(this.departmentId);
    // console.log("item category");
    // console.log(this.itemCategoryId);
    if(this.itemCategoryId ===1){
      console.log("kkkkkkkkkkkkk");
      this.isShownForEquipment=true;
      this.IssueRegisterService.getItemStorListForEquipmentIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
        this.selectedItemStoreList=res; 
        
        // for (var index in this.selectedItemStoreList) {
        //  index.
        // }
        // this.selectedItemStoreList.filter((userMenu) => {
        //   return userMenu.availableQty.filter((role) => role === '2');
        // });

       // this.selectedItemStoreList.filter(x=>x.availableQty !=0)
        //Sum available qty from list
        this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
                        .reduce((sum, current) => sum + current.availableQty, 0);

                        console.log("available qty");
                        console.log(this.totalAvailableQty);
      
        console.log(this.selectedItemStoreList);
        this.clearList();
        this.getItemStoreListonClick();
      });
    }
    else{
      this.isShown=true;
      this.IssueRegisterService.getSelectedItemStorListForIssueRegister(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
  
        this.selectedItemStoreList=res 

        this.selectedItemStoreList.find(x=>x.availableQty !==0)


        //Sum available qty from list
        this.totalAvailableQty = this.selectedItemStoreList.filter(item => item.availableQty)
        .reduce((sum, current) => sum + current.availableQty, 0);

        console.log("available qty");
        console.log(this.totalAvailableQty);
       // console.log("kkkkkkkkkkkkk");
        console.log(this.selectedItemStoreList);
        this.clearList();
        this.getItemStoreListonClick();
      });
    }
  }

  getItemNameById(id: number) {
    console.log(id);
    this.ItemDetailService.getItemNameById(id).subscribe(res => {
      this.selectedItemName = res;
      console.log(this.selectedItemName);
      this.itemValue = this.selectedItemName[0].value
      console.log(this.itemValue);
    });

    this.ItemDetailService.find(id).subscribe(res => {
      this.equipmentNameId = res.equipmentNameId;
      this.IssueRegisterForm.get('equipmentNameId').setValue(this.equipmentNameId);
    });
    this.ItemDetailService.find(id).subscribe(res => {
      console.log(res);
      this.equipmentName = res.equipmentName;
    //  this.DemandForm.get('equipmentNameId').setValue(this.equipmentName);
    });
  }
  //autocomplete for PartNo
  onPartNoSelectionChanged(item) {
    console.log(item.value);
    this.itemDetailId = item.value
    this.IssueRegisterForm.get('itemDetailId').setValue(item.value);
    this.IssueRegisterForm.get('partNo').setValue(item.text);
    this.ongetItemNameByItemDetailId(this.itemDetailId);
  }
  //autocomplete for pno
  // getSelectedTraineeCrewByPno(pno) {
  //   this.IssueRegisterService.getSelectedPno(pno).subscribe(response => {
  //     this.options = response;
  //     this.filteredOptions = response;
  //   })
  // }
  //autocomplete for PartNo
  // getSelectedItemDetailByPartNo(partNo) {
  //   this.IssueRegisterService.getSelectedPartNo(partNo).subscribe(response => {
  //     this.options = response;
  //     this.filteredOptions = response;
  //     console.log("88888");
  //   })
  // }
  getselectedToolsLocation(){
    this.IssueRegisterService.getselectedToolsLocation().subscribe(res => {
      this.selectedToolsLocation = res
      //console.log(this.selectedSparesCategory);      
    });
  }
  ongetItemNameByItemDetailId(itemDetailId){
    this.IssueRegisterService.getItemNameByItemDetailId(itemDetailId).subscribe(res=>{
      this.selectedItemNameValue=res 
      this.getitemdetailid = this.selectedItemNameValue[0].value,
      this.getitemname = this.selectedItemNameValue[0].text
    });
   }

  OnTextCheck(value,index,type ){

    console.log("iiiiiii");
    console.log(value);
    var getqty = (this.IssueRegisterForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
    this.calculateData =  getqty - value;
    (this.IssueRegisterForm.get('ItemStoreList') as FormArray).at(index).get('returnQty').setValue(this.calculateData);
   
    // if(this.calculateData < 0){
    //   let tempDirection;
    //   if (localStorage.getItem('isRtl') === 'true') {
    //     tempDirection = 'rtl';
    //   } else {
    //     tempDirection = 'ltr';
    //   }
    //   const dialogRef = this.dialog.open(NegativeDialogMessageComponent, {
    //     data: this.spareStockBySpRequest,
    //     direction: tempDirection,
    //   });
    // }

  //     console.log(this.spareStockBySpRequest);
  // console.log(calculateData);
}
  
  onSparesCategorySelectionChange(dropdown) {
    var departmentNameId = this.IssueRegisterForm.value['departmentNameId'];
    var sparesCategoryId = this.IssueRegisterForm.value['sparesCategoryId'];
    this.IssueRegisterService.getSelectedItemStorebyDepartmentNameIdAndSparesCategoryId(departmentNameId, dropdown).subscribe(res => {
      this.selectedItemStoreList = res
      console.log(this.selectedItemStoreList);
      this.clearList();
      this.getItemStoreListonClick();

      this.IssueRegisterService.getselectedPartNoByDepartmentNameIdAndSpareCategoryIdFromItemStore(departmentNameId, sparesCategoryId).subscribe(res => {
        this.selectedItemDetails = res
        // console.log(this.selectedDepartmentNames);      
      });
    });
    this.isShown = true;
  }

  onPartNoSelectionChange(dropdown) {
    this.itemName = "";
    var departmentNameId = this.IssueRegisterForm.value['departmentNameId'];
    var sparesCategoryId = this.IssueRegisterForm.value['sparesCategoryId'];
    this.IssueRegisterService.getselectedItemNameByDepartmentNameIdAndSpareCategoryIdItemDetailIdFromItemStore(departmentNameId, sparesCategoryId, dropdown).subscribe(res => {
      console.log(departmentNameId + "--" + sparesCategoryId + "--" + dropdown);
      //    console.log(res);
      for (let i = 0; i <= res.length; i++) {
        console.log("dddd");
        console.log(res[i].text);
        this.itemName = res[i].text;
        //  console.log(this.itemName);
      }
    });
    this.IssueRegisterService.getSelectedItemStorebyDepartmentNameIdAndSparesCategoryIdAndItemDetailId(departmentNameId, sparesCategoryId, dropdown).subscribe(res => {
      this.selectedItemStoreList = res

      for (let i = 0; i <= this.selectedItemStoreList.length; i++) {
        console.log(this.selectedItemStoreList[i])
        //  console.log(this.selectedItemStoreList[i].);
      }
      // console.log(this.selectedItemStoreList.);
      this.clearList();
      this.getItemStoreListonClick();
    });
    this.isShown = true;
  }

  // getselectedItemDetails(){
  //   this.itemDetailService.getselectedItemDetail().subscribe(res=>{
  //     this.selectedDepartmentNames=res
  //   });
  // }

  // getSelectedDepartment() {
  //   this.departmentNameService.getselectedDepertments().subscribe(res => {
  //     this.selectedDepartmentNames = res
  //     // console.log(this.selectedDepartmentNames);      
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.departmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getselectedSparesCategory() {
    this.IssueRegisterService.getselectedSparesCategory().subscribe(res => {
      this.selectedSparesCategory = res
      //console.log(this.selectedSparesCategory);      
    });
  }
  getselectedItemDetails() {
    this.IssueRegisterService.getselectedItemDetails().subscribe(res => {
      this.selectedItemDetails = res
    });
  }
  getselectedIssueStatuses() {
    this.IssueRegisterService.getselectedIssueStatuses().subscribe(res => {
      this.selectedIssueStatuses = res
    });
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.IssueRegisterForm.get('issueRegisterId').value;

    this.IssueRegisterForm.value.ItemStoreList = this.IssueRegisterForm.value.ItemStoreList.filter(x => x.isChecked == true);
    // if(this.IssueRegisterForm.value.ItemStoreList.where(x=>x.availableQty === x.issuedQty)){
    //   console.log("valid");
    // }
    //console.log(this.IssueRegisterForm.value.ItemStoreList.filter(x=>x.availableQty === ));
    // for (var char of this.IssueRegisterForm.value.ItemStoreList) {
    //   if(char.availableQty == char.issuedQty){
    //     console.log("saved");
    //   }
    // }

    console.log(this.IssueRegisterForm.value)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

        if (result) {
          this.IssueRegisterService.update(+id, this.IssueRegisterForm.value).subscribe(response => {
            this.router.navigateByUrl('/issue-management/add-issueregister');
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
      this.IssueRegisterService.submit(this.IssueRegisterForm.value).subscribe(response => {
        //this.router.navigateByUrl('/issue-management/issueregister-list');
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
    const id = row.issueRegisterId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.IssueRegisterService.delete(id).subscribe(() => {
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
