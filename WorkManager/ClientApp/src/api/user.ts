import { get, post, HttpResponseCallback } from '../helpers/api';

export enum UserRole {
  Worker = 0,
  Manager = 1,
}

export interface User {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  role: UserRole;
}

export interface UpdateCurrentUserPersonalInfoRequestDto {
  firstName: string;
  lastName: string;
}

export interface UpdateCurrentUserPasswordRequestDto {
  oldPassword: string;
  password: string;
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
