import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DemandService } from '../../../spares-management/service/Demand.service';
import { ItemDetailService } from '../../../spares-management/service/itemDetail.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { Demand } from '../../models/Demand';
import { MatTableDataSource } from '@angular/material/table';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { AcceptanceService } from '../../service/Acceptance.service';
import { ItemDetail } from '../../models/itemDetail';

@Component({
  selector: 'app-new-demand',
  templateUrl: './new-demand.component.html',
  styleUrls: ['./new-demand.component.sass']
})
export class NewDemandComponent implements OnInit {
  pageTitle: string;
  destination: string;
  btnText: string;
  DemandForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel: SelectedModel[];
  selectedAuthority: SelectedModel[];
  selectedItemDetails: SelectedModel[];
  selectedDeno: SelectedModel[];
  selectedTrade: SelectedModel[];
  selectedItemCategory:SelectedModel[];
  selectedSupplierValue: SelectedModel[];
  selectedFiscalYear: SelectedModel[];
  selectedItemType: SelectedModel[];
  selectedOccasionOfDemand: SelectedModel[];
  selectedDemandAuthority: SelectedModel[];
  selectedDepartmentName: SelectedModel[];
  selectedConditionOfItem: SelectedModel[];
  selectedTypeOfDemandValue: SelectedModel[];
  selectedDepartmentNames: SelectedModel[];
  selectedManufacture: SelectedModel[];
  selectedPartNo: SelectedModel[];
  selectedItemName: SelectedModel[];
  selectedEquipmentName:SelectedModel[];
  selectedGroupName:SelectedModel[];
  selectedItemStatus:SelectedModel[];
  selectedEquipment:SelectedModel[];
  selectedPartNos:SelectedModel[];
  selectedSurveyNo:SelectedModel[];
  itemDetail:ItemDetail;
  equipmentNameId:any;
  equipmentName:any;
  itemValue: string;
  itemDetailId: number;
  itemCategoryId:any;
  sparesCategoryId: 1;
  options = [];
  filteredOptions;
  isShown: boolean = false;
  masterData = MasterData;
  demandList: Demand[];
  isLoading = false;
  unitId:any;
  departmentId:any;
  selectedImc:any;
  userRole = Role;
  showPattOrModelDropdown=false;
  showIMC=false;
  survey:any;

  
  traineeId:any;
  role:any;
  branchId:any;
  groupArrays:{ equipmentName: string; courses: any; }[];

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText = "";

  minDate: Date;
  maxDate: Date;

  displayedColumns: string[] = ['ser','itemCategory','equipmentName','itemName','deno','demandQty','demandDate','demandAuthority','itemStatus', 'actions'];
  dataSource: MatTableDataSource<Demand> = new MatTableDataSource();
  constructor(private snackBar: MatSnackBar,private AcceptanceService: AcceptanceService,private authService: AuthService,private ItemDetailService: ItemDetailService, private itemDetailsService: ItemDetailService, private confirmService: ConfirmService, private DemandService: DemandService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('demandId');
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    const currentYear = new Date().getFullYear();
    this.minDate = new Date();

    if (id) {
      this.pageTitle = 'Edit Demand';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.DemandService.find(+id).subscribe(
        res => {
          this.DemandForm.patchValue({
            demandId: res.demandId,
            authorityId: res.authorityId,
            departmentNameId: res.departmentNameId,
            demandTypeId: res.demandTypeId,
            itemDetailId: res.itemDetailId,
            denoId: res.denoId,
            issueRegisterId:res.issueRegisterId,
            groupId:res.groupId,
            itemStatusId:res.itemStatusId,
            itemCategoryId:res.itemCategoryId,
            demandNo: res.demandNo,
            demandDate: res.demandDate,
            letterOuterNo: res.letterOuterNo,
            refPrice: res.refPrice,
            refPoNo: res.refPoNo,
            demandQty: res.demandQty,
            demandLetterNo: res.demandLetterNo,
            specDoc:res.specDoc,
            remarks: res.remarks,
            oldPrice: res.oldPrice,
            oldRefNo: res.oldRefNo,
            manufactureAddress: res.manufactureAddress,
            equipmentNameId:res.equipmentNameId,
            part: res.partNo,
            nameOfDemandee:res.nameOfDemandee,   
            imcNo:res.imcNo,
            surveyNo:res.surveyNo,
          });
          this.itemDetailId = res.itemDetailId;
          console.log("res.partNo")
          console.log(res)
          this.getItemNameById(this.itemDetailId);
          this.onDepartmentSelectionChange();
        }
      );
    } else {
      this.pageTitle = 'Create Demand';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
   
    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.DemandForm.get('departmentNameId').setValue(this.branchId);
      console.log("branch id");
      console.log(this.branchId);
      this.getSelectedDepartmentName(this.branchId);
      this.onDepartmentSelectionChange();
    }
    if(this.role == this.userRole.Admin){

      console.log("admin block")
      
      this.DemandForm.get('unitId').setValue(this.branchId);
      console.log(this.branchId)
      this.getSelectedDepartmentName(this.branchId);
    }

    this.getSelectedAuthority();
    this.getSelectedItemDetails();
    this.getSelectedDeno();
    this.getSelectedFiscalYear();
    this.getSelectedItemType();
    this.getSelectedOccasionOfDemand();
    this.getSelectedDemandAuthority();
    this.getSelectedConditionOfItem();
    this.getSelectedSuplier();
    this.getSelectedTypeOfDemand();
    this.getSelectedManufacture();
    this.getSelectedTrade();
    this.getSelectedItemCategory();
    this.getSelectedGroupName();
    this.getSelectedItemStatus();
    this.getSelectedSurveyNo();
  }
  intitializeForm() {
    this.DemandForm = this.fb.group({
      demandId: [0],
      authorityId: [''],
      part: [''],
      imc:[''],
      doc: [''],
      specDoc: [''],
      itemName: [''],
      itemDetailId: [''],
      denoId: [''],
      issueRegisterId:[''],
      demandTypeId:[''],
      unitId:[''],
      groupId:[''],
      itemStatusId:[''],
      manufactureId:[''],
      sparesCategoryId: [1],
      fiscalYearId: [''],
      itemCategoryId:[''],
      occasionOfDemandId: [''],
      equipmentNameId:[''],
      conditionOfItemId: [''],
      departmentNameId: [''],
      demandCompleteStatus: [0],
      demandQty: [''],
      nameOfDemandee:[''],
      demandLetterNo: [''],
      demandNo: [''],
      demandDate: [],
      letterOuterNo: [''],
      refPrice: [''],
      imcNo:[''],
      surveyNo:[''],
      refPoNo: [''],
      remarks: [''],
      oldPrice: [''],
      oldRefNo: [''],
      manufactureAddress: [''],
      status: [1],
      menuPosition: [1],
      isActive: [true],
      pattorimc:[''],
      surveyId:[''],
    })

    //autocomplete
    this.DemandForm.get('part').valueChanges
      .subscribe(value => {
        this.getSelectedTraineeByPno(value);
      })

       //autocomplete
    this.DemandForm.get('imc').valueChanges
    .subscribe(value => {
      this.getSelectedImc(value);
    })
  }

  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file);
      this.DemandForm.patchValue({
        doc: file,
      });
    }
  }
  onSurvey(dropdown){
    if(dropdown.isUserInput) {

     var itemCategoryId= this.DemandForm.value['itemCategoryId'];
      this.survey=dropdown.source.value;
      console.log("unit");
      console.log(this.unitId);
      console.log("d");
      console.log(this.departmentId);
      console.log("i");
      console.log(itemCategoryId);
      console.log("su");
      console.log(this.survey);
      this.DemandService.getSelectedAuthoritys(this.unitId,this.departmentId,itemCategoryId).subscribe(res=>{
        this.selectedSurveyNo=res;
        console.log("selectedSurveyNo")
        console.log(this.selectedSurveyNo)
      }); 

      console.log(this.survey)
    }
  }
  getSelectedSurveyNo(){
    var departmentId = this.DemandForm.value['departmentNameId'];
    this.ItemDetailService.findBaseSchoolNameId(departmentId).subscribe(res=>{
      var unitId=res.thirdLevel;

      this.DemandForm.get('unitId').setValue(this.unitId);
      console.log("UnitID");
    console.log(this.unitId);
    this.DemandService.getSelectedSurveyNo(unitId,departmentId).subscribe(res=>{
      this.selectedSurveyNo=res;
      console.log("selectedSurveyNo")
      console.log(this.selectedSurveyNo)
    }); 
    //this.getSelectedDemandNo();
    })
    
  }
  // getSelectedSurveyNo() {
  //   //var unitId = this.DemandForm.value['departmentNameId'];
  //   //var departmentId = this.DemandForm.value['departmentNameId'];
    
  //   this.DemandService.getSelectedSurveyNo(unitId,departmentId).subscribe(res => {
  //     this.selectedSurveyNo = res;
  //   });
  // }

  getSelectedDepartmentName(fourthLevel){
    console.log("444");
    console.log(fourthLevel)
    this.AcceptanceService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
      console.log(res)
    }); 
  }

  onPattOrModelSelectionChange(){
    var pattorimc = this.DemandForm.value['pattorimc'];
    if(pattorimc ==1){
       this.showPattOrModelDropdown=true;
       this.showIMC=false;
    }
    else if(pattorimc ==2){
      this.showIMC =true;
      this.showPattOrModelDropdown=false;
    }
    console.log(pattorimc);
  }
  onSpareCategorySelectionChange(dropdown){
    if (dropdown.isUserInput) {
    this.departmentId = this.DemandForm.value['departmentNameId'];
    this.itemCategoryId = dropdown.source.value;
   console.log("unit id");
   console.log(this.unitId);
   console.log("department id");
   console.log(this.departmentId);
   console.log("item category");
    this.DemandForm.get('itemCategoryId').setValue(this.itemCategoryId);

    this.isLoading = true;

    this.DemandService.getDemandListByDepartmentNameId(this.paging.pageIndex, this.paging.pageSize, this.searchText,this.unitId,this.departmentId,this.itemCategoryId).subscribe(response => {

      this.dataSource.data = response.items;
      console.log("dddddd");
      console.log(this.dataSource.data)
      this.paging.length = response.totalItemsCount
      this.isLoading = false;

            // this gives an object with dates as keys
            const groups = this.dataSource.data.reduce((groups, courses) => {
              const schoolName = courses.equipmentName;
              if (!groups[schoolName]) {
                groups[schoolName] = [];
              }
              groups[schoolName].push(courses);
              return groups;
            }, {});
      
            // Edit: to add it in the array format instead
            this.groupArrays = Object.keys(groups).map((equipmentName) => {
              return {
                equipmentName,
                courses: groups[equipmentName]
              };
            });
    })

    this.itemDetailsService.getPartNoByDepartmentNameId(this.departmentId,this.itemCategoryId).subscribe(res => {
      console.log("uuuu");
      this.selectedPartNos = res;
      console.log(this.filteredOptions);
    });
     }
   }

  onSparesCategorySelectionChangeGetEquipmentName( sparesCategoryId){
    this.ItemDetailService.getEquipmentNameBySparesCategoryId(sparesCategoryId).subscribe(res=>{
      this.selectedEquipmentName=res
    });
   }

  getPartNoPassItemCategoryIdInDemand(itemDetailId: number) {
    this.DemandService.getPartNoPassItemCategoryIdInDemand(itemDetailId).subscribe(res => {
      this.selectedItemDetails = res;
      console.log(this.filteredOptions);
    });
  }

  //autocomplete
  onTraineeSelectionChanged(item) {
    // console.log(item);
    this.itemDetailId = item.value
    this.itemCategoryId=item.value
    this.DemandForm.get('itemDetailId').setValue(item.value);
    this.DemandForm.get('part').setValue(item.text);
    this.getItemNameById(this.itemDetailId);

    console.log("itemdetailid");
    console.log(this.itemDetailId);


    this.getPartNoPassItemCategoryIdInDemand(this.itemCategoryId);
    console.log(item.value)
    console.log('V');

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }

  onImcSelectionChange(item) {
    // console.log(item);
    this.itemDetailId = item.value
    this.itemCategoryId=item.value
    this.DemandForm.get('itemDetailId').setValue(item.value);
    this.DemandForm.get('imc').setValue(item.text);
    this.getItemNameById(this.itemDetailId);

    console.log("itemdetailid");
    console.log(this.itemDetailId);


    this.getPartNoPassItemCategoryIdInDemand(this.itemCategoryId);
    console.log(item.value)
    console.log('V');

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }

  getSelectedTraineeByPno(pno) {

    var itemCategoryId = this.DemandForm.value['itemCategoryId'];
    var departmentNameId = this.DemandForm.value['departmentNameId'];
    var unitId = this.DemandForm.value['unitId'];
    this.DemandService.getSelectedPartNoByNameForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getSelectedImc(pno){
    var itemCategoryId = this.DemandForm.value['itemCategoryId'];
    var departmentNameId = this.DemandForm.value['departmentNameId'];
    var unitId = this.DemandForm.value['unitId'];
    this.DemandService.getSelectedImcForSpares(pno,unitId,departmentNameId,itemCategoryId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getSelectedItemDetails() {
    this.DemandService.getSelectedItemDetails().subscribe(res => {
      this.selectedItemDetails = res;
    });
  }
  getSelectedAuthority() {
    this.DemandService.getSelectedAuthority().subscribe(res => {
      this.selectedAuthority = res;
    });
  }
  
  getSelectedTypeOfDemand() {
    this.DemandService.getSelectedTypeOfDemand().subscribe(res => {
      this.selectedTypeOfDemandValue = res;
    });
  }
  getSelectedDeno() {
    this.DemandService.getSelectedDeno().subscribe(res => {
      this.selectedDeno = res;
    });
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

  getSelectedItemStatus(){
    this.DemandService.getSelectedItemStatus().subscribe(res => {
      this.selectedItemStatus = res;
    });
  }
  getSelectedSuplier() {
    this.DemandService.getSelectedSuplier().subscribe(res => {
      this.selectedSupplierValue = res;
    });
  }

  getSelectedFiscalYear() {
    this.DemandService.getSelectedFiscalYear().subscribe(res => {
      this.selectedFiscalYear = res;
    });
  }
  getSelectedItemType() {
    this.DemandService.getSelectedItemType().subscribe(res => {
      this.selectedItemType = res;
    });
  }

  getSelectedOccasionOfDemand() {
    this.DemandService.getSelectedOccasionOfDemand().subscribe(res => {
      this.selectedOccasionOfDemand = res;
    });
  }
  getSelectedDemandAuthority() {
    this.DemandService.getSelectedDemandAuthority().subscribe(res => {
      this.selectedDemandAuthority = res;
    });
  }

  GetDepartmentNameById(baseNameId){    
    this.DemandService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.selectedDepartmentName=res
      console.log(res)
    }); 
  }

  getSelectedGroupName(){
    this.DemandService.getSelectedGroupName().subscribe(res=>{
      this.selectedGroupName=res
      console.log(res)
    }); 
  }

  getSelectedConditionOfItem() {
    this.DemandService.getSelectedConditionOfItem().subscribe(res => {
      this.selectedConditionOfItem = res;
    });
  }

  onDepartmentSelectionChange() {
    this.isShown=true;
    this.departmentId = this.DemandForm.value['departmentNameId']; 
    this.getDemandsList(this.departmentId);

    this.ItemDetailService.findBaseSchoolNameId(this.departmentId).subscribe(res=>{
      this.unitId=res.thirdLevel;

     this.DemandForm.get('unitId').setValue(this.unitId);

     this.DemandService.getSelectedEquipmentName(this.masterData.sparescategory.spares,this.departmentId).subscribe(res => {
      this.selectedEquipment = res;
    });
   });
  }
  getItemNameById(id: number) {
    console.log(id);
    this.itemDetailsService.getItemNameById(id).subscribe(res => {
      this.selectedItemName = res;
      console.log("selected Item Name");
      console.log(this.selectedItemName);
      this.itemValue = this.selectedItemName[0].value
      console.log(this.itemValue);
    });
    this.itemDetailsService.find(id).subscribe(res => {
      this.equipmentNameId = res.equipmentNameId;
      this.DemandForm.get('equipmentNameId').setValue(this.equipmentNameId);
    });
    this.itemDetailsService.find(id).subscribe(res => {
      console.log(res);
      this.equipmentName = res.equipmentName;
    });

    this.itemDetailsService.find(this.itemDetailId).subscribe(res => {
      console.log("uuuu");
      this.selectedImc = res.imcNumber;
      console.log(this.selectedImc);
    });
  }
  onPartNoSelectionChange(dropdown) {
    if (dropdown.isUserInput) {
      console.log(dropdown.source.value);
      this.getItemNameById(dropdown.source.value);
    }
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  getDemandsList(departmentId) {

  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    var departmentId = this.DemandForm.get('departmentNameId').value;
    console.log(departmentId);
    this.getDemandsList(departmentId);
  }
  onSubmit() {
    const id = this.DemandForm.get('demandId').value;
    console.log(this.DemandForm)
    this.DemandForm.get('demandDate').setValue((new Date(this.DemandForm.get('demandDate').value)).toUTCString());

    console.log(this.DemandForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.DemandForm.value)) {
      const value = this.DemandForm.value[key];
      formData.append(key, value);
    }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item?').subscribe(result => {

        if (result) {
          this.DemandService.update(+id, formData).subscribe(response => {
            this.router.navigateByUrl('/spares-management/add-demand');
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
