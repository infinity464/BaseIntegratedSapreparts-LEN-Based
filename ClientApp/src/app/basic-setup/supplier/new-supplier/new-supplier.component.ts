import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierService } from '../../service/Supplier.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { JobDescriptionEntryPms } from '../../models/JobDescriptionEntryPms';
import { JobDescriptionEntryPmsService } from '../../service/JobDescriptionEntryPms.service';

@Component({
  selector: 'app-new-supplier',
  templateUrl: './new-supplier.component.html',
  styleUrls: ['./new-supplier.component.sass']
})
export class NewSupplierComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  SupplierForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedEnlistedType:SelectedModel[];

  constructor(private snackBar: MatSnackBar,private JobDescriptionEntryPmsService:JobDescriptionEntryPmsService,private confirmService: ConfirmService,private SupplierService: SupplierService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('supplierId'); 
    if (id) {
      this.pageTitle = 'Edit Supplier';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.SupplierService.find(+id).subscribe(
        res => {
          this.SupplierForm.patchValue({          
              supplierId: res.supplierId,
              companyName: res.companyName,
              presentAddress: res.presentAddress,
              permanentAddress: res.permanentAddress,
              phoneNumber: res.phoneNumber,
              telephoneNumber: res.telephoneNumber,
              emailAddress: res.emailAddress,
              fax: res.fax,
              enlistedType: res.enlistedType,
              contractPersonName: res.contractPersonName,
              contractPersonNumber: res.contractPersonNumber,
              remarks: res.remarks,
              status: res.status,
              isActive: res.isActive
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Supplier';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getSelectedEnlistedType();
  }
  intitializeForm() {
    this.SupplierForm = this.fb.group({
      supplierId: [0],
              companyName: ['', Validators.required],
              presentAddress: ['', Validators.required],
              permanentAddress: ['', Validators.required],
              phoneNumber: ['', Validators.required],
              telephoneNumber: [''],
              emailAddress: ['', Validators.required],
              fax: [''],
              enlistedType: [],
              contractPersonName: ['', Validators.required],
              contractPersonNumber:['', Validators.required],
              remarks:[''],
              status:[''],
              isActive: [true]
    })
  }


  getSelectedEnlistedType(){
    this.JobDescriptionEntryPmsService.getSelectedSpareRequiredStatus().subscribe(res=>{
      this.selectedEnlistedType=res
    }); 
  }

  onSubmit() {
    const id = this.SupplierForm.get('supplierId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.SupplierService.update(+id,this.SupplierForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/supplier-list');
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
      this.SupplierService.submit(this.SupplierForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/supplier-list');
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
