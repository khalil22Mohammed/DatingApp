import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class IntiService {
	private accountSarvice = inject(AccountService);

	inti() {
		const userString = localStorage.getItem('user');
		if (!userString) return of(null);
		const user = JSON.parse(userString);
		this.accountSarvice.currentUser.set(user);

		return of(null);
	}
}
