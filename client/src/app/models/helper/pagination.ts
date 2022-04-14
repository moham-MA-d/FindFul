export class Pagination {
  pageIndex: number;
  totalPages: number;
  pageSize: number;
  totalItems: number;
  pageSizeOptions: number[] = [5, 10, 25, 100];
}


export class PaginatedResult<T> {
  result: T;
  pagination: Pagination;
}
