import { Component, OnInit } from '@angular/core';
import { ComponentService } from 'src/app/services/component/component.service';

@Component({
  selector: 'app-left',
  templateUrl: './left.component.html',
  styleUrls: ['./left.component.css']
})
export class LeftComponent implements OnInit {

  isChatBlockActive: boolean;
  constructor(private routeService: ComponentService) { }

  ngOnInit(): void {
  }

  ngAfterViewChecked(): void {
    this.isChatBlockActive = this.routeService.isChatBlockEnable;
  }

}
