import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ItemInspection } from '../../models/ItemInspection';
import { ItemInspectionService } from '../../service/ItemInspection.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-iteminspection-list',
  templateUrl: './iteminspection-list.component.html',
  styleUrls: ['./iteminspection-list.component.sass']
})
export class ItemInspectionListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ItemInspection[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<ItemInspection> = new MatTableDataSource();

  selection = new SelectionModel<ItemInspection>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ItemInspectionService: ItemInspectionService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getItemInspections();
  }
 
  getItemInspections() {
    this.isLoading = true;
    this.ItemInspectionService.getItemInspections(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getItemInspections();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getItemInspections();
  } 

  deleteItem(row) {
    const id = row.itemInspectionId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ItemInspectionService.delete(id).subscribe(() => {
          this.getItemInspections();
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
