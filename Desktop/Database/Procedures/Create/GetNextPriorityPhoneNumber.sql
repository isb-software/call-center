CREATE PROCEDURE [dbo].[GetNextPriorityPhoneNumber]
AS
BEGIN
	SET NOCOUNT ON;

	WITH cte AS (
      SELECT TOP(1) PhoneNumber, CallAtempts
      FROM PriorityQueues WITH (ROWLOCK, READPAST)
	  WHERE NextTimeCall < GETDATE()
    ORDER BY NextTimeCall)
  DELETE FROM cte
    OUTPUT deleted.PhoneNumber, deleted.CallAtempts;
END