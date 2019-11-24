import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_model/Photo';
import { AuthService } from 'src/app/Services/Auth.service';
import { environment } from 'src/environments/environment';
import { AlertifyService } from 'src/app/Services/alertify.service';
import { FileUploader } from 'ng2-file-upload';
import { PhotoService } from 'src/app/Services/photo.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getMemberPhotoChenge = new EventEmitter<string>();

  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiURL;
  currentMain: Photo;

  constructor(private authService: AuthService,
      private photoService: PhotoService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + "users/" + this.authService.decodedToken.nameid + "/photos",
      authToken: "Bearer " + localStorage.getItem("token"),
      isHTML5: true,
      allowedFileType: ["image"],
      removeAfterUpload: true,
      autoUpload: true,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = false;//clear data in headers
    };
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain,
          approved: res.approved
        };
        if(res.isMain)
        {
          this.authService.changeMemberPhoto(photo.url);
          this.authService.currentUser.photoUrl = photo.url;
          localStorage.setItem( "user", JSON.stringify(this.authService.currentUser));
        }
        this.photos.push(photo);
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.photoService
      .setMainPhoto(this.authService.decodedToken.nameid, photo.id)
        .subscribe( () => {
            this.currentMain = this.photos.filter(p => p.isMain === true)[0];
            this.currentMain.isMain = false;
            photo.isMain = true;
            this.authService.changeMemberPhoto(photo.url);
            this.authService.currentUser.photoUrl = photo.url;
            localStorage.setItem( "user", JSON.stringify(this.authService.currentUser));
          },
          error => {
            this.alertify.error(error);
          }
      );
  }

  deletePhoto(id: number) {
    this.alertify.confirm("Are you sure, you want to delete this photo", () => {
      this.photoService
        .deletePhoto(this.authService.decodedToken.nameid, id)
        .subscribe(
          () => {
            this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
            this.alertify.success("photo have been deleted");
          },
          error => {
            this.alertify.error("failed to delete photo");
          }
        );
    });
  }

}
