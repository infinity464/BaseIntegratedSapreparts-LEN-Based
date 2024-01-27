import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { StatusOfJob } from '../../models/StatusOfJob';
import { StatusOfJobService } from '../../service/StatusOfJob.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-statusofjob-list',
  templateUrl: './statusofjob-list.component.html',
  styleUrls: ['./statusofjob-list.component.sass']
})
export class StatusOfJobListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: StatusOfJob[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<StatusOfJob> = new MatTableDataSource();

  selection = new SelectionModel<StatusOfJob>(true, []);
  
  constructor(private snackBar: MatSnackBar,private StatusOfJobService: StatusOfJobService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getStatusOfJobs();
  }
 
  getStatusOfJobs() {
    this.isLoading = true;
    this.StatusOfJobService.getStatusOfJobs(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getStatusOfJobs();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getStatusOfJobs();
  } 

  deleteItem(row) {
    const id = row.statusOfJobId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.StatusOfJobService.delete(id).subscribe(() => {
          this.getStatusOfJobs();
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
