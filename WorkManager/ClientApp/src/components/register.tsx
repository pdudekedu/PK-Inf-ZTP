import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { register } from '../api/account';
import { notifySuccess } from '../helpers/notifications';
import { pages } from '../helpers/pages';

export const Register = () => {
  const history = useHistory();

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  const handleRegister = () => {
    register(
      {
        firstName,
        lastName,
        userName,
        password,
      },
      {
        onSuccess: () => {
          notifySuccess(
            'Rejestracja przebiegła pomyślnie, możesz się już zalogować na swoje konto'
          );
          history.push(pages.login);
        },
      }
    );
  };

  return (
    <div className='dsc-center-wrapper'>
      <form className='dsc-center-content container rounded shadow bg-white p-4'>
        <div className='form-group'>
          <label htmlFor='firstNameInput'>Imię</label>
          <input
            type='text'
            className='form-control'
            id='firstNameInput'
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        </div>
        <div className='form-group'>
          <label htmlFor='lastNameInput'>Nazwisko</label>
          <input
            type='text'
            className='form-control'
            id='lastNameInput'
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </div>
        <div className='form-group'>
          <label htmlFor='userNameInput'>Nazwa użytkownika</label>
          <input
            type='text'
            className='form-control'
            id='userNameInput'
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
        </div>
        <div className='form-group'>
          <label htmlFor='passwordInput'>Hasło</label>
          <input
            type='password'
            className='form-control'
            id='passwordInput'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <input
          className='btn btn-block btn-primary mt-4'
          type='button'
          value='Rejestruj'
          onClick={handleRegister}
        />
        <div className='mt-3 text-center'>
          <Link to={pages.login}>Przejdź do strony logowania</Link>
        </div>
      </form>
    </div>
  );
};
