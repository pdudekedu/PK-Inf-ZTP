import React, { useEffect, useState } from 'react';
import {
  createProject,
  removeProject,
  ProjectDto,
  ProjectResourceDto,
  updateProject,
} from '../../api/project';
import { getResources, ResourceDto } from '../../api/resource';
import { getTeams, TeamDto } from '../../api/team';
import { removeElementBy } from '../../helpers/collections';
import { notifySuccess } from '../../helpers/notifications';
import { ConfirmModal } from '../common/confirm-model';
import { FormInput } from '../common/form-input';
import { FormSelect } from '../common/form-select';
import { FormSelectWithButton } from '../common/form-select-with-button';
import { FormTextArea } from '../common/form-textarea';

const emptyResource: ResourceDto = {
  id: 0,
  name: '',
  description: null,
};
const emptyTeam: TeamDto = {
  id: 0,
  name: '',
  description: null,
  users: [],
};

export const EditProjectPanel = ({
  project,
  onUpdated,
  onRemoved,
  onAdded,
  onCancel,
}: {
  project: ProjectDto | null;
  onUpdated: (project: ProjectDto) => void;
  onRemoved: (project: ProjectDto) => void;
  onAdded: (project: ProjectDto) => void;
  onCancel: () => void;
}) => {
  const [showConfirm, setShowConfirm] = useState(false);

  const [availableResources, setAvailableResources] = useState<ResourceDto[]>([
    emptyResource,
  ]);
  const [resourcesToAdd, setResourcesToAdd] = useState<ResourceDto[]>([
    emptyResource,
  ]);
  const [resourceToAdd, setResourceToAdd] = useState(0);

  const [availableTeams, setAvailableTeams] = useState<TeamDto[]>([emptyTeam]);
  const [teamsToAdd, setTeamsToAdd] = useState<TeamDto[]>([emptyTeam]);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [resources, setResources] = useState<ProjectResourceDto[]>([]);
  const [team, setTeam] = useState(0);

  useEffect(() => {
    getResources({
      onSuccess: (response) => {
        const resources = [emptyResource, ...response.result];
        setAvailableResources(resources);
        setResourcesToAdd(resources);
      },
    });

    getTeams({
      onSuccess: (response) => {
        const teams = [emptyTeam, ...response.result];
        setAvailableTeams(teams);
        setTeamsToAdd(teams);
      },
    });
  }, []);

  useEffect(() => {
    setName(project?.name ?? '');
    setDescription(project?.description ?? '');
    setResources(project?.resources ?? []);

    if (project) {
      setResourcesToAdd(
        availableResources.filter(
          (u) => !project.resources.some((tm) => tm.id === u.id)
        )
      );
    }
  }, [project, availableResources]);

  useEffect(() => {
    setTeam(project?.team?.id ?? 0);

    if (project) {
      if (!availableTeams.some((x) => x.id === project.team.id)) {
        setTeamsToAdd([
          ...availableTeams,
          { ...project.team, users: [], description: null },
        ]);
      } else {
        setTeamsToAdd([...availableTeams]);
      }
    }
  }, [project, availableTeams]);

  const handleSave = () => {
    if (project && team) {
      if (project.id) {
        updateProject(
          project.id,
          {
            name,
            description: description ?? null,
            resources,
            team: {
              id: team,
              name: '',
            },
          },
          {
            onSuccess: (response) => {
              const { name, description, resources } = response.result;

              setName(name);
              setDescription(description ?? '');
              setResources(resources ?? []);

              onUpdated(response.result);

              notifySuccess('Dane projektu zaktualizowane pomyślnie!');
            },
          }
        );
      } else {
        createProject(
          {
            name,
            description: description ?? null,
            resources,
            team: {
              id: team,
              name: '',
            },
          },
          {
            onSuccess: (response) => {
              onAdded(response.result);

              notifySuccess('Projekt dodany pomyślnie!');
            },
          }
        );
      }
    }
  };

  const handleCancel = () => onCancel();

  const handleRemove = () => {
    setShowConfirm(false);
    if (project) {
      removeProject(project.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Projekt usunięty pomyślnie!');
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
        text='Czy na pewno chcesz usunąć projekt?'
        onCancel={() => setShowConfirm(false)}
        onConfirm={handleRemove}
      />
      {!project && <div>Wybierz projekt do edycji lub dodaj nowy</div>}
      {project && (
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
          <FormSelect
            id='team'
            label='Zespół'
            value={team}
            onChange={setTeam}
            options={teamsToAdd.map((x) => ({ label: x.name, value: x.id }))}
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
