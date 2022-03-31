import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl, FormControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})

// ControlValueAccessor : Defines an interface that acts as a bridge between the Angular forms API and a native element in the DOM.
export class TextInputComponent implements ControlValueAccessor {

  @Input() label: string;
  @Input() errorColor: string = 'red';
  @Input() type: string = 'text';


  // We are going to inject control inside this component
  // @Self()  : Self metadata. Self decorator and metadata.
  // With this way of injecting @Self() in the constructor Angular Dependency injection consider it as a local instance
  constructor(@Self() public ngCongrol: NgControl) {
    this.ngCongrol.valueAccessor = this;
  }

  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }

  // setDisabledState?(isDisabled: boolean): void {
  //   throw new Error('Method not implemented.');
  // }

}
