import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-home',
  template: `
      <ion-content class="ion-padding">
          <h1 class="ion-padding" style="color: #3880ff">Article feed</h1>
          <ion-list>
              <ion-card *ngFor="let article of articles" (click)="deleteArticle(article)">
                  <ion-card-title class="ion-padding">{{article.headline}}</ion-card-title>
                  <ion-card-content>
                      <ion-img
                              src="{{article.articleImgUrl}}"
                              alt="{{article.articleImgUrl}}"
                      ></ion-img>
                  </ion-card-content>
                  <ion-card-content>
                      {{article.body}}
                  </ion-card-content>
                  <ion-card-content>
                      <i>Written by {{article.author}}</i>
                  </ion-card-content>
              </ion-card>
          </ion-list>

          <ion-fab slot="fixed" vertical="bottom" horizontal="center">
              <ion-fab-button href="add">
                  <ion-icon name="add"></ion-icon>
              </ion-fab-button>
          </ion-fab>
      </ion-content>


  `,
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  articles: Article[] = [];

  constructor(private http: HttpClient) {
    this.http = http;
    this.getAllArticles();
  }

  async getAllArticles() {
    const call = this.http.get<Article[]>("http://localhost:5000/api/feed");
    const result = await firstValueFrom<Article[]>(call);
    this.articles = result;
  }

  async deleteArticle(article: Article) {
    if (confirm("Are you sure you want to delete this article? (" + article.headline + ")")) {

      const call =
        this.http.delete("http://localhost:5000/api/articles/" + article.articleId);

      await firstValueFrom(call);

      function removeArticle(art: Article) {
        return art.articleId != article.articleId;
      }

      this.articles = this.articles.filter(removeArticle)
    }
  }
}

export interface Article {
  articleId: number,
  headline: string,
  body: string,
  author: string,
  articleImgUrl: string
}

@Component({
  selector: 'app-add',
  template: `
      <ion-content class="ion-padding">
          <h1 class="ion-padding" style="color: #3880ff">Add a new article</h1>
          <ion-card class="ion-padding">
              <ion-input [formControl]="headline" placeholder="Enter article headline here ..."></ion-input>
              <ion-input [formControl]="body" placeholder="Enter article body here ..."></ion-input>
              <ion-input [formControl]="author" placeholder="Enter article author here ..."></ion-input>
              <ion-input [formControl]="imageUrl" placeholder="Enter image URL here ..."></ion-input>
          </ion-card>

          <ion-fab class="ion-padding">
              <ion-fab-button (click)="createNewArticle()">
                  <ion-icon name="add"></ion-icon>
              </ion-fab-button>
          </ion-fab>


          <ion-fab slot="fixed" vertical="bottom" horizontal="center">
              <ion-fab-button href="../home">
                  <ion-icon name="arrow-back"></ion-icon>
              </ion-fab-button>
          </ion-fab>
      </ion-content>
  `,
  styleUrls: ['home.page.scss'],
})
export class AddPage {
  articles: Article[] = [];

  headline = new FormControl('',
    [Validators.required, Validators.minLength(5), Validators.maxLength(30)]);

  body = new FormControl('',
    [Validators.required, Validators.maxLength(1000)]);

  author = new FormControl('',
    [Validators.required, Validators.pattern('(?:Bob|Rob|Dob|Lob)')]);

  imageUrl = new FormControl('',
    [Validators.required]);

  formControlGroup = new FormGroup({
    headline: this.headline,
    body: this.body,
    author: this.author,
    articleImgUrl: this.imageUrl
  })

  constructor(private http: HttpClient) {
    this.http = http;
  }

  async createNewArticle() {
    const call = this.http.post<Article>("http://localhost:5000/api/articles", this.formControlGroup.value);
    await firstValueFrom(call).then(
      (response) => {
        this.articles.push(response);
        console.log(response)
      },
      (error: HttpErrorResponse) => {
        console.log(error)

      }
    );

    this.headline.setValue('');
    this.body.setValue('');
    this.author.setValue('');
    this.imageUrl.setValue('');
  }
}
