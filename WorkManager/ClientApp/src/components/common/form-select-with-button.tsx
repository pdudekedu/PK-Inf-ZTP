import React from 'react';
import { FormSelectOption } from './form-select';

export const FormSelectWithButton = ({
  id,
  value,
  options,
  onChange,
  onClick,
}: {
  id: string;
  value: number;
  options: FormSelectOption[];
  onChange: (value: number) => void;
  onClick: (value: number) => void;
}) => {
  const forId = `fonr-input-${id}`;

  return (
    <div className='input-group'>
      <select
        className='form-control'
        id={forId}
        value={value}
        onChange={(e) => onChange(parseInt(e.target.value))}
      >
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      <button
        className='btn btn-secondary input-group-append'
        onClick={() => onClick(value)}
      >
        Dodaj
      </button>
    </div>
  );
};
