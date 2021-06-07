import { parse } from 'query-string';

export const getUrl = (page: string) =>
  `${window.location.protocol}//${window.location.host}${page}`;

export const getCurrentUrlParameter = (name: string) =>
  parse(window.location.search)[name] as string;
