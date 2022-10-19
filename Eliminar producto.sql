CREATE PROCEDURE [dbo].[SP_Eliminar_Producto]
    @Id_Producto int
AS
	BEGIN TRANSACTION
	  IF (
		    (@Id_Producto IS NOT NULL)	
			AND EXISTS (SELECT ProductoID FROM Producto WHERE ProductoID = @Id_Producto)
			)
			BEGIN TRY
				DELETE FROM Producto
				WHERE ProductoID = @Id_Producto
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH