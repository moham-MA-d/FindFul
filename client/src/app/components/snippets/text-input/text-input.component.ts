import { Component, forwardRef, Input, OnInit, Self } from '@angular/core';
import { ControlContainer, ControlValueAccessor, FormGroup, NgControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css'],
})

// ControlValueAccessor : Defines an interface that acts as a bridge between the Angular forms API and a native element in the DOM.
export class TextInputComponent implements OnInit {

  filteredOptions: Observable<string[]>;

  @Input() label: string;
  @Input() name: string;
  @Input() classNamesParent: string;
  @Input() classNames: string;
  @Input() hasLalbelTag: boolean = false;
  @Input() errorColor: string = 'red';
  @Input() type: string = 'text';
  @Input() data: any;
  @Input() autoCompleteOptions: string[];


  // We are going to inject control inside this component
  // @Self()  : Self metadata. Self decorator and metadata.
  // With this way of injecting @Self() in the constructor Angular Dependency injection consider it as a local instance
  constructor(@Self() public ngCongrol: NgControl) {
    this.ngCongrol.valueAccessor = this;
  }

  ngOnInit(): void { }


  ngOnChanges() {
    var component = this.ngCongrol.valueAccessor as TextInputComponent;
    if (component.name === "city") {
      this.filteredOptions = this.ngCongrol.valueChanges.pipe(
        startWith(''),
        map(value => this._filter(value))
      )
    }
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.autoCompleteOptions.filter(option => option?.toLowerCase()?.includes(filterValue));
  }


  autoCompleteSelected(selectedValue, control: any) {
    if (control.valueAccessor.name == "city") {

    }
  }

  writeValue(obj: any): void {
    //this.ngCongrol.control.setValue(this.data);
  }

  registerOnChange(fn: any): void {
    //this.ngCongrol.control.setValue(this.data);

  }
  registerOnTouched(fn: any): void {

  }

  // setDisabledState?(isDisabled: boolean): void {
  //   throw new Error('Method not implemented.');
  // }

}
