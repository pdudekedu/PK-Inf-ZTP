import { get, post, HttpResponseCallback, put, del } from '../helpers/api';

export enum UserRole {
  Worker = 0,
  Manager = 1,
}

export const getUserRoleName = (user: UserDto) =>
  user.role === UserRole.Manager ? 'Menadżer' : 'Pracownik';

export interface UserDto {
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

export const getCurrentUser = async (callback: HttpResponseCallback<UserDto>) =>
  get<UserDto>('users/current', callback);

export const updateCurrentUserPersonalInfo = async (
  request: UpdateCurrentUserPersonalInfoRequestDto,
  callback: HttpResponseCallback<UserDto>
) => post<UserDto>('users/current/personal-info', request, callback);

export const updateCurrentUserPassword = async (
  request: UpdateCurrentUserPasswordRequestDto,
  callback: HttpResponseCallback<UserDto>
) => post<UserDto>('users/current/password', request, callback);

export const getUsers = async (callback: HttpResponseCallback<UserDto[]>) =>
  get<UserDto[]>('users', callback);

export const updateUser = async (
  id: number,
  request: UpdateUserRequestDto,
  callback: HttpResponseCallback<UserDto>
) => put<UserDto>(`users/${encodeURIComponent(id)}`, request, callback);

export const removeUser = async (
  id: number,
  callback: HttpResponseCallback<UserDto>
) => del<UserDto>(`users/${encodeURIComponent(id)}`, callback);
