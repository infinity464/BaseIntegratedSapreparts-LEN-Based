import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,FormArray, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PreventiveMaintenanceService } from '../../service/PreventiveMaintenance.service';
import { DepartmentNameService } from 'src/app/basic-setup/service/DepartmentName.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectionModel } from '@angular/cdk/collections';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PreventiveMaintenance } from '../../models/PreventiveMaintenance';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { ScheduleMaintenenceService } from '../../service/ScheduleMaintenence.service';
import { IssueRegisterService } from 'src/app/issue-management/service/IssueRegister.service';
import { PmsNumberEntryService } from 'src/app/basic-setup/service/PmsNumberEntry.service';

@Component({
  selector: 'app-new-preventivemaintenance',
  templateUrl: './new-preventivemaintenance.component.html',
  styleUrls: ['./new-preventivemaintenance.component.sass']
})
export class NewPreventiveMaintenanceComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  itemDetailId:any;
  PreventiveMaintenanceForm: FormGroup;
  validationErrors: string[] = [];
  departmentName: SelectedModel[];
  maintenanceType:SelectedModel[];
  selectedDepartmentNames:SelectedModel[];
  maintenanceCategoryValue:SelectedModel[];
  subCategoryValue:SelectedModel[];
  selectedEquipment:SelectedModel[];
  maintenenceTypes:SelectedModel[];
  selectedMaintenanceCategory:SelectedModel[];
  jobDescriptionEntryPmsId:any;
  maintenanceCategoryId:any;
  maintenenceTypeId:any;
  selectedjic:SelectedModel[];
  preventiveMaintenanceList:PreventiveMaintenance[];
  preventiveList:any[];
  pmsJobDescriptionList:any[];
  pmsNo:any;
  isShown: boolean = false ;
  isShownPreventiveList: boolean = false ;
  masterData = MasterData;
  options = [];
  filteredOptions;
  departmentId:any;
  unitId:any;
  userRole = Role;
  issueRegisterIdForUpdate:any;
  
  traineeId:any;
  role:any;
  branchId:any;
  
  displayedColumns: string[] = ['ser','pattNo','pmsno', 'category',  'subCategory',  'completedDate', 'actions'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  constructor(private snackBar: MatSnackBar,private PmsNumberEntryService:PmsNumberEntryService,private IssueRegisterService:IssueRegisterService,private ScheduleMaintenenceService:ScheduleMaintenenceService,private ItemDetailService:ItemDetailService,private authService: AuthService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private PreventiveMaintenanceService: PreventiveMaintenanceService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('preventiveMaintenanceId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
 
    if (id) {
       //this.isShown=false;
       console.log(this.isShown);
      this.pageTitle = 'Edit Preventive Maintenance';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.PreventiveMaintenanceService.find(+id).subscribe(
        res => {
          this.PreventiveMaintenanceForm.patchValue({          

            preventiveMaintenanceId: res.preventiveMaintenanceId,
            unitId: res.unitId,
            issueRegisterId: res.issueRegisterId,
            departmentNameId: res.departmentNameId,
            itemDetailId: res.itemDetailId,
            pmsno:res.pmsno,
            maintenanceSubCategoryId:res.maintenanceSubCategoryId,
            jobDescriptionEntryPmsId:res.jobDescriptionEntryPmsId,
            //maintenanceCategoryId:res.maintenanceCategoryId,
            maintenanceTypeId:res.maintenanceTypeId,
            maintenanceCategoryId:res.maintenanceCategoryId,
            completedDate:res.completedDate,
            completedStatus:res.completedStatus,
            remarks: res.remarks,
            part: res.pattNo,
            isActive: res.isActive,
           // part:res.pattNo,
          
          });          
          this.itemDetailId = res.itemDetailId;
          this.onDepartmentSelectionChange();
          this.onCategorySelectionChange();
          this.onSubCategorySelectionChange();
          this.onJobDescriptionEntryPmsId();
          this.getPmsNoByItemDetailId();
          console.log("res.partNo")
          console.log(res)
          console.log("res.partNo")
        }
      );
    } else {
      this.pageTitle = 'Create Preventive Maintenance';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.PreventiveMaintenanceForm.get('departmentNameId').setValue(this.branchId);
     
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.PreventiveMaintenanceForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    this.getselectedMaintenanceCategory();
    this.getselectedSubCategory();
    this.getMaintenanceTypes();
  }
  intitializeForm() {
    this.PreventiveMaintenanceForm = this.fb.group({
      preventiveMaintenanceId: [0],
      unitId: [''],
      departmentNameId: [''],
      issueRegisterId: [],
      itemDetailId:[],
      part:[''],
      pmsno: [''],
      maintenanceSubCategoryId: [''],
      maintenanceTypeId:[''],
      maintenanceCategoryId: [''],
      jobDescriptionEntryPmsId:[''],
      completedDate: [''],
      completedStatus: [''],
      remarks: [''],
      isActive: [true],
      PreventiveList: this.fb.array([
        this.createIssueRegisterData()
      ]),
    
    })
     //autocomplete For pattNo
     this.PreventiveMaintenanceForm.get('part').valueChanges
     .subscribe(value => {
       this.getSelectedTraineeByPno(value);
     })
  }

  getControlLabel(index: number, type: string) {
    return (this.PreventiveMaintenanceForm.get('PreventiveList') as FormArray).at(index).get(type).value;
    // (this.PreventiveMaintenanceForm.get("PreventiveList") as FormArray)
    // .at(index)
    // .get("issueRegisterId")
    // .setValue(this.issueRegisterIdForUpdate);
  }

 
  private createIssueRegisterData() {

    return this.fb.group({
      preventiveMaintenanceId:[0],
      completedDate:  [''],
      completedStatus:[''],
      jic:[''],
      jobDescription:[''],
      byWhom:[''],
      issueRegisterId:[''],
      unitId:[''],
      departmentNameId:[''],
      itemDetailId:[''],
      maintenanceCategoryId:[''],
      maintenanceTypeId:[''],
      jobDescriptionEntryPmsId:[''],
    });
  }
//autocomplete For pattNo
  onTraineeSelectionChanged(item) {
    this.itemDetailId = item.value
    this.PreventiveMaintenanceForm.get('itemDetailId').setValue(item.value);
    this.PreventiveMaintenanceForm.get('part').setValue(item.text);
  }
  //autocomplete For pattNo
  getSelectedTraineeByPno(pno) {
    this.PreventiveMaintenanceService.getPartNo(pno).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }

  onCompletedButtonClick(event, data){
    console.log("event");
       console.log(event)

       console.log("data");
       console.log(data)
  }

  onNotCompletedButtonClick(event, data){
    data.value.issueRegisterId=this.issueRegisterIdForUpdate;
    console.log(data.value)

        this.confirmService.confirm('Confirm Return message', 'Are You Sure Completed This  Item?').subscribe(result => {
          if (result) {
            this.PreventiveMaintenanceService.submit(data.value).subscribe(response => {
              this.router.navigateByUrl('/maintenence-planning/preventivemaintenance-list');
             // this.reloadCurrentRoute();
              this.snackBar.open('Information Updated Successfully ', '', {
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

  onDepartmentSelectionChange(){
    this.isShown=true;
    this.departmentId =  this.PreventiveMaintenanceForm.value['departmentNameId'];; 
  
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;
     this.PreventiveMaintenanceForm.get('unitId').setValue(this.unitId);

     this.ScheduleMaintenenceService.getSelectedEquipmentNameFromIssueRegisterByParameter(this.unitId,this.departmentId,1).subscribe(res => {
      this.selectedEquipment = res;
    });
  
   });
  }
  onJobDescriptionEntryPmsId(){
  //  if(dropdown.isUserInput){
      this.jobDescriptionEntryPmsId=this.PreventiveMaintenanceForm.value['jobDescriptionEntryPmsId'];
    //}
  }

  getPmsNoByItemDetailId(){
    this.PreventiveMaintenanceService.getPmsNoByItemDetailId(this.itemDetailId).subscribe(res=>{
      this.pmsNo=res[0].text;
     console.log("response");
     console.log(this.pmsNo)
   });
  }
  // getLabel(i,type) {
  //   (this.PreventiveMaintenanceForm.get("PreventiveList") as FormArray)
  //   .at(i)
  //   .get("issueRegisterId")
  //   .setValue(this.issueRegisterIdForUpdate);

  //   var issueRegisterId=(this.PreventiveMaintenanceForm.get("PreventiveList") as FormArray)
  //     .at(i)
  //     .get(type).value;

  //     console.log("Issue"+issueRegisterId)
  //     this.issueRegisterIdForUpdate=issueRegisterId;
  // }

  
  onEquipmentSelectionChange(dropdown){
    if(dropdown.isUserInput){
      this.pmsNo="";
     this.isShown=true;
     this.issueRegisterIdForUpdate=dropdown.source.value;
      var issueRegisterId=dropdown.source.value;
      console.log("Issue");
      console.log(issueRegisterId);
     

      this.IssueRegisterService.find(issueRegisterId).subscribe(
       res => {
        //  console.log("res");
        //  console.log(res)

       

        this.PreventiveMaintenanceForm.get('issueRegisterId').setValue(issueRegisterId);
         this.itemDetailId = res.itemDetailId;
         this.PreventiveMaintenanceForm.get('itemDetailId').setValue(this.itemDetailId);
         console.log("eeeee");
         console.log(this.itemDetailId)

         this.PreventiveMaintenanceService.getPmsNoByItemDetailId(this.itemDetailId).subscribe(res=>{
          this.pmsNo=res[0].text;
         console.log("response");
         console.log(this.pmsNo)
         this.PreventiveMaintenanceForm.get('pmsno').setValue(this.pmsNo);
       });
         
        //  this.PmsNumberEntryService.findPmsNo(this.itemDetailId).subscribe(
        //   res => {
        //     console.log("res");
        //     console.log(res)
        //   })
       })
      // var itemDetailId = this.PreventiveMaintenanceForm.value['itemDetailId']
      // console.log("item detail id");
      // console.log(this.itemDetailId);
      //  this.PmsNumberEntryService.find(this.itemDetailId).subscribe(
      //   res => {
      //     console.log("res");
      //     console.log(res)
      //   })
    }
   }

   onCategorySelectionChange(){
  //  if(dropdown.isUserInput) {
   // this.departmentId =  this.PreventiveMaintenanceForm.value['departmentNameId'];
      this.maintenenceTypeId=this.PreventiveMaintenanceForm.value['maintenanceTypeId'];
      this.PreventiveMaintenanceService.getMaintenanceCategory(this.maintenenceTypeId).subscribe(res=>{
        this.selectedMaintenanceCategory=res
      }); 
 //   }
  }

  // onCategorySelectionChange(dropdown){
  //   if(dropdown.isUserInput) {
  //     this.maintenenceTypeId=dropdown.source.value;
  //     this.PreventiveMaintenanceService.getMaintenanceCategory(dropdown.source.value).subscribe(res=>{
  //       this.selectedMaintenanceCategory=res
  //     }); 
  //   }
  // }

  // onSubCategorySelectionChange(dropdown){
  //   if(dropdown.isUserInput) {
  //     this.isShownPreventiveList=true;
  //     this.maintenanceCategoryId = dropdown.source.value;
  //    this.PreventiveMaintenanceForm.get('jobDescriptionEntryPmsId').setValue(this.jobDescriptionEntryPmsId);

  //     var issueRegisterId=dropdown.source.value;
  //     console.log("DepartmentName id");
  //     console.log(this.departmentId);
  //     console.log("unit id");
  //     console.log(this.unitId);
  //     console.log("IssueRegister");
  //     console.log(issueRegisterId);
  //     console.log("item detail Id");
  //     console.log(this.itemDetailId);
  //     console.log("maintenenceTypeId");

  //     this.PreventiveMaintenanceService.getJobDescriptionEntryPmsIdByParameterRequest(this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId).subscribe(res=>{
  //       this.selectedjic=res
  //     }); 

  //     this.PreventiveMaintenanceService.getPreventiveMaintenanceyByParameterRequest(this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId).subscribe(res=>{
  //       this.preventiveList=res
  //       console.log("preventive list");
  //       console.log(this.preventiveList);
  //       this.clearList();
  //       this.getPreventiveListonClick();
  //     }); 
  //   }
  // }
  onSubCategorySelectionChange(){
   // if(dropdown.isUserInput) {
      this.isShownPreventiveList=true;
      this.maintenanceCategoryId = this.PreventiveMaintenanceForm.value['maintenanceCategoryId'];
   //  this.PreventiveMaintenanceForm.get('jobDescriptionEntryPmsId').setValue(this.jobDescriptionEntryPmsId);
     this.unitId=this.PreventiveMaintenanceForm.value['unitId'];

      var issueRegisterId=this.PreventiveMaintenanceForm.value['maintenanceCategoryId'];
      

      this.PreventiveMaintenanceService.getJobDescriptionEntryPmsIdByParameterRequest(this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId).subscribe(res=>{
        this.selectedjic=res
      }); 

      console.log("Unit Id");
      console.log(this.unitId);
      console.log("Department Id");
      console.log(this.departmentId);
      console.log("maintenence Type Id");
      console.log(this.maintenenceTypeId);
      console.log("maintenence Category Id");
      console.log(this.maintenanceCategoryId);
      console.log("Item Detail Id");
      console.log(this.itemDetailId);
      var issueRegisterId = this.PreventiveMaintenanceForm.value['issueRegisterId'];
      this.PreventiveMaintenanceForm.get("issueRegisterId").setValue(issueRegisterId);
      console.log("Unit Id"+issueRegisterId);


    

      this.PreventiveMaintenanceService.getPmsJobDescriptionListForPreventive(this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId).subscribe(res=>{
        this.pmsJobDescriptionList=res
        console.log("pms list");
        console.log(this.pmsJobDescriptionList);
        this.clearList();
        this.getPreventiveListonClick();
      }); 
     // this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId
      // this.PreventiveMaintenanceService.getPreventiveMaintenanceyByParameterRequest(this.unitId,this.departmentId,this.maintenenceTypeId,this.maintenanceCategoryId,this.itemDetailId).subscribe(res=>{
      //   this.preventiveList=res
      //   console.log("preventive list");
      //   console.log(this.preventiveList);
      //   this.clearList();
      //   this.getPreventiveListonClick();
      // }); 
  //  }
  }
  onJICSelectionChange(){
    var unitId = this.PreventiveMaintenanceForm.value['unitId'];
    var departmentNameId = this.PreventiveMaintenanceForm.value['departmentNameId'];
    var itemDetailId = this.PreventiveMaintenanceForm.value['itemDetailId'];
    console.log(unitId,departmentNameId,itemDetailId)
    this.PreventiveMaintenanceService.getJICByParameterRequest(unitId,departmentNameId,itemDetailId).subscribe(res=>{
      this.selectedjic=res
      console.log("JCO")
      console.log(res)
    });
  }
  clearList() {
    const control = <FormArray>this.PreventiveMaintenanceForm.controls["PreventiveList"];
    while (control.length) {
      control.removeAt(control.length - 1);
    }
    control.clearValidators();
  }

  getPreventiveListonClick() {
    const control = <FormArray>this.PreventiveMaintenanceForm.controls["PreventiveList"];
    for (let i = 0; i < this.pmsJobDescriptionList.length; i++) {
      control.push(this.createIssueRegisterData());
    }
    console.log('12');
    this.PreventiveMaintenanceForm.patchValue({ PreventiveList: this.pmsJobDescriptionList });
  }

  GetDepartmentNameById(baseNameId){    
    this.DepartmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
    }); 
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.PreventiveMaintenanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
    }); 
  }
  getMaintenanceTypes(){
    this.PreventiveMaintenanceService.getMaintenanceTypes().subscribe(res=>{
      this.maintenenceTypes=res
    });
  }
  getselectedMaintenanceCategory(){
    this.PreventiveMaintenanceService.getselectedMaintenanceCategory().subscribe(res=>{
      this.maintenanceCategoryValue=res
    });
  }
  getselectedSubCategory(){
    this.PreventiveMaintenanceService.getselectedSubCategory().subscribe(res=>{
      this.subCategoryValue=res
    });
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onSelectionChangegetPreventiveList(dropdown){
    if(dropdown.isUserInput){
      this.isShown=true;
      //var itemCategoryId=dropdown.source.value;
      console.log("base name");
        this.PreventiveMaintenanceService.getPreventiveList(this.departmentId,this.unitId).subscribe(res=>{
          this.preventiveList=res;
          console.log(this.preventiveList)
        }); 
    }
    }
  deleteItem(preventiveMaintenanceId) {
    const id = preventiveMaintenanceId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.PreventiveMaintenanceService.delete(id).subscribe(() => {

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
  
  onSubmit() {
    const id = this.PreventiveMaintenanceForm.get('preventiveMaintenanceId').value;   
    console.log(this.PreventiveMaintenanceForm.value)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.PreventiveMaintenanceService.update(+id,this.PreventiveMaintenanceForm.value).subscribe(response => {
          this.router.navigateByUrl('/maintenence-planning/add-preventivemaintenance');
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
      this.PreventiveMaintenanceService.submit(this.PreventiveMaintenanceForm.value).subscribe(response => {
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
