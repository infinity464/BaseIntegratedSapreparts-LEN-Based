import {PreventiveMaintenance} from './PreventiveMaintenance';

export interface IPreventiveMaintenancePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PreventiveMaintenance[];
}

export class PreventiveMaintenancePagination implements IPreventiveMaintenancePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: PreventiveMaintenance[] = [];


}
