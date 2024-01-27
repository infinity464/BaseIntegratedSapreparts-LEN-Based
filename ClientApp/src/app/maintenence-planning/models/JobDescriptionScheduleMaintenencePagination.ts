import {JobDescriptionScheduleMaintenence} from './JobDescriptionScheduleMaintenence';

export interface IJobDescriptionScheduleMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescriptionScheduleMaintenence[];
}

export class JobDescriptionScheduleMaintenencePagination implements IJobDescriptionScheduleMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescriptionScheduleMaintenence[] = [];


}
