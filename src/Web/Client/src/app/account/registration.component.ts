import {Component} from "@angular/core"
import {NgForm} from '@angular/forms';
import { HttpClient} from '@angular/common/http'


@Component({
  selector: 'registration-form',
  templateUrl: "registration.component.html"
})
export class RegistrationComponent {
    constructor(private http : HttpClient) {}
    userId : string = null!;
    name : string = null!;
    password : string = null!;
    email : string = null!;

    onSubmit(form : NgForm) {
        let body = {
            userId: this.userId,
            name : this.name,
            password: this.password,
            email : this.email
        };
        console.log(body);
        this.http.post("http://localhost:5117/Registration", body).subscribe({
            next : (data: any) => {console.log("aaa")},
            error: error => console.log(error)
        });
    }

}
