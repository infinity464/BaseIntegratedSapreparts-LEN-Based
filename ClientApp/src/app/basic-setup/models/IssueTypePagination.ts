import {IssueType} from './IssueType';

export interface IIssueTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: IssueType[];
}

export class IssueTypePagination implements IIssueTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: IssueType[] = [];


}
