export class RegisterModel {
    public firstName: string;
    public emailAddress: string;
    public lastName: string;
    public password: string;
    public phoneNumber: string;

    public constructor(firstName: string, lastName: string, email: string, password: string, phone: string){
        this.firstName = firstName;
        this.lastName = lastName;
        this.emailAddress = email;
        this.password = password;
        this.phoneNumber = phone;
    }

}