import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Member } from 'src/app/models/user/member';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileAlbum',
  templateUrl: './profileAlbum.component.html',
  styleUrls: ['./profileAlbum.component.css']
})
export class ProfileAlbumComponent implements OnInit {

  member: Member = new Member();
  photos: Observable<userPhoto[]>;

  constructor(private memberService: MemberService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadUserPhotos();
  }

  getUserInfo(event: Member){
    this.member = event;
  }

  loadUserPhotos() {
    let username = this.route.snapshot.paramMap.get("username");
    this.photos = this.memberService.getMemberPhotos(username);
  }
}
