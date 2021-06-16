import { get, HttpResponseCallback } from '../helpers/api';

export interface UserStatisticDto {
  userId: number;
  firstName: string;
  lastName: string;
  workTime: string;
  estimateWorkTime: string;
  punctuality: number;
  taskCount: number;
  projectCount: number;
}

export const getUsersStats = async (
  callback: HttpResponseCallback<UserStatisticDto[]>
) => get<UserStatisticDto[]>('reports/users', callback);

export interface ProjectStatisticDto {
  projectId: number;
  name: string;
  description: string;
  new: number;
  active: number;
  suspend: number;
  complete: number;
  state: number;
  workTime: string;
  estimateWorkTime: string;
  punctuality: number;
  team: string;
}

export const getProjectsStats = async (
  callback: HttpResponseCallback<ProjectStatisticDto[]>
) => get<ProjectStatisticDto[]>('reports/projects', callback);
