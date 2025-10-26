import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Result } from 'postcss';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrls: ['./nav.css']
})
export class Nav {
  protected accountServies =inject(AccountService);
  protected cards: any = {};
  

  login() {
    this.accountServies.login(this.cards).subscribe({
      next: result =>{
        console.log(result),
        this.cards={};

      },

      error: error =>alert(error.message)  

      
    })
  }
  logout() {
    this.accountServies.logout();
  }
}
