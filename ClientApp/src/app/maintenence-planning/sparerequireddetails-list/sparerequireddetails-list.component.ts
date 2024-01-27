import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MaintenanceType } from '../models/MaintenanceType';
import { MaintenanceTypeService } from '../service/MaintenanceType.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
 

@Component({
  selector: 'app-sparerequireddetails',
  templateUrl: './sparerequireddetails-list.component.html',
  styleUrls: ['./sparerequireddetails-list.component.sass']
})
export class SpareRequiredDetailsListComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: MaintenanceType[] = [];
  isLoading = false;
  spareRequiredStatusList: any;
  
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";

  displayedColumns: string[] = [ 'ser', 'itemName','jobDescription','remarks'];
  dataSource: MatTableDataSource<MaintenanceType> = new MatTableDataSource();
  
  constructor(private snackBar: MatSnackBar, private MaintenanceTypeService: MaintenanceTypeService, private router: Router,  private route: ActivatedRoute,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('jobDescriptionEntryPmsId'); 
    this.getMaintenanceTypes(id);
  }
 
  getMaintenanceTypes(spareRequiredStatusId) {
    this.isLoading = true;
    this.MaintenanceTypeService.getSpareRequiredEntryStatus(spareRequiredStatusId).subscribe(response => {
      this.spareRequiredStatusList = response; 
      console.log(this.spareRequiredStatusList)
    })
  }
}
