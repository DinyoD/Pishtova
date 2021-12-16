import { Component } from '@angular/core';

@Component({
  selector: 'app-time-counter',
  templateUrl: './time-counter.component.html',
  styleUrls: ['./time-counter.component.css']
})
export class TimeCounterComponent {

  constructor() { }

  countDownDate = new Date('may 18, 2022 08:00:00').getTime()
  days: string = '';
  hours: string = '';
  min: string = '';
  sec: string = '';
  timeIsOverResult : string  = 'Времето е изтекло!';
  timeIsOver: boolean = false;

  counter = setInterval(() => {
    let now = new Date().getTime();
    let distance = this.countDownDate - now;

    if (distance < 0) {
      this.timeIsOver= true;
    }
    let d = Math.floor(distance / (1000*60*60*24));
    let h = Math.floor((distance % (1000*60*60*24)) / (1000*60*60));
    let m = Math.floor((distance % (1000*60*60)) / (1000*60));
    let s = Math.floor((distance % (1000*60)) / 1000);

    this.days = d < 10 ? `0${d}` : d.toString();
    this.hours = h < 10 ? `0${h}` : h.toString();
    this.min = m < 10 ? `0${m}` : m.toString();
    this.sec = s < 10 ? `0${s}` : s.toString();
  })
}
