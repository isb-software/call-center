CREATE PROCEDURE [dbo].[GetNextNormalPhoneNumber]
AS
BEGIN
	SET NOCOUNT ON;

	WITH cte AS (
    SELECT TOP(1) PhoneNumber
      FROM NormalQueues WITH (ROWLOCK, READPAST)
    ORDER BY Id)
  DELETE FROM cte
    OUTPUT deleted.PhoneNumber;
END