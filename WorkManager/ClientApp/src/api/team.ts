import { get, post, HttpResponseCallback, put, del } from '../helpers/api';

export interface TeamMemberDto {
  id: number;
  firstName: string;
  lastName: string;
}

export interface TeamDto {
  id: number;
  name: string;
  description: string | null;
  users: TeamMemberDto[];
}

interface TeamRequestDto {
  name: string;
  description: string | null;
  users: TeamMemberDto[];
}

export const getTeams = async (callback: HttpResponseCallback<TeamDto[]>) =>
  get<TeamDto[]>('teams', callback);

export const createTeam = async (
  request: TeamRequestDto,
  callback: HttpResponseCallback<TeamDto>
) => post<TeamDto>('teams', request, callback);

export const updateTeam = async (
  id: number,
  request: TeamRequestDto,
  callback: HttpResponseCallback<TeamDto>
) => put<TeamDto>(`teams/${encodeURIComponent(id)}`, request, callback);

export const removeTeam = async (
  id: number,
  callback: HttpResponseCallback<TeamDto>
) => del<TeamDto>(`teams/${encodeURIComponent(id)}`, callback);
