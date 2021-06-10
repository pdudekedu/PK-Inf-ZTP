import { get, post, HttpResponseCallback, put, del } from '../helpers/api';

export enum UserRole {
  Worker = 0,
  Manager = 1,
}

export const getUserRoleName = (user: User) =>
  user.role === UserRole.Manager ? 'Menadżer' : 'Pracownik';

export interface User {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  role: UserRole;
}

interface UpdateCurrentUserPersonalInfoRequestDto {
  firstName: string;
  lastName: string;
}

interface UpdateCurrentUserPasswordRequestDto {
  oldPassword: string;
  password: string;
}

interface UpdateUserRequestDto {
  firstName: string;
  lastName: string;
  password: string;
  role: UserRole;
}

export const getCurrentUser = async (callback: HttpResponseCallback<User>) =>
  get<User>('users/current', callback);

export const updateCurrentUserPersonalInfo = async (
  request: UpdateCurrentUserPersonalInfoRequestDto,
  callback: HttpResponseCallback<User>
) => post<User>('users/current/personal-info', request, callback);

export const updateCurrentUserPassword = async (
  request: UpdateCurrentUserPasswordRequestDto,
  callback: HttpResponseCallback<User>
) => post<User>('users/current/password', request, callback);

export const getUsers = async (callback: HttpResponseCallback<User[]>) =>
  get<User[]>('users', callback);

export const updateUser = async (
  id: number,
  request: UpdateUserRequestDto,
  callback: HttpResponseCallback<User>
) => put<User>(`users/${encodeURIComponent(id)}`, request, callback);

export const removeUser = async (
  id: number,
  callback: HttpResponseCallback<User>
) => del<User>(`users/${encodeURIComponent(id)}`, callback);
