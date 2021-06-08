export const setCookie = (name: string, value: string, maxAge?: number) => {
  document.cookie = `${name}=${value}; max-age=${
    maxAge ?? 60 * 60
  }; secure; path=/`;
};

export const getCookie = (name: string) => {
  const cookieValue = document.cookie
    .split('; ')
    .find((row) => row.startsWith(`${name}=`));

  if (cookieValue) {
    return cookieValue.split('=')[1];
  }

  return null;
};

export const removeCookie = (name: string) => {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; secure; path=/`;
};
