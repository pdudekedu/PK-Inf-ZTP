import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import {
  updateCurrentUserPassword,
  updateCurrentUserPersonalInfo,
} from '../api/user';
import { authenticated } from '../authorization/authenticated';
import { useUserContext } from '../authorization/user-context';
import { notifySuccess } from '../helpers/notifications';
import { FormInput } from './common/form-input';

export const Profile = authenticated(() => {
  const history = useHistory();
  const { user, updatePersonalInfo } = useUserContext();

  const [firstName, setFirstName] = useState(user?.firstName ?? '');
  const [lastName, setLastName] = useState(user?.lastName ?? '');

  const [oldPassword, setOldPassword] = useState('');
  const [password, setPassword] = useState('');

  const handleUpdateUserInfo = () => {
    updateCurrentUserPersonalInfo(
      {
        firstName,
        lastName,
      },
      {
        onSuccess: (response) => {
          const { firstName, lastName } = response.result;
          setFirstName(firstName);
          setLastName(lastName);

          updatePersonalInfo(firstName, lastName);

          notifySuccess('Dane użytkownika zaktualizowane poprawnie!');
        },
      }
    );
  };

  const handleUpdateUserPassword = () => {
    updateCurrentUserPassword(
      {
        oldPassword,
        password,
      },
      {
        onSuccess: (response) => {
          setOldPassword('');
          setPassword('');

          notifySuccess('Hasło zaktualizowane poprawnie!');
        },
      }
    );
  };

  return (
    <div className='container-fluid p-3'>
      <div className='row pb-3'>
        <div className='col-12'>
          <button
            className='btn btn-outline-secondary'
            onClick={() => history.goBack()}
          >
            Powrót
          </button>
        </div>
      </div>
      <div className='row'>
        <div className='col-6'>
          <div className='card'>
            <div className='card-body'>
              <FormInput
                id='firstName'
                label='Imię'
                value={firstName}
                onChange={setFirstName}
              />
              <FormInput
                id='lastName'
                label='Nazwisko'
                value={lastName}
                onChange={setLastName}
              />
              <input
                className='btn btn-block btn-primary'
                type='button'
                value='Aktualizuj dane'
                onClick={handleUpdateUserInfo}
              />
            </div>
          </div>
        </div>
        <div className='col-6'>
          <div className='card'>
            <div className='card-body'>
              <FormInput
                id='oldPassword'
                label='Stare hasło'
                value={oldPassword}
                onChange={setOldPassword}
                type='password'
              />
              <FormInput
                id='password'
                label='Nowe hasło'
                value={password}
                onChange={setPassword}
                type='password'
              />
              <input
                className='btn btn-block btn-primary'
                type='button'
                value='Aktualizuj hasło'
                onClick={handleUpdateUserPassword}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
});
