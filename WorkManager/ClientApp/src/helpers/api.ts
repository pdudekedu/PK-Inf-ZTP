import { getCookie, removeCookie } from './cookies';
import { notifyError, notifyErrors } from './notifications';

interface ErrorDetails {
  statusCode: number;
  messages: string[];
}

type HttpResponse<T> = {
  statusCode: number;
  result: T;
};

export enum HttpStatusCodes {
  OK = 200,
  Created = 201,
  NotFound = 404,
  Conflict = 409,
}

export interface HttpResponseCallback<T> {
  onSuccess: (response: HttpResponse<T>) => void;
  onFail?: (error: ErrorDetails) => boolean;
}

export const get = async <T>(
  url: string,
  callback: HttpResponseCallback<T>
): Promise<void> => await http<T>(url, 'get', null, callback);

export const post = async <T>(
  url: string,
  body: any,
  callback: HttpResponseCallback<T>
): Promise<void> => await http<T>(url, 'post', body, callback);

export const put = async <T>(
  url: string,
  body: any,
  callback: HttpResponseCallback<T>
): Promise<void> => await http<T>(url, 'put', body, callback);

export const patch = async <T>(
  url: string,
  body: any,
  callback: HttpResponseCallback<T>
): Promise<void> => await http<T>(url, 'patch', body, callback);

export const del = async <T>(
  url: string,
  callback: HttpResponseCallback<T>
): Promise<void> => await http<T>(url, 'delete', null, callback);

const http = async <T>(
  url: string,
  method: 'get' | 'post' | 'put' | 'delete' | 'patch',
  body: any | null,
  callback: HttpResponseCallback<T>
): Promise<void> => {
  let headers: { [x: string]: string } = {
    'Content-Type': 'application/json',
  };
  const token = getCookie('auth');
  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  const response = await fetch(`/api/${url}`, {
    method: method,
    body: body ? JSON.stringify(body) : null,
    headers,
  });

  let json: any;

  try {
    json = await response.json();
  } catch {
    notifyError('Wystąpił błąd przetważania odpowiedzi serwera.');
  }

  if (response.ok) {
    callback.onSuccess({
      statusCode: response.status,
      result: json,
    });
  } else {
    if (response.status === 401) {
      removeCookie('auth');
    }

    if (json && json.statusCode && json.messages) {
      const errorDetails = json as ErrorDetails;
      if (!callback.onFail || !callback.onFail(errorDetails)) {
        notifyErrors(errorDetails.messages);
      }
    } else {
      notifyError('Wystąpił błąd po stronie serwera.');
    }
  }
};
