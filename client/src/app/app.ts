import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Member } from './model/member';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  private http = inject(HttpClient);
  protected title = 'Dating App';
  protected Members = signal<Member[]>([]);

  async ngOnInit() {
    this.Members.set(await this.getMembers());
  }
  
  async getMembers(): Promise<Member[]> {
    try {
      return lastValueFrom(this.http.get<Member[]>('https://localhost:5001/api/Members'));
    } catch (error) {
      console.error(error);
      throw error;
    }
  }
  
}

