import {SurveyAuthority} from './SurveyAuthority';

export interface ISurveyAuthorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SurveyAuthority[];
}

export class SurveyAuthorityPagination implements ISurveyAuthorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: SurveyAuthority[] = [];


}
