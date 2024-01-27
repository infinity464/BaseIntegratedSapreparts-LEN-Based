import {JobCategoryMaintenence} from './JobCategoryMaintenence';

export interface IJobCategoryMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobCategoryMaintenence[];
}

export class JobCategoryMaintenencePagination implements IJobCategoryMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobCategoryMaintenence[] = [];


}
