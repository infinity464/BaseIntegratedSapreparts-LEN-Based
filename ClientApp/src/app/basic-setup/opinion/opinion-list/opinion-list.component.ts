import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Opinion } from '../../models/Opinion';
import { OpinionService } from '../../service/Opinion.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-opinion-list',
  templateUrl: './opinion-list.component.html',
  styleUrls: ['./opinion-list.component.sass']
})
export class OpinionListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Opinion[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<Opinion> = new MatTableDataSource();

  selection = new SelectionModel<Opinion>(true, []);
  
  constructor(private snackBar: MatSnackBar,private OpinionService: OpinionService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getOpinions();
  }
 
  getOpinions() {
    this.isLoading = true;
    this.OpinionService.getOpinions(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getOpinions();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getOpinions();
  } 

  deleteItem(row) {
    const id = row.opinionId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.OpinionService.delete(id).subscribe(() => {
          this.getOpinions();
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
