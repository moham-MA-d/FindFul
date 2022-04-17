import { Component, OnInit, Input, EventEmitter, Output, ViewChild, AfterViewInit, Renderer2, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, AbstractControl } from '@angular/forms';
import { ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';
import { MatSlider } from '@angular/material/slider';
import { merge, Subject } from 'rxjs';
import { tap, takeUntil, map } from 'rxjs/operators';

export interface MatRangeSliderChange<T> {
  value: T;
}

@Component({
  selector: 'mat-range-slider',
  templateUrl: './mat-range-slider.component.html',
  styleUrls: ['./mat-range-slider.component.css'],
  providers: [{
      provide: NG_VALUE_ACCESSOR,
      useExisting: MatRangeSliderComponent,
      multi: true
    }
  ]
})
export class MatRangeSliderComponent<T extends {min: number, max: number}> implements OnInit, AfterViewInit, OnChanges, ControlValueAccessor {

  @Input() min;
  @Input() max;
  @Input() vertical;
  @Input() invert;
  @Input() disabled;
  @Input() color;
  @Input() tabIndex;
  @Input() step;
  @Input() thumbLabel;
  @Input() tickInterval;

  displayWith(value: number) {
    return value;
  }
  displayWith1(value: number) {
    return value;
  }

  @ViewChild('sMin', {static: true}) _sliderMin: MatSlider;
  @ViewChild('sMax', {static: true}) _sliderMax: MatSlider;

  beforeOnInit = true;

    /** `View -> model callback called when value changes` */
  _onChange: (value: T) => void = () => {};

  /** `View -> model callback called when autocomplete has been touched` */
  _onTouched = () => {};

  _fillBarEl: HTMLElement;

  _switchCase = false;

  private _ngOnDestroy = new Subject();

  formGroup = new FormGroup({
      min: new FormControl(),
      max: new FormControl()
  })

  get formGroupValueCorrected() {

    const ans = this.formGroup.value.max < this.formGroup.value.min ?
                {min: this.formGroup.value.max, max: this.formGroup.value.min } :
                this.formGroup.value;

    return ans;
  }

  @Input()
  get value(): T|null {
    // If the value needs to be read and it is still uninitialized, initialize
    // it to the current minimum value.
    if (this._value === null) {
      this.value = this.formGroupValueCorrected;
    }
    return this._value;
  }
  set value(value: T|null) {
    this.writeToFormGroup(value);
  }

  private _value: T|null = null;

  @Output() readonly change = new EventEmitter<MatRangeSliderChange<T>>();

  /** Event emitted when the slider thumb moves. */
  @Output() readonly input =  new EventEmitter<MatRangeSliderChange<T>>();

  /**
   * Emits when the raw value of the slider changes. This is here primarily
   * to facilitate the two-way binding for the `value` input.
   * @docs-private
   */
  @Output() readonly valueChange: EventEmitter<T> = new EventEmitter<T>();

  constructor(private _r2: Renderer2) {

    this.formGroup.valueChanges
    .pipe(takeUntil(this._ngOnDestroy))
    .subscribe( (v: T) => {

      if ( v.min > v.max ) {

        // this.formGroup.setValue({min: v.max, max: v.min})
        v = {min: v.max, max: v.min} as T;

      }

      this._onChange(v);
    });

  }

  ngOnInit() {

    this.resetFormGroup(true);

  }

  ngOnChanges(changes: SimpleChanges) {
    if ( this.beforeOnInit ) { return; }

    const fgv = this.formGroup.value;

    if (changes['max']) {
      if (changes['max'].currentValue < fgv.max) {
        if( this.formGroup.get('max') ) {

          const c = this._switchCase ? 'min' : 'max';
          this.formGroup.get(c)!.setValue(changes[c].currentValue);
        }
      }

      this.calculateFillBar();

    }
    if (changes['min']) {

      if (changes['min'].currentValue > fgv.min) {
        if( this.formGroup.get('min') ) {
          const c = this._switchCase ? 'max' : 'min';
          this.formGroup.get(c)!.setValue(changes[c].currentValue);
        }
      }

      this.calculateFillBar();

    }
    if (changes['value']) {

      this.calculateFillBar();

    }

    if (changes['invert']) {

      this.calculateFillBar();

    }

    if (changes['vertical']) {

      if (changes['vertical'].currentValue) {

        this._r2.setStyle(this._fillBarEl, 'margin-left', null );
        this._r2.setStyle(this._fillBarEl, 'width' , null);

      } else {

        this._r2.setStyle(this._fillBarEl, 'bottom', null );
        this._r2.setStyle(this._fillBarEl, 'height' , null);

      }

      this.calculateFillBar();

    }


  }




  ngAfterViewInit() {

    const a =
    merge(
      this._sliderMax.valueChange.pipe(map(max => this.correctRange(max!, 'max') )),
      this._sliderMin.valueChange.pipe(map(min => this.correctRange(min!) )),
    ).pipe(
      tap(v => this.valueChange.next(v))
    )

    const b =
    merge(
      this._sliderMax.input.pipe(map(_ => this.correctRange(_.value!, 'max') )),
      this._sliderMin.input.pipe(map(_ => this.correctRange(_.value!) )),
    ).pipe(
      tap(v => {
        this.input.next({value: v});
        this.calculateFillBar(v)
      })
    )

    const c =
    merge(
      this._sliderMax.change.pipe(map(_ => this.correctRange(_.value!, 'max') )),
      this._sliderMin.change.pipe(map(_ => this.correctRange(_.value!) )),
    ).pipe(
      tap(v => this.change.next({value: v}) )
    )

    merge(a,b,c).pipe(takeUntil(this._ngOnDestroy))
    .subscribe();

    this._fillBarEl = this._sliderMax._elementRef.nativeElement
    .children[0]
    .children[0]
    .children[1];

    this.beforeOnInit = false;

    // this._sliderMax.focus()

  }

  ngOnDestroy() {
    this._ngOnDestroy.next();
    this._ngOnDestroy.complete();
  }

  /** If min overtakes max or other way around we have to correct for that */
  correctRange(value: number, useCase: 'min' | 'max' = 'min', formGroupValue = this.formGroup.value) {

    let ans;
    if (useCase === 'min') {

        if (value <= formGroupValue.max) {

            ans = { ...formGroupValue, min: value };

            this._switchCase = false;

        } else {

            ans = { min: formGroupValue.max, max: value };

            this._switchCase = true;

        }

    } else {

        if (value > formGroupValue.min) {

          ans = { ...formGroupValue, max: value };

          this._switchCase = false;

        } else {

          ans = { min: value, max: formGroupValue.min };

          this._switchCase = true;

        }

    }

    return ans;
  }

  /** On (input) of mat-slider we need to span the fillbar between min and max */
  calculateFillBar(value: T = this.formGroupValueCorrected) {
    // TODO(optimise) we dont have to calc this every time! Use onChanges hook
    if (!this._fillBarEl) {
      return;
    }

    const r = this.max - this.min;

    // width in percent
    const _w_PCT = ((value.max - value.min) / r) * 100;

    const myDim = this.vertical ? 'height' : 'width'

    this._r2.setStyle(this._fillBarEl, myDim, _w_PCT + '%');

    let _ml_PCT;
    if (this.invert) {

      _ml_PCT = ((this.max - value.max) / r) * 100;

    } else {

      // margin-left in percent
      _ml_PCT = ((value.min - this.min) / r) * 100;

    }

    const myMargin = this.vertical ? 'bottom' : 'margin-left'

    this._r2.setStyle(this._fillBarEl, myMargin, _ml_PCT  + '%' );




  }

   // Implemented as part of ControlValueAccessor.
  writeValue(value: any): void {

    this.writeToFormGroup(value);

    this.calculateFillBar();
  }

  // Implemented as part of ControlValueAccessor.
  registerOnChange(fn: (value: T) => {}): void {
    this._onChange = fn;
  }

  // Implemented as part of ControlValueAccessor.
  registerOnTouched(fn: () => {}) {
    this._onTouched = fn;
  }

  // Implemented as part of ControlValueAccessor.
  setDisabledState(isDisabled: boolean) {
    this.disabled = isDisabled;
  }

  resetFormGroup(emit = false) {

    this.formGroup.setValue(
      {
        min: this.min ? this.min : 0,
        max: this.max ? this.max : 100
      },
      { emitEvent: emit }
    );

  }

  writeToFormGroup(value: any) {

    if ( !value && value !== 0 ) {

      this.resetFormGroup();

      return;
    }

    const tryValue = Number(value);

    const fv = this.formGroup.value;

    if (!Number.isNaN(tryValue)) {

      if (tryValue !== fv.min && tryValue !== fv.max ) {

        let ans = {};

        ans = tryValue <= fv.max ?
              { ...fv, min: tryValue } :
              { ...fv, max: tryValue }


        this.formGroup.setValue(ans, {emitEvent: false})

      }

      return;

    }

    const _b1 = typeof value === 'object'   &&
                value !== null              &&
                value.hasOwnProperty('min') &&
                value.hasOwnProperty('max');

    if ( _b1 ) {

      const _b2 = !Number.isNaN(Number(value.min)) &&
                  !Number.isNaN(Number(value.max));

      if ( _b2 ) {

        this.formGroup.setValue(value);

      } else {

        this.resetFormGroup();

      }

      return;

    }

    this.resetFormGroup();

  }


}
