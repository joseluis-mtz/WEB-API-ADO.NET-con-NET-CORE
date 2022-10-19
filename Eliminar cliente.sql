CREATE PROCEDURE [dbo].[SP_Eliminar_Cliente]
    @Id_cliente int
AS
	BEGIN TRANSACTION
	  IF (
		    (@Id_cliente IS NOT NULL)	
			AND EXISTS (SELECT ClienteID FROM Cliente WHERE ClienteID = @Id_cliente)
			)
			BEGIN TRY
				DELETE FROM Cliente
				WHERE ClienteID = @Id_cliente
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH