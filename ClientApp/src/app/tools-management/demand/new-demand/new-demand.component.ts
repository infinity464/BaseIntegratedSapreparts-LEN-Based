import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DemandService } from '../../../tools-management/service/Demand.service';
import { ItemDetailService } from '../../../spares-management/service/itemDetail.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import { Demand } from '../../models/Demand';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-demand',
  templateUrl: './new-demand.component.html',
  styleUrls: ['./new-demand.component.sass']
})
export class NewDemandComponent implements OnInit {
  pageTitle: string;
  destination:string;
  btnText:string;
  DemandForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedAuthority:SelectedModel[];
  selectedItemDetails:SelectedModel[];
 selectedDeno:SelectedModel[];
 selectedFiscalYear:SelectedModel[];
 selectedManufacture:SelectedModel[];
 selectedItemCategory:SelectedModel[];
 selectedTrade:SelectedModel[];
 selectedItemType:SelectedModel[];
 selectedOccasionOfDemand:SelectedModel[];
 selectedDemandAuthority:SelectedModel[];
 selectedDepartmentName:SelectedModel[];
 selectedConditionOfItem:SelectedModel[];
 selectedPartNo:SelectedModel[];
 selectedItemName:SelectedModel[];
 itemValue:string;
 itemDetailId:number;
 sparesCategoryId:any;
 options = [];
 filteredOptions;
 masterData = MasterData;
 isLoading = false;
 isShown: boolean = false;

 userRole = Role;
  
 traineeId:any;
 role:any;
 branchId:any;

 paging = {
  pageIndex: this.masterData.paging.pageIndex,
  pageSize: this.masterData.paging.pageSize,
  length: 1
}
searchText="";

displayedColumns: string[] = [ 'ser','itemDetail','itemName','imcNumber','deno', 'demandDate','demandQty','demandNo','refPrice','isActive', 'actions'];
dataSource: MatTableDataSource<Demand> = new MatTableDataSource();
constructor(private snackBar: MatSnackBar,private authService: AuthService,private itemDetailsService:ItemDetailService,private confirmService: ConfirmService,private DemandService: DemandService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('demandId'); 
     
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)


    if (id) {
      this.pageTitle = 'Edit Demand';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DemandService.find(+id).subscribe(
        res => {
          this.DemandForm.patchValue({          
            demandId: res.demandId,
            authorityId: res.authorityId,
            itemDetailId: res.itemDetailId,
            denoId: res.denoId,
            manufactureId:res.manufactureId,
            itemCategoryId:res.itemCategoryId,
            fiscalYearId: res.fiscalYearId,
            tradeId:res.tradeId,
            //itemTypeId: res.itemTypeId,
            occasionOfDemandId: res.occasionOfDemandId,
            sparesCategoryId: res.sparesCategoryId,
            //demandAuthorityId: res.demandAuthorityId,
            //demandTypeId: res.demandTypeId,
            //demandDocId: res.demandDocId,
            demandNo: res. demandNo,
            conditionOfItemId: res.conditionOfItemId,
            departmentNameId: res.departmentNameId,
            demandCompleteStatusId: res.demandCompleteStatusId,
            demandQty: res.demandQty,
            demandLetterNo: res.demandLetterNo,
            demandDate: res.demandDate,
            letterOuterNo: res.letterOuterNo,
            specDoc:res.specDoc,
            refPrice: res.refPrice,
            refPoNo: res.refPoNo,
            remarks: res.remarks,
            oldPrice: res.oldPrice,
            oldRefNo: res.oldRefNo,
            manufactureAddress: res.manufactureAddress,
            partNo:res.partNo,
            // status: res.status,
            // menuPosition: res.menuPosition,
            // isActive: res.isActive
          });    
          console.log(res.partNo)
          this.itemDetailId = res.itemDetailId 
          this.getItemNameById(this.itemDetailId);      
        }
      );
    } else {
      this.pageTitle = 'Create Demand';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.DemandForm.get('departmentNameId').setValue(this.branchId);
      this.onDepartmentNameSelectionChange();
    }
    this.getSelectedAuthority();
    this.getSelectedItemDetails();
    this.getSelectedDeno();
    this.getSelectedFiscalYear();
    this.getSelectedItemType();
    this.getSelectedOccasionOfDemand();
    this.getSelectedDemandAuthority();
    this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getSelectedConditionOfItem();
    this.getSelectedTrade();
    this.getSelectedItemCategory();
    this.getSelectedManufacture();
  }
  intitializeForm() {
    this.DemandForm = this.fb.group({
      demandId: [0],
      authorityId: [''],
      partNo:[''],
      doc:[''],
      specDocument:[''],
      specDoc:[''],
      manufactureId:[],
      itemCategoryId:[''],
      tradeId:[],
      demandNo:[''],
     // itemName:[''],
      itemDetailId: [''],
      denoId: [''],
      fiscalYearId: [''],
      itemTypeId:[''],
      occasionOfDemandId:[''],
      sparesCategoryId:[2],
      demandAuthorityId:[''],
      demandTypeId:[''],
      demandDocId: [''],
      conditionOfItemId:[''],
      departmentNameId: [''],
      demandCompleteStatus:[0], 
      demandQty:[''],
      demandLetterNo:[''],
      demandDate: [''],
      letterOuterNo: [''],
      refPrice:[''],
      refPoNo:[''],
      remarks: [''],
      oldPrice: [''],
      oldRefNo:[''],
      manufactureAddress: [''],
      status: [1],
      menuPosition: [1],
      isActive: [true]
    })
      //autocomplete
      this.DemandForm.get('partNo').valueChanges
      .subscribe(value => {
          this.getSelectedTraineeByPno(value);
      })
  }

    //autocomplete
    onTraineeSelectionChanged(item) {
      // console.log(item);
       this.itemDetailId = item.value 
       this.DemandForm.get('itemDetailId').setValue(item.value);
       this.DemandForm.get('partNo').setValue(item.text);

       this.getItemNameById(this.itemDetailId);

       this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
        // this.sparesCategoryId = res.sparesCategoryId;
        // this.DemandForm.get('sparesCategoryId').setValue(this.sparesCategoryId);
        // console.log(this.sparesCategoryId);
       })
      // this.baseSchoolNameId = this.CourseModuleForm.get('baseSchoolNameId').value;
   
      //    this.isShown=true;
      //  this.CourseNameService.getCourseModuleListByBaseSchoolNameIdCourseNameId(this.baseSchoolNameId,this.courseNameId).subscribe(response => {
      //    this.moduleList = response;
      //  })
   }

  //  getItemData(){
  //   var itemId = this.DemandForm.value['itemId'];
  //   console.log(itemId);
    // this.isShown=true;
    // this.DemandService.GetselectedDemandById(demandId).subscribe(res=>{
    //   this.demandData=res;
      
    //   console.log("demand result "+this.demandData)
    // });

    // this.DemandService.find(demandId).subscribe(res=>{
    //   this.itemDetailId=res.itemDetailId;
    //   this.ProcurementForm.get('itemDetailId').setValue(this.itemDetailId);
    // });
  // }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      // this.labelImport.nativeElement.value = file.name;
      console.log('jjjjjjj');
     console.log(file);
    // this.BIODataGeneralInfoForm.controls["picture"].setValue(event.target.files[0]);
      this.DemandForm.patchValue({
        doc: file,
      });
    }
  }

  onTenderSpecChanged(event){
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      // this.labelImport.nativeElement.value = file.name;
      console.log('tenderspecg');
     console.log(file);
    // this.BIODataGeneralInfoForm.controls["picture"].setValue(event.target.files[0]);
      this.DemandForm.patchValue({
        specDocument: file,
      });
    }
  }
   //autocomplete
        //  getSelectedTraineeByPno(pno){
        //    this.DemandService.getSelectedCourseByName(pno).subscribe(response => {
        //      this.options = response;
        //      this.filteredOptions = response;
        //    })
        //  }
  getSelectedTraineeByPno(pno){
    this.DemandService.getSelectedPartNoByDepartment(pno).subscribe(response => {
      this.options = response;
        this.filteredOptions = response;
    })
  }
  getSelectedTrade() {
    this.DemandService.getSelectedTrade().subscribe(res => {
      this.selectedTrade = res;
    });
  }
  getSelectedItemCategory() {
    this.DemandService.getSelectedItemCategory().subscribe(res => {
      this.selectedItemCategory = res;
    });
  }
  getSelectedManufacture() {
    this.DemandService.getSelectedManufacture().subscribe(res => {
      this.selectedManufacture = res;
    });
  }
  getSelectedItemDetails(){
    this.DemandService.getSelectedItemDetails().subscribe(res=>{
      this.selectedItemDetails=res;
    });
  }
  getSelectedAuthority(){
    this.DemandService.getSelectedAuthority().subscribe(res=>{
      this.selectedAuthority=res;
    });
  }
  getSelectedDeno(){
    this.DemandService.getSelectedDeno().subscribe(res=>{
      this.selectedDeno=res;
    });
  }

  getSelectedFiscalYear(){
    this.DemandService.getSelectedFiscalYear().subscribe(res=>{
      this.selectedFiscalYear=res;
    });
  }
  getSelectedItemType(){
    this.DemandService.getSelectedItemType().subscribe(res=>{
      this.selectedItemType=res;
    });
  }

  getSelectedOccasionOfDemand(){
    this.DemandService.getSelectedOccasionOfDemand().subscribe(res=>{
      this.selectedOccasionOfDemand=res;
    });
  }
  getSelectedDemandAuthority(){
    this.DemandService.getSelectedDemandAuthority().subscribe(res=>{
      this.selectedDemandAuthority=res;
    });
  }
  // getSelectedDepartmentName(){
  //   this.DemandService.getSelectedDepartmentName().subscribe(res=>{
  //     this.selectedDepartmentName=res;
  //   });
  // }
  GetDepartmentNameById(baseNameId){    
    this.DemandService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentName=res
      console.log(res)
    }); 
  }

  getSelectedConditionOfItem(){
    this.DemandService.getSelectedConditionOfItem().subscribe(res=>{
      this.selectedConditionOfItem=res;
    });
  }
  // getPartNoByDepartmentNameId(id:number){
  //   this.itemDetailsService.getPartNoByDepartmentNameId(,).subscribe(res=>{
  //     this.selectedPartNo=res;
  //     console.log(this.selectedPartNo); 
  //   });
  // }
  getItemNameById(id:number){
    console.log(id);
    this.itemDetailsService.getItemNameById(id).subscribe(res=>{
       this.selectedItemName=res;
       console.log(this.selectedItemName);
       this.itemValue = this.selectedItemName[0].value
      console.log(this.itemValue);
    });
  }
  onPartNoSelectionChange(dropdown){
    if(dropdown.isUserInput) {
      console.log(dropdown.source.value);
      this.getItemNameById(dropdown.source.value);
      //this.getPartNoByDepartmentNameId(dropdown.source.value);
    }
  }
  onDepartmentNameSelectionChange() {
    this.isShown = true;
    var departmentId = this.DemandForm.get('departmentNameId').value;
    this.getDemandsList(departmentId);
  }
  getDemandsList(departmentId) {
    this.isLoading = true;
    this.DemandService.getDemandListByDepartmentNameId(this.paging.pageIndex, this.paging.pageSize, this.searchText, this.masterData.sparescategory.spares, departmentId).subscribe(response => {

      this.dataSource.data = response.items;
      console.log("dddddd");
      console.log(this.dataSource.data)
      this.paging.length = response.totalItemsCount
      this.isLoading = false;
    })
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    var departmentId = this.DemandForm.get('departmentNameId').value;
    console.log(departmentId);
    this.getDemandsList(departmentId);
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
  onSubmit() {
    const id = this.DemandForm.get('demandId').value; 
    this.DemandForm.get('demandDate').setValue((new Date(this.DemandForm.get('demandDate').value)).toUTCString()) ;
    console.log(this.DemandForm.value)  

    const formData = new FormData();
    for (const key of Object.keys(this.DemandForm.value)) {
      const value = this.DemandForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        
        if (result) {
          this.DemandService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/tools-management/add-demand');
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
      this.DemandService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/tools-management/demand-list');
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
    const id = row.demandId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DemandService.delete(id).subscribe(() => {
          //this.getDemands();
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
