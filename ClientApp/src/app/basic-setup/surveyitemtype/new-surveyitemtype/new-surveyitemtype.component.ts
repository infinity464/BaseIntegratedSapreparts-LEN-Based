import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyItemTypeService } from '../../service/SurveyItemType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-surveyitemtype',
  templateUrl: './new-surveyitemtype.component.html',
  styleUrls: ['./new-surveyitemtype.component.sass']
})
export class NewSurveyItemTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  SurveyItemTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private SurveyItemTypeService: SurveyItemTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('surveyItemTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Survey Item Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SurveyItemTypeService.find(+id).subscribe(
        res => {
          this.SurveyItemTypeForm.patchValue({          

            surveyItemTypeId: res.surveyItemTypeId,
            surveyItemTypeName: res.surveyItemTypeName,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Survey Item Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.SurveyItemTypeForm = this.fb.group({
      surveyItemTypeId: [0],
      surveyItemTypeName: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.SurveyItemTypeForm.get('surveyItemTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.SurveyItemTypeService.update(+id,this.SurveyItemTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/surveyitemtype-list');
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
      this.SurveyItemTypeService.submit(this.SurveyItemTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/surveyitemtype-list');
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
