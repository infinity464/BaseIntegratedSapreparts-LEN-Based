import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { JobDescription } from '../../models/JobDescription';
import { JobDescriptionService } from '../../service/JobDescription.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-jobdescription-list',
  templateUrl: './jobdescription-list.component.html',
  styleUrls: ['./jobdescription-list.component.sass']
})
export class JobDescriptionListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: JobDescription[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<JobDescription> = new MatTableDataSource();

  selection = new SelectionModel<JobDescription>(true, []);
  
  constructor(private snackBar: MatSnackBar,private JobDescriptionService: JobDescriptionService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getJobDescriptions();
  }
 
  getJobDescriptions() {
    this.isLoading = true;
    this.JobDescriptionService.getJobDescriptions(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getJobDescriptions();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getJobDescriptions();
  } 

  deleteItem(row) {
    const id = row.jobDescriptionId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.JobDescriptionService.delete(id).subscribe(() => {
          this.getJobDescriptions();
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
