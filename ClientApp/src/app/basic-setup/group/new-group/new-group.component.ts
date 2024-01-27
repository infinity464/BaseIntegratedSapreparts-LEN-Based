import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from '../../service/Group.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-new-group',
  templateUrl: './new-group.component.html',
  styleUrls: ['./new-group.component.sass']
})
export class NewGroupComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  GroupForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private GroupService: GroupService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('groupId'); 
    if (id) {
      this.pageTitle = 'Edit Group';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.GroupService.find(+id).subscribe(
        res => {
          this.GroupForm.patchValue({          

            groupId: res.groupId,
            groupName: res.groupName,
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Group';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
  }
  intitializeForm() {
    this.GroupForm = this.fb.group({
      groupId: [0],
      groupName: [''],
      isActive: [true]
    })
  }
  
  onSubmit() {
    const id = this.GroupForm.get('groupId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.GroupService.update(+id,this.GroupForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/group-list');
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
      this.GroupService.submit(this.GroupForm.value).subscribe(response => {
        this.router.navigateByUrl('/basic-setup/group-list');
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
