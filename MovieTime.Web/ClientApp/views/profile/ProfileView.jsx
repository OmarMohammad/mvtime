import React from 'react';
import { connect } from 'react-redux';
import PropTypes from 'prop-types';

import { updateUser, getUser } from '../../modules/users';
import { authenticateById } from '../../modules/auth';

import ListWidget from '../../components/list-widget/ListWidget';
import Placeholder from '../../components/placeholder/Placeholder';
import Button from '../../components/button/Button';
import ProfilePicture from '../../components/profile/ProfilePicture';
import EditProfileModal from '../../components/profile/EditProfileModal';

import styles from './ProfileView.scss';

class ProfileView extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      canEditProfile: props.authId === (props.user && props.user.id),
      isLoading: true,
      isEditing: false,
      movies: [
        'Thor: Ragnarok',
        'Thor: Ragnarok',
        'Thor: Ragnarok',
        'Thor: Ragnarok',
        'Thor: Ragnarok',
      ],
    };
  }

  componentDidMount() {
    const { id } = this.props.match.params;
    this.props.getUser(id);
  }

  componentWillReceiveProps(nextProps) {
    if (Object.prototype.hasOwnProperty.call(nextProps.user, 'id')) {
      this.setState({
        isLoading: false,
        canEditProfile: nextProps.authId === (nextProps.user && nextProps.user.id),
      });
    } else if (this.props.user.id !== nextProps.match.params.id) {
      this.setState({
        isLoading: true,
      });
      this.props.getUser(nextProps.match.params.id);
    }
  }

  onEdit() {
    this.setState({
      isEditing: true,
    });
  }

  onDiscard() {
    this.setState({
      isEditing: false,
    });
  }

  onUpdate(user) {
    this.props.updateUser(user).then(() => {
      if (this.props.authId === user.id) {
        // re-authenticate so username in navigation updates
        this.props.authenticateById(this.props.authId);
      }
    });
  }

  render() {
    const { canEditProfile, isLoading } = this.state;
    const { firstName, lastName } = this.props.user;
    const { id } = this.props.match.params;

    return (
      <div className={styles.view}>
        <EditProfileModal
          hidden={!this.state.isEditing}
          hideModal={() => this.onDiscard()}
          onUpdate={user => this.onUpdate({ ...user, id })}
          user={this.props.user}
        />
        <div className={styles.view__background} />
        <div className={styles.view__header}>
          <div className={styles.header}>
            <div className={styles.header__picture}>
              <ProfilePicture
                className={styles.picture}
                source={`/assets/users/${id}.png`}
              />
            </div>
            <div className={styles.header__content}>
              <div className={styles.name}>
                <Placeholder isReady={!isLoading}>
                  <h1>{`${firstName} ${lastName}`}</h1>
                  <h3>
                    has watched ... movies worthy of ... hours and ... minutes
                  </h3>
                </Placeholder>
              </div>
            </div>
          </div>
          <div className={styles.buttons__container}>
            <div className={styles.buttons}>
              {canEditProfile &&
              <Button
                dark
                icon="pencil"
                onClick={() => this.onEdit()}
              >
                                    Edit
              </Button>}
              {/* <Button dark icon="user">Follow</Button> */}
            </div>
          </div>
          <div className={styles.content}>
            <ListWidget
              title="Wants to watch"
              movies={this.state.movies}
              history={this.props.history}
            />
            <ListWidget
              title="Has watched"
              movies={this.state.movies}
              history={this.props.history}
            />
          </div>
        </div>
      </div>
    );
  }
}

ProfileView.propTypes = {
  match: PropTypes.objectOf(PropTypes.any).isRequired,
  history: PropTypes.objectOf(PropTypes.any).isRequired,
  updateUser: PropTypes.func.isRequired,
  getUser: PropTypes.func.isRequired,
  authenticateById: PropTypes.func.isRequired,
  user: PropTypes.objectOf(PropTypes.any),
  authId: PropTypes.string,
};

ProfileView.defaultProps = {
  user: {},
  authId: null,
};

const mapStateToProps = (state, props) => ({
  user: state.users[props.match.params.id] || {},
  authId: state.auth.user && state.auth.user.id,
});

const mapDispatchToProps = {
  updateUser,
  getUser,
  authenticateById,
};

export default connect(mapStateToProps, mapDispatchToProps)(ProfileView);
