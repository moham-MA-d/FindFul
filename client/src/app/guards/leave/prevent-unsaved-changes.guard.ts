import { Injectable } from '@angular/core';
import { CanDeactivate} from '@angular/router';
import { ProfileEditComponent } from 'src/app/components/members/Profile/ProfileEdit/profileEdit/profileEdit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: ProfileEditComponent): boolean {
      if (component.editForm.dirty) {
        return confirm("Are you sure you want to continue? Any unsaved changes will be lost.")
      }
    return true;
  }
  
}
