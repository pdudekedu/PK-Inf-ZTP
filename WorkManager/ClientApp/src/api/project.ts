import { del, get, HttpResponseCallback, post, put } from '../helpers/api';

interface ProjectTeamDto {
  id: number;
  name: string;
}

export interface ProjectResourceDto {
  id: number;
  name: string;
}

export interface ProjectDto {
  id: number;
  name: string;
  description: string | null;
  resources: ProjectResourceDto[];
  team: ProjectTeamDto;
}

interface ProjectRequestDto {
  name: string;
  description: string | null;
  resources: ProjectResourceDto[];
  team: ProjectTeamDto;
}

export const getProjects = async (
  callback: HttpResponseCallback<ProjectDto[]>
) => get<ProjectDto[]>('projects', callback);

export const createProject = async (
  request: ProjectRequestDto,
  callback: HttpResponseCallback<ProjectDto>
) => post<ProjectDto>('projects', request, callback);

export const updateProject = async (
  id: number,
  request: ProjectRequestDto,
  callback: HttpResponseCallback<ProjectDto>
) => put<ProjectDto>(`projects/${encodeURIComponent(id)}`, request, callback);

export const removeProject = async (
  id: number,
  callback: HttpResponseCallback<ProjectDto>
) => del<ProjectDto>(`projects/${encodeURIComponent(id)}`, callback);
