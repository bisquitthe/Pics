import { imageConstants } from '../_constants';

export function images(state = {}, action) {
  switch (action.type) {
    case imageConstants.GETIMAGES_REQUEST:
      return {
        loading: true
      };
    case imageConstants.GETIMAGES_SUCCESS:
      return {
        items: action.users
      };
    case imageConstants.GETIMAGES_FAILURE:
      return {
        error: action.error
      };
    default:
      return state;
  }
}