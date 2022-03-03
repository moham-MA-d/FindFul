import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ComponentService } from 'src/app/services/component/component.service';

@Component({
  selector: 'app-left',
  templateUrl: './left.component.html',
  styleUrls: ['./left.component.css']
})
export class LeftComponent implements OnInit {

  isChatBlockActive: boolean;
  constructor(private routeService: ComponentService, private changeDetection: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  ngAfterViewChecked(): void {
      this.isChatBlockActive = this.routeService.isChatBlockEnable;

      //to run manually change detection in component
      //This will tell Angular to check the view and its children, in which case it will notice that 
      //  our loading state has changed.
      //The reason I used this method is that I'm changing the view after ngAfterViewChecked() method and by
      //  default it's not place to change the view.
      this.changeDetection.detectChanges();
    }

}
