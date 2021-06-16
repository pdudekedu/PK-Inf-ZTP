import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { EditUserPanel } from './edit-user-panel';
import { UserDto, UserRole } from '../../api/user';

describe('EditUserPanel', () => {
  it('should render placeholder text if no user passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockCancel = jest.fn();

    //act
    render(
      <EditUserPanel
        user={null}
        onUpdated={mockUpdated}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(
      screen.getByText('Wybierz użytkownika do edycji')
    ).toBeInTheDocument();
  });

  it.each`
    firstName | lastName      | role
    ${'Jan'}  | ${'Kowalski'} | ${UserRole.Worker}
    ${'Adam'} | ${'Nowak'}    | ${UserRole.Manager}
  `(
    `should render form contating inputs with values set if user with first name '$firstName', last name '$lastName' and role '$role' passed`,
    ({
      firstName,
      lastName,
      role,
    }: {
      firstName: string;
      lastName: string;
      role: UserRole;
    }) => {
      //arrange
      const mockUpdated = jest.fn();
      const mockRemoved = jest.fn();
      const mockCancel = jest.fn();

      const user: UserDto = {
        id: 1,
        userName: `${firstName} ${lastName}`,
        firstName,
        lastName,
        role,
      };

      //act
      render(
        <EditUserPanel
          user={user}
          onUpdated={mockUpdated}
          onRemoved={mockRemoved}
          onCancel={mockCancel}
        />
      );

      //assert
      expect(screen.getByLabelText('Imię')).toHaveValue(user.firstName);
      expect(screen.getByLabelText('Nazwisko')).toHaveValue(user.lastName);
      expect(screen.getByLabelText('Rola')).toHaveValue(role + '');
      expect(screen.getByLabelText('Rola')).toHaveDisplayValue(
        role === UserRole.Manager ? 'Menadżer' : 'Pracownik'
      );

      expect(screen.getByText('Zapisz')).toBeInTheDocument();
      screen
        .getAllByText('Anuluj')
        .forEach((el) => expect(el).toBeInTheDocument());
      expect(screen.getByText('Usuń')).toBeInTheDocument();
    }
  );

  it(`should execute cancel callback if cancel button clicked`, () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockCancel = jest.fn();

    const user: UserDto = {
      id: 1,
      userName: 'jk',
      firstName: 'Jan',
      lastName: 'Kowalski',
      role: UserRole.Worker,
    };

    render(
      <EditUserPanel
        user={user}
        onUpdated={mockUpdated}
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
