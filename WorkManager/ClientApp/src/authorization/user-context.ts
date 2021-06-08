import { createContext, useContext } from 'react';
import { User } from '../api/user';

export const UserContext = createContext<{
  user: User | null;
  login: (user: User) => void;
  logout: () => void;
}>({
  user: null,
  login: () => {},
  logout: () => {},
});

export const useUserContext = () => useContext(UserContext);
