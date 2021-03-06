import React from 'react';
import PropTypes from 'prop-types';

import { login } from '../../utils/auth';

import Modal from './Modal';
import styles from './LoginModal.scss';
import Input from '../input/Input';
import Button from '../button/Button';

class LoginModal extends React.Component {
  state = {
    password: '',
    email: this.props.email,
  };

  onChange(e) {
    this.setState({
      error: null,
      password: e.target.value,
    });
  }

  onSuccess(email, password) {
    this.props.onSuccess(email, password);
  }

  onDiscard() {
    this.props.onDiscard();
  }

  login() {
    login(this.state.email, this.state.password)
      .then(() => {
        this.onSuccess(this.state.email, this.state.password);
      })
      .catch((err) => {
        this.setState({ error: err.message });
      });
  }

  render() {
    return (
      <Modal className={styles.login} hidden={this.props.hidden} title="Don't leave us, please :c" hideModal={() => this.onDiscard()}>
        <span className={styles.error}>{this.state.error}</span>
        <Input name="email" label="Email" value={this.props.email} disabled />
        <Input name="password" label="Password" type="password" value={this.state.password} onChange={e => this.onChange(e)} />
        <hr />
        <div className={styles.buttons}>
          <Button onClick={() => this.onDiscard()}>Cancel</Button>
          <Button danger onClick={() => this.login()}>Delete me!</Button>
        </div>
      </Modal>
    );
  }
}

LoginModal.propTypes = {
  hidden: PropTypes.bool,
  email: PropTypes.string.isRequired,
  onSuccess: PropTypes.func.isRequired,
  onDiscard: PropTypes.func.isRequired,
};

LoginModal.defaultProps = {
  hidden: false,
};

export default LoginModal;
