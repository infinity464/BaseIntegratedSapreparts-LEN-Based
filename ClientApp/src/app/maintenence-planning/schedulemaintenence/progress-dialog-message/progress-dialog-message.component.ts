import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Inject } from '@angular/core';

@Component({
  selector: 'app-progress-dialog-message',
  templateUrl: './progress-dialog-message.component.html',
  styleUrls: ['./progress-dialog-message.component.sass']
})
export class ProgressDialogMessageComponent implements OnInit {
  isLoading = false;
  searchText="";

  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];
  
  constructor(public dialogRef: MatDialogRef<ProgressDialogMessageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,) { }
    onNoClick(): void {
      this.dialogRef.close();
    }
  ngOnInit() {

  }
}
