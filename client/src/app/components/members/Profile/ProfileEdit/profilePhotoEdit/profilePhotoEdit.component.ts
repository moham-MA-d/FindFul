import { Component, Input, OnInit } from '@angular/core';
import { UploadPhoto } from 'src/app/models/photo/uploadPhoto';
import { Member } from 'src/app/models/user/member';

@Component({
  selector: 'app-profilePhotoEdit',
  templateUrl: './profilePhotoEdit.component.html',
  styleUrls: ['./profilePhotoEdit.component.css']
})
export class ProfilePhotoEditComponent implements OnInit {

  @Input() member: Member;
  uploadPhotoInfo : UploadPhoto = {
    url: "Users/Add-Profile-Photo",
    className: "square",
  };

  constructor() { }

  ngOnInit() {
  }

}
