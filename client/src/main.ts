import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

//provifing the code for to bootstrap AppModule
platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
