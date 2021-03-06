import React, { useCallback, useEffect, useState } from 'react';
import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';
import Notifications from 'react-notify-toast';
import { pages } from './helpers/pages';
import { Login } from './components/login';
import { Register } from './components/register';
import { Board } from './components/board/board';
import { Users } from './components/users/users';
import { Resources } from './components/resources/resources';
import { Teams } from './components/teams/teams';
import { getCurrentUser, UserDto } from './api/user';
import { UserContext } from './authorization/user-context';
import { getCookie, removeCookie } from './helpers/cookies';
import { Profile } from './components/profile';
import { NavBar } from './components/common/nav-bar';
import { Projects } from './components/projects/projects';
import { ProjectsState } from './components/projects-state';
import { WorkTime } from './components/work-time';

export const App = () => {
  const [user, setUser] = useState<UserDto | null | 'unverified'>('unverified');

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

  const login = useCallback((user: UserDto) => setUser(user), []);

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
            <Route exact path={pages.resources} component={Resources}></Route>
            <Route exact path={pages.teams} component={Teams}></Route>
            <Route exact path={pages.projects} component={Projects}></Route>
            <Route
              exact
              path={pages.projectsState}
              component={ProjectsState}
            ></Route>
            <Route exact path={pages.workTime} component={WorkTime}></Route>
            <Route render={() => <Redirect to={pages.board} />} />
          </Switch>
        </div>
      </UserContext.Provider>
    </BrowserRouter>
  );
};
