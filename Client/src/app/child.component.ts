import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'
@Component({
  selector: 'child',
  template: `<h1>Привет это дочерний компонент<h1>`
})
export class ChildComponent {
    constructor(private http: HttpClient){}

    ngOnInit() {
        console.log(this.http.get("http://localhost:8091/api/article/GetAllArticles").subscribe({next:(data:any) => {
            console.log(data.result);
        }
    }));
    }
}
