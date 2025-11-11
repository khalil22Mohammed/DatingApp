import {
	ApplicationConfig,
	inject,
	provideAppInitializer,
	provideBrowserGlobalErrorListeners,
	provideZonelessChangeDetection
} from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { IntiService } from '../core/services/inti-service';
import { lastValueFrom } from 'rxjs';
import { errorInterceptor } from '../core/interceptors/error-interceptor';
import { jwtInterceptor } from '../core/interceptors/jwt-interceptor';

export const appConfig: ApplicationConfig = {
	providers: [
		provideBrowserGlobalErrorListeners(),
		provideZonelessChangeDetection(),
		provideRouter(routes, withViewTransitions()),
		provideHttpClient(withInterceptors([errorInterceptor, jwtInterceptor])),
		provideAppInitializer(() => {
			const intiService = inject(IntiService);
			return (async () => {
				try {
					await lastValueFrom(intiService.inti());
				} finally {
					const splash = document.getElementById('initial-splash');
					if (splash) {
						splash.remove();
					}
				}
			})();
		})
	]
};
