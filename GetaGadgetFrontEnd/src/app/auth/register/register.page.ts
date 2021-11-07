import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastController } from '@ionic/angular';
import { first } from 'rxjs/operators';
import { RegisterModel } from 'src/app/models/user/registerModel';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.page.html',
  styleUrls: ['./register.page.scss'],
})
export class RegisterPage implements OnInit {

  public isLoading: boolean;
  constructor(private authService: AuthService, private router: Router, private toastCtrl: ToastController) { }

  ngOnInit() {
  }
  onRegister(registerForm: NgForm) {
    this.isLoading = true;
    var model = new RegisterModel(registerForm.value.firstName,
                                  registerForm.value.lastName,
                                  registerForm.value.email,
                                  registerForm.value.password,
                                  registerForm.value.phoneNumber);

    this.authService.register(model).pipe(first()).subscribe(
      data => {
        if (data == true) {
          this.router.navigateByUrl('/login');
          this.toastCtrl.create({
            message: 'Register succesful. Please log in.',
            duration: 5000,
            position: 'bottom',
            color: 'success',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        else {
          this.toastCtrl.create({
            message: 'Register failed',
            duration: 5000,
            position: 'bottom',
            color: 'danger',
            buttons: ['Dismiss']
          }).then((el) => el.present());
        }
        this.isLoading = false;
      },
      error => {
        this.toastCtrl.create({
          message: 'Register failed',
          duration: 5000,
          position: 'bottom',
          color: 'danger',
          buttons: ['Dismiss']
        }).then((el) => el.present());

        this.isLoading = false;
      }
    )

  }

}