import {Progres} from './Progres';

export interface IProgresPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Progres[];
}

export class ProgresPagination implements IProgresPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Progres[] = [];


}
