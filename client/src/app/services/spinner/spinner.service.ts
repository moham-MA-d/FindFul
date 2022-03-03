import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  busyRequestCounter = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.busyRequestCounter++;
    this.spinnerService.show(undefined, {
      type: 'ball-atom',
      color: '#fff'
    });
  }  

  idle(){
    this.busyRequestCounter--;
    if (this.busyRequestCounter <= 0) {
      this.busyRequestCounter = 0;
      this.spinnerService.hide();
    }
  }

}
