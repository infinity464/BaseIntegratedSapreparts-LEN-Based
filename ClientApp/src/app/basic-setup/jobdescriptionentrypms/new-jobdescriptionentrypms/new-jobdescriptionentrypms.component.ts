import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { JobDescriptionEntryPmsService } from '../../service/JobDescriptionEntryPms.service';
import { MaintenanceCategoryService } from '../../service/MaintenanceCategory.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { JobDescriptionEntryPms } from '../../models/JobDescriptionEntryPms';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { PmsNumberEntryService } from '../../service/PmsNumberEntry.service';
import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';
import { MatTableDataSource } from '@angular/material/table';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-jobdescriptionentrypms',
  templateUrl: './new-jobdescriptionentrypms.component.html',
  styleUrls: ['./new-jobdescriptionentrypms.component.sass']
})
export class NewJobDescriptionEntryPmsComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  JobDescriptionEntryPmsForm: FormGroup;
  validationErrors: string[] = [];
  selectedPmsNumberEntry:SelectedModel[]; 
  selectedDepartmentNames:SelectedModel[]; 
  selectedByWhom:SelectedModel[]; 
  selectedMaintenanceTypes:SelectedModel[]; 
  selectedMaintenanceCategory:SelectedModel[]; 
  selectedPmsValue:SelectedModel[];
  selectedMaintenanceCategoryByDepartment:SelectedModel[];
  selectedSpareRequiredStatus:SelectedModel[];
  getJobDescriptionEntryPmsList:JobDescriptionEntryPms[];
  isShown: boolean = false ;
  masterData = MasterData;
  getpmsid: number;
  getpmsno: string;
  itemDetailId:any;
  userRole = Role;
  role:any;
  isLoading = false;
  branchId:any;
  traineeId:any;
  pmsNumberEntryId:any;
  //maintenanceTypeId:any;

  displayedColumns: string[] = [ 'ser',  'itemName','maintenanceType', 'maintenanceCategory','byWhom','jic','actions'];
  dataSource: MatTableDataSource<JobDescriptionEntryPms> = new MatTableDataSource();
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  constructor(private snackBar: MatSnackBar,private baseSchoolNameService: BaseSchoolNameService,private PmsNumberEntryService:PmsNumberEntryService,private confirmService: ConfirmService,private authService: AuthService,private JobDescriptionEntryPmsService: JobDescriptionEntryPmsService,private MaintenanceCategoryService: MaintenanceCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('jobDescriptionEntryPmsId'); 
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    if (id) {
      this.pageTitle = 'Edit Job Description Entry PMS';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.JobDescriptionEntryPmsService.find(+id).subscribe(
        res => {
          this.JobDescriptionEntryPmsForm.patchValue({          

            jobDescriptionEntryPmsId: res.jobDescriptionEntryPmsId,
            departmentNameId: res.departmentNameId,
            equipmentNameId: res.equipmentNameId,
            maintenanceTypeId:res.maintenanceTypeId,
            maintenanceCategoryId:res.maintenanceCategoryId,
            byWhomId:res.byWhomId,
            itemDetailId:res.itemDetailId,
            pmsNumberEntryId:res.pmsNumberEntryId,
            jobDescription:res.jobDescription,
            jic:res.jic,
            spareRequiredStatus:res.spareRequiredStatus,
            remarks: res.remarks,
          //  spareRequiredStatusId:res.spareRequiredStatusId,
          });       
          console.log("res");
          console.log(res);
        this.onDepartmentSelectionChange(res.departmentNameId);
        this.getMaintenanceCategory(res.maintenanceTypeId)
        }
      );
    } else {
      this.pageTitle = 'Create Job Description Entry PMS';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.JobDescriptionEntryPmsForm.get('departmentNameId').setValue(this.branchId);
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(res)
        this.JobDescriptionEntryPmsForm.get('unitId').setValue(res.thirdLevel);
        this.getPmsNumberEntry(res.thirdLevel, this.branchId);        
      });
     
      this.getSelectedDepartmentName(this.branchId);
      //this.ongetMaintenanceCategory(this.maintenanceTypeId);
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.JobDescriptionEntryPmsForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    // this.getPmsNumberEntry();
  //  this.getJobDescriptionEntryPmses();
    this.getByWhom();
    this.getMaintenanceTypes();
    this.getSelectedSpareRequiredStatus();
    //this. getMaintenanceCategory()
  }
  intitializeForm() {
    this.JobDescriptionEntryPmsForm = this.fb.group({
      jobDescriptionEntryPmsId: [0],
      departmentNameId: [],
      equipmentNameId: [''],
      unitId:[''],
      maintenanceTypeId:[],
      maintenanceCategoryId:[],
      byWhomId:[],
      pmsNumberEntryId:[],
      itemDetailId:[''],
      jobDescription:[''],
      jic:[''],
      spareRequiredStatus:[],
      remarks: [''],
      isActive: [true],
   //   spareRequiredStatusId:[''],
    
    })
  }

  getJobDescriptionEntryPmses() {
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(this.paging.pageIndex, this.paging.pageSize,this.searchText,res.thirdLevel, this.branchId)

        this.isLoading = true;
        this.JobDescriptionEntryPmsService.getJobDescriptionEntryPmss(this.paging.pageIndex, this.paging.pageSize,this.searchText,res.thirdLevel, this.branchId).subscribe(response => {
          
          this.dataSource.data = response.items; 
          this.paging.length = response.totalItemsCount    
          this.isLoading = false;
          console.log(this.dataSource.data);
        })    
      });
    }
  }

  getJobDescriptionEntryPmsByUnitAndDept() {
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(this.paging.pageIndex, this.paging.pageSize,this.searchText,res.thirdLevel, this.branchId)

        this.isLoading = true;
        this.JobDescriptionEntryPmsService.getJobDescriptionEntryPmsByUnitAndDept(this.paging.pageIndex, this.paging.pageSize,this.searchText,res.thirdLevel, this.branchId,this.pmsNumberEntryId).subscribe(response => {
          
          this.dataSource.data = response.items; 
          this.paging.length = response.totalItemsCount    
          this.isLoading = false;
          console.log(this.dataSource.data);
        })    
      });
    }
  }


  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getJobDescriptionEntryPmses();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getJobDescriptionEntryPmses();
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
      this.JobDescriptionEntryPmsService.getPmsNumberEntryByDepartment(dropdown.source.value).subscribe(res=>{
        this.getJobDescriptionEntryPmsList=res
        console.log( this.getJobDescriptionEntryPmsList);
      });
    }
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.JobDescriptionEntryPmsService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      //console.log(res)
      //console.log(res)
    }); 
  }

  

  getSelectedSpareRequiredStatus(){
    this.JobDescriptionEntryPmsService.getSelectedSpareRequiredStatus().subscribe(res=>{
      this.selectedSpareRequiredStatus=res
    }); 
  }

  getPmsNumberEntry(unitId,departmentNameId){
    console.log(unitId,departmentNameId);
    this.JobDescriptionEntryPmsService.getPmsNumberEntry(unitId,departmentNameId).subscribe(res=>{
      this.selectedPmsNumberEntry=res
    }); 
  }
  onPmsNumberEntrySelectionChangeGetPms(pmsNumberEntryId){
    console.log("pms");
    console.log(pmsNumberEntryId);
    this.pmsNumberEntryId = pmsNumberEntryId;

   this.JobDescriptionEntryPmsService.getPmsNoByPmsNumberEntryId(pmsNumberEntryId).subscribe(res=>{
     this.selectedPmsValue=res      
     this.getpmsid = this.selectedPmsValue[0].value,
     this.getpmsno = this.selectedPmsValue[0].text
     console.log(this.getpmsno);
   });
   this.PmsNumberEntryService.find(pmsNumberEntryId).subscribe(res=>{
    console.log(res);
    this.itemDetailId=res.itemDetailId;   //itemdetailId Pass   
    this.JobDescriptionEntryPmsForm.get('itemDetailId').setValue(this.itemDetailId);
    console.log(this.getpmsno);
  });
  this.getJobDescriptionEntryPmsByUnitAndDept()

  }
  getMaintenanceTypes(){
    this.JobDescriptionEntryPmsService.getMaintenanceTypes().subscribe(res=>{
      this.selectedMaintenanceTypes=res
    }); 
  }
  getMaintenanceCategory(maintenanceTypeId){
    this.JobDescriptionEntryPmsService.getMaintenanceCategory(maintenanceTypeId).subscribe(res=>{
      this.selectedMaintenanceCategory=res
    }); 
  }
  getByWhom(){
    this.JobDescriptionEntryPmsService.getByWhom().subscribe(res=>{
      this.selectedByWhom=res
    }); 
  }
  
  onSubmit() {
    const id = this.JobDescriptionEntryPmsForm.get('jobDescriptionEntryPmsId').value;  
    console.log(this.JobDescriptionEntryPmsForm.value); 
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.JobDescriptionEntryPmsService.update(+id,this.JobDescriptionEntryPmsForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-jobdescriptionentrypms');
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
      this.JobDescriptionEntryPmsService.submit(this.JobDescriptionEntryPmsForm.value).subscribe(response => {
        //this.router.navigateByUrl('/basic-setup/jobdescriptionentrypms-list');
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
    const id = row.jobDescriptionEntryPmsId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.JobDescriptionEntryPmsService.delete(id).subscribe(() => {

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
