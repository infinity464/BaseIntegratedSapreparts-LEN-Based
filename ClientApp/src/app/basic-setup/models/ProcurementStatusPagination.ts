import {ProcurementStatus} from './ProcurementStatus';

export interface IProcurementStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementStatus[];
}

export class ProcurementStatusPagination implements IProcurementStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ProcurementStatus[] = [];


}
