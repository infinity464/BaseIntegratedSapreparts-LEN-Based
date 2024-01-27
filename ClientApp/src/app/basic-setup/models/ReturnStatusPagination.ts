import {ReturnStatus} from './ReturnStatus';

export interface IReturnStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReturnStatus[];
}

export class ReturnStatusPagination implements IReturnStatusPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ReturnStatus[] = [];


}
