import { createContext, useContext } from 'react';
import { UserDto } from '../api/user';

export const UserContext = createContext<{
  user: UserDto | null;
  login: (user: UserDto) => void;
  updatePersonalInfo: (firstName: string, lastName: string) => void;
  logout: () => void;
}>({
  user: null,
  login: () => {},
  updatePersonalInfo: () => {},
  logout: () => {},
});

export const useUserContext = () => useContext(UserContext);
