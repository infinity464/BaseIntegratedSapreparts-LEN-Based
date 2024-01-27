import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReturnStatusService } from '../../service/ReturnStatus.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-returnstatus',
  templateUrl: './new-returnstatus.component.html',
  styleUrls: ['./new-returnstatus.component.sass']
})
export class NewReturnStatusComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ReturnStatusForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ReturnStatusService: ReturnStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('returnStatusId'); 
    if (id) {
      this.pageTitle = 'Edit Return Status';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ReturnStatusService.find(+id).subscribe(
        res => {
          this.ReturnStatusForm.patchValue({          

            returnStatusId: res.returnStatusId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Return Status';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ReturnStatusForm = this.fb.group({
      returnStatusId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ReturnStatusForm.get('returnStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ReturnStatusService.update(+id,this.ReturnStatusForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/returnstatus-list');
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
      this.ReturnStatusService.submit(this.ReturnStatusForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/returnstatus-list');
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
