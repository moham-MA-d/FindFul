import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { UploadPhoto } from 'src/app/models/photo/uploadPhoto';
import { UserToken } from 'src/app/models/user/user';
import { userPhoto } from 'src/app/models/user/userPhoto';
import { AccountService } from 'src/app/services/account/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent implements OnInit {

    @Input() uploadPhoto: UploadPhoto;
    uploader:FileUploader;
    hasBaseDropZoneOver:boolean = false;
    //hasAnotherDropZoneOver:boolean;
    //response:string;
    baseUrl = environment.apiUrl;
    userToken: UserToken;
    photos: Observable<userPhoto[]>;


    constructor (private accountService: AccountService){
      this.accountService.currentUserToken$.pipe(take(1)).subscribe(u => this.userToken = u);
    }

    ngOnInit(): void {
      this.InitializeUploader();
      this.DragDrop();
    }


    public fileOverBase(e:any) {
      this.hasBaseDropZoneOver = e;
    }

    InitializeUploader() {
      this.uploader = new FileUploader({
        url: this.baseUrl + this.uploadPhoto.url,
        authToken: 'Bearer ' + this.userToken.token,
        isHTML5: true,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024* 1024
        //disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
        //formatDataFunctionIsAsync: true,
        // formatDataFunction: async (item) => {
        //   return new Promise( (resolve, reject) => {
        //     resolve({
        //       name: item._file.name,
        //       length: item._file.size,
        //       contentType: item._file.type,
        //       date: new Date()
        //     });
        //   });
        // }
      });

      // this.hasBaseDropZoneOver = false;
      // this.hasAnotherDropZoneOver = false;

      // this.response = '';

      // this.uploader.response.subscribe( res => this.response = res );

      this.uploader.onAfterAddingFile = (file) => {
        // we set `withCredentials` to `false` because we are sending `authToken` otherwise we needed to optimize our server API
        //    to get files with crenetials
        file.withCredentials = false;
      }

      this.uploader.onSuccessItem = (item, response, status, headers) => {
        if (response) {
          const photo = JSON.parse(response);
        }
      }

    }

    DragDrop(){
      //selecting all required elements
      const dropArea = document.querySelector(".drag-area"),
      dragText = dropArea.querySelector("header"),
      button = dropArea.querySelector("button"),
      input = dropArea.querySelector("input");

      const zoneInfo = document.querySelector(".upload-zone-info");


      let file; //this is a global variable and we'll use it inside multiple functions

      button.onclick = () => {
        input.click(); //if user click on the button then the input also clicked
      }

      input.addEventListener("change", function(){
        //getting user select file and [0] this means if user select multiple files then we'll select only the first one
        file = this.files[0];
        dropArea.classList.add("active");
        showFile(); //calling function
      });


      //If user Drag File Over DropArea
      dropArea.addEventListener("dragover", (event)=>{
        event.preventDefault(); //preventing from default behaviour
        dropArea.classList.add("active");
        dragText.textContent = "Release to Upload File";
      });

      //If user leave dragged File from DropArea
      dropArea.addEventListener("dragleave", ()=>{
          dropArea.classList.remove("active");
          dropArea.classList.remove("actived");
          zoneInfo.classList.remove("enabled");
        dragText.textContent = "Drag & Drop to Upload File";
      });

      //If user drop File on DropArea
      dropArea.addEventListener("drop", (event) => {
        event.preventDefault(); //preventing from default behaviour
        //getting user select file and [0] this means if user select multiple files then we'll select only the first one
        file = createDragEvent(event);

        showFile(); //calling function
      });

      function createDragEvent(e) {
        return e.dataTransfer.files[0];
      }

      function showFile() {
        let fileType = file.type; //getting selected file type
        console.log(" showFile : ", fileType);
        let validExtensions = ["image/jpeg", "image/jpg", "image/png"]; //adding some valid image extensions in array
        if(validExtensions.includes(fileType)){ //if user selected file is an image file
          let fileReader = new FileReader(); //creating new FileReader object
          fileReader.onload = ()=>{
            let fileURL = fileReader.result; //passing user file source in fileURL variable
            let imgTag = `<img src="${fileURL}" alt="" style="height: 100%;">`; //creating an img tag and passing user selected file source inside src attribute
            dropArea.classList.add("actived");
            zoneInfo.classList.add("enabled");
            dropArea.innerHTML = imgTag; //adding that created img tag inside dropArea container
          }
          fileReader.readAsDataURL(file);
        } else {
          alert("This is not an Image File!");
          dropArea.classList.remove("active");
          zoneInfo.classList.remove("enabled");

          dragText.textContent = "Drag & Drop to Upload File";
        }
      }

    }


    // public fileOverAnother(e:any):void {
    //   this.hasAnotherDropZoneOver = e;
    // }
  }
