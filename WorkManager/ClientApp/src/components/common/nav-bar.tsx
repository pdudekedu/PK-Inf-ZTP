import React from 'react';
import { Link, useHistory, useLocation } from 'react-router-dom';
import { getUserRoleName, UserRole } from '../../api/user';
import { useUserContext } from '../../authorization/user-context';
import { pages } from '../../helpers/pages';

export const NavBar = () => {
  const location = useLocation();
  const history = useHistory();
  const { user, logout } = useUserContext();

  const handleLogout = () => {
    logout();
    history.push(pages.login);
  };

  const getUserSide = () => {
    if (user) {
      return (
        <div className='dropdown'>
          <button
            className='nav-item btn btn-primary dropdown-toggle rounded-pill pt-1 pb-1 d-inline-flex align-items-center'
            id='navbarUserActionsDropdown'
            type='button'
            data-toggle='dropdown'
            aria-haspopup='true'
            aria-expanded='false'
          >
            {`${user.firstName} ${user.lastName}`}
          </button>
          <div
            className='dropdown-menu dropdown-menu-right'
            aria-labelledby='navbarUserActionsDropdown'
          >
            <div className='small text-center text-secondary'>
              {getUserRoleName(user)}
            </div>
            <div className='dropdown-divider'></div>
            <Link className='dropdown-item' to={pages.profile}>
              Edytuj profil
            </Link>
            <button className='dropdown-item' onClick={handleLogout}>
              Wyloguj
            </button>
          </div>
        </div>
      );
    } else {
      return null;
    }
  };

  const getLinkClass = (page: string) => {
    if (location.pathname === page) {
      return 'nav-item active';
    }
    return 'nav-item';
  };

  const getMenuOptions = () => {
    if (!user) {
      return null;
    }

    if (user.role === UserRole.Manager) {
      return (
        <>
          <li className={getLinkClass(pages.board)} key='nav-board'>
            <Link className='nav-link' to={pages.board}>
              Tablica
            </Link>
          </li>
          <li className={getLinkClass(pages.resources)} key='nav-resources'>
            <Link className='nav-link' to={pages.resources}>
              Zasoby
            </Link>
          </li>
          <li className={getLinkClass(pages.users)} key='nav-users'>
            <Link className='nav-link' to={pages.users}>
              Użytkownicy
            </Link>
          </li>
        </>
      );
    } else {
      return (
        <>
          <li className={getLinkClass(pages.board)} key='nav-board'>
            <Link className='nav-link' to={pages.board}>
              Tablica
            </Link>
          </li>
        </>
      );
    }
  };

  return (
    <nav className='navbar navbar-expand fixed-top navbar-dark bg-dark text-light'>
      <Link className='navbar-brand' to={pages.board}>
        WorkManager
      </Link>
      <ul className='navbar-nav'>{getMenuOptions()}</ul>
      <div className='ml-auto'>{getUserSide()}</div>
    </nav>
  );
};
