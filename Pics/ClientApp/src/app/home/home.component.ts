import { Component, OnInit, ViewChild } from '@angular/core';
import { first } from 'rxjs/operators';

import { User, Image, ImagesWithPagingInfo } from '../_models';
import { UserService, ImageService } from '../_services';
import { DomSanitizer } from '@angular/platform-browser';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  faTrash = faTrash;
  currentUser: User;
  images: Image[];
  uploadingFile: File;
  totalImagesCount: number;
  countPerPage: number;
  currentPage: number = 1;
  @ViewChild('file') file;
  constructor(private userService: UserService, private imageService: ImageService, private sanitizer: DomSanitizer) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.loadImages(this.currentPage);
  }

  handlePage(e) {
    this.currentPage = e;
    this.loadImages(e);
  }

  private loadImages(pageNumber: number) {
    this.imageService.getImages(pageNumber).pipe(first()).subscribe(imagesInfo => {
      this.images = imagesInfo.images;
      this.images.forEach(image => {
        image.safeBase64 = this.sanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + image.base64);
      });
      this.totalImagesCount = imagesInfo.totalCount;
      this.countPerPage = imagesInfo.countPerPage;
    });
  }

  private newImage(file: File) {
    this.imageService.newImage(file).pipe(first()).subscribe(image => {
      if (this.images.length < this.countPerPage) {
        image.safeBase64 = this.sanitizer.bypassSecurityTrustUrl('data:image/jpg;base64,' + image.base64);
        this.images.push(image);
        this.totalImagesCount++;
      }
    }, error => { });
  }

  removeImage(id: string) {
    this.imageService.removeImage(id).pipe(first()).subscribe(() => {
      const index = this.images.findIndex(img => img.id === id);
      this.images.splice(index, 1);
      this.totalImagesCount--;
      this.loadImages(this.currentPage);
    });
  }

  handleClick() {
    this.newImage(this.uploadingFile);
  }

  handleFileChange(e) {
    this.uploadingFile = e.srcElement.files[0];
  }
}
