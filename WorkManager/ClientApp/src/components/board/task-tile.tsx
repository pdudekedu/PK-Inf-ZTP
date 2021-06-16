import React from 'react';
import {
  TaskDto,
  TaskState,
  TaskStateAction,
  TaskStateFlow,
} from '../../api/task';

export const TaskTile = ({
  task,
  selected,
  onClick,
  onChangeState,
}: {
  task: TaskDto;
  selected: boolean;
  onClick: (task: TaskDto) => void;
  onChangeState: (task: TaskDto, toState: TaskState) => void;
}) => (
  <div onClick={() => onClick(task)}>
    <div
      className={`card mb-2 vm-clicable ${
        selected ? 'bg-light border-primary' : ''
      }`}
    >
      <div className='card-body p-2'>
        <h6 className='card-title'>
          <span className='text-info mr-2'>Z-{task.id}</span>
          <span>{task.name}</span>
        </h6>
        <div className='card-text'>{task.description}</div>
        <div className='d-flex mt-2 flex-row justify-content-end'>
          <div className='btn-group'>
            {TaskStateFlow[task.state].map((state) => (
              <button
                key={state}
                className='btn btn-outline-secondary btn-sm'
                onClick={(e) => {
                  onChangeState(task, state);
                  e.stopPropagation();
                }}
              >
                {TaskStateAction[state]}
              </button>
            ))}
          </div>
        </div>
      </div>
    </div>
  </div>
);
