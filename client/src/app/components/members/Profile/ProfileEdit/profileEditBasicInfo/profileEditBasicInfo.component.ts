import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { ChangeDetectorRef, Component, HostListener, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, FormGroup, NgForm, Validators, FormControl, FormGroupDirective } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { filter, map, startWith } from 'rxjs/operators';
import { Member } from 'src/app/models/user/member';
import { MemberService } from 'src/app/services/member/member.service';

@Component({
  selector: 'app-profileEditBasicInfo',
  templateUrl: './profileEditBasicInfo.component.html',
  styleUrls: ['./profileEditBasicInfo.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: ProfileEditBasicInfoComponent, useExisting: ProfileEditBasicInfoComponent }]

})
export class ProfileEditBasicInfoComponent implements OnInit {

  countriesFilteredOptions: Observable<string[]>;

  @ViewChild('editForm') editForm: NgForm;
  @Input() member: Member;
  basicInfoForm: FormGroup;
  date = new Date((new Date().getTime() - 3888000000));

  countries: string[] = [];
  cities: string[] = [];

  //to access browser events
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private memberService: MemberService,
    private toaster: ToastrService,
    private fb: FormBuilder,
    private readonly changeDetectorRef: ChangeDetectorRef,
    private httpClient: HttpClient
  ) {
  }

  ngOnInit() {
    this.GetCountries();
  }

  ngOnChanges() {
    this.InitializeForm();

    this.countriesFilteredOptions = this.basicInfoForm.controls.country.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    )

  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.countries.filter(option => option?.toLowerCase()?.includes(filterValue));
  }

  //emailFormControl = new FormControl('', [Validators.required, Validators.email]);

  ngAfterViewChecked() {
    this.changeDetectorRef.detectChanges();
  }
  InitializeForm() {
    this.basicInfoForm = this.fb.group({
      firstName: [this.member.firstName, Validators.required],
      lastName: ['', Validators.required],
      email: new FormControl({ value: '', disabled: true }, [Validators.required, Validators.email]),
      phone: new FormControl({ value: '', disabled: true }, [Validators.required]),
      dateOfBirth: [''],
      country: [''],
      city: [''],
    })
  }

  updateMember() {
    return this.memberService.updateMember(this.member).subscribe(() => {
      this.toaster.success("Successfully Updated!");
      // to keep and preserve the values of the form we pass this.member
      this.editForm.reset(this.member);
    });
  }

  countrySelected(countryName: string) {
    this.cities = [];
    this.GetCities(countryName);
  }

  GetCountries() {

    const httpOptions = {
      headers: new HttpHeaders({
        'Accept': 'application/json',
        'X-Powered-By': 'Express',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': '*',

      })
    }
    var r =  this.httpClient.get('https://countriesnow.space/api/v0.1/countries/positions', httpOptions);

     r.subscribe((res: any) => {
      res.data.forEach(element => {
        this.countries.push(element.name);
       });
    });
  }

  GetCities(countryName: string) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Accept': 'application/json',
        'X-Powered-By': 'Express',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': '*',
        'Content-Type': 'application/json',
      })
    }
    const body = JSON.stringify({"country": countryName});


    var r =  this.httpClient.post('https://countriesnow.space/api/v0.1/countries/cities', body, httpOptions);

     r.subscribe((res: any) => {
      res.data.forEach(element => {
        this.cities.push(element);
       });
    });
  }

  matcher = new MyErrorStateMatcher();

}
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
