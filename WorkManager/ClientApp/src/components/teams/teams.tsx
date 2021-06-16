import React, { useEffect, useState } from 'react';
import { getTeams, TeamDto } from '../../api/team';
import { authenticatedManager } from '../../authorization/authenticatedManager';
import { toChunks } from '../../helpers/collections';
import { EditTeamPanel } from './edit-team-panel';

export const Teams = authenticatedManager(() => {
  const [teams, setTeams] = useState<TeamDto[]>([]);
  const [selectedTeam, setSelectedTeam] = useState<TeamDto | null>(null);

  useEffect(() => {
    getTeams({
      onSuccess: (response) => {
        setTeams(response.result);
      },
    });
  }, []);

  const handleAdd = (team: TeamDto) => {
    setTeams([...teams, team]);
    setSelectedTeam(team);
  };

  const handleUpdate = (team: TeamDto) => {
    const toReplace = teams.find((u) => u.id === team.id);
    if (toReplace) {
      const newTeams = [...teams];
      newTeams[teams.indexOf(toReplace)] = team;
      setTeams(newTeams);
    }
  };

  const handleRemove = (team: TeamDto) => {
    const toReplace = teams.find((u) => u.id === team.id);
    if (toReplace) {
      const newTeams = [...teams];
      newTeams.splice(teams.indexOf(toReplace), 1);
      setTeams(newTeams);
    }
  };

  const handleCancel = () => setSelectedTeam(null);

  const handleCreate = () =>
    setSelectedTeam({
      id: 0,
      name: '',
      description: null,
      users: [],
    });

  return (
    <div className='p-3'>
      <div className='row'>
        <div className='col-9 pr-3'>
          {toChunks(teams, 4).map((teamsRow, i) => (
            <div className='row pb-3' key={i}>
              {teamsRow.map((team) => (
                <div
                  key={team.id}
                  className='col-3'
                  onClick={() => setSelectedTeam(team)}
                >
                  <div
                    className={`card vm-clicable ${
                      team === selectedTeam ? 'bg-light border-primary' : ''
                    }`}
                  >
                    <div className='card-body'>
                      <h5 className='card-title'>{team.name}</h5>
                      <div className='card-text'>{team.description}</div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          ))}
        </div>
        <div className='col-3 pl-3'>
          <button
            className='btn btn-block btn-primary mb-3'
            onClick={handleCreate}
          >
            Dodaj nowy zespół
          </button>
          <EditTeamPanel
            team={selectedTeam}
            onUpdated={handleUpdate}
            onAdded={handleAdd}
            onCancel={handleCancel}
            onRemoved={handleRemove}
          />
        </div>
      </div>
    </div>
  );
});
