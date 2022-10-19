CREATE PROCEDURE [dbo].[SP_Obtener_PedidoDetalle]
	@Id_Pedido int = 0,
	@Id_Producto int = 0
AS
	IF (@Id_Pedido = 0)
	 BEGIN
		SELECT PedidoID, ProductoID, Cantidad, ValorUnitario, ValorTotal
		FROM  PedidoDetalle
		WHERE PedidoID = @Id_Pedido
		  AND ProductoID = @Id_Producto
	 END
    ELSE
	 BEGIN
		SELECT PedidoID, ProductoID, Cantidad, ValorUnitario, ValorTotal
		FROM  PedidoDetalle
		WHERE PedidoID = @Id_Pedido
	 END