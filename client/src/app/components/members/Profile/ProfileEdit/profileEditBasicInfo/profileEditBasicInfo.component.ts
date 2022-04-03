import { ChangeDetectorRef, Component, HostListener, Input, OnInit, ViewChild, ViewEncapsulation  } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileEditBasicInfo',
  templateUrl: './profileEditBasicInfo.component.html',
  styleUrls: ['./profileEditBasicInfo.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class ProfileEditBasicInfoComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm;
  @Input() member: Member;
  basicInfoForm: FormGroup;
  date = new Date((new Date().getTime() - 3888000000));

  //to access browser events
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private memberService: MemberService, private toaster: ToastrService, private fb: FormBuilder, private readonly changeDetectorRef: ChangeDetectorRef) {

   }

  ngOnInit() {
    this.InitializeForm();
  }
  ngAfterViewChecked() {
    this.changeDetectorRef.detectChanges();
  }
  InitializeForm() {
    this.basicInfoForm = this.fb.group({
      firstName : ['', Validators.required],
      lastName : ['', Validators.required],
      email: new FormControl({value: '', disabled: true}, [Validators.required, Validators.email]),
      dateOfBirth: ['']
    })
  }

  updateMember() {
    return this.memberService.updateMember(this.member).subscribe(() => {
      this.toaster.success("Successfully Updated!");
      // to keep and preserve the values of the form we pass this.member
      this.editForm.reset(this.member);
    });
  }

}
