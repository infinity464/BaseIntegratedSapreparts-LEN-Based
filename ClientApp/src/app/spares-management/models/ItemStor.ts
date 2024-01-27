export interface ItemStor {
    itemStorId: number;
    acceptanceId: number;
    procurementId: number;
    demandId:number;
    denoId: number;
    departmentNameId: number;
    equipmentNameId:number;
    itemCategoryId:number;
    sparesCategoryId:number;
    storeQty:number;
    plrNo:string;
    countryOfOrigin:string;
    manufacture:string;
    dateOfexpiry:string;
    dateOfExpiry:Date;
    conditionOfItemId:number;
    lifeLimitItemId:number;
    toolsLocationId:number;
    serviceLifeTypeId:number;
    endLifeTypeId: number;
    acctStoreId: number;
    overhaulingTypeId:number;
    retirementTypeId: number;
    itemDetailId:number;
    itemSerNo:string;
    icmNo:string;
    shelfLife:string;
    endShalfLife:string;
    warrantyStartDate:Date;
    warrantyEndDate:Date;
    itemReceivedDate:Date;
    totalReceivedQty:number;
    availableQty:number;
    issuedQty:number;
    demandQty:string;
    demandDate:Date;
    letterOuterNo:string;
    refPoNo:string;
    tenderNumber:string;
    dateOfTenderFloat:Date;
    tenderopeningDate:Date;
    tenderPublishDate:Date;
    tenderNotice:string;
    location:string;
    serviceLife:string;
    endLifeTime:string;
    accessories:string;
    stockRegisterPageNo:string;
    retirmentLife:string;
    remarks:string;
    arcDoc:string;
    cofcDoc:string;
    otherDoc:string;
    oemDoc:string;
    status:string;
    isActive: boolean;
    issueQty:string;
    isRefundable: boolean;
    permanentQty:number,
    tyQty:number,
    surveyQty :number,
    repairQty:number,
    shipFittedQty:number
}