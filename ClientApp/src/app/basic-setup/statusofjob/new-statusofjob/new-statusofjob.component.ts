import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusOfJobService } from '../../service/StatusOfJob.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-statusofjob',
  templateUrl: './new-statusofjob.component.html',
  styleUrls: ['./new-statusofjob.component.sass']
})
export class NewStatusOfJobComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  StatusOfJobForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private StatusOfJobService: StatusOfJobService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('statusOfJobId'); 
    if (id) {
      this.pageTitle = 'Edit Status of Job';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.StatusOfJobService.find(+id).subscribe(
        res => {
          this.StatusOfJobForm.patchValue({          

            statusOfJobId: res.statusOfJobId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Status of Job';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.StatusOfJobForm = this.fb.group({
      statusOfJobId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.StatusOfJobForm.get('statusOfJobId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.StatusOfJobService.update(+id,this.StatusOfJobForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/statusofjob-list');
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
      this.StatusOfJobService.submit(this.StatusOfJobForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/statusofjob-list');
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
