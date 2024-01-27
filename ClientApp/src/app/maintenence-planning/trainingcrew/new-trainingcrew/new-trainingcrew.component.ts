import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingCrewService } from '../../service/TrainingCrew.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MasterData } from 'src/assets/data/master-data';
import { TrainingCrew } from '../../models/TrainingCrew';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-trainingcrew',
  templateUrl: './new-trainingcrew.component.html',
  styleUrls: ['./new-trainingcrew.component.sass']
})
export class NewTrainingCrewComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  TrainingCrewForm: FormGroup;
  validationErrors: string[] = [];
  departmentName:SelectedModel[]; 
  selectRank:SelectedModel[]; 
  selectOfficersStatuses:SelectedModel[]; 
  isShown: boolean = false ;
  masterData = MasterData;
  trainingCrewList:TrainingCrew[];

  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  

  displayedColumns: string[] = ['ser', 'pno', 'rank','name','dateOfJoin', 'duties', 'aviationCategory','mobile', 'email','officersStatus', 'actions'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private TrainingCrewService: TrainingCrewService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('trainingCrewId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
 
    if (id) {
      this.pageTitle = 'Edit Training Crew';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.TrainingCrewService.find(+id).subscribe(
        res => {
          this.TrainingCrewForm.patchValue({          

            trainingCrewId: res.trainingCrewId,
            courseId: res.courseId,
            officersStatusId: res.officersStatusId,
            departmentNameId: res.departmentNameId,
            rankId:res.rankId,
            pno: res.pno,
            name: res.name,
            dateOfJoin: res.dateOfJoin,
            duties: res.duties,
            aviationCategory: res.aviationCategory,
            mobile: res.mobile,
            email: res.email,
            remarks: res.remarks,
            isActive: res.isActive,
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Training Crew';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.TrainingCrewForm.get('departmentNameId').setValue(this.branchId);
      this.onDepartmentNameSelectionChange();
    }
    this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedRank();
    this.getselectedOfficersStatuses();
  }
  intitializeForm() {
    this.TrainingCrewForm = this.fb.group({
      trainingCrewId: [0],
      courseId: [],
      departmentNameId: [],
      officersStatusId: [],
      rankId:[],
      pno: [''],
      name: [''],
      dateOfJoin: [''],
      duties: [''],
      aviationCategory: [''],
      mobile: [''],
      email: [''],
      remarks: [''],
      isActive: [true],
    
    })
  }
  onDepartmentNameSelectionChange(){
    this.isShown=true;
    var departmentId = this.TrainingCrewForm.get('departmentNameId').value;  
    if(departmentId) {
      //var pno =this.TrainingCrewForm.value['pno'];
      //console.log(dropdown.source.value, pno);
      this.TrainingCrewService.getTrainingCrewListByDepartmentNameId(departmentId).subscribe(res=>{
        this.trainingCrewList=res
        console.log( this.trainingCrewList);
      });
    }
  }
  // getselectedDepartmentNames(){
  //       this.TrainingCrewService.getselectedDepartmentNames().subscribe(res=>{
  //         this.departmentName=res
  //         console.log(this.departmentName);
  //       });
  //     }
  GetDepartmentNameById(baseNameId){    
    this.TrainingCrewService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log(res)
    }); 
  }
  getselectedRank(){
    this.TrainingCrewService.getselectedRank().subscribe(res=>{
      this.selectRank=res
      console.log(this.selectRank);
    });
  }
      getselectedOfficersStatuses(){
        this.TrainingCrewService.getselectedOfficersStatuses().subscribe(res=>{
          this.selectOfficersStatuses=res
          console.log(this.selectOfficersStatuses);
        });
      }
  
      reloadCurrentRoute() {
        let currentUrl = this.router.url;
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigate([currentUrl]);
        });
      }
  onSubmit() {
    const id = this.TrainingCrewForm.get('trainingCrewId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.TrainingCrewService.update(+id,this.TrainingCrewForm.value).subscribe(response => {
            this.router.navigateByUrl('/maintenence-planning/add-trainingcrew');
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
      this.TrainingCrewService.submit(this.TrainingCrewForm.value).subscribe(response => {
        //this.router.navigateByUrl('/maintenence-planning/trainingcrew-list');
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
    const id = row.trainingCrewId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.TrainingCrewService.delete(id).subscribe(() => {
          //this.getTrainingCrews();
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

