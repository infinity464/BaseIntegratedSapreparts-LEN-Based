import { Component, OnInit, ViewChild,ElementRef  } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { PreventiveMaintenance } from '../../models/PreventiveMaintenance';
import { PreventiveMaintenanceService } from '../../service/PreventiveMaintenance.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import{MasterData} from 'src/assets/data/master-data'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

@Component({
  selector: 'app-preventivemaintenance',
  templateUrl: './preventivemaintenance-list.component.html',
  styleUrls: ['./preventivemaintenance-list.component.sass']
})
export class PreventiveMaintenanceListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: PreventiveMaintenance[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = ['ser','jobDescription','jic','byWhome','completedDate','actions'];
  dataSource: MatTableDataSource<PreventiveMaintenance> = new MatTableDataSource();


   selection = new SelectionModel<PreventiveMaintenance>(true, []);

  
  constructor(private snackBar: MatSnackBar,private PreventiveMaintenanceService: PreventiveMaintenanceService,private router: Router,private confirmService: ConfirmService) { }

  ngOnInit() {
    this.getPreventiveMaintenances();
  }
 
  getPreventiveMaintenances() {
    this.isLoading = true;
    this.PreventiveMaintenanceService.getPreventiveMaintenances(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
  
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getPreventiveMaintenances();
 
  }
  
  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getPreventiveMaintenances();
  } 

  deleteItem(row) {
    const id = row.preventiveMaintenanceId; 

    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {

      console.log(result);
      if (result) {
        this.PreventiveMaintenanceService.delete(id).subscribe(() => {
          this.getPreventiveMaintenances();
          this.snackBar.open('PreventiveMaintenance Information Deleted Successfully ', '', {
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
