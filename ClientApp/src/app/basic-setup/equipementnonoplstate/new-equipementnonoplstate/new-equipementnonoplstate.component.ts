import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IssueRegisterService } from '../../service/IssueRegister.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-equipementnonoplstate',
  templateUrl: './new-equipementnonoplstate.component.html',
  styleUrls: ['./new-equipementnonoplstate.component.sass']
})
export class NewEquipementnonoplStateComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  IssueRegisterForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  statusValue:number;

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private IssueRegisterService: IssueRegisterService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
   
    const id = this.route.snapshot.paramMap.get('issueRegisterId'); 
    console.log(id)
    if (id) {
      this.intitializeForm();
      this.pageTitle = 'Edit Equipement NON-OPL State';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.IssueRegisterService.find(+id).subscribe(
        res => {
          this.statusValue = res.status; 
          this.IssueRegisterForm.patchValue({          
           
            issueRegisterId: res.issueRegisterId,
            status: res.status,
            reason:res.reason
          
          });  
                
        }
      );
    } 
  }
  intitializeForm() {
    this.IssueRegisterForm = this.fb.group({
      issueRegisterId: [0],
      oplStatus:[1],
      status: [''],
      reason:['']
    })
  }
  
  onSubmit() {
    console.log( this.IssueRegisterForm.value)
    const id = this.IssueRegisterForm.get('issueRegisterId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.IssueRegisterService.update(+id,this.IssueRegisterForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/equipementnonoplstate-list');
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
      this.IssueRegisterService.submit(this.IssueRegisterForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/equipementnonoplstate-list');
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
