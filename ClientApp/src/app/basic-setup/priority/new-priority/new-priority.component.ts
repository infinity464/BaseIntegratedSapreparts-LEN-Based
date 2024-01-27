import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PriorityService } from '../../service/Priority.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-priority',
  templateUrl: './new-priority.component.html',
  styleUrls: ['./new-priority.component.sass']
})
export class NewPriorityComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  PriorityForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private PriorityService: PriorityService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('priorityId'); 
    if (id) {
      this.pageTitle = 'Edit Priority';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.PriorityService.find(+id).subscribe(
        res => {
          this.PriorityForm.patchValue({          

            priorityId: res.priorityId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Priority';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.PriorityForm = this.fb.group({
      priorityId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.PriorityForm.get('priorityId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.PriorityService.update(+id,this.PriorityForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/priority-list');
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
      this.PriorityService.submit(this.PriorityForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/priority-list');
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
