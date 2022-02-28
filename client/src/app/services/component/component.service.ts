import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ComponentService {

isChatBlockEnable: boolean = false;

constructor() { }

setChatBlockEnable(value: boolean){
  this.isChatBlockEnable = value;
}

}
