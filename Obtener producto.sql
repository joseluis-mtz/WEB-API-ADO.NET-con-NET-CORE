CREATE PROCEDURE [dbo].[SP_Obtener_Producto]
	@Id_Producto int = 0	
AS
	IF (@Id_Producto = 0)
	 BEGIN
		SELECT ProductoID, NombreProd, PrecioUnit, EstadoProd
		FROM  Producto
	 END
    ELSE
	 BEGIN
		SELECT ProductoID, NombreProd, PrecioUnit, EstadoProd
		FROM  Producto
		WHERE ProductoID = @Id_Producto
	 END