import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MaintenanceAuthority } from '../../models/MaintenanceAuthority';
import { MaintenanceAuthorityService } from '../../service/MaintenanceAuthority.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-maintenanceauthority-list',
  templateUrl: './maintenanceauthority-list.component.html',
  styleUrls: ['./maintenanceauthority-list.component.sass']
})
export class MaintenanceAuthorityListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: MaintenanceAuthority[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<MaintenanceAuthority> = new MatTableDataSource();

  selection = new SelectionModel<MaintenanceAuthority>(true, []);
  
  constructor(private snackBar: MatSnackBar,private MaintenanceAuthorityService: MaintenanceAuthorityService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getMaintenanceAuthoritys();
  }
 
  getMaintenanceAuthoritys() {
    this.isLoading = true;
    this.MaintenanceAuthorityService.getMaintenanceAuthoritys(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getMaintenanceAuthoritys();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getMaintenanceAuthoritys();
  } 

  deleteItem(row) {
    const id = row.maintenanceAuthorityId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.MaintenanceAuthorityService.delete(id).subscribe(() => {
          this.getMaintenanceAuthoritys();
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
