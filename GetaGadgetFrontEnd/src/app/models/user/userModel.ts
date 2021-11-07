import { UserRole } from "../../common/userRole";

export class UserModel {
    private _emailAddress: string;
    private _firstName: string;
    private _lastName: string;
    private _userRole: UserRole;
    private _token: string;

    constructor(JSON: any){
        this._emailAddress = JSON.emailAddress;
        this._firstName = JSON.FirstName;
        this._lastName = JSON.LastName;
        this._userRole = JSON.UserRole;
    }

    get userRole(){
        return this._userRole;
    }

    get firstName(){
        return this._firstName;
    }

    get lastName(){
        return this._lastName;
    }

    get token(){
        return this._token;
    }

    get emailAddress(){
        return this._emailAddress;
    }
}