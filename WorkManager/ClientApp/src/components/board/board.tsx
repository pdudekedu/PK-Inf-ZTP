import React, { useEffect, useState } from 'react';
import {
  getTasksFor,
  TaskDto,
  TaskState,
  updateTaskState,
} from '../../api/task';
import { getProjectsForCurrentUser, ProjectDto } from '../../api/user';
import { authenticated } from '../../authorization/authenticated';
import { EditTaskPanel } from './edit-task-panel';
import { TaskTile } from './task-tile';

const defaultTasks = {
  [TaskState.New]: [],
  [TaskState.InProgress]: [],
  [TaskState.Waiting]: [],
  [TaskState.Done]: [],
};

export const Board = authenticated(() => {
  const [projects, setProjects] = useState<ProjectDto[]>([]);
  const [selectedProject, setSelectedProject] =
    useState<ProjectDto | null>(null);

  const [tasks, setTasks] =
    useState<{
      [TaskState.New]: TaskDto[];
      [TaskState.InProgress]: TaskDto[];
      [TaskState.Waiting]: TaskDto[];
      [TaskState.Done]: TaskDto[];
    }>(defaultTasks);
  const [selectedTask, setSelectedTask] = useState<TaskDto | null>(null);

  useEffect(() => {
    getProjectsForCurrentUser({
      onSuccess: (response) => {
        setProjects(response.result);
        setSelectedProject(response.result[0]);
      },
    });
  }, []);

  useEffect(() => {
    if (selectedProject) {
      getTasksFor(selectedProject.id, {
        onSuccess: (response) => {
          setTasks({
            [TaskState.New]: response.result.filter(
              (t) => t.state === TaskState.New
            ),
            [TaskState.InProgress]: response.result.filter(
              (t) => t.state === TaskState.InProgress
            ),
            [TaskState.Waiting]: response.result.filter(
              (t) => t.state === TaskState.Waiting
            ),
            [TaskState.Done]: response.result.filter(
              (t) => t.state === TaskState.Done
            ),
          });
        },
      });
    } else {
      setTasks(defaultTasks);
    }
    setSelectedTask(null);
  }, [selectedProject]);

  const handleAdd = (task: TaskDto) => {
    setTasks({
      ...tasks,
      [TaskState.New]: [...tasks[TaskState.New], task],
    });
    setSelectedTask(task);
  };

  const handleUpdate = (task: TaskDto) => {
    const toReplace = tasks[task.state].find((u) => u.id === task.id);
    if (toReplace) {
      const newTasks = { ...tasks };
      newTasks[task.state][tasks[task.state].indexOf(toReplace)] = task;
      setTasks(newTasks);
    }
  };

  const handleRemove = (task: TaskDto) => {
    const toReplace = tasks[task.state].find((u) => u.id === task.id);
    if (toReplace) {
      const newTasks = { ...tasks };
      newTasks[task.state].splice(tasks[task.state].indexOf(toReplace), 1);
      setTasks(newTasks);
    }
  };

  const handleCancel = () => setSelectedTask(null);

  const handleChangeState = (task: TaskDto, toState: TaskState) => {
    if (selectedProject) {
      updateTaskState(selectedProject.id, task.id, toState, {
        onSuccess: (response) => {
          const newTask = response.result;

          const newTasks = {
            ...tasks,
            [newTask.state]: [...tasks[newTask.state], newTask],
          };
          console.log(newTasks[task.state]);

          const toRemove = tasks[task.state].find((u) => u.id === task.id);
          console.log(toRemove);
          if (toRemove) {
            console.log(tasks[task.state].indexOf(toRemove));
            newTasks[task.state].splice(tasks[task.state].indexOf(toRemove), 1);
          }
          console.log(newTasks[task.state]);

          setTasks(newTasks);
        },
      });
    }
  };

  const handleCreate = () =>
    setSelectedTask({
      id: 0,
      name: '',
      description: null,
      state: TaskState.New,
      resources: [],
      user: null,
    });

  const displayTasksFor = (state: TaskState) =>
    tasks[state].map((task) => (
      <TaskTile
        key={task.id}
        task={task}
        selected={task === selectedTask}
        onClick={setSelectedTask}
        onChangeState={handleChangeState}
      />
    ));

  return (
    <div className='p-3'>
      <div className='row'>
        <div className='col-9 pl-3'>
          <div className='row'>
            <div className='col-12'>
              <select
                className='form-control'
                value={selectedProject?.id}
                onChange={(e) =>
                  setSelectedProject(
                    projects.find((p) => p.id === parseInt(e.target.value)) ??
                      null
                  )
                }
              >
                {projects.map(({ id, name }) => (
                  <option key={id} value={id}>
                    {name}
                  </option>
                ))}
              </select>
            </div>
          </div>
          <div className='row'>
            <div className='col-3 mt-2 mb-2 pl-2 pr-2 border-right border-mutated'>
              <h6 className='text-center text-secondary p-2'>NOWE</h6>
              {displayTasksFor(TaskState.New)}
            </div>
            <div className='col-3 mt-2 mb-2 pl-2 pr-2 border-right border-mutated'>
              <h6 className='text-center text-secondary p-2'>AKTYWNE</h6>
              {displayTasksFor(TaskState.InProgress)}
            </div>
            <div className='col-3 mt-2 mb-2 pl-2 pr-2 border-right border-mutated'>
              <h6 className='text-center text-secondary p-2'>WSTRZYMANE</h6>
              {displayTasksFor(TaskState.Waiting)}
            </div>
            <div className='col-3 mt-2 mb-2 pl-2 pr-2'>
              <h6 className='text-center text-secondary p-2'>ZAKOŃCZONE</h6>
              {displayTasksFor(TaskState.Done)}
            </div>
          </div>
        </div>
        <div className='col-3 pl-3'>
          <button
            className='btn btn-block btn-primary mb-3'
            onClick={handleCreate}
            disabled={!selectedProject}
          >
            Dodaj nowe zadanie
          </button>
          {selectedProject && (
            <EditTaskPanel
              projectId={selectedProject.id}
              task={selectedTask}
              onUpdated={handleUpdate}
              onAdded={handleAdd}
              onCancel={handleCancel}
              onRemoved={handleRemove}
            />
          )}
        </div>
      </div>
    </div>
  );
});
