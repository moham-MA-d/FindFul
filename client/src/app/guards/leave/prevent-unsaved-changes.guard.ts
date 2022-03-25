import { Injectable } from '@angular/core';
import { CanDeactivate} from '@angular/router';
import { ProfileEditBasicInfoComponent } from 'src/app/components/members/Profile/ProfileEdit/profileEditBasicInfo/profileEditBasicInfo.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: ProfileEditBasicInfoComponent): boolean {
      if (component.editForm.dirty) {
        return confirm("Are you sure you want to continue? Any unsaved changes will be lost.")
      }
    return true;
  }
  
}
