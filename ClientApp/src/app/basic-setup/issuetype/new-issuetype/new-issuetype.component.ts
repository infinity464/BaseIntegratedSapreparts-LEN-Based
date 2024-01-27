import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IssueTypeService } from '../../service/IssueType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-issuetype',
  templateUrl: './new-issuetype.component.html',
  styleUrls: ['./new-issuetype.component.sass']
})
export class NewIssueTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  IssueTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private IssueTypeService: IssueTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('issueTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Issue Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.IssueTypeService.find(+id).subscribe(
        res => {
          this.IssueTypeForm.patchValue({          

            issueTypeId: res.issueTypeId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Issue Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.IssueTypeForm = this.fb.group({
      issueTypeId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.IssueTypeForm.get('issueTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.IssueTypeService.update(+id,this.IssueTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/issuetype-list');
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
      this.IssueTypeService.submit(this.IssueTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/issuetype-list');
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
