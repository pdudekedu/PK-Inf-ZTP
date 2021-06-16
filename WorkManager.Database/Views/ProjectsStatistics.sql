CREATE VIEW [dbo].[ProjectsStatistics]
	AS 
WITH CTE_TASKSTATES AS (
	SELECT ProjectId, [0] New, [1] Active, [2] [Suspend], [3] [Complete] FROM (
		SELECT ProjectId, [State] FROM TASKS
	) T
	PIVOT (
	COUNT([State])
	FOR STATE IN ([0], [1], [2], [3])) PIV
)
, CTE_TIME AS (
	SELECT ProjectId, SUM(WorkTime) WorkTime, SUM(DATEDIFF(SECOND, EstimateStart, DATEADD(DAY,1,EstimateEnd))) EstimateWorkTime, 
		   (1 - (SUM(Exceeded+0.0)/COUNT(1)))*100 Punctuality 
	FROM TaskStatistics
	GROUP BY ProjectId
)
SELECT P.ID ProjectId, P.NAME, P.DESCRIPTION, TS.New, TS.Active, TS.Suspend, TS.Complete, 
	CASE 
		WHEN TS.Active > 0 THEN 1
		WHEN TS.Suspend > 0 THEN 2
		WHEN TS.Complete > 0 THEN 3
		ELSE 0
	END [State],
	Teams.Name Team,
	ISNULL(CTE_TIME.WorkTime, 0.0) WorkTime,
	ISNULL(CTE_TIME.EstimateWorkTime, 0.0) EstimateWorkTime,
	ISNULL(Punctuality,100) Punctuality
FROM PROJECTS P
JOIN CTE_TASKSTATES TS ON TS.ProjectId = P.ID 
LEFT JOIN CTE_TIME ON CTE_TIME.ProjectId = P.ID 
JOIN TEAMS ON TEAMID = TEAMS.ID 
