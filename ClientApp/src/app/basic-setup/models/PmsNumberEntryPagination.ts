import {PmsNumberEntry} from './PmsNumberEntry';

export interface IPmsNumberEntryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PmsNumberEntry[];
}

export class PmsNumberEntryPagination implements IPmsNumberEntryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PmsNumberEntry[] = [];


}
