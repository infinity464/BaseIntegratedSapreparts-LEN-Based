import {SpareRequiredEntry} from './SpareRequiredEntry';

export interface ISpareRequiredEntryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SpareRequiredEntry[];
}

export class SpareRequiredEntryPagination implements ISpareRequiredEntryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SpareRequiredEntry[] = [];


}
