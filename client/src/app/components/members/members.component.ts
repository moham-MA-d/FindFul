import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { UserEnums } from 'src/app/enum/userEnums';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { UserParameters } from 'src/app/models/user/userParameters';
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
  pageSize = 10;
  pageSizeOptions: number[] = [];


  //Filter
  enumSexValues = UserEnums.Sex;
  enumSexKeys=[];
  enumGenderValues = UserEnums.Gender;
  enumGenderKeys=[];
  enumSortValues = UserEnums.OrderBy;
  enumSortKeys=[];
  //Age Slider Configuration
  autoTicks = false;
  invert = false;
  showTicks = false;
  step = 1;
  thumbLabel = false;
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

  constructor(private memberService: MemberService) {
    this.userParams = memberService.userParams;
  }

  ngOnInit(): void {
    this.enumSexKeys = Object.keys(this.enumSexValues).filter(e => isNaN(+e));
    this.enumGenderKeys = Object.keys(this.enumGenderValues).filter(e => isNaN(+e));
    this.enumSortKeys = Object.keys(this.enumSortValues);
    this.loadMembers();
    this.pageSizeOptions = this.pagination.pageSizeOptions;

  }

  loadMembers() {
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

