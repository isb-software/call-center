CREATE PROCEDURE [dbo].[GetNextNormalPhoneNumber]
AS
BEGIN
	SET NOCOUNT ON;

	WITH cte AS (
      SELECT TOP(1) PhoneNumber, CallAtempts
      FROM NormalQueues WITH (ROWLOCK, READPAST))
  DELETE FROM cte
    OUTPUT deleted.PhoneNumber, deleted.CallAtempts;
END