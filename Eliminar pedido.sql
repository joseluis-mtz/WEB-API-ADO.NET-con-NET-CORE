CREATE PROCEDURE [dbo].[SP_Eliminar_Pedido]
    @Id_Pedido int
AS
	BEGIN TRANSACTION
--Consultar si el pedido existe en la tabla PedidoDetalle, la cual es la relación de productos solicitados
--Si el resultado es VERDADERO, entonces primero eliminamos en la tabla PedidoDetalle y posteriormente en la tabla Pedido, 
--ya que existe una relación entre las dos tablas.
	  IF (
		    (@Id_Pedido IS NOT NULL)	
			AND EXISTS (SELECT PedidoID FROM PedidoDetalle
			             WHERE PedidoID = @Id_Pedido
				   )
             )
			BEGIN TRY
				DELETE FROM PedidoDetalle
				WHERE PedidoID = @Id_Pedido

				DELETE FROM Pedido
				WHERE PedidoID = @Id_Pedido

				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH
	 ELSE IF (
	           EXISTS (SELECT PedidoID FROM Pedido WHERE PedidoID = @Id_Pedido)
	         )
			   BEGIN
				 DELETE FROM Pedido
				 WHERE PedidoID = @Id_Pedido
				COMMIT TRANSACTION
			   END