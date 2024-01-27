import {MaintenanceAuthority} from './MaintenanceAuthority';

export interface IMaintenanceAuthorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: MaintenanceAuthority[];
}

export class MaintenanceAuthorityPagination implements IMaintenanceAuthorityPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: MaintenanceAuthority[] = [];


}
