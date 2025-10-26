import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, Signal } from '@angular/core';
import { tap } from 'rxjs/operators';
import { LoginCreds, RegisterCreds, User } from '../../types/user';

@Injectable({
	providedIn: 'root'
})
export class AccountService {
	private http = inject(HttpClient);
	currentUser = signal<User | null>(null);
	baseUrl: string = 'https://localhost:5001/api/';
	register(cards: RegisterCreds) {
		return this.http.post<User>(this.baseUrl + 'account/register', cards).pipe(
			tap((user) => {
				if (user) {
					localStorage.setItem('user', JSON.stringify(user));
					this.currentUser.set(user);
				}
			})
		);
	}
	login(cards: LoginCreds) {
		return this.http.post<User>(this.baseUrl + 'account/login', cards).pipe(
			tap((user) => {
				if (user) {
					localStorage.setItem('user', JSON.stringify(user));
					this.currentUser.set(user);
				}
			})
		);
	}
	setCurrentUser(user: User) {
		localStorage.setItem('user', JSON.stringify(user));
		this.currentUser.set(user);
	}

	logout() {
		localStorage.removeItem('user');
		this.currentUser.set(null);
	}
}
