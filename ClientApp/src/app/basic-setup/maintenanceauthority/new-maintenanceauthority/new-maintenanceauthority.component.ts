import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MaintenanceAuthorityService } from '../../service/MaintenanceAuthority.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-maintenanceauthority',
  templateUrl: './new-maintenanceauthority.component.html',
  styleUrls: ['./new-maintenanceauthority.component.sass']
})
export class NewMaintenanceAuthorityComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  MaintenanceAuthorityForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private MaintenanceAuthorityService: MaintenanceAuthorityService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('maintenanceAuthorityId'); 
    if (id) {
      this.pageTitle = 'Edit Maintenance Authority';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.MaintenanceAuthorityService.find(+id).subscribe(
        res => {
          this.MaintenanceAuthorityForm.patchValue({          

            maintenanceAuthorityId: res.maintenanceAuthorityId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Maintenance Authority';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.MaintenanceAuthorityForm = this.fb.group({
      maintenanceAuthorityId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.MaintenanceAuthorityForm.get('maintenanceAuthorityId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.MaintenanceAuthorityService.update(+id,this.MaintenanceAuthorityForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/maintenanceauthority-list');
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
      this.MaintenanceAuthorityService.submit(this.MaintenanceAuthorityForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/maintenanceauthority-list');
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
