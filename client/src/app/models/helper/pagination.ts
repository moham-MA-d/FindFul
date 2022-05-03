import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";

export class Pagination {

  pageIndex!: number;
  totalPages!: number;
  pageSize!: number;
  totalItems!: number;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  constructor() {}

  public getPaginationHeaders(pageIndex: number, pageSize: number, username: string = "") {

    // HttpParams: gives us the ability to serialize the parameters and add them to query string.
    let params = new HttpParams();

    if (pageIndex !== null && pageSize !== null) {
      //query string:
      params = params.append('pageIndex', pageIndex.toString());
      params = params.append('pageSize', pageSize.toString());
    }

    return params;
  }

  public getPaginationResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T | null>  = new PaginatedResult<T>() ;

    // in normal usage of get() this will give us the response body
    // when we observing the response and use this to pass the params to it the we get the Full response back
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body ; // response.body = Member[]
        if (response.headers.get("Pagination") !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get("Pagination"));
        }
        return paginatedResult;
      })
    );
  }
}


export class PaginatedResult<T> {
  result!: T;
  pagination!: Pagination;
}
