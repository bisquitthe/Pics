import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import StackGrid from "react-stack-grid";
import { imageActions } from '../_actions';

class HomePage extends React.Component {
  componentDidMount() {
    console.error("LOH");
    this.props.dispatch(imageActions.getImages(1));
  }

  handleDeleteUser(id) {
    return (e) => this.props.dispatch(imageActions.deleteImage(id));
  }

  render() {
    const { user, images } = this.props;
    return (
      <div className="col-md-6 col-md-offset-3">
        <h1>Hi {user.login}!</h1>
        <p>
          <Link to="/login">Logout</Link>
        </p>
        <StackGrid>
          {images.items.map((image, index) =>
            <img key={image.id} src={image.filename} />
          )}
        </StackGrid>
      </div>
    );
  }
}

function mapStateToProps(state) {
  const { images, authentication } = state;
  const { user } = authentication;
  return {
    user,
    images
  };
}

const connectedHomePage = connect(mapStateToProps)(HomePage);
export { connectedHomePage as HomePage };