import {Component} from 'react';

export default class Login extends Component{
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div>
        <form>
          <input id="login"/>
          <input id="password"/>
        </form>
      </div>
    );
  }
}

