CREATE TRIGGER [Trigger_ProductoPedido]
	ON [dbo].[PedidoDetalle]
	FOR DELETE
	AS
	BEGIN
		--SET NOCOUNT ON agregado para evitar conjuntos de resultados adicionales
		-- interferir con las instrucciones SELECT.
		SET NOCOUNT ON

		-- obtener el identificación del registro eliminado
	  DECLARE @PedidoID INT, @VlrTotalPedido DECIMAL(18,0)
	  SELECT @PedidoID = [PedidoID]
	  FROM DELETED

	  SELECT @VlrTotalPedido = SUM(ValorTotal) FROM PedidoDetalle
	  WHERE PedidoID = @PedidoID

	  -- Insertar declaraciones para desencadenar aquí
	  UPDATE Pedido SET [ValorTotal] = @VlrTotalPedido
	  WHERE Pedido.PedidoID= @PedidoID
	END