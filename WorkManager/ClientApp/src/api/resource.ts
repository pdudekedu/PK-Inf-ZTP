import { get, post, HttpResponseCallback, put, del } from '../helpers/api';

export interface ResourceDto {
  id: number;
  name: string;
  description: string | null;
}

interface ResourceRequestDto {
  name: string;
  description: string;
}

export const getResources = async (
  callback: HttpResponseCallback<ResourceDto[]>
) => get<ResourceDto[]>('resources', callback);

export const createResource = async (
  request: ResourceRequestDto,
  callback: HttpResponseCallback<ResourceDto>
) => post<ResourceDto>('resources', request, callback);

export const updateResource = async (
  id: number,
  request: ResourceRequestDto,
  callback: HttpResponseCallback<ResourceDto>
) => put<ResourceDto>(`resources/${encodeURIComponent(id)}`, request, callback);

export const removeResource = async (
  id: number,
  callback: HttpResponseCallback<ResourceDto>
) => del<ResourceDto>(`resources/${encodeURIComponent(id)}`, callback);
