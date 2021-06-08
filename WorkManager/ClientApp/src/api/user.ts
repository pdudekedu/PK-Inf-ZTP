import { get, HttpResponseCallback } from '../helpers/api';

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

export const getCurrentUser = async (callback: HttpResponseCallback<User>) =>
  get<User>('users/current', callback);

export const getUsers = async (callback: HttpResponseCallback<User[]>) =>
  get<User[]>('users', callback);
