import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PmsNumberEntryService } from '../../service/PmsNumberEntry.service';
import { MaintenanceCategoryService } from '../../service/MaintenanceCategory.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PmsNumberEntry } from '../../models/PmsNumberEntry';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-new-pmsnumberentry',
  templateUrl: './new-pmsnumberentry.component.html',
  styleUrls: ['./new-pmsnumberentry.component.sass']
})
export class NewPmsNumberEntryComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  PmsNumberEntryForm: FormGroup;
  validationErrors: string[] = [];
  selectedEquipmentName:SelectedModel[]; 
  selectedDepartmentNames:SelectedModel[]; 
  selectedItemNames:SelectedModel[]; 
  selectedMaintenanceCategoryByDepartment:SelectedModel[];
  getPmsNumberEntryList:PmsNumberEntry[];
  isShown: boolean = false ;
  masterData = MasterData;
  isLoading = false;
  userRole = Role;
  role:any;
  branchId:any;
  traineeId:any;
  unitId:any;
  //departmentNameId:number;

  displayedColumns: string[] = [ 'ser','itemName','pmsNo',  'actions'];
  dataSource: MatTableDataSource<PmsNumberEntry> = new MatTableDataSource();
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  constructor(private snackBar: MatSnackBar,private baseSchoolNameService: BaseSchoolNameService,private confirmService: ConfirmService,private authService: AuthService,private PmsNumberEntryService: PmsNumberEntryService,private MaintenanceCategoryService: MaintenanceCategoryService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('pmsNumberEntryId');  
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)
    if (id) {
      this.pageTitle = 'Edit PMS Number Entry';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.PmsNumberEntryService.find(+id).subscribe(
        res => {
          this.PmsNumberEntryForm.patchValue({          

            pmsNumberEntryId: res.pmsNumberEntryId,
            departmentNameId: res.departmentNameId,
            equipmentNameId: res.equipmentNameId,
            itemDetailId:res.itemDetailId,
            pmsNo: res.pmsNo,
          });       
        this.onDepartmentSelectionChange(res.departmentNameId);
        this.onDepartmentSelectionChangegetItemDetail()
        }
      );
    } else {
      this.pageTitle = 'Create PMS Number Entry';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.PmsNumberEntryForm.get('departmentNameId').setValue(this.branchId);
      this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
        console.log(res)
        this.PmsNumberEntryForm.get('unitId').setValue(res.thirdLevel);
        this.unitId = this.PmsNumberEntryForm.get('unitId').value;
      });
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChangegetItemDetail()
     // this.getPmsNumberEntrysList(this.unitId,this.branchId);
     // this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.PmsNumberEntryForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
    //this.getEquipmentName();
  }
  intitializeForm() {
    this.PmsNumberEntryForm = this.fb.group({
      pmsNumberEntryId: [0],
      departmentNameId: [''],
      equipmentNameId: [''],
      itemDetailId:[''],
      unitId:[''],
      pmsNo: [''],
      isActive: [true],
    
    })
  }

  onItemSelectionChange(){
    var itemDetailId = this.PmsNumberEntryForm.get('itemDetailId').value;
    console.log(itemDetailId);
   this.getPmsNumberEntrysList(this.unitId,this.branchId,itemDetailId)
  }
  getPmsNumberEntrysList(unitId,departmentNameId,itemDetailId) {
    this.isLoading = true;
    this.PmsNumberEntryService.getPmsNumberEntrysList(this.paging.pageIndex, this.paging.pageSize,this.searchText,unitId,departmentNameId,itemDetailId).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  getPmsNumberEntries() {
    this.isLoading = true;
    this.PmsNumberEntryService.getPmsNumberEntrys(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getPmsNumberEntries();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getPmsNumberEntries();
  } 
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onDepartmentSelectionChange(dropdown){
    this.isShown=true;
    if(dropdown.isUserInput) {
      this.PmsNumberEntryService.getPmsNumberEntryByDepartment(dropdown.source.value).subscribe(res=>{
        //this.onDepartmentSelectionChangegetItemDetail(dropdown)
        this.getPmsNumberEntryList=res
        console.log( this.getPmsNumberEntryList);
      });
    }
  }
  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.PmsNumberEntryService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      //console.log(res)
      //console.log(res)
    }); 
  }
  // getEquipmentName(){
  //   this.PmsNumberEntryService.getEquipmentName().subscribe(res=>{
  //     this.selectedEquipmentName=res
  //   }); 
  // }
  onDepartmentSelectionChangegetItemDetail(){
    var departmentNameId = this.PmsNumberEntryForm.value['departmentNameId'];
    this.PmsNumberEntryService.getItemDetail(departmentNameId).subscribe(res=>{
      this.selectedItemNames=res
      console.log('this.selectedItemNames')

      console.log(this.selectedItemNames)
    }); 
  }
  
  onSubmit() {
    const id = this.PmsNumberEntryForm.get('pmsNumberEntryId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.PmsNumberEntryService.update(+id,this.PmsNumberEntryForm.value).subscribe(response => {
           this.router.navigateByUrl('/basic-setup/add-pmsnumberentry');
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
      this.PmsNumberEntryService.submit(this.PmsNumberEntryForm.value).subscribe(response => {
        //this.router.navigateByUrl('/basic-setup/pmsnumberentry-list');
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
  deleteItem(row) {
    const id = row.pmsNumberEntryId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.PmsNumberEntryService.delete(id).subscribe(() => {

         // this.getCastes();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
          this.reloadCurrentRoute();
        })
      }
    })    
  }

}
