import React, { useEffect, useState } from 'react';
import { getResources, ResourceDto } from '../../api/resource';
import { authenticatedManager } from '../../authorization/authenticatedManager';
import { toChunks } from '../../helpers/collections';
import { EditResourcePanel } from './edit-resource-panel';

export const Resources = authenticatedManager(() => {
  const [resources, setResources] = useState<ResourceDto[]>([]);
  const [selectedResource, setSelectedResource] =
    useState<ResourceDto | null>(null);

  useEffect(() => {
    getResources({
      onSuccess: (response) => {
        setResources(response.result);
      },
    });
  }, []);

  const handleAdd = (resource: ResourceDto) => {
    setResources([...resources, resource]);
    setSelectedResource(resource);
  };

  const handleUpdate = (resource: ResourceDto) => {
    const toReplace = resources.find((u) => u.id === resource.id);
    if (toReplace) {
      const newResources = [...resources];
      newResources[resources.indexOf(toReplace)] = resource;
      setResources(newResources);
    }
  };

  const handleRemove = (resource: ResourceDto) => {
    const toReplace = resources.find((u) => u.id === resource.id);
    if (toReplace) {
      const newResources = [...resources];
      newResources.splice(resources.indexOf(toReplace), 1);
      setResources(newResources);
    }
  };

  const handleCancel = () => setSelectedResource(null);

  const handleCreate = () =>
    setSelectedResource({
      id: 0,
      name: '',
      description: null,
    });

  return (
    <div className='p-3'>
      <div className='row'>
        <div className='col-9 pr-3'>
          {toChunks(resources, 4).map((resourcesRow, i) => (
            <div className='row pb-3' key={i}>
              {resourcesRow.map((resource) => (
                <div
                  key={resource.id}
                  className='col-3'
                  onClick={() => setSelectedResource(resource)}
                >
                  <div
                    className={`card vm-clicable ${
                      resource === selectedResource
                        ? 'bg-light border-primary'
                        : ''
                    }`}
                  >
                    <div className='card-body'>
                      <h5 className='card-title'>{resource.name}</h5>
                      <div className='card-text'>{resource.description}</div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          ))}
        </div>
        <div className='col-3 pl-3'>
          <button
            className='btn btn-block btn-primary mb-3'
            onClick={handleCreate}
          >
            Dodaj nowy zasób
          </button>
          <EditResourcePanel
            resource={selectedResource}
            onUpdated={handleUpdate}
            onAdded={handleAdd}
            onCancel={handleCancel}
            onRemoved={handleRemove}
          />
        </div>
      </div>
    </div>
  );
});
