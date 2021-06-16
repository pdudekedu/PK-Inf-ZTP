import React from 'react';

export const ConfirmModal = ({
  show,
  text,
  onConfirm,
  onCancel,
}: {
  show: boolean;
  text: string;
  onConfirm: () => void;
  onCancel: () => void;
}) => {
  return (
    <div
      className='modal fade show'
      id='exampleModal'
      tabIndex={-1}
      role='dialog'
      aria-labelledby='exampleModalLabel'
      aria-hidden='true'
      style={{ display: show ? 'block' : 'none' }}
    >
      <div className='modal-dialog' role='document'>
        <div className='modal-content'>
          <div className='modal-body'>{text}</div>
          <div className='modal-footer'>
            <button
              type='button'
              className='btn btn-secondary'
              data-dismiss='modal'
              onClick={onCancel}
            >
              Anuluj
            </button>
            <button
              type='button'
              className='btn btn-primary'
              onClick={onConfirm}
            >
              Zatwierdź
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
