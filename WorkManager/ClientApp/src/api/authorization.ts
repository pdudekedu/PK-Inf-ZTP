import { HttpResponseCallback, post } from '../helpers/api';
import { UserRole } from './user';

interface LoginReqestDto {
  userName: string;
  password: string;
}

interface LoginResponseDto {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  role: UserRole;
  token: string;
}

interface RegisterRequestDto {
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
}

export const login = async (
  requestDto: LoginReqestDto,
  callback: HttpResponseCallback<LoginResponseDto>
) => post<LoginResponseDto>('authorization/login', requestDto, callback);

export const register = async (
  requestDto: RegisterRequestDto,
  callback: HttpResponseCallback<void>
) => post<void>('authorization/register', requestDto, callback);
