import { get, HttpResponseCallback } from '../helpers/api';

export interface ProjectDto {
  id: number;
  name: string;
}

export const getProjects = async (
  callback: HttpResponseCallback<ProjectDto[]>
) => get<ProjectDto[]>('projects', callback);
