import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { take } from 'rxjs/operators';
import { UserEnums } from 'src/app/enum/userEnums';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { UserParameters } from 'src/app/models/user/userParameters';
import { AccountService } from 'src/app/services/account/account.service';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
})

export class MembersComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  members: Member[] | null = [];
  pagination: Pagination = new Pagination();
  userParams!: UserParameters;
  user: User = new User;

  pageIndex = 0;
  pageSize = 5;
  // MatPaginator Output
  // MatPaginator Output
  pageEvent: PageEvent = new PageEvent;
  pageSizeOptions: number[] = [];

  enumSexValues = UserEnums.Sex;
  enumSexKeys=[];

  enumGenderValues = UserEnums.Gender;
  enumGenderKeys=[];

  enumSortValues = UserEnums.OrderBy;
  enumSortKeys=[];

  //Age Slider Configuration
  autoTicks = false;
  disabled = false;
  invert = false;
  min = 18;
  max = 99;
  showTicks = false;
  step = 1;
  thumbLabel = false;
  value = 20;
  vertical = false;
  tickInterval = 1;
  color = 'primary';
  sliderValue: any;

  getSliderTickInterval(): number | 'auto' {
    if (this.showTicks) {
      return this.autoTicks ? 'auto' : this.tickInterval;
    }
    return 0;
  }
  formatLabel(value: number) {
    return value;
  }


  constructor(private memberService: MemberService, private accountService: AccountService) {

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParameters(user);
    })
  }

  ngOnInit(): void {
    this.enumSexKeys = Object.keys(this.enumSexValues);
    this.enumGenderKeys = Object.keys(this.enumGenderValues);
    this.enumSortKeys = Object.keys(this.enumSortValues);
    this.loadMembers();
    this.pageSizeOptions = this.pagination.pageSizeOptions;

  }

  loadMembers() {
    this.userParams.minAge = this.sliderValue?.min ?? this.value;
    this.userParams.maxAge = this.sliderValue?.max ?? this.max;

    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })

  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  onPageChanged(e: { pageIndex: number; pageSize: number; }) {

    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;

    this.userParams.pageIndex = e.pageIndex;
    this.userParams.pageSize = e.pageSize;

    this.loadMembers();
  }
}
