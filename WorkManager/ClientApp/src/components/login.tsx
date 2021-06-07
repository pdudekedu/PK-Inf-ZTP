import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { login } from '../api/account';
import { pages } from '../helpers/pages';
import { getCurrentUrlParameter } from '../helpers/url';

export const Login = () => {
  const history = useHistory();

  const [inProgress, setInProgress] = useState(false);
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = () => {
    setInProgress(true);
    login(
      {
        userName,
        password,
      },
      {
        onSuccess: () => {
          const returnUrl = getCurrentUrlParameter('ReturnUrl');
          setTimeout(() => {
            if (returnUrl) {
              history.push(returnUrl);
            } else {
              history.push(pages.board);
            }
          }, 2000);
        },
        onFail: () => {
          setInProgress(false);
          return false;
        },
      }
    );
  };

  if (inProgress) {
    return (
      <div className='dsc-center-wrapper'>
        <div className='dsc-center-content container rounded shadow bg-white p-4 text-center'>
          <div className='h4'>Weryfikacja poświadczeń...</div>
        </div>
      </div>
    );
  }

  return (
    <div className='dsc-center-wrapper'>
      <form className='dsc-center-content container rounded shadow bg-white p-4'>
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
          value='Zaloguj'
          onClick={handleLogin}
        />
        <div className='mt-3'>
          Nie masz konta? <Link to={pages.register}>Zarejestruj się!</Link>
        </div>
      </form>
    </div>
  );
};
