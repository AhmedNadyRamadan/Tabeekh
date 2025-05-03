export interface IPaginationQuery{
    pageNumber: number,
    limit: number,
}

export interface IPaginationData<T>{
    totalCount: number,
    items: T[];
}