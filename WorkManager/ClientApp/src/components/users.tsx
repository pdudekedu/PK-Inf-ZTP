import React, { useEffect, useState } from 'react';
import { getUsers, User } from '../api/user';
import { authenticatedManager } from '../authorization/authenticatedManager';

export const Users = authenticatedManager(() => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    getUsers({
      onSuccess: (response) => {
        setUsers(response.result);
      },
    });
  }, []);

  return (
    <div>
      {users.map((user) => (
        <div key={user.id}>
          {user.firstName} {user.lastName} - {user.role}
        </div>
      ))}
    </div>
  );
});
