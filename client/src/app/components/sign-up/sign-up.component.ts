import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GlobalRoutePath } from '../../app.routes';
import { AccountService } from '../../services/account.service';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { RegisterUser } from '../../models/registerUser';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [InputTextModule, FormsModule, ReactiveFormsModule, FloatLabelModule, CardModule, ButtonModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  formBuilder = inject(FormBuilder);
  
  registerForm = this.formBuilder.group({
    userName: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required]),
    password: new FormControl<string>('', [Validators.required]),
  });

  register() {
    const regUserData: RegisterUser = {
      userName: this.registerForm.get("userName")?.value ?? "",
      email: this.registerForm.get("email")?.value ?? "",
      password: this.registerForm.get("password")?.value ?? "",
    };

    this.accountService.register(regUserData)
      .subscribe(_ => {
        this.router.navigate([GlobalRoutePath.products]);
      });
  }
}
