import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SurveyAuthority } from '../../models/SurveyAuthority';
import { SurveyAuthorityService } from '../../service/SurveyAuthority.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-surveyauthority-list',
  templateUrl: './surveyauthority-list.component.html',
  styleUrls: ['./surveyauthority-list.component.sass']
})
export class SurveyAuthorityListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: SurveyAuthority[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'surveyAuthorityName',  'actions'];
  dataSource: MatTableDataSource<SurveyAuthority> = new MatTableDataSource();

  selection = new SelectionModel<SurveyAuthority>(true, []);
  
  constructor(private snackBar: MatSnackBar,private SurveyAuthorityService: SurveyAuthorityService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getSurveyAuthorities();
  }
 
  getSurveyAuthorities() {
    this.isLoading = true;
    this.SurveyAuthorityService.getSurveyAuthorities(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getSurveyAuthorities();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getSurveyAuthorities();
  } 

  deleteItem(row) {
    const id = row.surveyAuthorityId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SurveyAuthorityService.delete(id).subscribe(() => {
          this.getSurveyAuthorities();
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
