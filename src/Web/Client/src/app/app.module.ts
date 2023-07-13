import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppComponent } from './app.component';
import { ArticlesComponent } from './article/list-article.component';
import {Routes, RouterModule, RouterOutlet} from '@angular/router'
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './notfound.component';
import { ArticleComponent } from './article/article.component';
import { FormsModule }   from '@angular/forms';
import { RegistrationComponent } from './account/registration.component';
// определение маршрутов
const appRoutes: Routes =[
  { path: '', component: HomeComponent},
  { path: 'articles', component: ArticlesComponent},
  { path: "articles/:id", component : ArticleComponent},
  { path: "registration", component : RegistrationComponent},
  { path: '**', component: NotFoundComponent }

];

@NgModule({
  imports: [
    BrowserModule, HttpClientModule,  RouterModule.forRoot(appRoutes), FormsModule
  ],
  declarations: [
    AppComponent, ArticlesComponent, NotFoundComponent, HomeComponent, RegistrationComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
