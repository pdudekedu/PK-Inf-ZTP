import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import { EditResourcePanel } from './edit-resource-panel';
import { ResourceDto } from '../../api/resource';

describe('EditResourcePanel', () => {
  it('should render placeholder text if no resource passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    //act
    render(
      <EditResourcePanel
        resource={null}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(
      screen.getByText('Wybierz zasób do edycji lub dodaj nowy')
    ).toBeInTheDocument();
  });

  it('should render empty form if new resource object passed', () => {
    //arrange
    const mockUpdated = jest.fn();
    const mockRemoved = jest.fn();
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    const resource: ResourceDto = {
      id: 0,
      name: '',
      description: null,
    };

    //act
    render(
      <EditResourcePanel
        resource={resource}
        onUpdated={mockUpdated}
        onAdded={mockAdded}
        onRemoved={mockRemoved}
        onCancel={mockCancel}
      />
    );

    //assert
    expect(screen.getByLabelText('Nazwa')).toHaveValue('');
    expect(screen.getByLabelText('Opis')).toHaveValue('');
    expect(screen.getByText('Zapisz')).toBeInTheDocument();
    screen
      .getAllByText('Anuluj')
      .forEach((el) => expect(el).toBeInTheDocument());
    expect(screen.getByText('Usuń')).toBeInTheDocument();
  });

  it.each`
    name         | description
    ${'zasób 1'} | ${null}
    ${'zasób 1'} | ${'opis zasobu'}
  `(
    `should render form contating inputs with values set if resource with name '$name' and description '$description' passed`,
    ({ name, description }: { name: string; description: string | null }) => {
      //arrange
      const mockUpdated = jest.fn();
      const mockRemoved = jest.fn();
      const mockAdded = jest.fn();
      const mockCancel = jest.fn();

      const resource: ResourceDto = {
        id: 1,
        name,
        description,
      };

      //act
      render(
        <EditResourcePanel
          resource={resource}
          onUpdated={mockUpdated}
          onAdded={mockAdded}
          onRemoved={mockRemoved}
          onCancel={mockCancel}
        />
      );

      //assert
      expect(screen.getByLabelText('Nazwa')).toHaveValue(resource.name);
      expect(screen.getByLabelText('Opis')).toHaveValue(
        resource.description ?? ''
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
    const mockAdded = jest.fn();
    const mockCancel = jest.fn();

    const resource: ResourceDto = {
      id: 1,
      name: 'zasób 1',
      description: 'opis',
    };

    render(
      <EditResourcePanel
        resource={resource}
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
