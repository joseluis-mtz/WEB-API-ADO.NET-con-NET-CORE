CREATE TRIGGER [Trigger_ValorTotal]
	ON [dbo].[PedidoDetalle]
	FOR INSERT, UPDATE
	AS
	BEGIN
		--SET NOCOUNT ON agregado para evitar conjuntos de resultados adicionales
		-- interferir con las instrucciones SELECT.
	  SET NOCOUNT ON;

	  -- obtener el último valor de identificación del registro insertado o actualizado
	  DECLARE @PedidoID INT, @VlrTotalPedido DECIMAL(18,0)
	  SELECT @PedidoID = [PedidoID]
	  FROM INSERTED

	  SELECT @VlrTotalPedido = SUM(ValorTotal) FROM PedidoDetalle
	  WHERE PedidoID = @PedidoID

	  -- Insertar declaraciones para desencadenar aquí
	  UPDATE Pedido SET [ValorTotal] = @VlrTotalPedido
	  WHERE Pedido.PedidoID= @PedidoID
	END