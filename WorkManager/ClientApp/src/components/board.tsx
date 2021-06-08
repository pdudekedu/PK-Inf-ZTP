import React, { useEffect, useState } from 'react';
import { getProjects, ProjectDto } from '../api/project';
import { authenticated } from '../authorization/authenticated';

export const Board = authenticated(() => {
  const [projects, setProjects] = useState<ProjectDto[]>([]);

  useEffect(() => {
    getProjects({
      onSuccess: (response) => {
        setProjects(response.result);
      },
    });
  }, []);

  return (
    <div>
      {projects.map((project) => (
        <div key={project.id}>{project.name}</div>
      ))}
    </div>
  );
});
