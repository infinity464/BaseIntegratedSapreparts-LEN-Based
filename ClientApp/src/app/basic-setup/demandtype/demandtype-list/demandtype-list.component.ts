import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DemandType } from '../../models/DemandType';
import { DemandTypeService } from '../../service/DemandType.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-demandtype-list',
  templateUrl: './demandtype-list.component.html',
  styleUrls: ['./demandtype-list.component.sass']
})
export class DemandTypeListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: DemandType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<DemandType> = new MatTableDataSource();

  selection = new SelectionModel<DemandType>(true, []);
  
  constructor(private snackBar: MatSnackBar,private DemandTypeService: DemandTypeService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getDemandTypes();
  }
 
  getDemandTypes() {
    this.isLoading = true;
    this.DemandTypeService.getDemandTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getDemandTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDemandTypes();
  } 

  deleteItem(row) {
    const id = row.demandTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DemandTypeService.delete(id).subscribe(() => {
          this.getDemandTypes();
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
