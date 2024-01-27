import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementTypeService } from '../../service/ProcurementType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-procurementtype',
  templateUrl: './new-procurementtype.component.html',
  styleUrls: ['./new-procurementtype.component.sass']
})
export class NewProcurementTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProcurementTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProcurementTypeService: ProcurementTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('procurementTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Procurement Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementTypeService.find(+id).subscribe(
        res => {
          this.ProcurementTypeForm.patchValue({          

            procurementTypeId: res.procurementTypeId,
            typeName: res.typeName,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Procurement Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ProcurementTypeForm = this.fb.group({
      procurementTypeId: [0],
      typeName: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ProcurementTypeForm.get('procurementTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementTypeService.update(+id,this.ProcurementTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/procurementtype-list');
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
      this.ProcurementTypeService.submit(this.ProcurementTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/procurementtype-list');
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
