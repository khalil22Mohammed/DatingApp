import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Member } from './model/member';
import { Nav } from '../layout/nav/nav';
import { AccountService } from '../core/services/account-service';
import { Home } from '../features/home/home';
import { User } from '../types/user';

@Component({
	selector: 'app-root',
	imports: [ Nav, Home ],
	templateUrl: './app.html',
	styleUrls: [ './app.css' ]
})
export class App implements OnInit {
	private accountService = inject(AccountService);
	private http = inject(HttpClient);
	protected title = 'Dating App';
	protected Members = signal<User[]>([]);
	public MembersFormApp: User[] = [];
	async ngOnInit() {
		this.Members.set(await this.getMembers());
		this.MembersFormApp = this.Members();
		this.setaccountUser();
	}

	setaccountUser() {
		const userString = localStorage.getItem('user');
		if (!userString) return;
		const user = JSON.parse(userString);
		this.accountService.currentUser.set(user);
	}
	async getMembers(): Promise<User[]> {
		try {
			return lastValueFrom(this.http.get<User[]>('https://localhost:5001/api/Members'));
		} catch (error) {
			console.error(error);
			throw error;
		}
	}
}
