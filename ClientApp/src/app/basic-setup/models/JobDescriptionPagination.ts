import {JobDescription} from './JobDescription';

export interface IJobDescriptionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescription[];
}

export class JobDescriptionPagination implements IJobDescriptionPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescription[] = [];


}
