CREATE PROCEDURE [dbo].[SP_Obtener_Cliente]
    @Id_cliente int = 0
AS
	IF (@Id_cliente = 0)
	 BEGIN
		SELECT ClienteID, Nombre, Apellidos, Direccion, Telefono
		FROM  Cliente
	 END
    ELSE
	 BEGIN
		SELECT ClienteID, Nombre, Apellidos, Direccion, Telefono
		FROM  Cliente
		WHERE ClienteID = @Id_cliente
	 END