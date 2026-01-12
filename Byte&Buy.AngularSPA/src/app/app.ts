import { Component, signal } from '@angular/core';
import { Navbar } from "./shared/ui/navbar/navbar";
import { Footer } from "./shared/ui/footer/footer";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('ByteBuy');
}
