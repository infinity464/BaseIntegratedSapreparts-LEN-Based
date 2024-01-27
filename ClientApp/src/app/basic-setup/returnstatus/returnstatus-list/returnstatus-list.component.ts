import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ReturnStatus } from '../../models/ReturnStatus';
import { ReturnStatusService } from '../../service/ReturnStatus.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-returnstatus-list',
  templateUrl: './returnstatus-list.component.html',
  styleUrls: ['./returnstatus-list.component.sass']
})
export class ReturnStatusListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ReturnStatus[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<ReturnStatus> = new MatTableDataSource();

  selection = new SelectionModel<ReturnStatus>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ReturnStatusService: ReturnStatusService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getReturnStatuses();
  }
 
  getReturnStatuses() {
    this.isLoading = true;
    this.ReturnStatusService.getReturnStatuses(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getReturnStatuses();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReturnStatuses();
  } 

  deleteItem(row) {
    const id = row.returnStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ReturnStatusService.delete(id).subscribe(() => {
          this.getReturnStatuses();
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
