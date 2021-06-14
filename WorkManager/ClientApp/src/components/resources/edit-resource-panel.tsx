import React, { useEffect, useState } from 'react';
import {
  createResource,
  removeResource,
  ResourceDto,
  updateResource,
} from '../../api/resource';
import { notifySuccess } from '../../helpers/notifications';
import { FormInput } from '../common/form-input';
import { FormTextArea } from '../common/form-textarea';

export const EditResourcePanel = ({
  resource,
  onUpdated,
  onRemoved,
  onAdded,
  onCancel,
}: {
  resource: ResourceDto | null;
  onUpdated: (resource: ResourceDto) => void;
  onRemoved: (resource: ResourceDto) => void;
  onAdded: (resource: ResourceDto) => void;
  onCancel: () => void;
}) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    setName(resource?.name ?? '');
    setDescription(resource?.description ?? '');
  }, [resource]);

  const handleSave = () => {
    if (resource) {
      if (resource.id) {
        updateResource(
          resource.id,
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

              notifySuccess('Dane zasobu zaktualizowane pomyślnie!');
            },
          }
        );
      } else {
        createResource(
          {
            name,
            description: description ?? null,
          },
          {
            onSuccess: (response) => {
              onAdded(response.result);

              notifySuccess('Zasób dodany pomyślnie!');
            },
          }
        );
      }
    }
  };

  const handleCancel = () => onCancel();

  const handleRemove = () => {
    if (resource) {
      removeResource(resource.id, {
        onSuccess: (response) => {
          onRemoved(response.result);

          notifySuccess('Zasób usunięty pomyślnie!');
        },
      });
    }
  };

  return (
    <div>
      {!resource && <div>Wybierz zasób do edycji lub dodaj nowy</div>}
      {resource && (
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
            label='Nazwisko'
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
