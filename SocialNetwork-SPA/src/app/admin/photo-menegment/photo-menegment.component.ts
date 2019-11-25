import { Component, OnInit } from '@angular/core';
import { Photo } from 'src/app/_model/Photo';
import { AdminService } from 'src/app/Services/admin.service';
import { AlertifyService } from 'src/app/Services/alertify.service';
import { PhotoService } from 'src/app/Services/photo.service';

@Component({
  selector: 'app-photo-menegment',
  templateUrl: './photo-menegment.component.html',
  styleUrls: ['./photo-menegment.component.css']
})
export class PhotoMenegmentComponent implements OnInit {
  photos: Photo[];
  constructor(
    private adminService: AdminService,
    private alertify: AlertifyService,
    private photoService: PhotoService
  ) { }

  ngOnInit() {
    this.getPhotosForModeration();
  }

  getPhotosForModeration() {
    this.adminService.getPhotosForModeration().subscribe(
      photos => {
        this.photos = photos;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  approvePhoto(photoId: number) {
    this.adminService.approvePhoto(photoId).subscribe(
      () => {
        this.photos.splice(
          // tslint:disable-next-line: triple-equals
          this.photos.findIndex(p => p.id == photoId),
          1
        );
        this.alertify.success('Photo Was upproved!');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  deletePhoto(id: number) {
    this.adminService.deletePhoto(id).subscribe(
      () => {
        this.photos.splice(
          this.photos.findIndex(p => p.id === id),
          1
        );
        this.alertify.success('photo have been deleted');
      },
      error => {
        this.alertify.error('failed to delete photo');
      }
    );
  }
}
