import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { ActivatedRoute} from '@angular/router';
class Article {
    constructor(public id: number,
        public title : string,
        public text : string,
        public owner : string
        ){}
}
@Component({
  selector: 'article',
  templateUrl: "article.component.html"
})
export class ArticleComponent {
    id : number = -1;
    article : Article = null!;
    
    constructor(private http: HttpClient,
        private activeRoute : ActivatedRoute){
            activeRoute.params.subscribe(params => this.id=params['id'])
        }
    
    ngOnInit() {
        console.log(this.http.get(`http://localhost:5020/GetArticle/${this.id}`).subscribe({next:(data:any) => {
            this.article = data;
        }
    }));
    }
}
