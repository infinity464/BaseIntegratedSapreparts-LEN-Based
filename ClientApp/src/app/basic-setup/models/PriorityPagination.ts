import {Priority} from './Priority';

export interface IPriorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Priority[];
}

export class PriorityPagination implements IPriorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Priority[] = [];


}
