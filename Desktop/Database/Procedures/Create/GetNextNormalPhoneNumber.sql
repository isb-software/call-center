CREATE PROCEDURE [dbo].[GetNextNormalPhoneNumber]
AS
BEGIN
	SET NOCOUNT ON;

	WITH cte AS (
      SELECT TOP(1) PhoneNumber, CallAtempts
      FROM NormalQueues WITH (ROWLOCK, READPAST)
	  WHERE NextTimeCall < GETDATE()
    ORDER BY NextTimeCall)
  DELETE FROM cte
    OUTPUT deleted.PhoneNumber, deleted.CallAtempts;
END