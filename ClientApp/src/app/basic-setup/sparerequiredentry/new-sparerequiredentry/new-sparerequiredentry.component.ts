import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { SpareRequiredEntryService } from '../../service/SpareRequiredEntry.service';
import { MaintenanceCategoryService } from '../../service/MaintenanceCategory.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { SpareRequiredEntry } from '../../models/SpareRequiredEntry';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { JobDescriptionEntryPmsService } from '../../service/JobDescriptionEntryPms.service';

import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';


@Component({
  selector: 'app-new-sparerequiredentry',
  templateUrl: './new-sparerequiredentry.component.html',
  styleUrls: ['./new-sparerequiredentry.component.sass']
})
export class NewSpareRequiredEntryComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  SpareRequiredEntryForm: FormGroup;
  validationErrors: string[] = [];
  selectedItemName:SelectedModel[]; 
  selectedDepartmentNames:SelectedModel[]; 
  selectedJobDescription:SelectedModel[]; 
  selectedJicValue:SelectedModel[];
  selectedMaintenanceCategoryByDepartment:SelectedModel[];
  selectedPmsNumberEntry:SelectedModel[];
  getSpareRequiredEntryList:SpareRequiredEntry[];
  isShown: boolean = false ;
  masterData = MasterData;
  getjicid: number;
  getjic: string;
  userRole = Role;
  role:any;
  branchId:any;
  traineeId:any;
  maintenanceTypeId:any;
  maintenanceCategoryId:any;
  byWhomId:any;
  isLoading = false;

  displayedColumns: string[] = [ 'ser', 'departmentName','itemName','jobDescription','actions'];
  dataSource: MatTableDataSource<SpareRequiredEntry> = new MatTableDataSource();
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  constructor(private snackBar: MatSnackBar,private baseSchoolNameService: BaseSchoolNameService,private confirmService: ConfirmService,private JobDescriptionEntryPmsService: JobDescriptionEntryPmsService,private authService: AuthService,private SpareRequiredEntryService: SpareRequiredEntryService,private MaintenanceCategoryService: MaintenanceCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('spareRequiredEntryId'); 
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    if (id) {
      this.pageTitle = 'Edit Spare Required Entry';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SpareRequiredEntryService.find(+id).subscribe(
        res => {
          this.SpareRequiredEntryForm.patchValue({          

            spareRequiredEntryId: res.spareRequiredEntryId,
            departmentNameId: res.departmentNameId,
            equipmentNameId: res.equipmentNameId,
            jobDescriptionEntryPmsId: res.jobDescriptionEntryPmsId,
            itemDetailId:res.itemDetailId,
            maintenanceTypeId:res.maintenanceTypeId,
            maintenanceCategoryId:res.maintenanceCategoryId,
            byWhomId:res.byWhomId,
            pmsNumberEntryId:res.pmsNumberEntryId,
            remarks:res.remarks,
          });       
        this.onDepartmentSelectionChange(res.departmentNameId);
        this.getItemDetail();
        this.onJobDescriptionSelectionChangeGetPms(res.jobDescriptionEntryPmsId)
        }
      );
    } else {
      this.pageTitle = 'Create Spare Required Entry';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.SpareRequiredEntryForm.get('departmentNameId').setValue(this.branchId);
     
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(res)
        this.SpareRequiredEntryForm.get('unitId').setValue(res.thirdLevel);
        // this.getPmsNumberEntry(res.thirdLevel, this.branchId);        
      });

      this.getSelectedDepartmentName(this.branchId);
      this.getItemDetail()
      this.getSpareRequiredEntries();
      //this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.SpareRequiredEntryForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    this.getJobDescription();
  }
  intitializeForm() {
    this.SpareRequiredEntryForm = this.fb.group({
      spareRequiredEntryId: [0],
      departmentNameId: [],
      equipmentNameId: [''],
      unitId:[''],
      jobDescriptionEntryPmsId: [''],
      itemDetailId:[''],
      maintenanceTypeId:[''],
      maintenanceCategoryId:[''],
      byWhomId:[''],
      pmsNumberEntryId:[''],
      remarks:[''],
      isActive: [true],
    
    })
  }
  getSpareRequiredEntries() {
    this.isLoading = true;
    this.SpareRequiredEntryService.getSpareRequiredEntrys(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }


  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getSpareRequiredEntries();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getSpareRequiredEntries();
  } 

  getPmsNumberEntry(unitId,departmentNameId){
    console.log(unitId,departmentNameId);
    this.JobDescriptionEntryPmsService.getPmsNumberEntry(unitId,departmentNameId).subscribe(res=>{
      this.selectedPmsNumberEntry=res
    }); 
  }


  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onDepartmentSelectionChange(dropdown){
    this.isShown=true;
    if(dropdown.isUserInput) {
      this.SpareRequiredEntryService.getSpareRequiredEntryByDepartment(dropdown.source.value).subscribe(res=>{
        this.getSpareRequiredEntryList=res
        console.log( this.getSpareRequiredEntryList);
      });
    }
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.SpareRequiredEntryService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      //console.log(res)
      //console.log(res)
    }); 
  }
  getItemDetail(){
    var departmentNameId = this.SpareRequiredEntryForm.value['departmentNameId'];
    this.SpareRequiredEntryService.getItemDetail(departmentNameId).subscribe(res=>{
      this.selectedItemName=res
    }); 
  }
  getJobDescription(){
    this.SpareRequiredEntryService.getJobDescription().subscribe(res=>{
      this.selectedJobDescription=res
    }); 
  }
  onJobDescriptionSelectionChangeGetPms(jobDescriptionEntryPmsId){
    console.log(jobDescriptionEntryPmsId);
   this.SpareRequiredEntryService.getJicByJobDescriptionEntryPmsId(jobDescriptionEntryPmsId).subscribe(res=>{
     this.selectedJicValue=res      
     this.getjicid = this.selectedJicValue[0].value,
     this.getjic = this.selectedJicValue[0].text
     console.log(this.getjic);
   });
   this.JobDescriptionEntryPmsService.find(jobDescriptionEntryPmsId).subscribe(res=>{
    console.log(res);
    this.maintenanceTypeId=res.maintenanceTypeId;   //Id Pass   
    this.maintenanceCategoryId=res.maintenanceCategoryId; 
    this.byWhomId=res.byWhomId; 
    this.SpareRequiredEntryForm.get('maintenanceTypeId').setValue(this.maintenanceTypeId);
    this.SpareRequiredEntryForm.get('maintenanceCategoryId').setValue(this.maintenanceCategoryId);
    this.SpareRequiredEntryForm.get('byWhomId').setValue(this.byWhomId);
    this.SpareRequiredEntryForm.get('pmsNumberEntryId').setValue(res.pmsNumberEntryId);
    this.SpareRequiredEntryForm.get('itemDetailId').setValue(res.itemDetailId);
    console.log(this.getjic);
  });
  }
  
  onSubmit() {
    const id = this.SpareRequiredEntryForm.get('spareRequiredEntryId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.SpareRequiredEntryService.update(+id,this.SpareRequiredEntryForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-sparerequiredentry');
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
      this.SpareRequiredEntryService.submit(this.SpareRequiredEntryForm.value).subscribe(response => {
        //this.router.navigateByUrl('/basic-setup/sparerequiredentry-list');
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
    const id = row.spareRequiredEntryId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SpareRequiredEntryService.delete(id).subscribe(() => {

         // this.getCastes();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
          this.reloadCurrentRoute();
        })
      }
    })    
  }

}
