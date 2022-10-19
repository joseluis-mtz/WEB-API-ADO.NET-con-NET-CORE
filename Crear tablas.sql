CREATE TABLE [dbo].[Producto] (
    [ProductoID] INT           NOT NULL,
    [NombreProd] NVARCHAR (50) NOT NULL,
    [PrecioUnit] DECIMAL (18)  NOT NULL,
    [EstadoProd] CHAR (1)      NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductoID] ASC),
    CONSTRAINT [CK_Producto_Estado] CHECK ([EstadoProd]='I' OR [EstadoProd]='A')
);

CREATE TABLE [dbo].[Cliente] (
    [ClienteID] INT            NOT NULL,
    [Nombre]    NVARCHAR (50)  NOT NULL,
    [Apellidos] NVARCHAR (50)  NOT NULL,
    [Direccion] NVARCHAR (100) NOT NULL,
    [Telefono]  NCHAR (10)     NOT NULL,
    PRIMARY KEY CLUSTERED ([ClienteID] ASC)
);

CREATE TABLE [dbo].[Pedido] (
    [PedidoID]     INT          IDENTITY (1, 1) NOT NULL,
    [ClienteID]    INT          NOT NULL,
    [FechaPedido]  DATE         NOT NULL,
    [EstadoPedido] CHAR (1)     NULL,
    [ValorTotal]   DECIMAL (18) NOT NULL,
    PRIMARY KEY CLUSTERED ([PedidoID] ASC),
    CONSTRAINT [FK_Pedido_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [CK_Pedido_EstadoPedido] CHECK ([EstadoPedido]='I' OR [EstadoPedido]='A')
);

CREATE TABLE [dbo].[PedidoDetalle] (
    [ProductoID]    INT          NOT NULL,
    [PedidoID]      INT          NOT NULL,
    [Cantidad]      INT          NOT NULL,
    [ValorUnitario] DECIMAL (18) NOT NULL,
    [ValorTotal]    DECIMAL (18) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductoID] ASC, [PedidoID] ASC),
    CONSTRAINT [FK_PedidoDetalle_Producto] FOREIGN KEY ([ProductoID]) REFERENCES [dbo].[Producto] ([ProductoID]),
    CONSTRAINT [FK_PedidoDetalle_Pedido] FOREIGN KEY ([PedidoID]) REFERENCES [dbo].[Pedido] ([PedidoID])
);