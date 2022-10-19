CREATE PROCEDURE [dbo].[SP_Agregar_Producto]
	@Id_Producto int = 0,	
	@Nom_Producto nvarchar(50),	
	@Pre_Producto decimal(18,0),
	@Est_Producto char(1) = 'A'
AS
	BEGIN TRANSACTION
		IF (
			(@Id_Producto IS NOT NULL)
			AND (@Nom_Producto IS NOT NULL AND LEN(@Nom_Producto) !=0)
			AND (@Pre_Producto IS NOT NULL AND LEN(@Pre_Producto) !=0)			
			AND NOT EXISTS (SELECT ProductoID FROM Producto WHERE ProductoID = @Id_Producto)
			)
			BEGIN TRY
				INSERT INTO Producto
				(
				 ProductoID,
				 NombreProd,
				 PrecioUnit,
				 EstadoProd
				)
				VALUES
				(
				 @Id_Producto,
				 @Nom_Producto,
				 @Pre_Producto,
				 @Est_Producto
				)				
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH
		ELSE
			BEGIN TRY
				UPDATE Producto SET 
					NombreProd=@Nom_Producto,
					PrecioUnit=@Pre_Producto,
					EstadoProd=@Est_Producto
				WHERE ProductoID = @Id_Producto
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH