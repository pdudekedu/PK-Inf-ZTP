import React, { useEffect, useState } from 'react';
import { createTask, removeTask, TaskDto, updateTask } from '../../api/task';
import { notifySuccess } from '../../helpers/notifications';
import { FormInput } from '../common/form-input';
import { FormTextArea } from '../common/form-textarea';

export const EditTaskPanel = ({
  projectId,
  task,
  onUpdated,
  onRemoved,
  onAdded,
  onCancel,
}: {
  projectId: number;
  task: TaskDto | null;
  onUpdated: (task: TaskDto) => void;
  onRemoved: (task: TaskDto) => void;
  onAdded: (task: TaskDto) => void;
  onCancel: () => void;
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    setName(task?.name ?? '');
    setDescription(task?.description ?? '');
  }, [task]);

  const handleSave = () => {
    if (task) {
      if (task.id) {
        updateTask(
          projectId,
          task.id,
          {
            name,
            description: description ?? null,
          },
          {
            onSuccess: (response) => {
              const { name, description } = response.result;

              setName(name);
              setDescription(description ?? '');

              onUpdated(response.result);

              notifySuccess('Dane zadania zaktualizowane pomyślnie!');
            },
          }
        );
      } else {
        createTask(
          projectId,
          {
            name,
            description: description ?? null,
          },
          {
            onSuccess: (response) => {
              onAdded(response.result);

              notifySuccess('Zadanie dodane pomyślnie!');
            },
          }
        );
      }
    }
  };

  const handleCancel = () => onCancel();

  const handleRemove = () => {
    if (task) {
      removeTask(projectId, task.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Zadanie usunięte pomyślnie!');
        },
      });
    }
  };

  return (
    <div>
      {!task && <div>Wybierz zadanie do edycji lub dodaj nowe</div>}
      {task && (
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
            onClick={handleCancel}
          />
          <input
            className='btn btn-block btn-danger'
            type='button'
            value='Usuń'
            onClick={handleRemove}
          />
        </div>
      )}
    </div>
  );
};
