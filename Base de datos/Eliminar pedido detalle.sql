CREATE PROCEDURE [dbo].[SP_Eliminar_PedidoDetalle]
@Id_Pedido int,	
@Id_Producto int = 0
AS
BEGIN TRANSACTION
IF((@Id_Pedido IS NOT NULL)	AND (@Id_Producto IS NOT NULL AND @Id_Producto = 0) AND EXISTS (SELECT PedidoID FROM PedidoDetalle WHERE PedidoID = @Id_Pedido))			
BEGIN
	DELETE FROM PedidoDetalle WHERE PedidoID = @Id_Pedido
	COMMIT TRANSACTION
END		
ELSE IF((@Id_Pedido IS NOT NULL) AND (@Id_Producto IS NOT NULL AND @Id_Producto != 0) AND EXISTS (SELECT PedidoID FROM PedidoDetalle WHERE PedidoID = @Id_Pedido AND ProductoID = @Id_Producto))
BEGIN TRY
	DELETE FROM PedidoDetalle WHERE PedidoID = @Id_Pedido AND ProductoID = @Id_Producto
	COMMIT TRANSACTION
END TRY			
BEGIN CATCH
	ROLLBACK TRANSACTION					
	THROW
END CATCH