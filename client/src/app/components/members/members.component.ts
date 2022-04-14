import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})

export class MembersComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;

  members: Member[];
  pagination: Pagination = new Pagination();
  pageIndex = 0;
  pageSize = 5;

  // MatPaginator Output
  pageEvent: PageEvent;
  pageSizeOptions: number[];

  constructor(private memberService: MemberService) { }

  ngOnInit(): void {
    this.loadMembers();
    this.pageSizeOptions = this.pagination.pageSizeOptions;
  }

  loadMembers() {
    this.memberService.getMembers(this.pageIndex, this.pageSize).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;

    })
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }
  onPageChanged(e) {
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.loadMembers();
  }
}
