import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';

  constructor(private basketService: BasketService, private accountServce: AccountService) { }

  ngOnInit(): void {
    this.loadBasket();
    this.loadUser();

  }


  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      })
    }
  }


  loadUser(){
    const token = localStorage.getItem('token');

      this.accountServce.loadCurrentUser(token).subscribe(()=> {
        console.log('loaded user');
      }, error => {
        console.log(error);
      })
    
  }
}
