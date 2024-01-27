import {ItemInspection} from './ItemInspection';

export interface IItemInspectionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ItemInspection[];
}

export class ItemInspectionPagination implements IItemInspectionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ItemInspection[] = [];


}
