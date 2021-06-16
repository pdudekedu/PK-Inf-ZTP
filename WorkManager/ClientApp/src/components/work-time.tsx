import React, { useEffect, useState } from 'react';
import { getUsersStats, UserStatisticDto } from '../api/reports';
import { toChunks } from '../helpers/collections';

export const WorkTime = () => {
  const [users, setUsers] = useState<UserStatisticDto[]>([]);

  useEffect(() => {
    getUsersStats({
      onSuccess: (response) => {
        setUsers(response.result);
      },
    });
  }, []);

  return (
    <div className='p-3'>
      {toChunks(users, 4).map((usersRow, i) => (
        <div className='row pb-3' key={i}>
          {usersRow.map((user) => (
            <div key={user.userId} className='col-3'>
              <div className={`card`}>
                <div className='card-body'>
                  <h5 className='card-title font-weight-bold'>
                    {user.firstName} {user.lastName}
                  </h5>
                  <div className='card-text small'>
                    <div className='row no-gutters pb-2'>
                      <div className='col-6'>
                        <span>Czas pracy:</span>{' '}
                        <span className='font-weight-bold'>
                          {user.workTime}
                        </span>
                        <br />
                        <span>Wykonalność:</span>{' '}
                        <span className='font-weight-bold'>
                          {user.punctuality.toFixed(2)}%
                        </span>
                      </div>
                      <div className='col-6'>
                        <span>Liczba zadań:</span>{' '}
                        <span className='font-weight-bold'>
                          {user.taskCount}
                        </span>
                        <br />
                        <span>Liczba projektów:</span>{' '}
                        <span className='font-weight-bold'>
                          {user.projectCount}
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};
