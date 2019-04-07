CREATE PROCEDURE [dbo].[GetNextPriorityPhoneNumber]
AS
BEGIN
	SET NOCOUNT ON;

	WITH cte AS (
    SELECT TOP(1) PhoneNumber
      FROM PriorityQueues WITH (ROWLOCK, READPAST)
    ORDER BY Id)
  DELETE FROM cte
    OUTPUT deleted.PhoneNumber;
END