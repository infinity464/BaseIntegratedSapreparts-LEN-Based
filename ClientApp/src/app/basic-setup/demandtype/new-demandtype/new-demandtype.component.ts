import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {DemandTypeService } from '../../service/DemandType.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-demandtype',
  templateUrl: './new-demandtype.component.html',
  styleUrls: ['./new-demandtype.component.sass']
})
export class NewDemandTypeComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DemandTypeForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private DemandTypeService: DemandTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('demandTypeId'); 
    if (id) {
      this.pageTitle = 'Edit Demand Type';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DemandTypeService.find(+id).subscribe(
        res => {
          this.DemandTypeForm.patchValue({          

            demandTypeId: res.demandTypeId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Demand Type';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.DemandTypeForm = this.fb.group({
      demandTypeId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.DemandTypeForm.get('demandTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.DemandTypeService.update(+id,this.DemandTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/demandtype-list');
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
      this.DemandTypeService.submit(this.DemandTypeForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/demandtype-list');
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
