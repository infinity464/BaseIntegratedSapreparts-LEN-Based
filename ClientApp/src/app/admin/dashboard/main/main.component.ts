import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from '../service/Dashboard.service';
import { MonitoringService } from 'src/app/monitoring/service/monitoring.service';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexYAxis,
  ApexPlotOptions,
  ApexStroke,
  ApexLegend,
  ApexFill,
} from 'ng-apexcharts';
import { MasterData } from 'src/assets/data/master-data';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export type areaChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
  legend: ApexLegend;
  colors: string[];
};

export type barChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  colors: string[];
};




@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  @ViewChild('chart') chart: ChartComponent;
  public areaChartOptions: Partial<areaChartOptions>;
  public barChartOptions: Partial<barChartOptions>;
  masterData = MasterData;
  //variables 

  pendingDemandCount:any;
  trainingCrewCount:any;
  pendingProcurementCount:any;
  equipmentOplCount:any;
  equipmentNonOplCount:any;
  pendingAcceptancesCount:any;
  availableQtyList:any;
  DashboardForm: FormGroup;
  CountavailableQty:any;
  FlyingTimeByAricraftList:any;
  CountFlyingTimeByAricraft:any;
  AricraftFlyingList:any;
  CountAricraftFlying:any;
  RemainProcurementQtyList:any;
  CountRemainProcurement:any;
  unitList:any;
  selectedDepartmentNames:any;
  selectedFiscalYear:any;
  deptName:string = 'All';
  RemainProcurement:string = 'All';
  FlyingTimeByDeptName:string = 'All';
  FlyingByDeptName:string = 'All';
  StatusByDeptName:string = 'All';
  StatusList:any;
  CountStatus:any;
  isShown: boolean = false ;
  equipmentCount:any;
  spareCount:any;
  toolscount:any;
  DemandsByParamsCount:any;
  procurementCount:any;
  sftcount:any;
  surveyCount:any;
  issuedItemTy:any;
  onGoingCorrectiveCount:any;
  scheduleJobCount:any=0;
  scheduleJobCountTotal:any=0;
  scheduledMaintananceList:any;
  progressCount:number;
  totalProgress:any;
  completedProgress:number=0;
  equipmentState:any[];
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }

  procurementColumns: string[] = ['sl','tenderNumber','dateOfDelivery', 'dateOfTenderFloat', 'cstTec', 'qty', 'sftQty'];
  displayedAvailableColumns: string[] = [ 'ser', 'sparesCategory','deptName','partNo', 'nameOfItem', 'minimumStock','availableQty'];
  displayedFlyingColumns: string[] = [ 'ser', 'airCraftName','date', 'crew', 'callSign','mon','startUp','dur','endurance','fuel','opaOff','remarks'];
  //displayedAircraftInfoColumns: string[] = [ 'ser', 'deptName','airCraftName', 'flyTime'];

  constructor(private datepipe: DatePipe,private monitoringService: MonitoringService,private fb: FormBuilder,private dashboardService: DashboardService) {}

  ngOnInit() {
    this.intitializeForm();
    this.getPendingDemand();
    //this.getRemainProcurementQty();
    this.getPendingProcurements();
    this.getPendingAcceptances();
    this.getTrainingCrew();
    this.getSelectedUnits();
    //this.getFlyingTimeByAricraft();
    this.getInitialCounts();
    this.getSelectedFiscalYear();
  }

  intitializeForm() {
    this.DashboardForm = this.fb.group({
      unitId:[],
      departmentNameId:[],
      fiscalYearId:[],
      // equipmentNameId:[],
      // itemDetailId:[],
      
    })
  }

  getSelectedUnits(){
    this.isShown=true;
    this.dashboardService.getSelectedBaseSchool().subscribe(res=>{
      this.unitList=res;
    });
  }

  getSelectedDepartmentName(){
    var fourthLevel = this.DashboardForm.value['unitId'];
    console.log(fourthLevel)
    this.dashboardService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }
  getTotalProgressForDashBoard(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;
    console.log(findArr)

    this.monitoringService.findScheduledMaintanences(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
      this.scheduledMaintananceList=res;
      console.log(this.scheduledMaintananceList)
      var count;
      var totalProgress=0;
      for (let i = 0; i < this.scheduledMaintananceList.length; i++) {
        this.totalProgress +=this.scheduledMaintananceList[i].progress
        // console.log("Total Progress");
        if(this.scheduledMaintananceList[i].progress !=null)
        {
          // console.log(this.scheduledMaintananceList[i].progress);
          count=this.scheduledMaintananceList[i].progress.split("%",1);
          var newCount= Number(count[0])
          totalProgress +=newCount;
        }
      }
       this.progressCount=this.scheduledMaintananceList.length*100;
       this.completedProgress=(totalProgress*100)/this.progressCount;
      console.log(this.completedProgress);
    }); 
  }

  getDataByYear(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var fiscalYearId = this.DashboardForm.value['fiscalYearId'];
    var findArr = this.DashboardForm.value;

    console.log(findArr);
    this.getScheduleJobCountTotal();
    this.getScheduleJobCount();
    this.getTotalProgressForDashBoard();

  }
  getSelectedFiscalYear(){
    this.dashboardService.getSelectedFiscalYear().subscribe(res => {
      this.selectedFiscalYear = res;
    });
  }
  getInitialCounts(){
    // this.getEquipmentStateCount(0,0,0); //opl
    // this.getEquipmentStateCount(0,0,1); //non-opl
    this.getListForEquipmentState();
    this.getSpareToolAndEquipmentCount(0,0,1);//spare
    this.getSpareCount(0,0,2);
    this.getToolsCount(0,0,8);
    this.getDemandCountForDashBoard();
    this.getProcurementCountForDashBoard();
    this.getSftCountForDashBoard();
    this.getSurveyAmountPending();
    this.getIssuedItemTy();
    this.getOngoingCorrective();
    
  }
  getPendingDemand(){
    this.dashboardService.getPendingDemands(0).subscribe(response => {   
      this.pendingDemandCount=response.length;
    })
  }

  getPendingProcurements(){
    this.dashboardService.getPendingProcurements(0).subscribe(response => {   
      this.pendingProcurementCount=response.length;
    })
  }

  getListForEquipmentState(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;
  
    this.monitoringService.getListForEquipementNonOparatonal(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId).subscribe(res=>{
    this.equipmentState=res;
    this.equipmentOplCount= this.equipmentState.filter(x=>x.status ==0).length
    this.equipmentNonOplCount = this.equipmentState.filter(x=>x.status ==1).length
    }); 
    
  }

  // getEquipmentStateCount(unitId,departmentId,operationStatus){
  //   this.dashboardService.getEquipmentStateCount(unitId,departmentId,operationStatus).subscribe(response => {   
  //     if(operationStatus == 0){
  //       this.equipmentOplCount=response.length;
  //       console.log(this.equipmentOplCount)
  //     }else{
  //       this.equipmentNonOplCount=response.length;
  //       console.log(this.equipmentNonOplCount)
  //     }
  //   })
  // }
  getSpareToolAndEquipmentCount(unitId,departmentId,itemCategoryId){
    this.dashboardService.getSpareToolAndEquipmentCount(unitId,departmentId,itemCategoryId).subscribe(response => {   
        this.equipmentCount=response.length;
    })
  }

  
  getSpareCount(unitId,departmentId,itemCategoryId){
    this.dashboardService.getSpareCount(unitId,departmentId,itemCategoryId).subscribe(response => {   
        this.spareCount=response.length;
    })
  }
  getToolsCount(unitId,departmentId,itemCategoryId){
    this.dashboardService.getToolsCount(unitId,departmentId,itemCategoryId).subscribe(response => {   
        this.toolscount=response.length;
    })
  }

 

  onSubmit(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;
    console.log(findArr) 
    // this.getEquipmentStateCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId, 0); //opl
    // this.getEquipmentStateCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId, 1); //non-opl
    this.getSpareToolAndEquipmentCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId, 1); //equipment
    this.getSpareCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId, 2); //tools
    this.getToolsCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId, 8);
   this.getListForEquipmentState();
   this.getDemandCountForDashBoard();
   this.getProcurementCountForDashBoard();
   this.getSftCountForDashBoard();
   this.getSurveyAmountPending();
   this.getIssuedItemTy();
   this.getOngoingCorrective();
  //  this.getScheduleJobCount();
  //  this.getScheduleJobCountTotal();
  //  this.getTotalProgressForDashBoard();
  }

  getDemandCountForDashBoard(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;

    this.monitoringService.findDemands(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.DemandsByParamsCount=res.length
      console.log(res)
    }); 
  }

  getProcurementCountForDashBoard(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;

    this.monitoringService.findProcurements(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.procurementCount=res.length
      console.log(res)
    }); 
  }
  getSftCountForDashBoard(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;

    this.monitoringService.findSft(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.demandId == null ? 0 : findArr.demandId).subscribe(res=>{
      this.sftcount=res.length
      console.log(res)
    }); 
  }

  getSurveyAmountPending(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;

    this.monitoringService.getSurveyMonitoringList(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId, findArr.equipmentNameId == null ? 0 : findArr.equipmentNameId).subscribe(res=>{
      this.surveyCount=res.length;
    });  
  }

  getIssuedItemTy(){
    var unitId = this.DashboardForm.value['unitId'];
    var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.DashboardForm.value;
    this.monitoringService.findIssueRegister(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.issueTypeId == null ? 2 : 2).subscribe(res=>{
      this.issuedItemTy=res.length
      console.log("8888888");
      console.log(res)
    }); 
  }
getOngoingCorrective(){
  var unitId = this.DashboardForm.value['unitId'];
  var departmentNameId = this.DashboardForm.value['departmentNameId'];
  var findArr = this.DashboardForm.value;
  this.monitoringService.findOngoingCorrective(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
    this.onGoingCorrectiveCount=res.length
    console.log(res)
  }); 
}
getScheduleJobCount(){
  var unitId = this.DashboardForm.value['unitId'];
  var departmentNameId = this.DashboardForm.value['departmentNameId'];
  var findArr = this.DashboardForm.value;
  this.monitoringService.findScheduledMaintanenceJobCount(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
    this.scheduleJobCount=res.length;
    console.log(res)
  }); 
}

getScheduleJobCountTotal(){
  var unitId = this.DashboardForm.value['unitId'];
  var departmentNameId = this.DashboardForm.value['departmentNameId'];
  var findArr = this.DashboardForm.value;
  this.monitoringService.findScheduledMaintanenceJobCountTotal(findArr.unitId == null ? 0 : findArr.unitId,findArr.departmentNameId == null ? 0 : findArr.departmentNameId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
    this.scheduleJobCountTotal=res.length;
    console.log(res)
  }); 
}

  getPendingAcceptances(){
    this.dashboardService.getPendingAcceptances(0).subscribe(response => {   
      this.pendingAcceptancesCount=response.length;
    })
  }

  getAvailableQty(){
    this.dashboardService.getAvailableQty(0).subscribe(response => {   
      this.availableQtyList=response;
      this.CountavailableQty = response.length;
    })
  }
  
  inActiveItem(id, name){  
    this.deptName = name;
    this.dashboardService.getAvailableQty(id).subscribe(response => {
      this.availableQtyList = response; 
      this.CountavailableQty = response.length;
    }) 
  }
  getDemandSpGetCompleteStatus(){
    this.dashboardService.getDemandSpGetCompleteStatus(0).subscribe(response => { 
      this.StatusList=response;
      this.CountStatus = response.length;
      console.log(this.StatusList)
    })
  }
  StatusByDept(id, name){  
    this.StatusByDeptName = name;
    this.dashboardService.getDemandSpGetCompleteStatus(id).subscribe(response => {   
      this.StatusList=response;
      this.CountStatus = response.length;
    }) 
  }
  getTrainingCrew(){
    this.dashboardService.getTrainingCrew(0).subscribe(response => {   
      this.trainingCrewCount=response.length;
      console.log(this.trainingCrewCount)
    })
  }
}
