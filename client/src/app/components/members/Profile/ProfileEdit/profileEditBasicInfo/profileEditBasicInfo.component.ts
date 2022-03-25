import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileEditBasicInfo',
  templateUrl: './profileEditBasicInfo.component.html',
  styleUrls: ['./profileEditBasicInfo.component.css']
})
export class ProfileEditBasicInfoComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  @Input() member: Member;

  //to access browser events
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  
  constructor(private memberService: MemberService, private toaster: ToastrService) {

   }

  ngOnInit() {
  }

 
  updateMember() {
    return this.memberService.updateMember(this.member).subscribe(() => {
      this.toaster.success("Successfully Updated!");
      // to keep and preserve the values of the form we pass this.member
      this.editForm.reset(this.member);
    });

  }
}
