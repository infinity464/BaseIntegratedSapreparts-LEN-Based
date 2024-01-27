import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {Deno} from '../../../../app/basic-setup/models/Deno';
import { DenoService } from 'src/app/basic-setup/service/Deno.service';
// import { Deno } from '../../models/Deno';
// import { DenoService } from '../../service/Deno.service';
// import
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import{MasterData} from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Inject } from '@angular/core';

@Component({
  selector: 'app-spare-stocklist',
  templateUrl: './spare-stocklist.component.html',
  styleUrls: ['./spare-stocklist.component.sass']
})
export class SpareStocklistComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Deno[] = [];
  isLoading = false;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];
  dataSource: MatTableDataSource<Deno> = new MatTableDataSource();
  //spareStockBySpRequest:any[];
  selection = new SelectionModel<Deno>(true, []);
  
  constructor(public dialogRef: MatDialogRef<SpareStocklistComponent>,
    @Inject(MAT_DIALOG_DATA) public spareStockBySpRequest: any,private snackBar: MatSnackBar,private DenoService: DenoService,private router: Router,private confirmService: ConfirmService) { }
    onNoClick(): void {
      this.dialogRef.close();
    }
  ngOnInit() {
    this.getDenos();
  }
 
  getDenos() {
    this.isLoading = true;
    this.DenoService.getDenos(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      
      this.dataSource.data = response.items; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getDenos();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDenos();
  } 

  deleteItem(row) {
    const id = row.denoId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DenoService.delete(id).subscribe(() => {
          this.getDenos();
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
