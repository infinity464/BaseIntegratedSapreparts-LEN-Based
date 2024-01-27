import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemInspectionService } from '../../service/ItemInspection.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-iteminspection',
  templateUrl: './new-iteminspection.component.html',
  styleUrls: ['./new-iteminspection.component.sass']
})
export class NewItemInspectionComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ItemInspectionForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ItemInspectionService: ItemInspectionService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('itemInspectionId'); 
    if (id) {
      this.pageTitle = 'Edit Inspection';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ItemInspectionService.find(+id).subscribe(
        res => {
          this.ItemInspectionForm.patchValue({          

            itemInspectionId: res.itemInspectionId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Inspection';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ItemInspectionForm = this.fb.group({
      itemInspectionId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ItemInspectionForm.get('itemInspectionId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ItemInspectionService.update(+id,this.ItemInspectionForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/iteminspection-list');
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
      this.ItemInspectionService.submit(this.ItemInspectionForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/iteminspection-list');
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
