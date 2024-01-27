export interface Demand {
 demandId: number,
 authorityId: number,
 itemDetailId: number,
 denoId: number,
 manufactureId:number;
 itemCategoryId:number;
 fiscalYearId: number,
 tradeId:number;
 itemTypeId: number,
 occasionOfDemandId: number,
 demandAuthorityId: number,
 sparesCategoryId:number,
 demandTypeId: number,
 demandDocId: number,
 demandNo:string;
 conditionOfItemId: number,
 departmentNameId: number,
 demandCompleteStatusId: number,
 demandQty: string,
 demandLetterNo: string,
 demandDate: Date,
 letterOuterNo: string,
 specDoc:string;
 refPrice: string,
 refPoNo: string,
 remarks: string,
 oldPrice: string,
 oldRefNo: string,
 manufactureAddress: string,
 status: number,
 menuPosition: number,
 isActive: boolean,
 partNo:string
}