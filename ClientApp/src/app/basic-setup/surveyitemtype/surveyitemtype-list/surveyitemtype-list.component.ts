import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SurveyItemType } from '../../models/SurveyItemType';
import { SurveyItemTypeService } from '../../service/SurveyItemType.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-surveyitemtype-list',
  templateUrl: './surveyitemtype-list.component.html',
  styleUrls: ['./surveyitemtype-list.component.sass']
})
export class SurveyItemTypeListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: SurveyItemType[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'surveyItemTypeName',  'actions'];
  dataSource: MatTableDataSource<SurveyItemType> = new MatTableDataSource();

  selection = new SelectionModel<SurveyItemType>(true, []);
  
  constructor(private snackBar: MatSnackBar,private SurveyItemTypeService: SurveyItemTypeService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getSurveyItemTypes();
  }
 
  getSurveyItemTypes() {
    this.isLoading = true;
    this.SurveyItemTypeService.getSurveyItemTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getSurveyItemTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getSurveyItemTypes();
  } 

  deleteItem(row) {
    const id = row.surveyItemTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.SurveyItemTypeService.delete(id).subscribe(() => {
          this.getSurveyItemTypes();
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
