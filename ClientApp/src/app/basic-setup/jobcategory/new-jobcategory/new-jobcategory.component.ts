import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JobCategoryService } from '../../service/JobCategory.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-jobcategory',
  templateUrl: './new-jobcategory.component.html',
  styleUrls: ['./new-jobcategory.component.sass']
})
export class NewJobCategoryComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  JobCategoryForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private JobCategoryService: JobCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('jobCategoryId'); 
    if (id) {
      this.pageTitle = 'Edit Job Category';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.JobCategoryService.find(+id).subscribe(
        res => {
          this.JobCategoryForm.patchValue({          

            jobCategoryId: res.jobCategoryId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Job Category';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.JobCategoryForm = this.fb.group({
      jobCategoryId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.JobCategoryForm.get('jobCategoryId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.JobCategoryService.update(+id,this.JobCategoryForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/jobcategory-list');
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
      this.JobCategoryService.submit(this.JobCategoryForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/jobcategory-list');
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
