CREATE VIEW [dbo].[UsersStatistics]
	AS
WITH CTE_TASKS AS (
	SELECT UserId, COUNT(*) TaskCount FROM Tasks
	WHERE UserId IS NOT NULL
	GROUP BY UserId
)
,CTE_PROJECTS AS (
	SELECT Team_User.UserId, COUNT(1) ProjectCount FROM Team_User
	JOIN Projects ON Projects.TeamId = Team_User.TeamId
	GROUP BY Team_User.UserId
)
,CTE_TIMES AS (
	SELECT UserId, SUM(WorkTime) WorkTime, SUM(DATEDIFF(SECOND, EstimateStart, DATEADD(DAY,1,EstimateEnd))) EstimateWorkTime, 
		   (1 - (SUM(EXCEEDED+0.0)/COUNT(1)))*100 Punctuality
	FROM TaskStatistics
	GROUP BY UserId
)
SELECT 
	CTE_TASKS.UserId, 
	Users.FirstName, 
	Users.LastName, 
	ISNULL(WorkTime, 0.0) WorkTime,
	ISNULL(EstimateWorkTime, 0.0) EstimateWorkTime,
	Punctuality, 
	TaskCount, 
	ProjectCount 
FROM CTE_TIMES
JOIN CTE_PROJECTS ON CTE_PROJECTS.UserId = CTE_TIMES.UserId
JOIN CTE_TASKS ON CTE_TASKS.UserId = CTE_TIMES.UserId
JOIN Users ON Users.Id = CTE_TASKS.UserId
