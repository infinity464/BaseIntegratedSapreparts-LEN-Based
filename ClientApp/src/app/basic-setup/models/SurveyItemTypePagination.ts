import {SurveyItemType} from './SurveyItemType';

export interface ISurveyItemTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SurveyItemType[];
}

export class SurveyItemTypePagination implements ISurveyItemTypePagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SurveyItemType[] = [];


}
