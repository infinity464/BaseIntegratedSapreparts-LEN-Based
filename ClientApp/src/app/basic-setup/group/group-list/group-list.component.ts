import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Group } from '../../models/Group';
import { GroupService } from '../../service/Group.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.sass']
})
export class GroupListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Group[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'groupName',  'actions'];
  dataSource: MatTableDataSource<Group> = new MatTableDataSource();

  selection = new SelectionModel<Group>(true, []);
  
  constructor(private snackBar: MatSnackBar,private GroupService: GroupService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getGroups();
  }
 
  getGroups() {
    this.isLoading = true;
    this.GroupService.getGroups(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getGroups();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getGroups();
  } 

  deleteItem(row) {
    const id = row.groupId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.GroupService.delete(id).subscribe(() => {
          this.getGroups();
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
