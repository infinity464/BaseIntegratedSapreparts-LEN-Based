import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Progres } from '../../models/Progres';
import { ProgresService } from '../../service/Progres.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-progres-list',
  templateUrl: './progres-list.component.html',
  styleUrls: ['./progres-list.component.sass']
})
export class ProgresListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Progres[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'name',  'actions'];
  dataSource: MatTableDataSource<Progres> = new MatTableDataSource();

  selection = new SelectionModel<Progres>(true, []);
  
  constructor(private snackBar: MatSnackBar,private ProgresService: ProgresService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getProgress();
  }
 
  getProgress() {
    this.isLoading = true;
    this.ProgresService.getProgress(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getProgress();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getProgress();
  } 

  deleteItem(row) {
    const id = row.progresId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.ProgresService.delete(id).subscribe(() => {
          this.getProgress();
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
