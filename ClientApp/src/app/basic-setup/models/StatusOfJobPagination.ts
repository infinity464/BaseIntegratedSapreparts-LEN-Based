import {StatusOfJob} from './StatusOfJob';

export interface IStatusOfJobPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: StatusOfJob[];
}

export class StatusOfJobPagination implements IStatusOfJobPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: StatusOfJob[] = [];


}
