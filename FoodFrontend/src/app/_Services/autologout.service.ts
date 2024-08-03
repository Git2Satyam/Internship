import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

const MINUTES_UNITL_AUTO_LOGOUT = 60 // in mins
const CHECK_INTERVAL = 600000 // in ms
const STORE_KEY =  'lastAction';
@Injectable({
  providedIn: 'root'
})
export class AutologoutService {
 val: any;
  constructor(private router: Router) {
    this.check();
    this.initListener();
    this.initInterval();
    localStorage.setItem(STORE_KEY,Date.now().toString());
   }

   getLastAction() {
    return parseInt(localStorage.getItem(STORE_KEY)!);
  }

   setLastAction(lastAction: number){
    localStorage.setItem(STORE_KEY,Date.now().toString());
   }

   initListener() {
    document.body.addEventListener('click', () => this.reset());
    document.body.addEventListener('mouseover',()=> this.reset());
    document.body.addEventListener('mouseout',() => this.reset());
    document.body.addEventListener('keydown',() => this.reset());
    document.body.addEventListener('keyup',() => this.reset());
    document.body.addEventListener('keypress',() => this.reset());
    window.addEventListener("storage",() => this.storageEvt());
  }

  reset() {
    this.setLastAction(Date.now());
  }

  initInterval() {
    setInterval(() => {
      console.log('autologout service');
      this.check();
    }, CHECK_INTERVAL);
  }

  check() {
    const now = Date.now();
    const timeleft = this.getLastAction() + MINUTES_UNITL_AUTO_LOGOUT * 60 * 1000;
    const diff = timeleft - now;
    const isTimeout = diff < 0;

    if (isTimeout)  {
      localStorage.removeItem(STORE_KEY);
      this.router.navigate(['login']);
    }
  }

  storageEvt(){
    console.log("storage");
    this.val = localStorage.getItem(STORE_KEY);
  }
}
