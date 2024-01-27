import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ByWhom } from '../../models/ByWhom';
import { ByWhomService } from '../../service/ByWhome.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-bywhome-list',
  templateUrl: './bywhome-list.component.html',
  styleUrls: ['./bywhome-list.component.sass']
})
export class ByWhomListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ByWhom[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<ByWhom> = new MatTableDataSource();

  selection = new SelectionModel<ByWhom>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ByWhomService: ByWhomService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getByWhomes();
  }
 
  getByWhomes() {
    this.isLoading = true;
    this.ByWhomService.getByWhoms(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getByWhomes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getByWhomes();
  } 
  deleteItem(row) {
    const id = row.byWhomId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ByWhomService.delete(id).subscribe(() => {
          this.getByWhomes();
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
  // deleteItem(row) {
  //   const id = row.byWhomId; 
  //   this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
  //     console.log(result);
  //     if (result) {
  //       this.ByWhomService.delete(id).subscribe(() => {
  //         this.getByWhomes();
  //         this.snackBar.open('Information Deleted Successfully ', '', {
  //           duration: 2000,
  //           verticalPosition: 'bottom',
  //           horizontalPosition: 'right',
  //           panelClass: 'snackbar-danger'
  //         });
  //       })
  //     }
  //   })    
  // }
}
