/* eslint-disable react/no-unused-state */
import React from 'react';
import { withRouter } from 'react-router-dom';
import PropTypes from 'prop-types';
import cn from 'classnames';

import styles from './ListWidget.scss';
import MoviePoster from '../movie/MoviePoster';

class ListWidget extends React.Component {
  state = {
    movies: [],
    movieCount: null,
    firstLoad: true,
  };

  onClick(url) {
    this.props.history.push(url);
  }

  render() {
    if (this.props.movies.length === 0) {
      return null;
    }
    const { length } = this.props.movies;
    const movies = this.props.movies.slice(0, 4);
    return (
      <div className={styles.wrapper}>
        <h4>{this.props.title}</h4>
        <div className={styles['list-widget']}>
          {movies.map((element, index) => {
            if (length > 4 && index === 3) {
              return (<MoviePoster
                key={`movie-${element.movieId}`}
                className={cn(styles.poster, styles['last-poster'])}
                source={element.poster}
                alt={`${element.title} poster`}
                onClick={() => this.onClick(`/list?userId=${this.props.userId}&type=${this.props.type}`)}
              />
              );
            }
            return (<MoviePoster
              key={`movie-${element.movieId}`}
              className={styles.poster}
              source={element.poster}
              alt={`${element.title} poster`}
              onClick={() => this.onClick(`/movies/${element.movieId}`)}
            />);
          })}
        </div>
      </div>
    );
  }
}

ListWidget.propTypes = {
  movies: PropTypes.arrayOf(PropTypes.any).isRequired,
  history: PropTypes.objectOf(PropTypes.any).isRequired,
  title: PropTypes.string.isRequired,
  userId: PropTypes.string,
  type: PropTypes.string,
};

ListWidget.defaultProps = {
  userId: '',
  type: '',
};

export default withRouter(ListWidget);
