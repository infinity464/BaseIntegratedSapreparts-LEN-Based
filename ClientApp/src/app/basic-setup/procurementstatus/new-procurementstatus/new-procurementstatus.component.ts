import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementStatusService } from '../../service/ProcurementStatus.service';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-procurementstatus',
  templateUrl: './new-procurementstatus.component.html',
  styleUrls: ['./new-procurementstatus.component.sass']
}) 
export class NewProcurementStatusComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  AuthorityForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProcurementStatusService: ProcurementStatusService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('procurementStatusId'); 
    if (id) {
      this.pageTitle = 'Edit Procurement Status';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementStatusService.find(+id).subscribe(
        res => {
          this.AuthorityForm.patchValue({          

            procurementStatusId: res.procurementStatusId,
            name: res.name,
            remarks: res.remarks,
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Procurement Status';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.AuthorityForm = this.fb.group({
      procurementStatusId: [0],
      name: ['', Validators.required],
      remarks: [''],
      isActive: [true],
    
    })
  }
  
  onSubmit() {
    const id = this.AuthorityForm.get('procurementStatusId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.ProcurementStatusService.update(+id,this.AuthorityForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/procurementstatus-list');
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
      this.ProcurementStatusService.submit(this.AuthorityForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/procurementstatus-list');
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
