import React, { useEffect, useState } from 'react';
import {
  createTeam,
  removeTeam,
  TeamDto,
  TeamMemberDto,
  updateTeam,
} from '../../api/team';
import { getUsers, UserDto } from '../../api/user';
import { removeElementBy } from '../../helpers/collections';
import { notifySuccess } from '../../helpers/notifications';
import { ConfirmModal } from '../common/confirm-model';
import { FormInput } from '../common/form-input';
import { FormSelectWithButton } from '../common/form-select-with-button';
import { FormTextArea } from '../common/form-textarea';

const emptyUser: UserDto = {
  id: 0,
  firstName: '',
  lastName: '',
  role: 0,
  userName: '',
};

export const EditTeamPanel = ({
  team,
  onUpdated,
  onRemoved,
  onAdded,
  onCancel,
}: {
  team: TeamDto | null;
  onUpdated: (team: TeamDto) => void;
  onRemoved: (team: TeamDto) => void;
  onAdded: (team: TeamDto) => void;
  onCancel: () => void;
}) => {
  const [showConfirm, setShowConfirm] = useState(false);

  const [availableUsers, setAvailableUsers] = useState<UserDto[]>([emptyUser]);
  const [usersToAdd, setUsersToAdd] = useState<UserDto[]>([emptyUser]);
  const [userToAdd, setUserToAdd] = useState(0);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [users, setUsers] = useState<TeamMemberDto[]>([]);

  useEffect(() => {
    getUsers({
      onSuccess: (response) => {
        const users = [emptyUser, ...response.result];
        setAvailableUsers(users);
        setUsersToAdd(users);
      },
    });
  }, []);

  useEffect(() => {
    setName(team?.name ?? '');
    setDescription(team?.description ?? '');
    setUsers(team?.users ?? []);

    if (team) {
      setUsersToAdd(
        availableUsers.filter((u) => !team.users.some((tm) => tm.id === u.id))
      );
    }
  }, [team, availableUsers]);

  const handleSave = () => {
    if (team) {
      if (team.id) {
        updateTeam(
          team.id,
          {
            name,
            description: description ?? null,
            users,
          },
          {
            onSuccess: (response) => {
              const { name, description, users } = response.result;

              setName(name);
              setDescription(description ?? '');
              setUsers(users ?? []);

              onUpdated(response.result);

              notifySuccess('Dane zespołu zaktualizowane pomyślnie!');
            },
          }
        );
      } else {
        createTeam(
          {
            name,
            description: description ?? null,
            users,
          },
          {
            onSuccess: (response) => {
              onAdded(response.result);

              notifySuccess('Zespół dodany pomyślnie!');
            },
          }
        );
      }
    }
  };

  const handleCancel = () => onCancel();

  const handleRemove = () => {
    setShowConfirm(false);
    if (team) {
      removeTeam(team.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Zespół usunięty pomyślnie!');
        },
      });
    }
  };

  const handleAddUser = () => {
    if (userToAdd) {
      const user = availableUsers.find((x) => x.id === userToAdd);

      if (user) {
        if (!users.some((x) => x.id === user.id)) {
          setUsers([...users, user]);
        }

        setUsersToAdd(removeElementBy(usersToAdd, (u) => u.id === user.id));
        setUserToAdd(0);
      }
    }
  };

  const handleRemoveUser = (id: number) => {
    const user = availableUsers.find((x) => x.id === id);
    console.log(user);
    if (user) {
      if (!usersToAdd.some((x) => x.id === user.id)) {
        setUsersToAdd([...usersToAdd, user]);
      }

      setUsers(removeElementBy(users, (u) => u.id === user.id));
      setUserToAdd(0);
    }
  };

  return (
    <div>
      <ConfirmModal
        show={showConfirm}
        text='Czy na pewno chcesz usunąć zespół?'
        onCancel={() => setShowConfirm(false)}
        onConfirm={handleRemove}
      />
      {!team && <div>Wybierz zespół do edycji lub dodaj nowy</div>}
      {team && (
        <div>
          <FormInput
            id='name'
            label='Nazwa'
            value={name}
            onChange={setName}
            maxLength={200}
          />
          <FormTextArea
            id='description'
            label='Opis'
            value={description}
            onChange={setDescription}
          />
          <div className='form-group'>
            <label htmlFor='users'>Użytkownicy</label>
            <FormSelectWithButton
              id='users'
              value={userToAdd}
              options={usersToAdd.map(({ id, firstName, lastName }) => ({
                value: id,
                label: `${firstName} ${lastName}`,
              }))}
              onChange={setUserToAdd}
              onClick={handleAddUser}
            />
            <div
              data-testid='users-container'
              className='d-flex flex-wrap align-items-start bg-white wm-items-container'
            >
              {users.map(({ id, firstName, lastName }) => (
                <span
                  key={id}
                  className='d-flex flex-row align-items-center m-2 p-1 border border-secondary rounded'
                >
                  <span>
                    {firstName} {lastName}
                  </span>
                  <button
                    className='btn btn-light btn-sm ml-2'
                    onClick={() => handleRemoveUser(id)}
                  >
                    Usuń
                  </button>
                </span>
              ))}
            </div>
          </div>

          <input
            className='btn btn-block btn-primary'
            type='button'
            value='Zapisz'
            onClick={handleSave}
          />
          <input
            className='btn btn-block btn-secondary'
            type='button'
            value='Anuluj'
            data-testid='btn-cancel'
            onClick={handleCancel}
          />
          <input
            className='btn btn-block btn-danger'
            type='button'
            value='Usuń'
            onClick={() => setShowConfirm(true)}
          />
        </div>
      )}
    </div>
  );
};
