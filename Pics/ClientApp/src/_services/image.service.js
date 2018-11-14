import { authHeader } from '../_helpers';

export const imageService = {
  getImages,
  pagesCount,
  removeImage,
  newImage
};

function getImages(pageNumber) {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`${constants.getImages}/${pageNumber}`, requestOptions)
    .then(handleResponse);
}

function pagesCount() {
  const requestOptions = {
    method: 'GET',
    headers: authHeader()
  };

  return fetch(`${constants.getPagesCount}`, requestOptions)
    .then(handleResponse);
}

function removeImage(id) {
  const requestOptions = {
    method: 'DELETE',
    headers: authHeader()
  };

  return fetch(`${constants.removeImage}/${id}`, requestOptions)
    .then(handleResponse);
}

function newImage(file) {
  const requestOptions = {
    method: 'POST',
    headers: authHeader(),
    body: file
  };

  return fetch(`${constants.newImage}`, requestOptions)
    .then(handleResponse);
}

function handleResponse(response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}