import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { UserParameters } from 'src/app/models/user/userParameters';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.css']
})
export class FollowingComponent implements OnInit {

  member: Member = new Member();
  userParams!: UserParameters;
  members: Member[];
  pagination: Pagination = new Pagination();

  pageIndex = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [];

  constructor(private memberService: MemberService, private route: ActivatedRoute) {
    this.userParams = memberService.userParams;
   }

  ngOnInit() {
    this.loadFollowing();
  }

  getUserInfo(event: Member){
    this.member = event;
  }

  loadFollowing() {
    let username = this.route.snapshot.paramMap.get("username");
    this.userParams.username = username;

    this.memberService.getFollowing(this.userParams).subscribe(response => {
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


    this.loadFollowing();
  }
}
