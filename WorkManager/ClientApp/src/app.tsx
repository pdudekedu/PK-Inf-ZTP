import React from 'react';
import { BrowserRouter, Route, Switch, Redirect } from 'react-router-dom';
import Notifications from 'react-notify-toast';
import { pages } from './helpers/pages';
import { Login } from './components/login';
import { Register } from './components/register';
import { Board } from './components/board';

export const App = () => {
  return (
    <>
      <Notifications />
      <BrowserRouter>
        <Switch>
          <Route exact path={pages.login} component={Login}></Route>
          <Route exact path={pages.register} component={Register}></Route>
          <Route exact path={pages.board} component={Board}></Route>
          <Route render={() => <Redirect to={pages.board} />} />
        </Switch>
      </BrowserRouter>
    </>
  );
};
