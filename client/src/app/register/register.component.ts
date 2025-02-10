import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
[x: string]: any;
  private AccountService = inject(AccountService);
  cancelRegister = output<boolean>();
  model: any = {}

  register() {
    this.AccountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error:error => console.log(error)
    })
  }

  cancel () {
    this.cancelRegister.emit(false);
  }
}
