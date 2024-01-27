import {CorrectiveMaintenence} from './CorrectiveMaintenence';

export interface ICorrectiveMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CorrectiveMaintenence[];
}

export class CorrectiveMaintenencePagination implements ICorrectiveMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: CorrectiveMaintenence[] = [];


}
