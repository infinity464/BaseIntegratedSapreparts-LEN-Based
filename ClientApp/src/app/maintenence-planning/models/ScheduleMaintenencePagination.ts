import {ScheduleMaintenence} from './ScheduleMaintenence';

export interface IScheduleMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ScheduleMaintenence[];
}

export class ScheduleMaintenencePagination implements IScheduleMaintenencePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ScheduleMaintenence[] = [];
}
