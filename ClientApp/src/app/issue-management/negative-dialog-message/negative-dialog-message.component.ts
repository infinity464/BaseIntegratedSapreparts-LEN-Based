import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {Inject } from '@angular/core';

@Component({
  selector: 'app-negative-dialog-message',
  templateUrl: './negative-dialog-message.component.html',
  styleUrls: ['./negative-dialog-message.component.sass']
})
export class NegativeDialogMessageComponent implements OnInit {
  isLoading = false;
  searchText="";

  displayedColumns: string[] = ['sl', 'nameOfItem','availableQty'];
  
  constructor(public dialogRef: MatDialogRef<NegativeDialogMessageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,) { }
    onNoClick(): void {
      this.dialogRef.close();
    }
  ngOnInit() {

  }
}
