import {JobCategory} from './JobCategory';

export interface IJobCategoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobCategory[];
}

export class JobCategoryPagination implements IJobCategoryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobCategory[] = [];


}
