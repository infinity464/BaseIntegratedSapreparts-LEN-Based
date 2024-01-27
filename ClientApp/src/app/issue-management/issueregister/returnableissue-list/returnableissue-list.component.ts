import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { IssueRegisterService } from '../../service/IssueRegister.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from 'src/app/basic-setup/service/DepartmentName.service';
import { ItemStor } from 'src/app/spares-management/models/ItemStor';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { style } from '@angular/animations';
import { IssueRegister } from '../../models/IssueRegister';
import { MasterData } from 'src/assets/data/master-data';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { DemandService } from 'src/app/spares-management/service/Demand.service';
import { AcceptanceService } from 'src/app/spares-management/service/Acceptance.service';

@Component({
  selector: 'app-returnableissue-list',
  templateUrl: './returnableissue-list.component.html',
  styleUrls: ['./returnableissue-list.component.sass']
})
export class ReturnableIssueListComponent implements OnInit {
  pageTitle: string;
  destination:string;
  masterData = MasterData;
  btnText:string;
  IssueRegisterForm: FormGroup;
  validationErrors: string[] = [];
  selectedItemDetails:SelectedModel[]; 
  selectedIssueStatuses:SelectedModel[]; 
  selectedReturnStatus:SelectedModel[];
  selectedDepartmentNames:SelectedModel[];
  selectedSparesCategory:SelectedModel[];
  selectedItemCategory:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedIssueType:SelectedModel[];
  IssueRegisterList:IssueRegister[];
  isShown: boolean = false ;
  itemCategoryId:any;
  isDynamicFormShown:boolean=false;
  checked:boolean=false;
  message:string;
  titleColor:'#FF0000';
  itemName:string;
  userRole = Role;
  departmentId:any;
  unitId:any;
  issueTypeId:any;

  
  traineeId:any;
  role:any;
  branchId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedColumns: string[] = [ 'ser', 'partNO', 'itemName', 'issueStatusId','issueQty', 'issueDate','issuedTo', 'actions'];
  constructor(private snackBar: MatSnackBar,private ItemDetailService: ItemDetailService,private AcceptanceService: AcceptanceService,private DemandService: DemandService,private authService: AuthService,private itemDetailService:ItemDetailService,private departmentNameService:DepartmentNameService,private confirmService: ConfirmService,private IssueRegisterService: IssueRegisterService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    
    this.intitializeForm();

    this.pageTitle = 'Returnable Register';

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
  //  this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedSparesCategory();
    this.getSelectedIssueType();
    this.getSelectedItemCategory();
    this.getSelectedReturnStatus();
  }
  intitializeForm() {
    this.IssueRegisterForm = this.fb.group({
      issueRegisterId: [0],
      unitId:[''],
      sparesCategoryId:[],
      departmentNameId:[],
      itemCategoryId:[''],
      issueTypeId:[''],
      equipmentNameId:[''],
      ItemStoreList: this.fb.array([
        this.createIssueRegisterData()
      ]),
    })
  }

  onIssueTypeSelectionChange(dropdown){
    if(dropdown.isUserInput) {
      this.issueTypeId = dropdown.source.value;

      this.isShown=true;
      this.IssueRegisterService.getIssueRegisterList(this.unitId,this.departmentId,this.itemCategoryId,this.issueTypeId).subscribe(res=>{
        this.IssueRegisterList=res.filter(x=>x.issueQty !== x.returnQty);
        console.log(this.IssueRegisterList)
        this.clearList();
        this.getItemStoreListonClick();
      });
    }
  }
  
  getControlLabel(index: number, type: string) {
    return (this.IssueRegisterForm.get('ItemStoreList') as FormArray).at(index).get(type).value;
  }

  private createIssueRegisterData() {

    return this.fb.group({
      deno: [''],
      itemDetail: [''],
      partNo: [''],
      returnStatusId:[''],
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
      returnBy:[''],
      issueQty:[''],
      issueDate:[''],
      itemName:[''],
      returnDate:[''],
      issueRegisterId:['']
    });
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  onButtonClick(event, data){

    console.log("data");
    console.log(data);

          if (data.value.issueQty < data.value.returnQty+Number(data.value.returnQtyUpdate)) {
        this.message = 'Issue QTY not Smaller then Return QTY';
        this.confirmService.confirm(this.message, 'Are you sure you want to do this', 'OK', 'red').subscribe(result => {
          if (result) {
            console.log(result);
            this.snackBar.open('OK ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }
        })
      }
      else{
        this.confirmService.confirm('Confirm Return message', 'Are You Sure Return This  Item?').subscribe(result => {

          if (result) {
            this.IssueRegisterService.addingReturnQtyToStore(data.value.itemStoreId,data.value).subscribe(response => {
              this.IssueRegisterService.insertLogTable(data.value).subscribe(response => {
                this.reloadCurrentRoute();
                //this.router.navigateByUrl('/issue-management/issueregister-list');
              })
              this.router.navigateByUrl('/issue-management/returnableissue-list');
              this.snackBar.open('Return Successfully ', '', {
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

      }
    // console.log(this.IssueRegisterForm.value);

      // if (data.value.availableQty < data.value.issuedQty) {
      //   this.message = 'Available QTY not Smaller then Issue QTY';
      //   this.confirmService.confirm(this.message, 'Are you sure you want to do this', 'OK', 'red').subscribe(result => {
      //     if (result) {
      //       console.log(result);
      //       this.snackBar.open('OK ', '', {
      //         duration: 2000,
      //         verticalPosition: 'bottom',
      //         horizontalPosition: 'right',
      //         panelClass: 'snackbar-success'
      //       });
      //     }
      //   })
      // }
      // this.isDynamicFormShown = true;
  
  clearList() {
    const control = <FormArray>this.IssueRegisterForm.controls["ItemStoreList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getItemStoreListonClick() {
    const control = <FormArray>this.IssueRegisterForm.controls["ItemStoreList"];
    for (let i = 0; i < this.IssueRegisterList.length; i++) {
      control.push(this.createIssueRegisterData());
    }
    console.log('12');
    this.IssueRegisterForm.patchValue({ ItemStoreList: this.IssueRegisterList });
  }
  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
    this.departmentId = this.IssueRegisterForm.value['departmentNameId'];
    this.itemCategoryId = dropdown.source.value;

    this.IssueRegisterForm.get('itemCategoryId').setValue(this.itemCategoryId);
     }
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

  getSelectedIssueType(){
    this.IssueRegisterService.getSelectedIssueType().subscribe(res=>{
      this.selectedIssueType=res.filter(x=>x.value !=1) 
    });
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

  getSelectedItemCategory() {
    this.DemandService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }

  getSelectedReturnStatus() {
    this.IssueRegisterService.getSelectedReturnStatus().subscribe(res => {
      this.selectedReturnStatus = res;
    });
  }
  

  getSelectedIssueRegisterList(){
          var departmentNameId =this.IssueRegisterForm.value['departmentNameId'];
          var sparesCategoryId =this.IssueRegisterForm.value['sparesCategoryId'];
          console.log(departmentNameId);
          console.log(sparesCategoryId);
          this.IssueRegisterService.getSelectedIssueRegisterList(departmentNameId,sparesCategoryId).subscribe(res=>{
            this.IssueRegisterList=res
            console.log(this.IssueRegisterList);
          });
          this.isShown=true;
  }


  // getSelectedDepartment(){
  //   this.departmentNameService.getselectedDepertments().subscribe(res=>{
  //     this.selectedDepartmentNames=res
  //    // console.log(this.selectedDepartmentNames);      
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.departmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getselectedSparesCategory(){
    this.IssueRegisterService.getselectedSparesCategoryForReturnableIssue().subscribe(res=>{
      this.selectedSparesCategory=res
      //console.log(this.selectedSparesCategory);      
    });
  }
  

}
