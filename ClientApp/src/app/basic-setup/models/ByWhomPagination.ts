import {ByWhom} from './ByWhom';

export interface IByWhomPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ByWhom[];
}

export class ByWhomPagination implements IByWhomPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ByWhom[] = [];


}
