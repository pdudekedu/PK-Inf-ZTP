import React from 'react';

export const FormInput = ({
  id,
  label,
  value,
  type,
  onChange,
}: {
  id: string;
  label: string;
  value: string;
  type?: 'text' | 'password';
  onChange: (value: string) => void;
}) => {
  const forId = `fonr-input-${id}`;

  return (
    <div className='form-group'>
      <label htmlFor={forId}>{label}</label>
      <input
        type={type ?? 'text'}
        className='form-control'
        id={forId}
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
    </div>
  );
};
