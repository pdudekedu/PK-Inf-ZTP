import React, { useEffect, useState } from 'react';
import { getProjects, ProjectDto } from '../../api/project';
import { authenticatedManager } from '../../authorization/authenticatedManager';
import { toChunks } from '../../helpers/collections';
import { EditProjectPanel } from './edit-project-panel';

export const Projects = authenticatedManager(() => {
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [selectedProject, setSelectedProject] =
    useState<ProjectDto | null>(null);

  useEffect(() => {
    getProjects({
      onSuccess: (response) => {
        setProjects(response.result);
      },
    });
  }, []);

  const handleAdd = (project: ProjectDto) => {
    setProjects([...projects, project]);
    setSelectedProject(project);
  };

  const handleUpdate = (project: ProjectDto) => {
    const toReplace = projects.find((u) => u.id === project.id);
    if (toReplace) {
      const newProjects = [...projects];
      newProjects[projects.indexOf(toReplace)] = project;
      setProjects(newProjects);
    }
  };

  const handleRemove = (project: ProjectDto) => {
    const toReplace = projects.find((u) => u.id === project.id);
    if (toReplace) {
      const newProjects = [...projects];
      newProjects.splice(projects.indexOf(toReplace), 1);
      setProjects(newProjects);
    }
  };

  const handleCancel = () => setSelectedProject(null);

  const handleCreate = () =>
    setSelectedProject({
      id: 0,
      name: '',
      description: null,
      resources: [],
      team: {
        id: 0,
        name: '',
      },
    });

  return (
    <div className='p-3'>
      <div className='row'>
        <div className='col-9 pr-3'>
          {toChunks(projects, 4).map((projectsRow, i) => (
            <div className='row pb-3' key={i}>
              {projectsRow.map((project) => (
                <div
                  key={project.id}
                  className='col-3'
                  onClick={() => setSelectedProject(project)}
                >
                  <div
                    className={`card vm-clicable ${
                      project === selectedProject
                        ? 'bg-light border-primary'
                        : ''
                    }`}
                  >
                    <div className='card-body'>
                      <h5 className='card-title'>{project.name}</h5>
                      <div className='card-text'>{project.description}</div>
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
            Dodaj nowy projekt
          </button>
          <EditProjectPanel
            project={selectedProject}
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
