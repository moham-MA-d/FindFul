export interface Pagination {

  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalItems: number;

}


export class PaginatedResult<T> {
  result: T;
  pagination: Pagination;
}
