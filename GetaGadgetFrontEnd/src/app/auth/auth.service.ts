import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { last, map } from 'rxjs/operators';
import { API_URL, LOGIN_URL, REGISTER_URL } from 'src/environments/environment';
import { UserRole } from '../common/userRole';
import { RegisterModel } from '../models/user/registerModel';
import { UserModel } from '../models/user/userModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<UserModel>;
  public currentUser: Observable<UserModel>;
  static currentUser: any;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(sessionStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserName(): String {
    if (this.currentUserSubject.value != null) {
      var user = this.currentUserSubject.value;
      return user.lastName + ' ' + user.firstName;
    }
    else {
      return null;
    }
  }

  public get currentUserEmail(): String {
    if (this.currentUserSubject.value != null) {
      var user = this.currentUserSubject.value;
      return user.emailAddress;
    }
    else {
      return null;
    }
  }

  public currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  public isAuthenticated(): boolean {
    return this.currentUserSubject.value != null;
  }

  public isAdmin(): boolean {
    if (this.isAuthenticated() === true) {
      return this.currentUserSubject.value.userRole === UserRole.Admin;
    } else {
      return false;
    }
  }

  login(emailAddress: string, password: String) {
    return this.http.post(API_URL + LOGIN_URL, { emailAddress, password }).pipe(
      map((user: UserModel) => {
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      })
    );
  }

  logout() {
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  register(model: RegisterModel) {

    return this.http.post(API_URL + REGISTER_URL, model).pipe(
      map((result: boolean) => {
        return result;
      })
    );
  }

  // public getProfile(){
  //   return this.http.get(API_URL + PROFILE_URL).pipe(
  //     map((data : Profile) => {
  //       return data;
  //     })
  //   );
  // }

  // editProfile(model: Profile){

  //   return this.http.post(API_URL + PROFILE_URL, model).pipe(
  //     map((result: boolean) =>{
  //       return result;
  //     })
  //   );
  // }
}