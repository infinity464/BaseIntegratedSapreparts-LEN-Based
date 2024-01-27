import {Opinion} from './Opinion';

export interface IOpinionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Opinion[];
}

export class OpinionPagination implements IOpinionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Opinion[] = [];


}
