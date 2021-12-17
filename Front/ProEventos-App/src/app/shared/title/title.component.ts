import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {
  @Input() title: string | undefined;
  @Input() subtitle = 'Desde 2011';
  @Input() iconClass = 'fa fa-user';
  @Input() btnList = false;

  constructor(private router: Router) { }

  ngOnInit() {
    //this.title = text;
  }

  list(): void{
    this.router.navigate([`/${this.title?.toLocaleLowerCase()}/lista`])
  }
}
