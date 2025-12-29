import { Component, signal } from '@angular/core';
import { Navbar } from "./components/navbar/navbar";
import { Footer } from "./components/footer/footer";
import { Register } from "./components/auth/register/register";
import { RouterOutlet } from '@angular/router';
import { Login } from './components/auth/login/login';

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('ByteBuy');
}
