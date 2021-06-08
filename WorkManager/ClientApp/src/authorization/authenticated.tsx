import { stringify } from 'query-string';
import React from 'react';
import { Redirect, useLocation } from 'react-router-dom';
import { pages } from '../helpers/pages';
import { useUserContext } from './user-context';

export const authenticated =
  <T extends object>(Component: React.ComponentType<T>) =>
  (props: T) => {
    const location = useLocation();
    const { user } = useUserContext();

    return user ? (
      <Component {...props} />
    ) : (
      <Redirect
        to={{
          pathname: pages.login,
          search: stringify({
            returnUrl: location.pathname,
          }),
        }}
      />
    );
  };
