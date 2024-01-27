import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ProcurementType } from '../../models/ProcurementType';
import { ProcurementTypeService } from '../../service/ProcurementType.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-procurementtype-list',
  templateUrl: './procurementtype-list.component.html',
  styleUrls: ['./procurementtype-list.component.sass']
})
export class ProcurementTypeListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ProcurementType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'typeName',  'actions'];
  dataSource: MatTableDataSource<ProcurementType> = new MatTableDataSource();

  selection = new SelectionModel<ProcurementType>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ProcurementTypeService: ProcurementTypeService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getProcurementTypes();
  }
 
  getProcurementTypes() {
    this.isLoading = true;
    this.ProcurementTypeService.getProcurementTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getProcurementTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProcurementTypes();
  } 

  deleteItem(row) {
    const id = row.procurementTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ProcurementTypeService.delete(id).subscribe(() => {
          this.getProcurementTypes();
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
