CREATE PROCEDURE [dbo].[SP_Agregar_Cliente]
	@Id_cliente int = 0,	
	@Nom_Cliente nvarchar(50),	
	@Ape_Cliente nvarchar(50),
	@Dir_cliente nvarchar(100),
	@Tel_Cliente nvarchar(10)
AS
	BEGIN TRANSACTION
		IF (
			(@Id_cliente IS NOT NULL)
			AND (@Nom_Cliente IS NOT NULL AND LEN(@Nom_Cliente) !=0)
			AND (@Ape_Cliente IS NOT NULL AND LEN(@Ape_Cliente) !=0)			
			AND (@Dir_cliente IS NOT NULL AND LEN(@Dir_cliente) !=0)
			AND (@Tel_Cliente IS NOT NULL AND LEN(@Tel_Cliente) !=0)
			AND NOT EXISTS (SELECT ClienteID FROM Cliente WHERE ClienteID = @Id_cliente)
			)
			BEGIN TRY
				INSERT INTO Cliente
				(
				 ClienteID,
				 Nombre,
				 Apellidos,
				 Direccion,
				 Telefono
				)
				VALUES
				(
				 @Id_cliente,
				 @Nom_Cliente,
				 @Ape_Cliente,
				 @Dir_cliente,
				 @Tel_Cliente
				)				
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH
		ELSE
		   BEGIN TRY
				UPDATE Cliente SET 
					Nombre=@Nom_Cliente,
					Apellidos=@Ape_Cliente,
					Direccion=@Dir_cliente,
					Telefono=@Tel_Cliente
				WHERE ClienteID = @Id_cliente
				COMMIT TRANSACTION
		   END TRY
		   BEGIN CATCH
				ROLLBACK TRANSACTION
				THROW
		   END CATCH