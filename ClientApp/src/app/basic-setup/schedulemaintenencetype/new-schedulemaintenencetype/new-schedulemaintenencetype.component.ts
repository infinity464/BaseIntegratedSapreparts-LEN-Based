import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ScheduleMaintenenceTypeService } from '../../service/ScheduleMaintenenceType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-schedulemaintenencetype',
  templateUrl: './new-schedulemaintenencetype.component.html',
  styleUrls: ['./new-schedulemaintenencetype.component.sass']
})
export class NewScheduleMaintenenceTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ScheduleMaintenenceTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ScheduleMaintenenceTypeService: ScheduleMaintenenceTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('scheduleMaintenenceTypeId'); 
    if (id) {
      this.pageTitle = 'Edit ScheduleMaintenenceType';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ScheduleMaintenenceTypeService.find(+id).subscribe(
        res => {
          this.ScheduleMaintenenceTypeForm.patchValue({          
            scheduleMaintenenceTypeId: res.scheduleMaintenenceTypeId,
            name: res.name,
            isActive: res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create ScheduleMaintenenceType';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ScheduleMaintenenceTypeForm = this.fb.group({
      scheduleMaintenenceTypeId: [0],
      name: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ScheduleMaintenenceTypeForm.get('scheduleMaintenenceTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ScheduleMaintenenceTypeService.update(+id,this.ScheduleMaintenenceTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/schedulemaintenencetype-list');
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
      this.ScheduleMaintenenceTypeService.submit(this.ScheduleMaintenenceTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/schedulemaintenencetype-list');
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
