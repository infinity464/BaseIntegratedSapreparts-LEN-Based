import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { EquipmentNameService } from '../../service/EquipmentName.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EquipmentName } from '../../models/EquipmentName';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';
import { ItemDetailService } from 'src/app/spares-management/service/itemDetail.service';
import { DemandService } from 'src/app/spares-management/service/Demand.service';
import { AcceptanceService } from 'src/app/spares-management/service/Acceptance.service';

@Component({
  selector: 'app-new-equipmentname',
  templateUrl: './new-equipmentname.component.html',
  styleUrls: ['./new-equipmentname.component.sass']
})
export class NewEquipmentNameComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  sparesCategoryId:number;
  EquipmentNameForm: FormGroup;
  validationErrors: string[] = [];
  departmentName: SelectedModel[];
  public files: any[];
  equipmentNameList:EquipmentName[];
  baseSchoolNameList:SelectedModel[];
  selectedDepartmentNames:SelectedModel[];
  selectedItemCategory:SelectedModel[];
  isShown: boolean = false ;
  masterData = MasterData;
  unitId:any;
  departmentId:any;

  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedColumns: string[] = [ 'ser', 'itemCategory', 'name', 'actions'];
  constructor(private snackBar: MatSnackBar,private DemandService: DemandService,private AcceptanceService: AcceptanceService,private demandService: DemandService,private ItemDetailService: ItemDetailService,private baseSchoolNameService: BaseSchoolNameService,private authService: AuthService, private confirmService: ConfirmService,private EquipmentNameService: EquipmentNameService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) {
    this.files = [];
   }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('equipmentNameId');

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Equipment Name';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.EquipmentNameService.find(+id).subscribe(
        res => {
          this.EquipmentNameForm.patchValue({          

            equipmentNameId: res.equipmentNameId,
            departmentNameId:res.departmentNameId,
            sparesCategoryId:res.sparesCategoryId,
            itemCategoryId:res.itemCategoryId,
            name: res.name,
          //  remarks: res.remarks          
          });  
        }
      );
    } else {
      this.pageTitle = 'Create Equipment Name';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.EquipmentNameForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.EquipmentNameForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }
  //  this.GetDepartmentNameById(this.unitId);
    this.getSelectedBaseSchoolName();
    this.getSelectedItemCategory();
  }
  intitializeForm() {
    this.EquipmentNameForm = this.fb.group({
      equipmentNameId: [0],
      departmentNameId:[],
      sparesCategoryId:[1],
      itemCategoryId:[''],
      // itemCategorysId:[],
      // itemCategoryId:[],
      unitId:[''],
      name:[''],
     // remarks: [''],
      isActive: [true]
    
    })
  }
  onSpareCategorySelectionChange(dropdown){
  if(dropdown.isUserInput){
    this.isShown=true;
    var itemCategoryId=dropdown.source.value;
    console.log("base name");

    //  console.log("Unit Id");
    //  console.log(this.unitId);
    //  console.log("Department Name Id");
    //  console.log(this.departmentId);
    //  console.log("Item Category Id");
    //  console.log(itemCategoryId);
      this.EquipmentNameService.getEquipmentNameList(this.unitId,this.departmentId,itemCategoryId).subscribe(res=>{
        this.equipmentNameList=res;
        console.log(this.equipmentNameList)
      }); 
  }
  }

  onDepartmentSelectionChange() {
    this.departmentId = this.EquipmentNameForm.value['departmentNameId']; 
    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.EquipmentNameForm.get('unitId').setValue(this.unitId);

    //  this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentId).subscribe(res => {
    //   this.selectedEquipment = res;
    // });   
   });
  }

  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log("rrrrrrrrr");
      console.log(this.selectedDepartmentNames)
     // console.log(res)
    }); 
  }
  getSelectedItemCategory(){
    this.demandService.getSelectedItemCategory().subscribe(res=>{
      this.selectedItemCategory=res.filter(x=>x.value ==1)
      //console.log(this.selectedDepartmentName);      
    });
  }

  getSelectedBaseSchoolName(){
    this.isShown=true;
    this.baseSchoolNameService.getSelectedBaseSchool().subscribe(res=>{
      this.baseSchoolNameList=res;
      console.log("Base name list");
      console.log(this.baseSchoolNameList);
    });
  }
  // onUnitSelectionChange(){
  //   this.isShown=true;
  //   this.unitId = this.EquipmentNameForm.value['unitId'];
  //   this.GetDepartmentNameById(this.unitId);
  // }

  // onFileChanged(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //     console.log('dddd')
  //    console.log(file);
  //     this.AirCraftNameForm.patchValue({
  //       photo: file,
  //     });
  //   }
  // }
  onEquipmentNameListByDepartmentNameSelectionChange(){
    this.isShown=true;
    var departmentNameId =this.EquipmentNameForm.value['departmentNameId'];
    this.ItemDetailService.findBaseSchoolNameId(departmentNameId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.EquipmentNameForm.get('unitId').setValue(this.unitId);
     // console.log("value");
     // console.log(this.unitId);      
   });
      this.EquipmentNameService.getEquipmentNameListByDepartmentName(departmentNameId).subscribe(res=>{
        this.equipmentNameList=res
        console.log( this.equipmentNameList);
      });
  }
  // getDepartmentName(){
  //   this.EquipmentNameService.getselectedDepartmentNames().subscribe(res=>{
  //     this.departmentName=res
  //     //console.log(this.departmentName);
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.EquipmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res;
      console.log("base name");
      console.log(res)
    }); 
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.EquipmentNameForm.get('equipmentNameId').value;   
    console.log(this.EquipmentNameForm.value)
    //const formData = new FormData();
    //for (const key of Object.keys(this.EquipmentNameForm.value)) {
      //const value = this.EquipmentNameForm.value[key];
      //formData.append(key, value);
    //}
    //console.log(formData)
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
         console.log(result)
        if (result) {
          this.EquipmentNameService.update(+id,this.EquipmentNameForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/add-equipmentname');
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
      this.EquipmentNameService.submit(this.EquipmentNameForm.value).subscribe(response => {
        console.log(this.EquipmentNameForm)
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
    const id = row.equipmentNameId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.EquipmentNameService.delete(id).subscribe(() => {
          this.reloadCurrentRoute();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
  }

}
