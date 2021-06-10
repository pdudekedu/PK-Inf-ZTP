import React from 'react';

export type FormSelectOption = { value: number; label: string };

export const FormSelect = ({
  id,
  label,
  value,
  options,
  onChange,
}: {
  id: string;
  label: string;
  value: number;
  options: FormSelectOption[];
  onChange: (value: number) => void;
}) => {
  const forId = `fonr-input-${id}`;

  return (
    <div className='form-group'>
      <label htmlFor={forId}>{label}</label>
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
    </div>
  );
};
