import React from 'react';
import { Redirect } from 'react-router-dom';
import { pages } from '../helpers/pages';
import { useUserContext } from './user-context';

export const anonymous =
  <T extends object>(Component: React.ComponentType<T>) =>
  (props: T) => {
    const { user } = useUserContext();

    return !user ? <Component {...props} /> : <Redirect to={pages.board} />;
  };
