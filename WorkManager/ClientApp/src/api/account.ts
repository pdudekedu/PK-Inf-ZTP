import { HttpResponseCallback, post } from '../helpers/api';

interface LoginReqestDto {
  userName: string;
  password: string;
}

interface RegisterRequestDto {
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
}

export interface Account {
  userName: string;
  firstName: string;
  lastName: string;
  role: number;
}

export const login = async (
  requestDto: LoginReqestDto,
  callback: HttpResponseCallback<void>
) => post<void>('authorization/login', requestDto, callback);

export const register = async (
  requestDto: RegisterRequestDto,
  callback: HttpResponseCallback<void>
) => post<void>('authorization/register', requestDto, callback);
