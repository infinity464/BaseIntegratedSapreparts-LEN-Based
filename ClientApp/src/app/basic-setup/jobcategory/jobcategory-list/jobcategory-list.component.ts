import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { JobCategory } from '../../models/JobCategory';
import { JobCategoryService } from '../../service/JobCategory.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-jobcategory-list',
  templateUrl: './jobcategory-list.component.html',
  styleUrls: ['./jobcategory-list.component.sass']
})
export class JobCategoryListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: JobCategory[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<JobCategory> = new MatTableDataSource();

  selection = new SelectionModel<JobCategory>(true, []);
  
  constructor(private snackBar: MatSnackBar,private JobCategoryService: JobCategoryService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getJobCategorys();
  }
 
  getJobCategorys() {
    this.isLoading = true;
    this.JobCategoryService.getJobCategorys(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getJobCategorys();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getJobCategorys();
  } 

  deleteItem(row) {
    const id = row.jobCategoryId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.JobCategoryService.delete(id).subscribe(() => {
          this.getJobCategorys();
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
