import {
  del,
  get,
  HttpResponseCallback,
  patch,
  post,
  put,
} from '../helpers/api';

export enum TaskState {
  New,
  InProgress,
  Waiting,
  Done,
}

export const TaskStateFlow = {
  [TaskState.New]: [TaskState.InProgress],
  [TaskState.InProgress]: [TaskState.Waiting, TaskState.Done],
  [TaskState.Waiting]: [TaskState.InProgress, TaskState.Done],
  [TaskState.Done]: [TaskState.InProgress],
};

export const TaskStateAction = {
  [TaskState.New]: 'Otwórz',
  [TaskState.InProgress]: 'Rozpocznij',
  [TaskState.Waiting]: 'Wstrzymaj',
  [TaskState.Done]: 'Zamknij',
};

export interface TaskDto {
  id: number;
  name: string;
  state: TaskState;
  description: string | null;
  // estimateStart: Date | null;
  // estimateEnd: Date | null;
}

export interface TaskRequestDto {
  name: string;
  description: string | null;
  // estimateStart: Date | null;
  // estimateEnd: Date | null;
}

const getTasksUrlFor = (projectId: number, taskId?: number) =>
  `projects/${encodeURIComponent(projectId)}/tasks${
    taskId ? `/${encodeURIComponent(taskId)}` : ''
  }`;

export const getTasksFor = async (
  projectId: number,
  callback: HttpResponseCallback<TaskDto[]>
) => get<TaskDto[]>(getTasksUrlFor(projectId), callback);

export const createTask = async (
  projectId: number,
  request: TaskRequestDto,
  callback: HttpResponseCallback<TaskDto>
) => post<TaskDto>(getTasksUrlFor(projectId), request, callback);

export const updateTask = async (
  projectId: number,
  taskId: number,
  request: TaskRequestDto,
  callback: HttpResponseCallback<TaskDto>
) => put<TaskDto>(getTasksUrlFor(projectId, taskId), request, callback);

export const updateTaskState = async (
  projectId: number,
  taskId: number,
  state: TaskState,
  callback: HttpResponseCallback<TaskDto>
) => patch<TaskDto>(getTasksUrlFor(projectId, taskId), { state }, callback);

export const removeTask = async (
  projectId: number,
  taskId: number,
  callback: HttpResponseCallback<TaskDto>
) => del<TaskDto>(getTasksUrlFor(projectId, taskId), callback);
