import React, { useCallback, useEffect, useState } from 'react';
import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';
import Notifications from 'react-notify-toast';
import { pages } from './helpers/pages';
import { Login } from './components/login';
import { Register } from './components/register';
import { Board } from './components/board';
import { Users } from './components/users';
import { getCurrentUser, User } from './api/user';
import { UserContext } from './authorization/user-context';
import { getCookie, removeCookie } from './helpers/cookies';
import { Profile } from './components/profile';
import { NavBar } from './components/common/nav-bar';

export const App = () => {
  const [user, setUser] = useState<User | null | 'unverified'>('unverified');

  useEffect(() => {
    if (getCookie('auth')) {
      getCurrentUser({
        onSuccess: (response) => {
          setUser(response.result);
        },
        onFail: () => {
          setUser(null);
          return true;
        },
      });
    } else {
      setUser(null);
    }
  }, []);

  const login = useCallback((user: User) => setUser(user), []);

  const updatePersonalInfo = useCallback(
    (firstName: string, lastName: string) =>
      setUser((u) =>
        u && u !== 'unverified' ? { ...u, firstName, lastName } : u
      ),
    []
  );

  const logout = useCallback(() => {
    removeCookie('auth');
    setUser(null);
  }, []);

  if (user === 'unverified') {
    return null;
  }

  return (
    <BrowserRouter>
      <UserContext.Provider value={{ user, login, logout, updatePersonalInfo }}>
        <Notifications />
        <NavBar />
        <div className='dsc-container'>
          <Switch>
            <Route exact path={pages.login} component={Login}></Route>
            <Route exact path={pages.register} component={Register}></Route>
            <Route exact path={pages.profile} component={Profile}></Route>
            <Route exact path={pages.board} component={Board}></Route>
            <Route exact path={pages.users} component={Users}></Route>
            <Route render={() => <Redirect to={pages.board} />} />
          </Switch>
        </div>
      </UserContext.Provider>
    </BrowserRouter>
  );
};
