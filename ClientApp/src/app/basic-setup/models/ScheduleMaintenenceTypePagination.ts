import {ScheduleMaintenenceType} from './ScheduleMaintenenceType';

export interface IScheduleMaintenenceTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ScheduleMaintenenceType[];
}

export class ScheduleMaintenenceTypePagination implements IScheduleMaintenenceTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: ScheduleMaintenenceType[] = [];


}
