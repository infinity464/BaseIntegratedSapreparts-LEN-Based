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
  selector: 'app-new-survey',
  templateUrl: './new-survey.component.html',
  styleUrls: ['./new-survey.component.sass']
})
export class NewSurveyComponent implements OnInit {
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
  getitemdetailid: number;
  getitemname: string;
  itemValue:any;
  IssueRegisterSurveyList: any[];
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

  options = [];
  filteredOptions;

  traineeId:any;
  role:any;
  branchId:any;

  displayedColumns: string[] = [ 'ser', 'nameOfItem','authority', 'itemType', 'surveyNo', 'surveyDate', 'byWhom'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar,private issueRegisterService: IssueRegisterService,private ServeyService:SurveyService,private ItemDetailService:ItemDetailService,private DemandService:DemandService,private AcceptanceService: AcceptanceService, private authService: AuthService, private itemDetailService: ItemDetailService, private departmentNameService: DepartmentNameService, private confirmService: ConfirmService, private SurveyService: SurveyService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

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
      this.pageTitle = 'Survey';
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
  }
  intitializeForm() {
    this.SurveyForm = this.fb.group({
      departmentNameId:[],
      unitId:[],
      issueRegisterId: [''],    
      surveyAuthorityId: [''],
      surveyItemTypeId: [''],
      surveyNumber:[],
      surveyDate:[''],
      byWhom:[''],    
    })
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

  onDepartmentSelectionChange() {
    this.departmentId = this.SurveyForm.value['departmentNameId']; 
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.SurveyForm.get('unitId').setValue(this.unitId);

     this.ServeyService.getSelectedEquipmentName(this.unitId,this.departmentId).subscribe(res => {
      this.selectedEquipment = res;
    }); 
    this.ServeyService.getSurveyList(this.unitId,this.departmentId).subscribe(res => {
      this.IssueRegisterSurveyList = res;
      console.log(this.IssueRegisterSurveyList);
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
   console.log("unit id");
   console.log(this.unitId);
   console.log("department id");
   console.log(this.departmentId);
   console.log("item category");
   console.log(this.itemCategoryId);
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
    //const id = this.SurveyForm.get('departmentNameId').value;

   // this.SurveyForm.value.ItemStoreList = this.SurveyForm.value.ItemStoreList.filter(x => x.isChecked == true);

    console.log(this.SurveyForm.value)
    // if (id) {
    //   this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

    //     if (result) {
    //       this.SurveyService.update(+id, this.SurveyForm.value).subscribe(response => {
    //         this.router.navigateByUrl('/issue-management/add-Survey');
    //         this.snackBar.open('Information Updated Successfully ', '', {
    //           duration: 2000,
    //           verticalPosition: 'bottom',
    //           horizontalPosition: 'right',
    //           panelClass: 'snackbar-success'
    //         });
    //       }, error => {
    //         this.validationErrors = error;
    //       })
    //     }
    //   })
    // } else {
      this.SurveyService.saveSurvey(this.SurveyForm.value).subscribe(response => {
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
    //}

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
