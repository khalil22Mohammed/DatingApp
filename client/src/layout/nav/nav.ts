import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Result } from 'postcss';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastService } from '../../core/services/toast-service';

@Component({
	selector: 'app-nav',
	standalone: true,
	imports: [ FormsModule, RouterLink, RouterLinkActive ],
	templateUrl: './nav.html',
	styleUrls: [ './nav.css' ]
})
export class Nav implements OnInit {
	protected accountServies = inject(AccountService);
	private toast = inject(ToastService);
	private router = inject(Router);
	protected cards: any = {};

	ngOnInit(): void {
		if (!this.accountServies.currentUser()) {
			const userString = localStorage.getItem('user');
			if (userString) {
				try {
					this.accountServies.currentUser.set(JSON.parse(userString));
				} catch {}
			}
		}
	}

	login() {
		this.accountServies.login(this.cards).subscribe({
			next: () => {
				this.router.navigateByUrl('/member');
				this.toast.success('Logged in successfully');
				this.cards = {};
			},

			error: (error) => {
				this.toast.error(error.error);
			}
		});
	}
	logout() {
		this.accountServies.logout();
		this.router.navigateByUrl('/');
	}
}
