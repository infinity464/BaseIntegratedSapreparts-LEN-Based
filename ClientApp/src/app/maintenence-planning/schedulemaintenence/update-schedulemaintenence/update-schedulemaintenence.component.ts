import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DenoService } from '../../../../app/basic-setup/service/Deno.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { CorrectiveMaintenenceService } from '../../service/CorrectiveMaintenence.service';
import {ScheduleMaintenenceService} from '../../service/ScheduleMaintenence.service'

@Component({
  selector: 'app-update-schedulemaintenence',
  templateUrl: './update-schedulemaintenence.component.html',
  styleUrls: ['./update-schedulemaintenence.component.sass']
})
  
export class UpdateScheduleMaintenenceComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DenoForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedbyWhoms:SelectedModel[];
  selectedProgress:SelectedModel[];
  selectedFiscalYear:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private ScheduleMaintenenceService:ScheduleMaintenenceService,private CorrectiveMaintenenceService: CorrectiveMaintenenceService,private confirmService: ConfirmService,private DenoService: DenoService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('scheduleMaintenanceId'); 
    if (id) {
      this.pageTitle = 'Update Schedule Maintenence';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ScheduleMaintenenceService.find(+id).subscribe(
        res => {
          this.DenoForm.patchValue({          

            // denoId: res.denoId,
            // name: res.name,
            // remarks:res.remarks

            scheduleMaintenanceId:res.scheduleMaintenanceId,
            unitId: res.unitId,
            departmentNameId:res.departmentNameId,
            issueRegisterId:res.issueRegisterId,
            itemDetailId:res.itemDetailId,
            scheduleMaintenenceTypeId:res.scheduleMaintenenceTypeId,
            fiscalYearId:res.fiscalYearId,
            jobDescriptionScheduleMaintenenceId:res.jobDescriptionScheduleMaintenenceId,
            byWhomId:res.byWhomId,
            progresId:res.progresId,
            drNo:res. drNo,
            startDate:res.startDate,
            completedDate:res.completedDate,
            menuPosition:res.menuPosition,
            completedStatus:res.completedStatus,
            jobDescriptionScheduleMaintenence:res.jobDescriptionScheduleMaintenence
          }); 
           console.log("response");
           console.log(res);
        }
      );
    } else {
      this.pageTitle = 'Update Schedule Maintenence';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getSelectedByWhoms();
    this.getSelectedProgress();
    this.getSelectedFiscalYear();
  }
  intitializeForm() {
    this.DenoForm = this.fb.group({
      scheduleMaintenanceId:  [''],
      unitId:  [''],
      departmentNameId:  [''],
      issueRegisterId:  [''],
      itemDetailId:  [''],
      scheduleMaintenenceTypeId:  [''],
      fiscalYearId:  [''],
      jobDescriptionScheduleMaintenenceId:  [''],
      byWhomId:  [''],
      progresId:  [''],
      drNo: [''] ,
      startDate:  [''],
      completedDate:  [''],
      menuPosition:  [''],
      completedStatus:[''],
      jobDescriptionScheduleMaintenence:['']
    })
  }
  
  getSelectedByWhoms(){
    this.CorrectiveMaintenenceService.getSelectedByWhoms().subscribe(res => {
      this.selectedbyWhoms = res;
    });
  }
  getSelectedProgress(){
    this.ScheduleMaintenenceService.getSelectedProgress().subscribe(res => {
      this.selectedProgress = res;
    });
  }
  getSelectedFiscalYear(){
    this.ScheduleMaintenenceService.getSelectedFiscalYear().subscribe(res => {
      this.selectedFiscalYear = res;
    });
  }
  onSubmit() {
    const id = this.DenoForm.get('scheduleMaintenanceId').value;   
    console.log(this.DenoForm.value);
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ScheduleMaintenenceService.updateScheduleMaintenence(+id,this.DenoForm.value).subscribe(response => {
            this.router.navigateByUrl('/maintenence-planning/add-schedulemaintenence');
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
      this.DenoService.submit(this.DenoForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/deno-list');
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
