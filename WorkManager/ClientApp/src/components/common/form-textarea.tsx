import React from 'react';

export const FormTextArea = ({
  id,
  label,
  value,
  maxLength,
  onChange,
}: {
  id: string;
  label: string;
  value: string;
  maxLength?: number;
  onChange: (value: string) => void;
}) => {
  const forId = `fonr-input-${id}`;

  return (
    <div className='form-group'>
      <label htmlFor={forId}>{label}</label>
      <textarea
        className='form-control'
        id={forId}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        maxLength={maxLength}
      />
    </div>
  );
};
