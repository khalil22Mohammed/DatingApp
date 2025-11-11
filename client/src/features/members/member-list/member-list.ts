import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member.service';
import { Observable } from 'rxjs';
import { Member } from '../../../types/member';
import { AsyncPipe } from '@angular/common';
import { MemberCard } from "../member-card/member-card";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [AsyncPipe, MemberCard],
  templateUrl: './member-list.html',
  styleUrl: './member-list.css'
})
export class MemberList {
  private memberService = inject(MemberService);

  protected members$: Observable<Member[]>;
  members: Member | undefined;
  constructor() {
    this.members$ = this.memberService.getMembers();
  }

  getMembers() {
    this.memberService.getMembers().subscribe({
      next: () => {
        this.members$ = this.memberService.getMembers();
        console.log("members$", this.members$);

      },
      error: (error: any) => {
        console.log(error);
      }
    })
  }
}
