import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProgresService } from '../../service/Progres.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-progres',
  templateUrl: './new-progres.component.html',
  styleUrls: ['./new-progres.component.sass']
})
export class NewProgresComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  ProgresForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private ProgresService: ProgresService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('progresId'); 
    if (id) {
      this.pageTitle = 'Edit Progres';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProgresService.find(+id).subscribe(
        res => {
          this.ProgresForm.patchValue({          

            progresId: res.progresId,
            name: res.name,
            remarks: res. remarks,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Progres';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.ProgresForm = this.fb.group({
      progresId: [0],
      name: [''],
      remarks: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.ProgresForm.get('progresId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.ProgresService.update(+id,this.ProgresForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/progres-list');
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
      this.ProgresService.submit(this.ProgresForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/progres-list');
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
