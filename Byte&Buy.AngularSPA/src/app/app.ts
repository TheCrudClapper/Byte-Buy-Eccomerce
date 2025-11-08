import { Component, signal } from '@angular/core';
import { Login } from './components/auth/login/login';
import { Navbar } from "./components/navbar/navbar";
import { Footer } from "./components/footer/footer";
import { Register } from "./components/auth/register/register";

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer, Register],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('ByteBuy.AngularSPA');
}
