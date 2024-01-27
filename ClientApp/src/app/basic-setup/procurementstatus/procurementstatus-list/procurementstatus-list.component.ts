import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ProcurementStatus } from '../../models/ProcurementStatus';
import { ProcurementStatusService } from '../../service/ProcurementStatus.service';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
 

@Component({
  selector: 'app-procurementstatus',
  templateUrl: './procurementstatus-list.component.html',
  styleUrls: ['./procurementstatus-list.component.sass']
})
export class ProcurementStatusListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ProcurementStatus[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name', 'remarks', 'actions'];
  dataSource: MatTableDataSource<ProcurementStatus> = new MatTableDataSource();
  
  constructor(private snackBar: MatSnackBar, private ProcurementStatusService: ProcurementStatusService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getProcurementStatuss();
  }
 
  getProcurementStatuss() {
    this.isLoading = true;
    this.ProcurementStatusService.getProcurementStatuss(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getProcurementStatuss();
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProcurementStatuss();
  }

  deleteItem(row) {
    const id = row.procurementStatusId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ProcurementStatusService.delete(id).subscribe(() => {
          this.getProcurementStatuss();
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
