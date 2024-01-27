import {DemandType} from './DemandType';

export interface IDemandTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DemandType[];
}

export class DemandTypePagination implements IDemandTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: DemandType[] = [];


}
