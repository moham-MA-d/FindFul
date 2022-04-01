import { Component, forwardRef, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css'],
})

// ControlValueAccessor : Defines an interface that acts as a bridge between the Angular forms API and a native element in the DOM.
export class TextInputComponent implements ControlValueAccessor, OnInit {

  @Input() label: string;
  @Input() classNamesParent: string;
  @Input() classNames: string;
  @Input() hasLalbelTag: boolean = false;
  @Input() errorColor: string = 'red';
  @Input() type: string = 'text';
  @Input() data: any;

  // We are going to inject control inside this component
  // @Self()  : Self metadata. Self decorator and metadata.
  // With this way of injecting @Self() in the constructor Angular Dependency injection consider it as a local instance
  constructor(@Self() public ngCongrol: NgControl) {
    this.ngCongrol.valueAccessor = this;
  }

  ngOnInit(): void {

  }

  writeValue(obj: any): void {
    this.ngCongrol.control.setValue(this.data);
  }

  registerOnChange(fn: any): void {
    this.ngCongrol.control.setValue(this.data);

  }
  registerOnTouched(fn: any): void {

  }

  // setDisabledState?(isDisabled: boolean): void {
  //   throw new Error('Method not implemented.');
  // }

}
