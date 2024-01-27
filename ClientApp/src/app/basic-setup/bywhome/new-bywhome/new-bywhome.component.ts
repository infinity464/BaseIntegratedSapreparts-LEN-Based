import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ByWhomService } from '../../service/ByWhome.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-bywhome',
  templateUrl: './new-bywhome.component.html',
  styleUrls: ['./new-bywhome.component.sass']
})
export class NewByWhomComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ByWhomForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ByWhomService: ByWhomService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('byWhomId'); 
    if (id) {
      this.pageTitle = 'Edit By Whom';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ByWhomService.find(+id).subscribe(
        res => {
          this.ByWhomForm.patchValue({          

            byWhomId: res.byWhomId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create By Whom';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ByWhomForm = this.fb.group({
      byWhomId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ByWhomForm.get('byWhomId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ByWhomService.update(+id,this.ByWhomForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/bywhome-list');
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
      this.ByWhomService.submit(this.ByWhomForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/bywhome-list');
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
