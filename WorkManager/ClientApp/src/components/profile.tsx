import React from 'react';
import { authenticated } from '../authorization/authenticated';

export const Profile = authenticated(() => {
  return <div>Profile</div>;
});
