import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { JobCategoryMaintenenceService } from '../../service/JobCategoryMaintenence.service';
// import { JobCategoryMaintenenceService } from '../../service/JobCategoryMaintenence.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-jobcategorymaintenence',
  templateUrl: './new-jobcategorymaintenence.component.html',
  styleUrls: ['./new-jobcategorymaintenence.component.sass']
})
export class NewJobCategoryMaintenenceComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  JobCategoryMaintenenceForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private JobCategoryMaintenenceService: JobCategoryMaintenenceService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('jobCategoryMaintenenceId'); 
    if (id) {
      this.pageTitle = 'Edit JobCategory Maintenence';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.JobCategoryMaintenenceService.find(+id).subscribe(
        res => {
          this.JobCategoryMaintenenceForm.patchValue({          

            jobCategoryMaintenenceId: res.jobCategoryMaintenenceId,
            categoryName: res.categoryName,
            isActive: res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create JobCategory Maintenence';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.JobCategoryMaintenenceForm = this.fb.group({
      // JobCategoryMaintenenceId: [0],
      // name: ['', Validators.required],
      // remarks: ['', Validators.required],
      // isActive: [true],
      // status:[true]

      jobCategoryMaintenenceId: [0],
      categoryName: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.JobCategoryMaintenenceForm.get('jobCategoryMaintenenceId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.JobCategoryMaintenenceService.update(+id,this.JobCategoryMaintenenceForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/jobcategorymaintenence-list');
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
      this.JobCategoryMaintenenceService.submit(this.JobCategoryMaintenenceForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/jobcategorymaintenence-list');
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
