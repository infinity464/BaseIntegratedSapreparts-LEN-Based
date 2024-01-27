import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MaintenanceScheduleService } from '../../service/MaintenanceSchedule.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MaintenanceSchedule } from '../../models/MaintenanceSchedule';
import { MasterData } from 'src/assets/data/master-data';
import { MaintenanceSubCategoryService } from '../../service/maintenanceSubCategory.service';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-maintenanceschedule',
  templateUrl: './new-maintenanceschedule.component.html',
  styleUrls: ['./new-maintenanceschedule.component.sass']
})
export class NewMaintenanceScheduleComponent implements OnInit {
  pageTitle: string;
  destination: string;
  btnText: string;
  departmentNameId: number;
  maintenanceCategoryId: number;
  maintenanceSubCategoryId: number;
  MaintenanceScheduleForm: FormGroup;
  validationErrors: string[] = [];
  selectedStatus: SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedAirCraftName: SelectedModel[];
  selectedType: SelectedModel[];
  selectedCategory: SelectedModel[];
  selectedSubCategory: SelectedModel[];
  selectedExtensionValue: SelectedModel[];
  selectedMaintenanceTypes: SelectedModel[];
  selectedMaintenanceTypes1: any;
  allowedExtension: string;
  selectedMaintenancePlanning: SelectedModel[];
  selectedNestInsDateValue: SelectedModel[];

  getsubcategoryid: number;
  getmaintenanceplanningid: number;
  getextensionname: string;
  getnextdate: Date;
  maintenanceScheduleList: MaintenanceSchedule[];
  isShown: boolean = false;
  masterData = MasterData;

  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  

  displayedColumns: string[] = ['ser', 'airCraftName', 'categoryType', 'category', 'maintenanceDocument', 'actions'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar, private authService: AuthService, private MaintenanceSubCategoryService: MaintenanceSubCategoryService, private confirmService: ConfirmService, private MaintenanceScheduleService: MaintenanceScheduleService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('maintenanceScheduleId');
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
 
    if (id) {
      this.pageTitle = 'Edit Maintenance Schedule';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.MaintenanceScheduleService.find(+id).subscribe(
        res => {
          this.MaintenanceScheduleForm.patchValue({
            maintenanceScheduleId: res.maintenanceScheduleId,
            maintenancePlanningId: res.maintenancePlanningId,
            airCraftNameId: res.airCraftNameId,
            slNo: res.slNo,
            maintenanceTypeId: res.maintenanceTypeId,
            maintenanceCategoryId: res.maintenanceCategoryId,
            //maintenanceSubCategoryId: res.maintenanceSubCategoryId,
            maintenancePlanningStatusId: res.maintenancePlanningStatusId,
            departmentNameId: res.departmentNameId,
            lastInspDate: res.lastInspDate,
            //nestInspDate: res.nestInspDate,
            allowedExtension: res.allowedExtension,
            extensionGiven: res.extensionGiven,
            extensionDay: res.extensionDay,
            requiredDay: res.requiredDay,
            maintenanceDocument: res.maintenanceDocument,
            extensionDocument: res.extensionDocument,
            othersDocument: res.othersDocument,
            jobListDocument: res.jobListDocument,
            requiredSpearsDoc: res.requiredSpearsDoc,
            requiredToolsDoc: res.requiredToolsDoc,
            requiredConsumablesDoc: res.requiredConsumablesDoc,
            toleranceDocument: res.toleranceDocument,
            remarks: res.remarks,


          });
          this.onDepartmentNameSelectionChangeGetAirCraftName(),
            //this.onDepartmentNameSelectionChangeGetMaintenanceType(res.departmentNameId),  
            this.onDepartmentNameAndTypeSelectionChangeGetCategory(res.maintenanceTypeId),
            //this.onDepartmentNameAndCategorySelectionChangeGetSubCategory(res.maintenanceCategoryId),  
            //this.onSubCategorySelectionChangeGetExtension(res.maintenanceSubCategoryId),
            this.onLastInsDateelectionChangeGetNestInspDate(res.maintenancePlanningId)
        } 
      );
    } else {
      this.pageTitle = 'Create Maintenance Schedule';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.MaintenanceScheduleForm.get('departmentNameId').setValue(this.branchId);
      this.onDepartmentNameSelectionChangeGetAirCraftName();
    }
    this.getselectedplanningStatus();
    this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedMaintenanceTypes();
    this.getselectedSubCategory();
  }
  intitializeForm() {
    this.MaintenanceScheduleForm = this.fb.group({
      maintenanceScheduleId: [0],
      maintenancePlanningId: [],
      airCraftNameId: [],
      slNo: [''],
      maintenanceTypeId: [],
      maintenanceCategoryId: [],
      //maintenanceSubCategoryId:[],
      maintenancePlanningStatusId: [1],
      departmentNameId: [],
      lastInspDate: [''],
      nestInspDate: [''],
      allowedExtension: [''],
      extensionGiven: [''],
      extensionDay: [''],
      requiredDay: [''],
      maintenanceDocument: [''],
      doc: [''],
      extensionDocument: [''],
      othersDocument: [''],
      jobListDocument: [''],
      jobList: [''],
      requiredSpearsDoc: [''],
      spearsDoc: [''],
      requiredToolsDoc: [''],
      toolsDoc: [''],
      requiredConsumablesDoc: [''],
      consumableDoc: [''],
      toleranceDocument: [''],
      remarks: [''],

      isActive: [true],

    })
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      // this.labelImport.nativeElement.value = file.name;
      console.log(file);
      // this.BIODataGeneralInfoForm.controls["picture"].setValue(event.target.files[0]);
      this.MaintenanceScheduleForm.patchValue({ 
        doc: file,
      });
    }
  }
  onDepartmentNameSelectionChangeGetAirCraftName() {
    var departmentNameId = this.MaintenanceScheduleForm.value['departmentNameId'];
    this.MaintenanceScheduleService.getAirCraftNameByDepartmentNameId(departmentNameId).subscribe(res => {
      this.selectedAirCraftName = res
    });
  }

  onDepartmentNameAndTypeSelectionChangeGetCategory(maintenanceTypeId) {
    this.MaintenanceScheduleService.getCategoryByDepartmentNameIdAndMaintenanceTypeId(maintenanceTypeId).subscribe(res => {
      this.selectedCategory = res
    });
  }
  getselectedSubCategory() {
    
    this.MaintenanceScheduleService.getselectedMaintenancePlanning().subscribe(res => {
      this.selectedSubCategory = res;
      //console.log(res)
    });
  }
  onSubCategorySelectionChangeGetExtension(maintenanceSubCategoryId) {
    this.MaintenanceScheduleService.getAllowedExtensionBySubCategoryId(maintenanceSubCategoryId).subscribe(res => {
      this.selectedExtensionValue = res
      this.getsubcategoryid = this.selectedExtensionValue[0].value,
        this.getextensionname = this.selectedExtensionValue[0].text
        
    });
  }
  onLastInsDateelectionChangeGetNestInspDate(maintenancePlanningId) {
    //var maintenanceSubCategoryId=this.MaintenanceScheduleForm.value['maintenanceSubCategoryId'];
    //this.MaintenanceScheduleForm.get('maintenanceSubCategoryId').setValue(this.maintenanceSubCategoryId);
    //console.log(this.maintenanceSubCategoryId)
    this.MaintenanceScheduleService.getAllowedNestInspDateByMaintenancePlanningId(maintenancePlanningId).subscribe(res => {
      this.selectedNestInsDateValue = res
      this.getmaintenanceplanningid = this.selectedNestInsDateValue[0].value,
        this.getnextdate = this.selectedNestInsDateValue[0].text

      // var departmentNameId=this.MaintenanceScheduleForm.value['departmentNameId'];
      // var maintenanceCategoryId=this.MaintenanceScheduleForm.value['maintenanceCategoryId'];
      // var maintenanceSubCategoryId=this.MaintenanceScheduleForm.value['maintenanceSubCategoryId'];

      // console.log(departmentNameId+"-"+maintenanceCategoryId+"-"+maintenanceSubCategoryId);

      // this.MaintenanceScheduleService.getselectedAllowedExtensionBySubCategoryIdDepartmentIdAndCategoryId(departmentNameId,maintenanceCategoryId,maintenanceSubCategoryId).subscribe(res=>{
      //   this.selectedMaintenanceTypes1=res
      //   console.log("eeeeeee")
      //   console.log(this.selectedMaintenanceTypes1);      
      // });

    });
  }
  //  getselectedAllowedExtensionBySubCategoryIdDepartmentIdAndCategoryId(departmentNameId:number,maintenanceCategoryId:number, maintenanceSubCategoryId:number){
  //   // var departmentNameId=this.MaintenanceScheduleForm.value['departmentNameId'];
  //   // var maintenanceCategoryId=this.MaintenanceScheduleForm.value['maintenanceCategoryId'];
  //   // var maintenanceSubCategoryId=this.MaintenanceScheduleForm.value['maintenanceSubCategoryId'];
  //   //var allowedExtension=this.MaintenanceScheduleForm.value['allowedExtension']
  //   this.MaintenanceScheduleService.getselectedAllowedExtensionBySubCategoryIdDepartmentIdAndCategoryId(departmentNameId,maintenanceCategoryId,maintenanceSubCategoryId).subscribe(res=>{
  //     this.selectedMaintenanceTypes1=res
  //     console.log("eeeeeee")
  //     console.log(this.selectedMaintenanceTypes1);      
  //   });

  // }
  onMaintenanceScheduleListSelectionChange(dropdown) {
    this.isShown = true;
    if (dropdown.isUserInput) {
      var departmentNameId = this.MaintenanceScheduleForm.value['departmentNameId'];
      this.MaintenanceScheduleService.maintenanceScheduleListByDepartmentAndAirCraftName(dropdown.source.value, departmentNameId).subscribe(res => {
        this.maintenanceScheduleList = res;
      });

    }
  }
  getselectedMaintenanceTypes() {
    this.MaintenanceScheduleService.getselectedMaintenanceTypes().subscribe(res => {
      this.selectedMaintenanceTypes = res
    });
  }
  // getselectedMaintenanceCategorys(){
  //   this.MaintenanceScheduleService.getselectedMaintenanceCategorys().subscribe(res=>{
  //     this.selectedCategorys=res
  //     console.log(this.selectedCategorys);      
  //   });
  // }
  // getselectedMaintenanceSubCategorys(){
  //   this.MaintenanceScheduleService.getselectedMaintenanceSubCategorys().subscribe(res=>{
  //     this.selectedSubCategorys=res
  //     console.log(this.selectedSubCategorys);      
  //   });
  // }
  getselectedplanningStatus() {
    this.MaintenanceScheduleService.getselectedplanningStatus().subscribe(res => {
      this.selectedStatus = res
    });
  }
  // getselectedDepartmentNames() {
  //   this.MaintenanceScheduleService.getselectedDepartmentNames().subscribe(res => {
  //     this.selectedDepartmentNames = res
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.MaintenanceScheduleService.getSelectedSchoolName(baseNameId).subscribe(res=>{
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
    const id = this.MaintenanceScheduleForm.get('maintenanceScheduleId').value;
    //console.log(this.MaintenanceScheduleForm)
    this.MaintenanceScheduleForm.get('lastInspDate').setValue((new Date(this.MaintenanceScheduleForm.get('lastInspDate').value)).toUTCString());
    //this.MaintenanceScheduleForm.get('nestInspDate').setValue((new Date(this.MaintenanceScheduleForm.get('nestInspDate').value)).toUTCString());
    console.log(this.MaintenanceScheduleForm.value)
    const formData = new FormData();
    for (const key of Object.keys(this.MaintenanceScheduleForm.value)) {
      const value = this.MaintenanceScheduleForm.value[key];
      formData.append(key, value);
    }


    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

        if (result) {
          this.MaintenanceScheduleService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/maintenence-planning/add-maintenanceschedule');
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
    }
    else {
      this.MaintenanceScheduleService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/maintenence-planning/maintenanceSchedule-list');
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
    const id = row.maintenanceScheduleId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.MaintenanceScheduleService.delete(id).subscribe(() => {
          //this.getMaintenanceSchedules();
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
