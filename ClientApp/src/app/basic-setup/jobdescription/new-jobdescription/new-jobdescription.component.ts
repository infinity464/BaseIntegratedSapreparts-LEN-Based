import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JobDescriptionService } from '../../service/JobDescription.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-jobdescription',
  templateUrl: './new-jobdescription.component.html',
  styleUrls: ['./new-jobdescription.component.sass']
})
export class NewJobDescriptionComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  JobDescriptionForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private JobDescriptionService: JobDescriptionService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('jobDescriptionId'); 
    if (id) {
      this.pageTitle = 'Edit Job Description';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.JobDescriptionService.find(+id).subscribe(
        res => {
          this.JobDescriptionForm.patchValue({          

            jobDescriptionId: res.jobDescriptionId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Job Description';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.JobDescriptionForm = this.fb.group({
      jobDescriptionId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.JobDescriptionForm.get('jobDescriptionId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.JobDescriptionService.update(+id,this.JobDescriptionForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/jobdescription-list');
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
      this.JobDescriptionService.submit(this.JobDescriptionForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/jobdescription-list');
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
