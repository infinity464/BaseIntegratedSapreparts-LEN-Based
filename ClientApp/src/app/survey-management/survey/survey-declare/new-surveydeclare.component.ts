import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import {SurveyService} from '../../service/Survey.Service';
// import { SurveyService } from '../../service/Survey.service';
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
import { IssueRegisterService } from 'src/app/issue-management/service/IssueRegister.service';

@Component({
  selector: 'app-new-surveydeclare',
  templateUrl: './new-surveydeclare.component.html',
  styleUrls: ['./new-surveydeclare.component.sass']
})
export class NewSurveyDeclareComponent implements OnInit {
  pageTitle: string;
  destination: string;
  btnText: string;
  SurveyForm: FormGroup;
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
  selectedSurveyAuthority:SelectedModel[];
  selectedSurveyItemType:SelectedModel[];
  surveyDeclareList:any;
  getitemdetailid: number;
  getitemname: string;
  itemValue:any;
  selectedItemStoreList: ItemStor[];
  isShown: boolean = false;
  isDynamicFormShown: boolean = false;
  checked: boolean = false;
  message: string;
  titleColor: '#FF0000';
  itemName: string;
  trainingCrewId:number;
  itemDetailId:number;
  masterData = MasterData;
  userRole = Role;
  departmentId:any;
  unitId:any;
  partNo:any;
  itemCategoryId:any;
  isShownForEquipment:boolean = false;
  qtyAlart:boolean = false;

  options = [];
  filteredOptions;

  traineeId:any;
  role:any;
  branchId:any;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  showPattOrModelDropdown=false;
  showIMC=false;
  displayedColumns: string[] = [ 'ser', 'nameOfItem','toolsLocationName', 'issueQty', 'surveyStatus','action'];
  constructor(private snackBar: MatSnackBar,private IssueRegisterService:IssueRegisterService,private ServeyService:SurveyService,private ItemDetailService:ItemDetailService,private DemandService:DemandService,private AcceptanceService: AcceptanceService, private authService: AuthService, private itemDetailService: ItemDetailService, private departmentNameService: DepartmentNameService, private confirmService: ConfirmService, private SurveyService: SurveyService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('surveyItemId');

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    

    if (id) {
      this.pageTitle = 'Edit Survey';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SurveyService.find(+id).subscribe(
        res => {
          this.SurveyForm.patchValue({

            surveyItemId: res.surveyItemId,
            denoId: res.denoId,
            itemStatusId: res.itemStatusId,
            itemDetailId: res.itemDetailId,
            qty: res.qty,
            surveyNo: res.surveyNo,
            byWhom:res.byWhom,
            nsdSrNo: res.nsdSrNo,
            surveyDate: res.surveyDate,
            itemSerNo: res.itemSerNo,
            surveyDocument: res.surveyDocument,
            demandNo: res.demandNo,
            returnStore: res.returnStore,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            unitId: res.unitId,
            departmentNameId: res.departmentNameId,
            itemCategoryId: res.itemCategoryId,
            equipmentNameId: res.equipmentNameId,
            surveyAuthorityId: res.surveyAuthorityId,
            surveyItemTypeId: res.surveyItemTypeId
          });
        }
      );
    } else {
      this.pageTitle = 'Survey Declare';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.SurveyForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.SurveyForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    this.getSelectedItemCategory();
    this.getSelectedSurveyAuthority();
    this.getSelectedSurveyItemType();
    this.getselectedToolsLocation();
    this.getListForSurveyDeclare();
  }
  intitializeForm() {
    this.SurveyForm = this.fb.group({
    surveyItemId: [0],
    denoId: [''],
    itemStatusId: [''],
    surveyAuthorityId:[],
    surveyItemTypeId:[''],
    authorityId:[''],
    itemDetailId: [''],
    typeOfItem:[''],
    toolsLocationId:[''],
    qty: [''],
    part:[''],
    surveyNo: [''],
    nsdSrNo: [''],
    surveyDate: [''],
    byWhom:[''],
    itemSerNo: [''],
    surveyDocument: [''],
    demandNo: [''],
    returnStore: [''],
    menuPosition: [''],
    isActive: [true],
    unitId: [''],
    departmentNameId: [''],
    itemCategoryId: [''],
    equipmentNameId: [''],
    pattorimc:[''],
    imc:[''],
    ItemStoreList: this.fb.array([
      this.createIssueRegisterData()
    ]),
    })
           //autocomplete
           this.SurveyForm.get('part').valueChanges
           .subscribe(value => {
             this.getSelectedTraineeByPno(value);
           })
   
           this.SurveyForm.get('imc').valueChanges
           .subscribe(value => {
             this.getSelectedImc(value);
           })
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
      qtyAlart:[''],
      itemReceivedDate: [''],
      issueTypeId:[''],
      nameOfItem:[''],
      warrantyEndDate: [],
      dateOfExpiry:[''],
      acctStore: [''],
     // nameOfItem:[''],
      toolsLocationName:[''],
      issueQty:[''],
      surveyQty:[''],
      surveyStatus:[''],
      issueRegisterId:[''],
      availableQtyAfterIssue:['']
    });
  }

  getControlLabel(index: number, type: string) {
    return (this.SurveyForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  }
   //autocomplete
   onTraineeSelectionChanged(item) {
    this.itemDetailId = item.value
    this.SurveyForm.get('itemDetailId').setValue(item.value);
    this.SurveyForm.get('part').setValue(item.text);

    this.getItemNameById(this.itemDetailId);
    
    console.log("unitid");
    console.log(this.unitId);
    console.log("department Id");
    console.log(this.departmentId);
    console.log("itemDetailId");
    console.log(this.itemDetailId);
    console.log("item category id");
    console.log(this.itemCategoryId);
    this.SurveyService.getListForSurveyDeclareFilter(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
      this.surveyDeclareList=res;


      if(this.itemCategoryId ===1){
        console.log("kkkkkkkkkkkkk");
        this.isShownForEquipment=true;
      }
      else{
        this.isShown=true;
      }

      this.clearList();
      this.getItemStoreListonClick();

      console.log("surveyDeclareList")
      console.log(this.surveyDeclareList)
    }); 
  }

  onImcSelectionChange(item) {
    this.itemDetailId = item.value
    this.SurveyForm.get('itemDetailId').setValue(item.value);
    this.SurveyForm.get('imc').setValue(item.text);

    this.getItemNameById(this.itemDetailId);
    this.SurveyService.getListForSurveyDeclareFilter(this.unitId,this.departmentId,this.itemCategoryId,this.itemDetailId).subscribe(res=>{
      this.surveyDeclareList=res;

      if(this.itemCategoryId ===1){
        console.log("kkkkkkkkkkkkk");
        this.isShownForEquipment=true;
      }
      else{
        this.isShown=true;
      }

      this.clearList();
      this.getItemStoreListonClick();
      
      console.log("surveyDeclareList")
      console.log(this.surveyDeclareList)
    }); 
  }
  // getControlLabel(index: number, type: string) {
  //   return (this.SurveyForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  // }

  clearList() {
    const control = <FormArray>this.SurveyForm.controls["ItemStoreList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getItemStoreListonClick() {
    const control = <FormArray>this.SurveyForm.controls["ItemStoreList"];
    for (let i = 0; i < this.surveyDeclareList.length; i++) {
      control.push(this.createIssueRegisterData());
    }
    console.log('12');
    console.log(this.surveyDeclareList);
    this.SurveyForm.patchValue({ ItemStoreList: this.surveyDeclareList });
  }

  getSelectedImc(pno){
    var itemCategoryId = this.SurveyForm.value['itemCategoryId'];
    var departmentNameId = this.SurveyForm.value['departmentNameId'];
    var unitId = this.SurveyForm.value['unitId'];
    this.DemandService.getSelectedImcForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }

  getSelectedTraineeByPno(pno) {
    var itemCategoryId = this.SurveyForm.value['itemCategoryId'];
    var departmentNameId = this.SurveyForm.value['departmentNameId'];
    var unitId = this.SurveyForm.value['unitId'];
    this.DemandService.getSelectedPartNoByNameForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
      this.partNo = this.SurveyForm.value['part'].value;
       console.log("dddd");
    })
  }


  OnTextCheck(value, index, issueQty,availableQtyAfterIssue) {
    console.log(value);
    var issueQtyValue = (this.SurveyForm.get('ItemStoreList') as FormArray)
      .at(index)
      .get(issueQty).value;

    var availableQtyValue = (this.SurveyForm.get('ItemStoreList') as FormArray)
      .at(index)
      .get(availableQtyAfterIssue).value;

    var calculateData = issueQtyValue - availableQtyValue;
    // (this.SurveyForm.get('ItemStoreList') as FormArray)
    //   .at(index)
    //   .get("returnQty")
    //   .setValue(calculateData);
    console.log(issueQtyValue,availableQtyValue,calculateData);

    if(value > calculateData){
      console.log("invalid");
      this.qtyAlart = true;
      
      (this.SurveyForm.get('ItemStoreList') as FormArray)
      .at(index)
      .get("qtyAlart")
      .setValue(1);
    }else{
      this.qtyAlart = false;
      (this.SurveyForm.get('ItemStoreList') as FormArray)
      .at(index)
      .get("qtyAlart")
      .setValue(0);
    }
  }


  getListForSurveyDeclare(){

    //var unitId = this.SurveyForm.value['unitId'];
    var departmentId = this.SurveyForm.value['departmentNameId'];
    this.ItemDetailService.findBaseSchoolNameId(departmentId).subscribe(res=>{
      var unitId=res.thirdLevel;

      this.SurveyForm.get('unitId').setValue(this.unitId);
      console.log("UnitID");
    console.log(this.unitId);
    this.SurveyService.getListForSurveyDeclare(unitId,departmentId).subscribe(res=>{
      this.surveyDeclareList=res;
      console.log("surveyDeclareList")
      console.log(this.surveyDeclareList)
    }); 
    //this.getSelectedDemandNo();
    })
    
  }
  onPattOrModelSelectionChange(){
    var pattorimc = this.SurveyForm.value['pattorimc'];
    console.log(pattorimc+"oo");
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
  getselectedToolsLocation(){
    this.IssueRegisterService.getselectedToolsLocation().subscribe(res => {
      this.selectedToolsLocation = res
    });
  }
  getSelectedSurveyAuthority() {
    this.ServeyService.getSelectedSurveyAuthority().subscribe(res => {
      this.selectedSurveyAuthority = res;
    });
  }

  getSelectedSurveyItemType() {
    this.ServeyService.getSelectedSurveyItemType().subscribe(res => {
      this.selectedSurveyItemType = res;
    });
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log("rrrrrrrrr");
      console.log(this.selectedDepartmentNames)
    }); 
  }

  onCheckboxChange(event, data) {
    console.log("event");
    console.log(data.value);
    var qty;
    if(this.itemCategoryId !=1){
      console.log("spares");
     qty=data.value.availableQtyAfterIssue + data.value.surveyQty;
    }
    else{
      console.log("equipment");
      qty= data.value.availableQtyAfterIssue + 1;
    }
    var status=0;
    console.log(qty);
    this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ServeyService.updateSurvayStatus(data.value.issueRegisterId,qty).subscribe(() => {
          this.reloadCurrentRoute();
          this.getListForSurveyDeclare();
          this.snackBar.open('Information Updated Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    }) 
  }
  updateSurvayStatusList(issueRegisterId) {
    // const id = row.issueRegisterId; 
    var status = 1;
    console.log(issueRegisterId);
    this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
      console.log(result);
      // if (result) {
      //   this.ServeyService.updateSurvayStatus(issueRegisterId,status).subscribe(() => {
      //     this.getListForSurveyDeclare();
      //     this.snackBar.open('Information Updated Successfully ', '', {
      //       duration: 2000,
      //       verticalPosition: 'bottom',
      //       horizontalPosition: 'right',
      //       panelClass: 'snackbar-danger'
      //     });
      //   })
      // }
    })
  }
  // reloadCurrentRoute() {
  //   let currentUrl = this.router.url;
  //   this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
  //     this.router.navigate([currentUrl]);
  //   });
  // }
  updateSurvayStatus(row) {
    const id = row.issueRegisterId; 
    var status = 1;
    console.log(id);
    this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ServeyService.updateSurvayStatus(id,status).subscribe(() => {
           this.reloadCurrentRoute();
          this.getListForSurveyDeclare();
          this.snackBar.open('Information Updated Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
  }

  onDepartmentSelectionChange() {
    this.departmentId = this.SurveyForm.value['departmentNameId']; 
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.SurveyForm.get('unitId').setValue(this.unitId);

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
    this.departmentId = this.SurveyForm.value['departmentNameId'];
    this.itemCategoryId = dropdown.source.value;
  //  console.log("unit id");
  //  console.log(this.unitId);
  //  console.log("department id");
  //  console.log(this.departmentId);
  //  console.log("item category");
  //  console.log(this.itemCategoryId);
    this.SurveyForm.get('itemCategoryId').setValue(this.itemCategoryId);
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
  }
  //autocomplete for PartNo
  onPartNoSelectionChanged(item) {
    console.log(item.value);
    this.itemDetailId = item.value
    this.SurveyForm.get('itemDetailId').setValue(item.value);
    this.SurveyForm.get('partNo').setValue(item.text);
  }
  
  GetDepartmentNameById(baseNameId){    
    this.departmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.SurveyForm.get('surveyItemId').value;

   // this.SurveyForm.value.ItemStoreList = this.SurveyForm.value.ItemStoreList.filter(x => x.isChecked == true);

    console.log(this.SurveyForm.value)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

        if (result) {
          this.SurveyService.update(+id, this.SurveyForm.value).subscribe(response => {
            this.router.navigateByUrl('/issue-management/add-Survey');
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
      this.SurveyService.submit(this.SurveyForm.value).subscribe(response => {
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
    const id = row.SurveyId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SurveyService.delete(id).subscribe(() => {
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
