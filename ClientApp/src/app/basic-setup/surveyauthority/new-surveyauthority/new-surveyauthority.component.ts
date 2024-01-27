import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyAuthorityService } from '../../service/SurveyAuthority.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-surveyauthority',
  templateUrl: './new-surveyauthority.component.html',
  styleUrls: ['./new-surveyauthority.component.sass']
})
export class NewSurveyAuthorityComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  SurveyAuthorityForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private SurveyAuthorityService: SurveyAuthorityService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('surveyAuthorityId'); 
    if (id) {
      this.pageTitle = 'Edit Survey Authority';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SurveyAuthorityService.find(+id).subscribe(
        res => {
          this.SurveyAuthorityForm.patchValue({          

            surveyAuthorityId: res.surveyAuthorityId,
            surveyAuthorityName: res.surveyAuthorityName,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Survey Authority';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.SurveyAuthorityForm = this.fb.group({
      surveyAuthorityId: [0],
      surveyAuthorityName: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.SurveyAuthorityForm.get('surveyAuthorityId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.SurveyAuthorityService.update(+id,this.SurveyAuthorityForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/surveyauthority-list');
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
      this.SurveyAuthorityService.submit(this.SurveyAuthorityForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/surveyauthority-list');
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
