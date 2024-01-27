import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OpinionService } from '../../service/Opinion.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-opinion',
  templateUrl: './new-opinion.component.html',
  styleUrls: ['./new-opinion.component.sass']
})
export class NewOpinionComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  OpinionForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private OpinionService: OpinionService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('opinionId'); 
    if (id) {
      this.pageTitle = 'Edit Opinion';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.OpinionService.find(+id).subscribe(
        res => {
          this.OpinionForm.patchValue({          

            opinionId: res.opinionId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Opinion';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.OpinionForm = this.fb.group({
      opinionId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.OpinionForm.get('opinionId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.OpinionService.update(+id,this.OpinionForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/opinion-list');
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
      this.OpinionService.submit(this.OpinionForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/opinion-list');
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
