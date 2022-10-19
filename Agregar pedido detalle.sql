CREATE PROCEDURE [dbo].[SP_Agregar_PedidoDetalle]
	@Id_Pedido int,	
	@Id_Producto int,
	@Cant_Prod int,	
	@VlrUni_Prod decimal(18,0)
AS
	BEGIN TRANSACTION
		IF (
			(@Id_Pedido IS NOT NULL)
			AND (@Id_Producto IS NOT NULL)			
			AND (@Cant_Prod IS NOT NULL AND LEN(@Cant_Prod) !=0)
			AND (@VlrUni_Prod IS NOT NULL AND LEN(@VlrUni_Prod) !=0)			
			AND NOT EXISTS (SELECT PedidoID 
			                 FROM PedidoDetalle
			                 WHERE PedidoID = @Id_Pedido
							   AND ProductoID = @Id_Producto)
			)
			BEGIN TRY
				INSERT INTO PedidoDetalle
				(
				 PedidoID,
				 ProductoID,
				 Cantidad,
				 ValorUnitario,
				 ValorTotal
				)
				VALUES
				(
				 @Id_Pedido,
				 @Id_Producto,
				 @Cant_Prod,
				 @VlrUni_Prod,
				 (@VlrUni_Prod * @Cant_Prod)
				)				
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION					
				THROW
			END CATCH
		ELSE
			BEGIN TRY
				UPDATE PedidoDetalle SET 
				    Cantidad=@Cant_Prod,
					ValorUnitario=@VlrUni_Prod,
					ValorTotal=(@VlrUni_Prod * @Cant_Prod)
				WHERE PedidoID = @Id_Pedido
				  AND ProductoID = @Id_Producto
				COMMIT TRANSACTION
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION
				THROW
			END CATCH