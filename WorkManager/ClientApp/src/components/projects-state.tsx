import React, { useEffect, useState } from 'react';
import { getProjectsStats, ProjectStatisticDto } from '../api/reports';
import { toChunks } from '../helpers/collections';

export const ProjectsState = () => {
  const [projects, setProjects] = useState<ProjectStatisticDto[]>([]);

  useEffect(() => {
    getProjectsStats({
      onSuccess: (response) => {
        setProjects(response.result);
      },
    });
  }, []);

  return (
    <div className='p-3'>
      {toChunks(projects, 4).map((projectsRow, i) => (
        <div className='row pb-3' key={i}>
          {projectsRow.map((project) => (
            <div key={project.projectId} className='col-3'>
              <div className={`card`}>
                <div className='card-body'>
                  <h5 className='card-title font-weight-bold'>
                    {project.name}
                  </h5>
                  <div className='card-text small'>
                    <div className='row no-gutters pb-2'>
                      {project.description}
                    </div>
                    <div className='row no-gutters pb-2'>
                      <div className='col-6'>
                        <span className='text-info'>Czas pracy</span>
                        <br />
                        <span>Szacunkowy:</span>{' '}
                        <span className='font-weight-bold'>
                          {project.estimateWorkTime}
                        </span>
                        <br />
                        <span>Rzeczywisty:</span>{' '}
                        <span className='font-weight-bold'>
                          {project.workTime}
                        </span>
                      </div>
                      <div className='col-6'>
                        <span></span>
                        <br />
                        <span>Zespół:</span>{' '}
                        <span className='font-weight-bold'>{project.team}</span>
                        <br />
                        <span>Wykonalność:</span>{' '}
                        <span className='font-weight-bold'>
                          {project.punctuality.toFixed(2)}%
                        </span>
                      </div>
                    </div>

                    <div className='row no-gutters'>
                      <div className='col-12'>
                        <div className='row no-gutters text-info'>
                          Liczba zadań
                        </div>
                        <div className='row no-gutters'>
                          <div className='col-3'>
                            <span>N:</span>{' '}
                            <span className='font-weight-bold'>
                              {project.new}
                            </span>
                          </div>
                          <div className='col-3'>
                            <span>A:</span>{' '}
                            <span className='font-weight-bold'>
                              {project.active}
                            </span>
                          </div>
                          <div className='col-3'>
                            <span>W:</span>{' '}
                            <span className='font-weight-bold'>
                              {project.suspend}
                            </span>
                          </div>
                          <div className='col-3'>
                            <span>Z:</span>{' '}
                            <span className='font-weight-bold'>
                              {project.complete}
                            </span>
                          </div>
                        </div>
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
