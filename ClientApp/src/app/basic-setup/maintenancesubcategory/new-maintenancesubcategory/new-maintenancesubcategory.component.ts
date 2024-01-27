import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MaintenanceSubCategoryService } from '../../service/maintenanceSubCategory.service';
import { MaintenanceCategoryService } from '../../service/MaintenanceCategory.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MaintenanceSubCategory } from '../../models/maintenanceSubCategory';
import { MasterData } from 'src/assets/data/master-data';

@Component({
  selector: 'app-new-maintenancesubcategory',
  templateUrl: './new-maintenancesubcategory.component.html',
  styleUrls: ['./new-maintenancesubcategory.component.sass']
})
export class NewMaintenanceSubCategoryComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  MaintenanceSubCategoryForm: FormGroup;
  validationErrors: string[] = [];
  selectedMaintenanceCategory:SelectedModel[]; 
  selectedDepartmentName:SelectedModel[]; 
  selectedMaintenanceCategoryByDepartment:SelectedModel[];
  selectedMaintenanceSubCategoryByDepartmentAndMaintenenceCategory:MaintenanceSubCategory[];
  isShown: boolean = false ;
  masterData = MasterData;

  displayedColumns: string[] = [ 'ser', 'maintenanceCategory', 'subCategoryName','allowedExtension', 'departmentName', 'remarks', 'actions'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private MaintenanceSubCategoryService: MaintenanceSubCategoryService,private MaintenanceCategoryService: MaintenanceCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('maintenanceSubCategoryId'); 
    if (id) {
      this.pageTitle = 'Edit Maintenance Sub Category';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.MaintenanceSubCategoryService.find(+id).subscribe(
        res => {
          this.MaintenanceSubCategoryForm.patchValue({          

            maintenanceSubCategoryId: res.maintenanceSubCategoryId,
            maintenanceCategoryId: res.maintenanceCategoryId,
            subCategoryName: res.subCategoryName,
            allowedExtension: res.allowedExtension,
            departmentNameId: res.departmentNameId,
            remarks: res.remarks,
          });       
        this.onDepartmentSelectionChange(res.departmentNameId);
        }
      );
    } else {
      this.pageTitle = 'Create Maintenance Sub Category';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    this.getselectedMaintenanceCategory();
    this.getselectedDepartmentName();
  }
  intitializeForm() {
    this.MaintenanceSubCategoryForm = this.fb.group({
      maintenanceSubCategoryId: [0],
      maintenanceCategoryId: [],
      subCategoryName: [''],
      allowedExtension: [''],
      departmentNameId: [],
      remarks: [''],
      isActive: [true],
    
    })
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }

  onDepartmentSelectionChange(dropdown){
    console.log(dropdown);
        this.MaintenanceCategoryService.getMaintenanceCategoryByDepartment(dropdown).subscribe(res=>{
          this.selectedMaintenanceCategoryByDepartment=res
        });
  }
  onMaintenanceCategorySelectionChange(dropdown){
    this.isShown=true;
    console.log(dropdown);
        var departmentNameId =this.MaintenanceSubCategoryForm.value['departmentNameId'];
        this.MaintenanceSubCategoryService.getSelectedMaintenanceSubCategory(departmentNameId,dropdown).subscribe(res=>{
        this.selectedMaintenanceSubCategoryByDepartmentAndMaintenenceCategory=res
        console.log(this.selectedMaintenanceSubCategoryByDepartmentAndMaintenenceCategory);      
      });
  }
  getselectedMaintenanceCategory(){
    this.MaintenanceSubCategoryService.getselectedMaintenanceCategory().subscribe(res=>{
      this.selectedMaintenanceCategory=res
     // console.log(this.selectedMaintenanceCategory);      
    });
  }
  getselectedDepartmentName(){
    this.MaintenanceSubCategoryService.getselectedDepartmentName().subscribe(res=>{
      this.selectedDepartmentName=res
     // console.log(this.selectedDepartmentName);      
    });
  }
  onSubmit() {
    const id = this.MaintenanceSubCategoryForm.get('maintenanceSubCategoryId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.MaintenanceSubCategoryService.update(+id,this.MaintenanceSubCategoryForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-maintenancesubcategory');
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
      this.MaintenanceSubCategoryService.submit(this.MaintenanceSubCategoryForm.value).subscribe(response => {
        //this.router.navigateByUrl('/basic-setup/maintenancesubcategory-list');
        this.reloadCurrentRoute();
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
