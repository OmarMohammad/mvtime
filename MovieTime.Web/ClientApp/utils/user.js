import betterFetch from './better-fetch';

const usersAPI = '/api/users/';

export function getUserData(id) {
  return betterFetch(usersAPI + id);
}

export function updateUserData(user, id) {
  return fetch(usersAPI + id, {
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    method: 'PUT',
    body: JSON.stringify(user),
  }).then(response => response);
}