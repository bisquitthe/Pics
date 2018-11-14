import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { User } from '../_models';

import { Image } from '../_models';
import { ImagesWithPagingInfo } from '../_models/imagesWithPagingInfo';

@Injectable()
export class ImageService {
    constructor(private http: HttpClient) { }

    getImages(pagenumber: number) {
        return this.http.get<ImagesWithPagingInfo>(`${environment.apiUrl}/images?page=${pagenumber}`);
    }

    removeImage(id: string) {
        return this.http.delete(`${environment.apiUrl}/images/remove?id=${id}`);
    }

    newImage(file: File) {
        const formData: FormData = new FormData();
        formData.append('uploadingFile', file, file.name);
        return this.http.post<Image>(`${environment.apiUrl}/images/new`, formData);
    }
}
