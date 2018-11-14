import {Image} from './image';

export class ImagesWithPagingInfo {
  images: Image[];
  totalCount: number;
  countPerPage: number;
}