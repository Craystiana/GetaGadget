export class ContactModel{
    public subject: string;
    public body: string;
    
    public constructor(subject: string, body: string){
        this.subject = subject;
        this.body = body;
    }
}