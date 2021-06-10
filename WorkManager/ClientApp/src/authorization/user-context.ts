import { createContext, useContext } from 'react';
import { User } from '../api/user';

export const UserContext = createContext<{
  user: User | null;
  login: (user: User) => void;
  updatePersonalInfo: (firstName: string, lastName: string) => void;
  logout: () => void;
}>({
  user: null,
  login: () => {},
  updatePersonalInfo: () => {},
  logout: () => {},
});

export const useUserContext = () => useContext(UserContext);
