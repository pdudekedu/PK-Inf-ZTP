import React, { useEffect, useState } from 'react';
import { getUserRoleName, getUsers, UserDto } from '../../api/user';
import { authenticatedManager } from '../../authorization/authenticatedManager';
import { toChunks } from '../../helpers/collections';
import { EditUserPanel } from './edit-user-panel';

export const Users = authenticatedManager(() => {
  const [users, setUsers] = useState<UserDto[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserDto | null>(null);

  useEffect(() => {
    getUsers({
      onSuccess: (response) => {
        setUsers(response.result);
      },
    });
  }, []);

  const handleUserUpdate = (user: UserDto) => {
    const toReplace = users.find((u) => u.id === user.id);
    if (toReplace) {
      const newUsers = [...users];
      newUsers[users.indexOf(toReplace)] = user;
      setUsers(newUsers);
    }
  };

  const handleUserRemove = (user: UserDto) => {
    const toReplace = users.find((u) => u.id === user.id);
    if (toReplace) {
      const newUsers = [...users];
      newUsers.splice(users.indexOf(toReplace));
      setUsers(newUsers);
    }
  };

  const handleCancel = () => setSelectedUser(null);

  return (
    <div className='p-3'>
      <div className='row'>
        <div className='col-9 pr-3'>
          {toChunks(users, 4).map((usersRow, i) => (
            <div className='row pb-3' key={i}>
              {usersRow.map((user) => (
                <div
                  key={user.id}
                  className='col-3'
                  onClick={() => setSelectedUser(user)}
                >
                  <div
                    className={`card vm-clicable ${
                      user === selectedUser ? 'bg-light border-primary' : ''
                    }`}
                  >
                    <div className='card-body'>
                      <h5 className='card-title'>
                        {user.firstName} {user.lastName}
                      </h5>
                      <div className='card-text'>{getUserRoleName(user)}</div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          ))}
        </div>
        <div className='col-3 pl-3'>
          <EditUserPanel
            user={selectedUser}
            onUpdated={handleUserUpdate}
            onCancel={handleCancel}
            onRemoved={handleUserRemove}
          />
        </div>
      </div>
    </div>
  );
});
