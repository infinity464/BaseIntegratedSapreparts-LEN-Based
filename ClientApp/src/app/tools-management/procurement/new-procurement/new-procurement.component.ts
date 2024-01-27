import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcurementService } from '../../service/Procurement.service';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentNameService } from '../../../basic-setup/service/DepartmentName.service';
import { DemandService } from '../../../spares-management/service/Demand.service';
import { Demand } from '../../models/Demand';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import { Procurement } from '../../models/Procurement';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-procurement',
  templateUrl: './new-procurement.component.html',
  styleUrls: ['./new-procurement.component.sass']
})
export class NewProcurementComponent implements OnInit {
  pageTitle: string;
  masterData = MasterData;
  destination:string;
  btnText:string;
  ProcurementForm: FormGroup;
  validationErrors: string[] = [];
  itemDetail: SelectedModel[];
  procurementStatus: SelectedModel[];
  principalName: SelectedModel[];
  selectManufacture: SelectedModel[];
  Supplier: SelectedModel[];
  partOfShipment: SelectedModel[];
  departmentName: SelectedModel[];
  cstTec: SelectedModel[];
  selectedPartNo: SelectedModel[];
  isShown: boolean = false ;
  demandData: Demand[];
  itemDetailId: number;
  sparesCategoryId: number;
  isLoading = false;
  cstTecYes:any;
  
  
  userRole = Role;
  
  traineeId:any;
  role:any;
  branchId:any;
  
  searchText="";
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  displayedDepColumns: string[] = [ 'ser', 'itemDetail','itemName','qty','dateOfDelivery', 'supplier', 'actions'];
  dataSource: MatTableDataSource<Procurement> = new MatTableDataSource();
  displayedColumns: string[] = ['sl','itemName','deno', 'demandQty', 'conditionOfItem','demandAuthority', 'occasionOfDemand', 'manufacture'];

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private DemandService:DemandService,private DepartmentNameService: DepartmentNameService,private confirmService: ConfirmService,private ProcurementService: ProcurementService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('procurementId'); 
    
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId,  this.branchId)

    if (id) {
      this.pageTitle = 'Edit Procurement';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ProcurementService.find(+id).subscribe(
        res => {
          this.ProcurementForm.patchValue({          
            procurementId: res.procurementId,
            demandId: res.demandId,
            itemDetailId: res.itemDetailId,
            procurementStatusId: res.procurementStatusId,
            //principalNameId: res.principalNameId,
            principalNameId: res.principalNameId,
            manufactureId:res.manufactureId,
            cstTecId:res.cstTecId,
            //localAgentId: res.localAgentId,
            supplierId: res.supplierId,
            //supplierAId:res.supplierAId,
            //supplierMId:res.supplierMId,
            //partOfShipmentId: res.partOfShipmentId,
            sparesCategoryId: res.sparesCategoryId,
            departmentNameId: res.departmentNameId,
            tenderNumber: res.tenderNumber,
            dateOfTenderFloat: res.dateOfTenderFloat,
            tenderopeningDate: res.tenderopeningDate,
            tenderPublishDate: res.tenderPublishDate,
            tenderNotice: res.tenderNotice,
            tenderSpecification: res.tenderSpecification,
            financialApproval: res.financialApproval,
            workOrder: res.workOrder,
            dateOfDelivery: res.dateOfDelivery,
            unitPrice: res.unitPrice,
            qty: res.qty,
            remarks: res.remarks,
            procurementDocument: res.procurementDocument,
            status: res.status,
            menuPosition: res.menuPosition,
            isActive: res.isActive
          }); 
          this.getselectedDemandsOnUpdate(res.departmentNameId);
          this.getDemandData();
        }
      );
    } else {
      this.pageTitle = 'Create  Procurement';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.ProcurementForm.get('departmentNameId').setValue(this.branchId);
      this.getselectedDemands();
    }
  }
  intitializeForm() {
    this.ProcurementForm = this.fb.group({
      
      procurementId: [0],
      demandId: [],
      itemDetailId: [],
      //procurementStatusId: [],
      principalNameId: [''],
      manufactureId:[''],
      cstTecId: [],
      //localAgentId: [],
      supplierId:[''],
      //supplierAId:[],
      //supplierMId:[],
      //partOfShipmentId: [],
      sparesCategoryId: [],
      departmentNameId: [],
      tenderNumber: [''],
      dateOfTenderFloat: [],
      tenderopeningDate: [],
      //tenderPublishDate: [],
      tenderNotice: [''],
      notice:[''],
      procurementCompleteStatus:[0],
      tenderSpecification: [''],
      doc:[''],
      financialApproval: [''],
      approval:[''],
      workOrder: [''],
      order:[''],
      dateOfDelivery: [],
      unitPrice: [''],
      qty: [''],
      remarks: [''],
      procurementDocument: [''],
     // status: [],
      //menuPosition: [],
      isActive: [true]
    })
    this.GetDepartmentNameById(this.masterData.schoolDept.navalAviation);
    this.getselectedItemDetails();
    this.getselectedSupplier();
    this.getselectedPrincipalNames();
    this.getselectedManufacture();
    this.getselectedPartOfShipments();
    //this.getselectedDemands();
    this.getselectedCstTecs();
  }

  onFileNChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('jjjjjjj');
     console.log(file);
      this.ProcurementForm.patchValue({
        notice: file,
      });
    }
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
     console.log(file);
      this.ProcurementForm.patchValue({
        doc: file,
      });
    }
  }
  onFileFChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('bbbbbb');
     console.log(file);
      this.ProcurementForm.patchValue({
        approval: file,
      });
    }
  }
  onFileWChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('jjjjjjj');
     console.log(file);
      this.ProcurementForm.patchValue({
        order: file,
      });
    }
  }
  // getselectedDepertments(){
  //   this.DepartmentNameService.getselectedDepertments().subscribe(res=>{
  //     this.departmentName=res;
  //   });    
  // }
  oncstTecYes(dropdown){

    if(dropdown.isUserInput) {
      //this.getProcurementsList(dropdown.source.value);
      this.cstTecYes=dropdown.source.value;
      console.log(this.cstTecYes)
    }
  }
  getselectedManufacture(){
    this.ProcurementService.getselectedManufacture().subscribe(res=>{
      this.selectManufacture=res;
    }); 
  }
  getselectedPrincipalNames(){
    this.ProcurementService.getselectedPrincipalNames().subscribe(res=>{
      this.principalName=res;
    });  
  }
  GetDepartmentNameById(baseNameId){    
    this.DepartmentNameService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log(res)
    }); 
  }

  getselectedItemDetails(){
    this.ProcurementService.getselectedItemDetails().subscribe(res=>{
      this.itemDetail=res;
    });    
  }

  // getselectedPrincipalNames(){
  //   this.ProcurementService.getselectedPrincipalNames().subscribe(res=>{
  //     this.principalName=res;
  //   });  
  // }
  // getselectedProcurementStatus(){
  //   this.ProcurementService.getselectedProcurementStatus().subscribe(res=>{
  //     this.procurementStatus=res;
  //   }); 
  // }
  // getselectedLocalAgents(){
  //   this.ProcurementService.getselectedLocalAgents().subscribe(res=>{
  //     this.localAgent=res;
  //   }); 
  // }
  getselectedSupplier(){
    this.ProcurementService.getselectedSupplier().subscribe(res=>{
      this.Supplier=res;
    }); 
  }
  // getselectedSupplierA(){
  //   this.ProcurementService.getselectedSupplierA().subscribe(res=>{
  //     this.SupplierA=res;
  //   }); 
  // }
  // getselectedSupplierM(){
  //   this.ProcurementService.getselectedSupplierM().subscribe(res=>{
  //     this.SupplierM=res;
  //   }); 
  // }
  getselectedPartOfShipments(){
    this.ProcurementService.getselectedPartOfShipments().subscribe(res=>{
      this.partOfShipment=res;
    }); 
  }

  getselectedDemands(){
    var departmentNameId = this.ProcurementForm.value['departmentNameId'];
    this.isShown=true;
    //this.getProcurementsList(departmentNameId);
    this.ProcurementService.getselectedDemands(departmentNameId, this.masterData.sparescategory.tools).subscribe(res=>{
      this.selectedPartNo=res;
    });
    this.getProcurementsList(departmentNameId);
  }

  getselectedDemandsOnUpdate(id){
    this.ProcurementService.getselectedDemandsOnUpdate(id, this.masterData.sparescategory.tools).subscribe(res=>{
      this.selectedPartNo=res;
      console.log(this.selectedPartNo)
    });
  }

  getselectedCstTecs(){
    this.ProcurementService.getselectedCstTecs().subscribe(res=>{
      this.cstTec=res;
    }); 
  }

  getDemandData(){
    var demandId = this.ProcurementForm.value['demandId'];
    console.log(demandId);
    this.isShown=true;
    this.DemandService.GetselectedDemandById(demandId).subscribe(res=>{
      this.demandData=res;
      
      console.log("demand result "+this.demandData)
    });

    this.DemandService.find(demandId).subscribe(res=>{
      this.itemDetailId=res.itemDetailId;
      this.sparesCategoryId=res.sparesCategoryId;
      this.ProcurementForm.get('itemDetailId').setValue(this.itemDetailId);
      this.ProcurementForm.get('sparesCategoryId').setValue(this.sparesCategoryId);
    });
    
  }
  // onDepartmentNameSelectionChange(dropdown){
  //   this.isShown=true;
  //   if(dropdown.isUserInput) {
  //     this.getProcurementsList(dropdown.source.value);
    
  //   }
  // }
  getProcurementsList(departmentId) {
    this.isLoading = true;
    this.ProcurementService.getProcurementListByDepartmentNameId(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.masterData.sparescategory.spares,departmentId).subscribe(response => {
      
      this.dataSource.data = response.items;
      //console.log("dddddd");
      console.log(this.dataSource.data )
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    var departmentId=this.ProcurementForm.get('departmentNameId').value;
    console.log(departmentId);
    this.getProcurementsList(departmentId);
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  
  onSubmit() {
    const id = this.ProcurementForm.get('procurementId').value;   
    //console.log(this.ProcurementForm.value)
    this.ProcurementForm.get('dateOfTenderFloat').setValue((new Date(this.ProcurementForm.get('dateOfTenderFloat').value)).toUTCString()) ;
    this.ProcurementForm.get('tenderopeningDate').setValue((new Date(this.ProcurementForm.get('tenderopeningDate').value)).toUTCString()) ;
    this.ProcurementForm.get('dateOfDelivery').setValue((new Date(this.ProcurementForm.get('dateOfDelivery').value)).toUTCString()) ;
    this.ProcurementForm.get('dateOfDelivery').setValue((new Date(this.ProcurementForm.get('dateOfDelivery').value)).toUTCString()) ;
    console.log('mmmmm')
    console.log(this.ProcurementForm.value)

    const formData = new FormData();
    for (const key of Object.keys(this.ProcurementForm.value)) {
      const value = this.ProcurementForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ProcurementService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/tools-management/add-procurement');
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
      this.ProcurementService.submit(formData).subscribe(response => {
        //this.router.navigateByUrl('/tools-management/procurement-list');
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
    const id = row.procurementId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe(result => {
      console.log(result);
      if (result) {
        this.ProcurementService.delete(id).subscribe(() => {
          //this.getProcurements();
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
