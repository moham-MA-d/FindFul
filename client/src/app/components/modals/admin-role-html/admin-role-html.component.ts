import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-admin-role-html',
  templateUrl: './admin-role-html.component.html',
  styleUrls: ['./admin-role-html.component.css']
})
export class AdminRoleHtmlComponent implements OnInit {
  showClassGrp: FormGroup;

  constructor() { }

  ngOnInit() {


    this.showClassGrp = new FormGroup({
      'Admin': new FormControl(false),
      'Moderator': new FormControl(false),
      'Member': new FormControl(false)
    });

  }

  onSubmit(){
    console.log(this.showClassGrp.value.amateur);
    this.showClassGrp.patchValue({amateur: false});
    console.log(this.showClassGrp.value.amateur);
  }

}
