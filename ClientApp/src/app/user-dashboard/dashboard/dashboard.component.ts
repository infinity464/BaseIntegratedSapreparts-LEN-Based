import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserDashboardService } from '../services/UserDashboard.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DashboardService } from 'src/app/admin/dashboard/service/Dashboard.service';
import { MonitoringService } from 'src/app/monitoring/service/monitoring.service';
import { Role } from 'src/app/core/models/role';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ItemDetailService } from '../../spares-management/service/itemDetail.service';
// import { AuthService } from 'src/app/core/service/auth.service';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexYAxis,
  ApexStroke,
  ApexLegend,
  ApexMarkers,
  ApexGrid,
  ApexFill,
  ApexTitleSubtitle,
  ApexNonAxisChartSeries,
  ApexResponsive,
} from 'ng-apexcharts';
import { MasterData } from 'src/assets/data/master-data';
import { environment } from 'src/environments/environment';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';

export type avgLecChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  colors: string[];
  yaxis: ApexYAxis;
  grid: ApexGrid;
  tooltip: ApexTooltip;
  legend: ApexLegend;
  fill: ApexFill;
  title: ApexTitleSubtitle;
};

export type pieChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  legend: ApexLegend;
  dataLabels: ApexDataLabels;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass'],
})
export class DashboardComponent implements OnInit {
  @ViewChild('chart') chart: ChartComponent;
  public avgLecChartOptions: Partial<avgLecChartOptions>;
  public pieChartOptions: Partial<pieChartOptions>;
  masterData = MasterData;
  ItemDetailForm: FormGroup;
  traineeId:any;
  isShown: boolean = false ;
  subjectLength:any;
  pno: any;
  name: any;
  position: any;
  name1: any;
  joiningDate: any;
  schoolName: any;
  schoolId: any;
  bulletinList:any;
  role:any;
  NoticeForInstructor:any;
  //fileUrl:any = environment.fileUrl;

  courseList:any;

  routineList:any;
  upcomingCoursesList:any;

  materialList:any;

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
  deptName:string = 'All';
  RemainProcurement:string = 'All';
  FlyingTimeByDeptName:string = 'All';
  FlyingByDeptName:string = 'All';
  StatusByDeptName:string = 'All';
  StatusList:any;
  CountStatus:any;
  //isShown: boolean = false ;
  equipmentCount:any;
  spareCount:any;
  toolscount:any;
  DemandsByParamsCount:any;
  procurementCount:any;
  sftcount:any;
  surveyCount:any;
  issuedItemTy:any;
  onGoingCorrectiveCount:any;
  scheduleJobCount:any;
  scheduleJobCountTotal:any;
  deptId:any;
  userRole = Role;
  branchId:any;
  baseSchoolNameList:SelectedModel[];
  unitId:any;
  progressCount:number;
  totalProgress:any;
  completedProgress:number;
  scheduledMaintananceList:any;
  equipmentState:any[];
  fileUrl:any = environment.fileUrl;
  userManual:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  displayedUpcommingColumns: string[] = ['ser', 'course','durationForm','subjectName'];
  displayedCourseColumns: string[] = ['ser','schoolName','course', 'subjectName'];
  displayedRoutineColumns: string[] = ['ser', 'date','schoolName','duration', 'course','subject', 'location'];
  displayedReadingMaterialColumns: string[] = ['ser','readingMaterialTitle','documentName','documentLink'];
  

  constructor(private fb: FormBuilder,private monitoringService: MonitoringService,private ItemDetailService: ItemDetailService,private dashboardService: DashboardService, private authService: AuthService, private datepipe: DatePipe, private route: ActivatedRoute,private userDashboardService: UserDashboardService) {}
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.traineeId, this.branchId)
    this.intitializeForm();
    this.getSelectedBaseSchoolName();
    //this.deptId =branchId;

    if(this.role != this.userRole.SuperAdmin && this.role != this.userRole.Admin){
      this.ItemDetailForm.get('departmentNameId').setValue(this.branchId);
      this.ItemDetailService.findBaseSchoolNameId(this.branchId).subscribe(res=>{

        this.unitId=res.thirdLevel;
        console.log("Unit Id");
        console.log(this.unitId);
        this.ItemDetailForm.get('unitId').setValue(this.unitId);
        console.log("UnitID");
        console.log(this.unitId);
        this.getInitialCounts();

      })
      this.getSelectedDepartmentName(this.branchId);
    }
    
    // if(this.role == this.userRole.Admin){

    //   console.log("admin block")
      
    //   this.ItemDetailForm.get('unitId').setValue(this.branchId);
    //   console.log(this.branchId)
    //   this.getSelectedDepartmentName(this.branchId);
    // }

    // this.chart1();
    // this.chart2();
    // console.log(this.traineeId+"ggggg")
    // this.getSpCurrentRoutineForStudentDashboard(this.traineeId);
    // this.instructorDashboardService.getSpInstructorInfoByTraineeId(this.traineeId).subscribe(res=>{
    //   if(res){
    //     this.courseList = res;
    //     let infoList=res;
    //     this.pno=infoList[0].pno,
    //     this.position=infoList[0].position,
    //     this.name=infoList[0].name,
    //     this.name1=infoList[0].name1,
    //     this.schoolName=infoList[0].schoolName,
    //     this.joiningDate=infoList[0].joiningDate, 
    //     this.schoolId=infoList[0].baseSchoolNameId, 
    //     this.getActiveBulletins(infoList[0].baseSchoolNameId)
    //     this.getNoticeBySchoolId(infoList[0].baseSchoolNameId)
    //     this.isShown=true;
    //     console.log(res)
    //   }else{
    //     this.isShown=false;
    //   }
      
    // });  
    
    // this.instructorDashboardService.getSpInstructorRoutineByTraineeId(this.traineeId).subscribe(res=>{
    //   this.routineList = res;
    //   console.log(res)
    // });

    // this.instructorDashboardService.getSpReadingMaterialByTraineeId(this.traineeId).subscribe(res=>{
    //   this.materialList = res;
    //   console.log(res)
    // });
    this.userDashboardService.getUserManualByRole(this.role).subscribe(response => {   
      console.log("user manual");
      console.log(response);
      this.userManual=response[0].doc;
      console.log(this.userManual);
    })

  }

  intitializeForm() {
    this.ItemDetailForm = this.fb.group({
      unitId:[],
      departmentNameId:[],
      itemCategoryId:[],
      equipmentNameId:[],
      itemDetailId:[],
      
    })
  }
  getInitialCounts(){
    console.log("unit id");
    console.log(this.unitId)
    // this.getEquipmentStateCount(this.unitId,this.branchId,0); //opl
    // this.getEquipmentStateCount(this.unitId,this.branchId,1); //non-opl
    this.getSpareToolAndEquipmentCount(this.unitId,this.branchId,1);//spare
    this.getSpareCount(this.unitId,this.branchId,2);
    this.getToolsCount(this.unitId,this.branchId,8);
    this.getDemandCountForDashBoard();
    this.getProcurementCountForDashBoard();
    this.getSftCountForDashBoard();
    this.getSurveyAmountPending();
    this.getIssuedItemTy();
    this.getOngoingCorrective();
    this.getScheduleJobCount();
    this.getScheduleJobCountTotal();
    this.getTotalProgressForDashBoard();
    this.getListForEquipmentState();
    //this.getListForEquipmentStateNonOpl();
  }

  getSelectedDepartmentName(fourthLevel){
    this.monitoringService.getSelectedSchoolName(fourthLevel).subscribe(res=>{
      this.selectedDepartmentNames=res
      console.log(res)
    }); 
  }

  getScheduleJobCountTotal(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;
    this.monitoringService.findScheduledMaintanenceJobCountTotal(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
      this.scheduleJobCountTotal=res.length;
      console.log(res)
    }); 
  }
  getSelectedBaseSchoolName(){
    this.isShown=true;
    this.monitoringService.getSelectedBaseSchool().subscribe(res=>{
      this.baseSchoolNameList=res;
    });
  }
  getScheduleJobCount(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;
    this.monitoringService.findScheduledMaintanenceJobCount(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.issueRegisterId == null ? 0 : findArr.issueRegisterId,findArr.scheduleMaintenenceTypeId == null ? 0 : findArr.scheduleMaintenenceTypeId,findArr.fiscalYearId == null ? 0 : findArr.fiscalYearId).subscribe(res=>{
      this.scheduleJobCount=res.length;
      console.log(res)
    }); 
  }

  getOngoingCorrective(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;
    this.monitoringService.findOngoingCorrective(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.onGoingCorrectiveCount=res.length
      console.log(res)
    }); 
  }

  getIssuedItemTy(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;
    this.monitoringService.findIssueRegister(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.issueTypeId == null ? 2 : 2).subscribe(res=>{
      this.issuedItemTy=res.length
      console.log("8888888");
      console.log(res)
    }); 
  }

  getSurveyAmountPending(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.getSurveyMonitoringList(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId, findArr.equipmentNameId == null ? 0 : findArr.equipmentNameId).subscribe(res=>{
      this.surveyCount=res.length;
    });  
  }


  getSftCountForDashBoard(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.findSft(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.demandId == null ? 0 : findArr.demandId).subscribe(res=>{
      this.sftcount=res.length
      console.log(res)
    }); 
  }

  getProcurementCountForDashBoard(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.findProcurements(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.procurementCount=res.length
      console.log(res)
    }); 
  }

  getTotalProgressForDashBoard(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.findScheduledMaintanences(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.scheduledMaintananceList=res;

      var count;
      var totalProgress=0;
      for (let i = 0; i < this.scheduledMaintananceList.length; i++) {
        this.totalProgress +=this.scheduledMaintananceList[i].progress
        console.log("Total Progress");
        if(this.scheduledMaintananceList[i].progress !=null)
        {
          console.log(this.scheduledMaintananceList[i].progress);
          count=this.scheduledMaintananceList[i].progress.split("%",1);
          var newCount= Number(count[0])
          totalProgress +=newCount;
        }
      }
       this.progressCount=this.scheduledMaintananceList.length*100;
       this.completedProgress=(totalProgress*100)/this.progressCount
      console.log(this.completedProgress)
    }); 
  }

  getDemandCountForDashBoard(){
    // var unitId = this.DashboardForm.value['unitId'];
    // var departmentNameId = this.DashboardForm.value['departmentNameId'];
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.findDemands(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId,findArr.itemCategoryId == null ? 0 : findArr.itemCategoryId,findArr.equipmentNameId== null ? 0 : findArr.equipmentNameId,findArr.itemDetailId== null ? 0 : findArr.itemDetailId).subscribe(res=>{
      this.DemandsByParamsCount=res.length
      console.log(res)
    }); 
  }
  getListForEquipmentState(){
    var findArr = this.ItemDetailForm.value;

    this.monitoringService.getListForEquipementNonOparatonal(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId).subscribe(res=>{
    this.equipmentState=res;
    this.equipmentOplCount= this.equipmentState.filter(x=>x.status ==0).length
    this.equipmentNonOplCount = this.equipmentState.filter(x=>x.status ==1).length
    }); 
    
  }
  // getListForEquipmentStateNonOpl(){
  //   var findArr = this.ItemDetailForm.value;

  //   this.monitoringService.getListForEquipementNonOparatonal(this.unitId == null ? 0 : this.unitId,this.branchId == null ? 0 : this.branchId).subscribe(res=>{
  //     this.equipmentNonOplCount=res.filter(x=>)
  //     this.equipmentNonOplCount=res.length
  //     console.log(res)
  //   }); 
    
  // }
  // getEquipmentStateCount(unitId,departmentId,operationStatus){
  //   this.dashboardService.getEquipmentStateCount(unitId,departmentId,operationStatus).subscribe(response => {   
  //     if(operationStatus == 0){
  //       this.equipmentOplCount=response.length;
  //       console.log("OPl count");
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
  onSubmit()
  {

  }
  // getToolsCount(unitId,departmentId,itemCategoryId){
  //   this.dashboardService.getToolsCount(unitId,departmentId,itemCategoryId).subscribe(response => {   
  //       this.toolscount=response.length;
  //   })
  // }





  // intitializeForm() {
  //   this.GetInstructorForm = this.fb.group({
  //     traineeId:[],
  //     traineeName:['']  
  //   })
  // }


  // onSubmit(){
  //   const id = this.GetInstructorForm.get('traineeId').value;  

    
    
  // }

  // getSpCurrentRoutineForStudentDashboard(id){
  //   this.instructorDashboardService.getSpCurrentRoutineForStudentDashboard(id).subscribe(res=>{
  //     this.upcomingCoursesList = res;
  //     console.log("dddd");
  //     console.log(res)
  //   });
  // }

  // getActiveBulletins(baseSchoolNameId){
  //   this.studentDashboardService.getActiveBulletinList(baseSchoolNameId).subscribe(res=>{
  //     this.bulletinList=res;  
  //     console.log(this.bulletinList);
  //   });
  // }

  // getNoticeBySchoolId(schoolId){
  //   let currentDateTime =this.datepipe.transform((new Date), 'MM/dd/yyyy');
  //   this.studentDashboardService.getNoticeBySchoolId(schoolId,currentDateTime).subscribe(response => {   
  //     this.NoticeForInstructor=response;
  //     console.log(response)
  //   })
  // }

  // private chart1() {
  //   this.avgLecChartOptions = {
  //     series: [
  //       {
  //         name: 'Avg. Lecture',
  //         data: [65, 72, 62, 73, 66, 74, 63, 67],
  //       },
  //     ],
  //     chart: {
  //       height: 350,
  //       type: 'line',
  //       foreColor: '#9aa0ac',
  //       dropShadow: {
  //         enabled: true,
  //         color: '#000',
  //         top: 18,
  //         left: 7,
  //         blur: 10,
  //         opacity: 0.2,
  //       },
  //       toolbar: {
  //         show: false,
  //       },
  //     },
  //     stroke: {
  //       curve: 'smooth',
  //     },
  //     xaxis: {
  //       categories: ['Jan', 'Feb', 'March', 'Apr', 'May', 'Jun', 'July', 'Aug'],
  //       title: {
  //         text: 'Weekday',
  //       },
  //     },
  //     yaxis: {},
  //     fill: {
  //       type: 'gradient',
  //       gradient: {
  //         shade: 'dark',
  //         gradientToColors: ['#35fdd8'],
  //         shadeIntensity: 1,
  //         type: 'horizontal',
  //         opacityFrom: 1,
  //         opacityTo: 1,
  //         stops: [0, 100, 100, 100],
  //       },
  //     },
  //     markers: {
  //       size: 4,
  //       colors: ['#FFA41B'],
  //       strokeColors: '#fff',
  //       strokeWidth: 2,
  //       hover: {
  //         size: 7,
  //       },
  //     },
  //     tooltip: {
  //       theme: 'dark',
  //       marker: {
  //         show: true,
  //       },
  //       x: {
  //         show: true,
  //       },
  //     },
  //   };
  // }
  // private chart2() {
  //   this.pieChartOptions = {
  //     series: [44, 55, 13, 43, 22],
  //     chart: {
  //       type: 'donut',
  //       width: 200,
  //     },
  //     legend: {
  //       show: false,
  //     },
  //     dataLabels: {
  //       enabled: false,
  //     },
  //     labels: ['Science', 'Mathes', 'Economics', 'History', 'Music'],
  //     responsive: [
  //       {
  //         breakpoint: 480,
  //         options: {},
  //       },
  //     ],
  //   };
  // }
}
