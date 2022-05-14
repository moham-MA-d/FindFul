import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from 'src/app/models/user/user';
import { AccountService } from 'src/app/services/account/account.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit{

  user: User;
  @Input() appHasRole: string[];

  //ViewContainerRef: Represents a container where one or more views can be attached to a component.
  //TemplateRef: Represents an embedded template that can be used to instantiate embedded views. To instantiate embedded views based on a template,
  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateReef: TemplateRef<any>,
    private accountService: AccountService) {
      accountService.currentUser$.pipe(take(1)).subscribe(u => this.user = u);
    }


  ngOnInit(): void {
    if (this.user == null || !this.user.roles) {
      this.viewContainerRef.clear();
      return
    }

    if (this.user.roles.some(r => this.appHasRole.includes(r))) {
      this.viewContainerRef.createEmbeddedView(this.templateReef);
    }
  }



}
