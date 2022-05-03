import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Pagination } from 'src/app/models/helper/pagination';
import { Member } from 'src/app/models/user/member';
import { UserParameters } from 'src/app/models/user/userParameters';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {


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
    this.loadFollowers();
  }

  getUserInfo(event: Member){
    this.member = event;
  }

  loadFollowers() {
    let username = this.route.snapshot.paramMap.get("username");
    this.userParams.username = username;

    this.memberService.getFollowers(this.userParams).subscribe(response => {
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


    this.loadFollowers();
  }
}
