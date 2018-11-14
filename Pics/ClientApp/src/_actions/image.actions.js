import { imageConstants } from '../_constants';
import { imageService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const imageActions = {
  getImages,
  pagesCount,
  newImage,
  removeImage
};

function getImages(pageNumber) {
  return dispatch => {
    dispatch(request(pageNumber));

    imageService.getImages(pageNumber)
      .then(
        images => success(images),
        error => failure(error)
      );
  };

  function request(pageNumber) { return { type: imageConstants.GETIMAGES_REQUEST, pageNumber } }
  function success(images) { console.log(images);return { type: imageConstants.GETIMAGES_SUCCESS, images } }
  function failure(error) { return { type: imageConstants.GETIMAGES_FAILURE, error } }
}

function pagesCount() {
  return dispatch => {
    dispatch(request());

    imageService.pagesCount()
      .then(
        pagesCount => success(pagesCount),
        error => failure(error)
      );
  };

  function request() { return { type: imageConstants.PAGESCOUNT_REQUEST, payload:''} }
  function success(pagesCount) { return { type: imageConstants.PAGESCOUNT_SUCCESS, pagesCount } }
  function failure(error) { return { type: imageConstants.PAGESCOUNT_FAILURE, error } }
}

function newImage(file) {
  return dispatch => {
    dispatch(request(file));

    imageService.newImage(file)
      .then(
        _ => success(_),
        error => failure(error)
      );
  };

  function request(file) { return { type: imageConstants.NEW_REQUEST, payload: file } }
  function success(str) { return { type: imageConstants.NEW_SUCCESS, str } }
  function failure(error) { return { type: imageConstants.NEW_FAILURE, error } }
}

function removeImage(id) {
  return dispatch => {
    dispatch(request(id));

    imageService.removeImage(id)
      .then(
        _ => success(_),
        error => failure(error)
      );
  };

  function request(id) { return { type: imageConstants.REMOVE_REQUEST, payload: id } }

  function success(str) { return { type: imageConstants.REMOVE_SUCCESS, str } }

  function failure(error) { return { type: imageConstants.REMOVE_FAILURE, error } }
}