import React, { Component } from 'react';

import { login } from '../../utils/auth';

export default class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: '',
      password: '',
      error: null,
    };

    this.handleInputChange = this.handleInputChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();

    login(this.state.email, this.state.password)
      .catch((err) => {
        this.setState({ error: err.message });
      });
  }

  handleInputChange(event) {
    const { target } = event;
    const { name } = target;
    const value = target.type === 'checkbox' ? target.checked : target.value;

    this.setState({
      [name]: value,
    });
  }

  render() {
    return (
      <div>
        <h2>Login</h2>
        <form onSubmit={this.handleSubmit}>
          {this.state.error}
          <div>
            <label htmlFor="email">E-mail</label>
            <input name="email" id="email" type="email" onChange={this.handleInputChange} value={this.state.email} />
          </div>
          <div>
            <label htmlFor="password">Password</label>
            <input name="password" id="password" type="text" onChange={this.handleInputChange} value={this.state.password} />
          </div>
          <input type="submit" value="Login" />
        </form>
      </div>
    );
  }
}
