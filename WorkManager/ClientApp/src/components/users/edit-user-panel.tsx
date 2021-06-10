import React, { useEffect, useState } from 'react';
import { removeUser, updateUser, User, UserRole } from '../../api/user';
import { notifySuccess } from '../../helpers/notifications';
import { FormInput } from '../common/form-input';
import { FormSelect, FormSelectOption } from '../common/form-select';

const roleOptions: FormSelectOption[] = [
  { value: UserRole.Worker, label: 'Pracownik' },
  { value: UserRole.Manager, label: 'Manager' },
];

export const EditUserPanel = ({
  user,
  onUpdated,
  onRemoved,
  onCancel,
}: {
  user: User | null;
  onUpdated: (user: User) => void;
  onRemoved: (user: User) => void;
  onCancel: () => void;
}) => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState(UserRole.Worker);

  useEffect(() => {
    setFirstName(user?.firstName ?? '');
    setLastName(user?.lastName ?? '');
    setPassword('');
    setRole(user?.role ?? UserRole.Worker);
  }, [user]);

  const handleUpdateUser = () => {
    if (user) {
      updateUser(
        user.id,
        {
          firstName,
          lastName,
          password,
          role,
        },
        {
          onSuccess: (response) => {
            const { firstName, lastName, role } = response.result;

            setFirstName(firstName);
            setLastName(lastName);
            setPassword('');
            setRole(role);

            onUpdated(response.result);

            notifySuccess('Dane użytkownika zaktualizowane pomyślnie!');
          },
        }
      );
    }
  };

  const handleRemoveUser = () => {
    if (user) {
      removeUser(user.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Użytkownik usunięty pomyślnie!');
        },
      });
    }
  };

  return (
    <div>
      {!user && <div>Wybierz użytkownika do edycji</div>}
      {user && (
        <div>
          <FormInput
            id='firstName'
            label='Imię'
            value={firstName}
            onChange={setFirstName}
          />
          <FormInput
            id='lastName'
            label='Nazwisko'
            value={lastName}
            onChange={setLastName}
          />
          <FormSelect
            id='role'
            label='Rola'
            value={role}
            onChange={setRole}
            options={roleOptions}
          />
          <FormInput
            id='password'
            label='Resetuj hasło'
            value={password}
            onChange={setPassword}
            type='password'
          />
          <input
            className='btn btn-block btn-primary'
            type='button'
            value='Zapisz'
            onClick={handleUpdateUser}
          />
          <input
            className='btn btn-block btn-secondary'
            type='button'
            value='Anuluj'
            onClick={onCancel}
          />
          <input
            className='btn btn-block btn-danger'
            type='button'
            value='Usuń'
            onClick={handleRemoveUser}
          />
        </div>
      )}
    </div>
  );
};
