import {JobDescriptionEntryPms} from './JobDescriptionEntryPms';

export interface IJobDescriptionEntryPmsPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescriptionEntryPms[];
}

export class JobDescriptionEntryPmsPagination implements IJobDescriptionEntryPmsPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: JobDescriptionEntryPms[] = [];


}
