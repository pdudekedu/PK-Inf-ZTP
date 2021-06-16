import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { EditTeamPanel } from './edit-team-panel';
import { TeamDto } from '../../api/team';

describe('EditTeamPanel', () => {
  it('should render placeholder text if no team passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    //act
    render(
      <EditTeamPanel
        team={null}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(
      screen.getByText('Wybierz zespół do edycji lub dodaj nowy')
    ).toBeInTheDocument();
  });

  it('should render empty form if new team object passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    const team: TeamDto = {
      id: 0,
      name: '',
      description: null,
      users: [],
    };

    //act
    render(
      <EditTeamPanel
        team={team}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(screen.getByLabelText('Nazwa')).toHaveValue('');
    expect(screen.getByLabelText('Opis')).toHaveValue('');
    const usersContainer = screen.getByTestId('users-container');
    expect(usersContainer).toBeInTheDocument();

    expect(screen.getByText('Zapisz')).toBeInTheDocument();
    screen
      .getAllByText('Anuluj')
      .forEach((el) => expect(el).toBeInTheDocument());
    expect(screen.getByText('Usuń')).toBeInTheDocument();
  });

  it.each`
    name          | description
    ${'zespół 1'} | ${null}
    ${'zespół 1'} | ${'opis zespołu'}
  `(
    `should render form contating inputs with values set if team with name '$name' and description '$description' passed`,
    ({ name, description }: { name: string; description: string | null }) => {
      //arrange
      const mockUpdated = jest.fn();
      const mockRemoved = jest.fn();
      const mockAdded = jest.fn();
      const mockCancel = jest.fn();

      const team: TeamDto = {
        id: 1,
        name,
        description,
        users: [
          {
            id: 1,
            firstName: 'Jan',
            lastName: 'Kowalski',
          },
        ],
      };

      //act
      render(
        <EditTeamPanel
          team={team}
          onUpdated={mockUpdated}
          onAdded={mockAdded}
          onRemoved={mockRemoved}
          onCancel={mockCancel}
        />
      );

      //assert
      expect(screen.getByLabelText('Nazwa')).toHaveValue(team.name);
      expect(screen.getByLabelText('Opis')).toHaveValue(team.description ?? '');
      expect(screen.getByTestId('users-container')).toBeInTheDocument();
      const usersContainer = screen.getByTestId('users-container');
      expect(usersContainer).toBeInTheDocument();
      expect(usersContainer).toContainHTML('<span>Jan Kowalski</span>');

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

    const team: TeamDto = {
      id: 1,
      name: 'zasób 1',
      description: 'opis',
      users: [],
    };

    render(
      <EditTeamPanel
        team={team}
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
