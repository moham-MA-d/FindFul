import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';
import { map } from 'rxjs/operators';
import * as Editor from 'src/app/libs/ckeditor/build/ckeditor';
import { environment } from 'src/environments/environment';
import { Post } from 'src/app/models/post/post';
import { PostService } from 'src/app/services/post/post.service';

export class UploadAdapter {
  private _loader;

  constructor(loader: any) {
    this._loader = loader;
  }

  upload() {
    return this._loader.file.then(
      (file: Blob) =>
        new Promise((resolve, reject) => {
          var myReader = new FileReader();
          myReader.onloadend = (e) => {
            resolve({ default: myReader.result });
          };

          myReader.readAsDataURL(file);
        })
    );
  }

  data: any = `<p>Hello, world!</p>`;
  editor = Editor;

}


@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  baseUrl = environment.apiUrl;
  public Editor = Editor;
  post: Post = new Post();
  public model = {
    editorData: '',
    //config: { toolbar: [ [ 'Undo' ] ] }
    config: {},
    placeholder: 'Type the content here!',
    cansSendPost: false
  };

  constructor(private postService: PostService) {
  }


  ngOnInit(): void {
  }

  onReady(eventData: { plugins: { get: (arg0: string) => { (): any; new(): any; createUploadAdapter: (loader: any) => UploadAdapter; }; }; }) {
    eventData.plugins.get('FileRepository').createUploadAdapter = function (loader) {
      console.log(btoa(loader.file));
      return new UploadAdapter(loader);
    };
  }

  onSendForm() {
    if (this.model.editorData.length > 8) {
      this.post.body =  this.model.editorData;
      return this.postService.sendPost(this.post).subscribe(r => {console.log("rrrr:", r)});
    }
  }

  onSearchChange(searchValue: string){
    if (this.model.editorData.length > 8) {
      this.model.cansSendPost = true;
    } else {
      this.model.cansSendPost = false;
    }
  }

  public onChange({ editor }: ChangeEvent) {
    //const data = editor.getData();
  }
}
