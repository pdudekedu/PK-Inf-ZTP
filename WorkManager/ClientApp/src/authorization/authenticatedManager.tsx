import { stringify } from 'query-string';
import React from 'react';
import { Redirect, useLocation } from 'react-router-dom';
import { UserRole } from '../api/user';
import { pages } from '../helpers/pages';
import { useUserContext } from './user-context';

export const authenticatedManager =
  <T extends object>(Component: React.ComponentType<T>) =>
  (props: T) => {
    const location = useLocation();
    const { user } = useUserContext();

    if (!user) {
      return (
        <Redirect
          to={{
            pathname: pages.login,
            search: stringify({
              returnUrl: location.pathname,
            }),
          }}
        />
      );
    } else if (user.role !== UserRole.Manager) {
      return <Redirect to={pages.board} />;
    } else {
      return <Component {...props} />;
    }
  };
