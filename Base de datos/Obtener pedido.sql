CREATE PROCEDURE [dbo].[SP_Obtener_Pedido]
	@Id_Pedido int = 0
AS
	IF (@Id_Pedido = 0)
	 BEGIN
		SELECT PedidoID, P.ClienteID, C.Nombre + ' ' + C.Apellidos AS NombreCliente, EstadoPedido, FechaPedido, ValorTotal
		FROM  Pedido P, Cliente C
		WHERE P.ClienteID = C.ClienteID
	 END
    ELSE
	 BEGIN
		SELECT PedidoID, P.ClienteID, C.Nombre + ' ' + C.Apellidos AS NombreCliente, EstadoPedido, FechaPedido, ValorTotal
		FROM  Pedido P, Cliente C
		WHERE P.ClienteID = C.ClienteID
		  AND PedidoID = @Id_Pedido
	 END