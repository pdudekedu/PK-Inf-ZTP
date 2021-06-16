import React, { useEffect, useState } from 'react';
import {
  getProjectResources,
  getProjectUsers,
  ProjectResourceDto,
  ProjectUserDto,
} from '../../api/project';
import { createTask, removeTask, TaskDto, updateTask } from '../../api/task';
import { removeElementBy } from '../../helpers/collections';
import { notifySuccess } from '../../helpers/notifications';
import { ConfirmModal } from '../common/confirm-model';
import { FormInput } from '../common/form-input';
import { FormSelect } from '../common/form-select';
import { FormSelectWithButton } from '../common/form-select-with-button';
import { FormTextArea } from '../common/form-textarea';

const emptyResource: ProjectResourceDto = {
  id: 0,
  name: '',
};
const emptyUser: ProjectUserDto = {
  id: 0,
  firstName: '',
  lastName: '',
};

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
  const [showConfirm, setShowConfirm] = useState(false);

  const [availableResources, setAvailableResources] = useState<
    ProjectResourceDto[]
  >([emptyResource]);
  const [resourcesToAdd, setResourcesToAdd] = useState<ProjectResourceDto[]>([
    emptyResource,
  ]);
  const [resourceToAdd, setResourceToAdd] = useState(0);

  const [availableUsers, setAvailableUsers] = useState<ProjectUserDto[]>([
    emptyUser,
  ]);
  const [usersToAdd, setUsersToAdd] = useState<ProjectUserDto[]>([emptyUser]);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [estimateStart, setEstimateStart] = useState('');
  const [estimateEnd, setEstimateEnd] = useState('');
  const [resources, setResources] = useState<ProjectResourceDto[]>([]);
  const [user, setUser] = useState(0);

  useEffect(() => {
    getProjectResources(projectId, {
      onSuccess: (response) => {
        const resources = [emptyResource, ...response.result];
        setAvailableResources(resources);
        setResourcesToAdd(resources);
      },
    });

    getProjectUsers(projectId, {
      onSuccess: (response) => {
        const users = [emptyUser, ...response.result];
        setAvailableUsers(users);
        setUsersToAdd(users);
      },
    });
  }, [projectId]);

  useEffect(() => {
    setName(task?.name ?? '');
    setDescription(task?.description ?? '');
    setResources(task?.resources ?? []);
    setEstimateStart(task?.estimateStart?.substring(0, 10) ?? '');
    setEstimateEnd(task?.estimateEnd?.substring(0, 10) ?? '');

    if (task) {
      setResourcesToAdd(
        availableResources.filter(
          (u) => !task.resources.some((tm) => tm.id === u.id)
        )
      );
    }
  }, [task, availableResources]);

  useEffect(() => {
    setUser(task?.user?.id ?? 0);

    if (task) {
      if (task.user && !availableUsers.some((x) => x.id === task.user?.id)) {
        setUsersToAdd([...availableUsers, { ...task.user }]);
      } else {
        setUsersToAdd([...availableUsers]);
      }
    }
  }, [task, availableUsers]);

  const handleSave = () => {
    if (task) {
      if (task.id) {
        updateTask(
          projectId,
          task.id,
          {
            name,
            description: description ?? null,
            resources,
            user: user ? { id: user, firstName: '', lastName: '' } : null,
            estimateEnd: estimateEnd ?? null,
            estimateStart: estimateStart ?? null,
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
            resources,
            user: user ? { id: user, firstName: '', lastName: '' } : null,
            estimateEnd: estimateEnd ?? null,
            estimateStart: estimateStart ?? null,
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
    setShowConfirm(false);
    if (task) {
      removeTask(projectId, task.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Zadanie usunięte pomyślnie!');
        },
      });
    }
  };

  const handleAddResource = () => {
    if (resourceToAdd) {
      const resource = availableResources.find((x) => x.id === resourceToAdd);

      if (resource) {
        if (!resources.some((x) => x.id === resource.id)) {
          setResources([...resources, resource]);
        }

        setResourcesToAdd(
          removeElementBy(resourcesToAdd, (u) => u.id === resource.id)
        );
        setResourceToAdd(0);
      }
    }
  };

  const handleRemoveResource = (id: number) => {
    const resource = availableResources.find((x) => x.id === id);
    console.log(resource);
    if (resource) {
      if (!resourcesToAdd.some((x) => x.id === resource.id)) {
        setResourcesToAdd([...resourcesToAdd, resource]);
      }

      setResources(removeElementBy(resources, (u) => u.id === resource.id));
      setResourceToAdd(0);
    }
  };

  return (
    <div>
      <ConfirmModal
        show={showConfirm}
        text='Czy na pewno chcesz usunąć zadanie?'
        onCancel={() => setShowConfirm(false)}
        onConfirm={handleRemove}
      />
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
          <FormInput
            id='estimateStart'
            label='Szacowany czas rozpoczęcia'
            type='date'
            value={estimateStart}
            onChange={setEstimateStart}
          />
          <FormInput
            id='estimateEnd'
            label='Szacowany czas zakończenia'
            type='date'
            value={estimateEnd}
            onChange={setEstimateEnd}
          />
          <FormSelect
            id='user'
            label='Wykonujący'
            value={user}
            onChange={setUser}
            options={usersToAdd.map((x) => ({
              label: `${x.firstName} ${x.lastName}`,
              value: x.id,
            }))}
          />
          <div className='form-group'>
            <label htmlFor='resources'>Zasoby</label>
            <FormSelectWithButton
              id='resources'
              value={resourceToAdd}
              options={resourcesToAdd.map(({ id, name }) => ({
                value: id,
                label: name,
              }))}
              onChange={setResourceToAdd}
              onClick={handleAddResource}
            />
            <div
              data-testid='resources-container'
              className='d-flex flex-wrap align-items-start bg-white wm-items-container'
            >
              {resources.map(({ id, name }) => (
                <span
                  key={id}
                  className='d-flex flex-row align-items-center m-2 p-1 border border-secondary rounded'
                >
                  <span>{name}</span>
                  <button
                    className='btn btn-light btn-sm ml-2'
                    onClick={() => handleRemoveResource(id)}
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
