import { Component, Input, input, signal } from '@angular/core';
import { Register } from '../account/register/register';
import { User } from '../../types/user';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [ Register ],
	templateUrl: './home.html',
	styleUrl: './home.css'
})
export class Home {
	protected registerMode = signal(false);

	showRegisterMode(value: boolean) {
		this.registerMode.set(value);
	}
}
