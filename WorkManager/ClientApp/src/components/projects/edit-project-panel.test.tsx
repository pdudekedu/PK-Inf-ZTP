import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { EditProjectPanel } from './edit-project-panel';
import { ProjectDto } from '../../api/project';

describe('EditProjectPanel', () => {
  it('should render placeholder text if no project passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    //act
    render(
      <EditProjectPanel
        project={null}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(
      screen.getByText('Wybierz projekt do edycji lub dodaj nowy')
    ).toBeInTheDocument();
  });

  it('should render empty form if new project object passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    const project: ProjectDto = {
      id: 0,
      name: '',
      description: null,
      resources: [],
      team: {
        id: 0,
        name: '',
      },
    };

    //act
    render(
      <EditProjectPanel
        project={project}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(screen.getByLabelText('Nazwa')).toHaveValue('');
    expect(screen.getByLabelText('Opis')).toHaveValue('');
    expect(screen.getByLabelText('Zespół')).toHaveDisplayValue('');
    const usersContainer = screen.getByTestId('resources-container');
    expect(usersContainer).toBeInTheDocument();

    expect(screen.getByText('Zapisz')).toBeInTheDocument();
    screen
      .getAllByText('Anuluj')
      .forEach((el) => expect(el).toBeInTheDocument());
    expect(screen.getByText('Usuń')).toBeInTheDocument();
  });

  it.each`
    name           | description        | team
    ${'projekt 1'} | ${null}            | ${'zespół 1'}
    ${'projekt 1'} | ${'opis projektu'} | ${'zespół 2'}
  `(
    `should render form contating inputs with values set if project with name '$name', description '$description' and team $team passed`,
    ({
      name,
      description,
      team,
    }: {
      name: string;
      description: string | null;
      team: string;
    }) => {
      //arrange
      const mockUpdated = jest.fn();
      const mockRemoved = jest.fn();
      const mockAdded = jest.fn();
      const mockCancel = jest.fn();

      const project: ProjectDto = {
        id: 1,
        name,
        description,
        resources: [
          {
            id: 1,
            name: 'zasób',
          },
        ],
        team: {
          id: 1,
          name: team,
        },
      };

      //act
      render(
        <EditProjectPanel
          project={project}
          onUpdated={mockUpdated}
          onAdded={mockAdded}
          onRemoved={mockRemoved}
          onCancel={mockCancel}
        />
      );

      //assert
      expect(screen.getByLabelText('Nazwa')).toHaveValue(project.name);
      expect(screen.getByLabelText('Opis')).toHaveValue(
        project.description ?? ''
      );
      expect(screen.getByLabelText('Zespół')).toHaveDisplayValue(
        project.team.name
      );
      const resourcesContainer = screen.getByTestId('resources-container');
      expect(resourcesContainer).toBeInTheDocument();
      expect(resourcesContainer).toContainHTML('<span>zasób</span>');

      expect(screen.getByText('Zapisz')).toBeInTheDocument();
      screen
        .getAllByText('Anuluj')
        .forEach((el) => expect(el).toBeInTheDocument());
      expect(screen.getAllByText('Usuń').length).toBeGreaterThan(0);
    }
  );

  it(`should execute cancel callback if cancel button clicked`, () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    const project: ProjectDto = {
      id: 1,
      name: 'zasób 1',
      description: 'opis',
      resources: [
        {
          id: 1,
          name: 'zasób',
        },
      ],
      team: {
        id: 1,
        name: 'zespół 1',
      },
    };

    render(
      <EditProjectPanel
        project={project}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //act
    fireEvent.click(screen.getByTestId('btn-cancel'));

    //assert
    expect(mockCancel.mock.calls.length).toBe(1);
  });
});
