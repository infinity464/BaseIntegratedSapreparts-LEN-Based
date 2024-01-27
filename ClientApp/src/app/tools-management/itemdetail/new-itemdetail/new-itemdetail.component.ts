import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemDetailService } from '../../service/itemDetail.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MasterData } from 'src/assets/data/master-data';
import { ItemDetail } from 'src/app/tools-management/models/itemDetail';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-itemdetail',
  templateUrl: './new-itemdetail.component.html',
  styleUrls: ['./new-itemdetail.component.sass']
})
export class NewItemDetailComponent implements OnInit {
  pageTitle: string;
  sparesCategoryId:string;
  destination:string;
  btnText:string;
  ItemDetailForm: FormGroup;
  validationErrors: string[] = [];
  selectedDepartment:SelectedModel[]; 
  selectedItemType:SelectedModel[]; 
  selectedTrades:SelectedModel[]; 
  itemDetailList:ItemDetail[];
  selectedItemCategory:SelectedModel[]; 
  isShown: boolean = false ;
  masterData = MasterData;

  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;

  displayedColumns: string[] = [ 'ser', 'partNo', 'nameOfItem','trade', 'minimumStock', 'presentStock', 'actions'];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private ItemDetailService: ItemDetailService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('itemDetailId'); 
    this.sparesCategoryId= this.route.snapshot.paramMap.get('sparesCategoryId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Item Detail';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ItemDetailService.find(+id).subscribe(
        res => {
          this.ItemDetailForm.patchValue({          

            itemDetailId: res.itemDetailId,
            itemCategoryId:res.itemCategoryId,
            departmentNameId:res.departmentNameId,
            partNo: res.partNo,
            imcNumber: res.imcNumber,
            serialNo:res.serialNo,
            model:res.model,
            brand:res.brand,
            nameOfItem: res.nameOfItem,
            itemCategoryTypeId:res.itemCategoryTypeId,
            sparesCategoryId:res.sparesCategoryId,
            itemTypeId:res.itemTypeId,
            minimumStock: res.minimumStock,
            alternatiovePrartNo: res.alternatiovePrartNo,
            tradeId: res.tradeId,
            remarks: res.remarks,
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Item Detail';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.onItemDetailListByDepartmentSelectionChange();
    }
    this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedItemType();
    this.getselectedTrades();
    this.getSelectedItemCategory();
  }
  intitializeForm() {
    this.ItemDetailForm = this.fb.group({
      itemDetailId: [0],
      itemCategoryId:[],
      departmentNameId:[],
      partNo: [''],
      imcNumber: [''],
      serialNo:[''],
      model:[''],
      brand:[''],
      nameOfItem: [''],
      itemCategoryTypeId:[],
      sparesCategoryId:[this.masterData.sparescategory.tools],
      itemTypeId:[],
      alternatiovePrartNo: [''],
      minimumStock:[''],
      tradeId:[],
      remarks: [''],
      isActive: [true],
    
    })
  }

  // getselectedDepartmentNames(){
  //   this.ItemDetailService.getselectedDepartmentNames().subscribe(res=>{
  //     this.selectedDepartment=res
  //     console.log(this.selectedDepartment);      
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.ItemDetailService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartment=res
      console.log(res)
    }); 
  }

  onItemDetailListByDepartmentSelectionChange(){
    this.isShown=true;
    var departmentNameId = this.ItemDetailForm.get('departmentNameId').value;
    this.getselectedPresentStocks(departmentNameId);
        // this.ItemDetailService.getSelectedItemDetailListByDepartment(departmentId).subscribe(res=>{
        //   this.itemDetailList=res
        //   console.log(this.itemDetailList);      
        // });
  }
  getselectedPresentStocks(departmentId){
    
    this.ItemDetailService.getselectedPresentStocks(departmentId).subscribe(res=>{
      this.itemDetailList=res
     console.log(this.itemDetailList);      
    });
  }
  getselectedItemType(){
    this.ItemDetailService.getselectedItemType().subscribe(res=>{
      this.selectedItemType=res
      console.log(this.selectedItemType);      
    });
  }
  getSelectedItemCategory(){
    this.ItemDetailService.getSelectedItemCategory().subscribe(res=>{
      this.selectedItemCategory=res
      //console.log(this.selectedDepartmentName);      
    });
  }
  getselectedTrades(){
    this.ItemDetailService.getselectedTrades().subscribe(res=>{
      this.selectedTrades=res
      console.log(this.selectedTrades);      
    });
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.ItemDetailForm.get('itemDetailId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {
        
        if (result) {
          this.ItemDetailService.update(+id,this.ItemDetailForm.value).subscribe(response => {
            this.router.navigateByUrl('/tools-management/add-itemdetail');
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
      this.ItemDetailService.submit(this.ItemDetailForm.value).subscribe(response => {
        //this.router.navigateByUrl('/tools-management/itemdetail-list');
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
