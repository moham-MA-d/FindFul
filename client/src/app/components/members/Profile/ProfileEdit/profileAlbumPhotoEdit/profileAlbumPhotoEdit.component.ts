import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, map, take } from 'rxjs/operators';
import { UploadPhoto } from 'src/app/models/photo/uploadPhoto';
import { Member } from 'src/app/models/user/member';
import { User } from 'src/app/models/user/user';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { AccountService } from 'src/app/services/account/account.service';
import { MemberService } from 'src/app/services/member/member.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-profileAlbumPhotoEdit',
  templateUrl: './profileAlbumPhotoEdit.component.html',
  styleUrls: ['./profileAlbumPhotoEdit.component.css']
})
export class ProfileAlbumPhotoEditComponent implements OnInit {

  @Input() member: Member;
  baseUrl = environment.apiUrl;
  user: User;
  photos: Observable<userPhoto[]>;
  uploadPhotoInfo : UploadPhoto = {
    url: "UserAlbum/AddPhoto",
    className: "square",
  };

  constructor (private accountService: AccountService, private memberService: MemberService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(u => this.user = u); 
  }
  
  ngOnInit(): void {
    this.loadUserPhotos();
  }
  
  loadUserPhotos(){
    this.photos = this.memberService.getMemberPhotos(this.user.userName);
  }

  deleteAlbumPhoto(photoId: number){
    this.memberService.deleteAlbumPhoto(photoId).subscribe(() => {
      this.photos =  this.photos.pipe(map(x => x.filter(f => f.id !== photoId)))
    });
  }
  
}
