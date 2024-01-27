import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ScheduleMaintenenceType } from '../../models/ScheduleMaintenenceType';
import { ScheduleMaintenenceTypeService } from '../../service/ScheduleMaintenenceType.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-schedulemaintenencetype-list',
  templateUrl: './schedulemaintenencetype-list.component.html',
  styleUrls: ['./schedulemaintenencetype-list.component.sass']
})
export class ScheduleMaintenenceTypeListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: ScheduleMaintenenceType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name', 'actions'];
  dataSource: MatTableDataSource<ScheduleMaintenenceType> = new MatTableDataSource();

  selection = new SelectionModel<ScheduleMaintenenceType>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ScheduleMaintenenceTypeService: ScheduleMaintenenceTypeService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getScheduleMaintenenceTypes();
  }
 
  getScheduleMaintenenceTypes() {
    this.isLoading = true;
    this.ScheduleMaintenenceTypeService.getScheduleMaintenenceTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getScheduleMaintenenceTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getScheduleMaintenenceTypes();
  } 

  deleteItem(row) {
    const id = row.scheduleMaintenenceTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ScheduleMaintenenceTypeService.delete(id).subscribe(() => {
          this.getScheduleMaintenenceTypes();
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
