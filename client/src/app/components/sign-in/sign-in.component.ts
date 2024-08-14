import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { LoginUser } from '../../models/loginUser';
import { Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { GlobalRoutePath } from '../../app.routes';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [InputTextModule, FormsModule, ReactiveFormsModule, FloatLabelModule, CardModule, ButtonModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  private formBuilder = inject(FormBuilder);
  
  loginForm = this.formBuilder.group({
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  login() {
    const logUserData: LoginUser = {
      email: this.loginForm.get("email")?.value ?? "",
      password: this.loginForm.get("password")?.value ?? "",
    };

    this.accountService.login(logUserData)
      .subscribe(_ => {
        this.router.navigate([GlobalRoutePath.Products]);
      });
  }
}
