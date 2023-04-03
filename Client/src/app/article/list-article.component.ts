import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'
class Article {
    constructor(public id: number,
        public title : string,
        public text : string,
        public owner : string
        ){}
}
@Component({
  selector: 'articles',
  templateUrl: "./list-article.component.html"
})
export class ArticlesComponent {
    articles : Array<Article> = new Array<Article>;
    
    constructor(private http: HttpClient){}
    
    ngOnInit() {
        console.log(this.http.get("http://localhost:8091/api/article/GetAllArticles").subscribe({next:(data:any) => {
            this.articles = data;
        }
    }));
    }
}
