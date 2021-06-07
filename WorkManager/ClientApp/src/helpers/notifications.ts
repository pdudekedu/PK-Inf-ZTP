import { notify } from 'react-notify-toast';

export const notifySuccess = (message: string) => {
  notify.show(message, 'success');
};

export const notifyError = (message: string) => {
  notify.show(message, 'error');
};

export const notifyErrors = (messages: string[]) => {
  notify.show(messages.join('\n'), 'error');
};
