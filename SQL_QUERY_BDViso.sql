Create database BDViso

Use BDViso

CREATE TABLE Proveedor(
    Proveedor_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre varchar(255),
    Direccion varchar(255)
);

CREATE TABLE Telefono(
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Proveedor_Id int FOREIGN KEY REFERENCES Proveedor(Proveedor_Id),
    Numero int 
);

CREATE TABLE Categoria(
	Categoria_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nombre varchar (255)
);

CREATE TABLE Marca(
	Marca_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nombre varchar (255)
);


CREATE TABLE Producto(
    Producto_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Proveedor_Id int FOREIGN KEY REFERENCES Proveedor(Proveedor_Id),
	Descripcion varchar (255),
	Categoria_Id int FOREIGN KEY REFERENCES Categoria(Categoria_Id),
	Cantidad int,
	Costo float(2),
	Precio float(2),
	Marca_Id int FOREIGN KEY REFERENCES Marca(Marca_Id)
);

CREATE TABLE Cliente(
    Cliente_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Nombre varchar(255),
    Apellido varchar(255) 
);

CREATE TABLE Tipo_Moneda(
    Moneda_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Tipo varchar(255),
);

CREATE TABLE Factura(
    Numero_Factura int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Fecha varchar(255),
    Cliente_Id int FOREIGN KEY REFERENCES Cliente(Cliente_Id),
	Monto_final int,
	Moneda_Id int FOREIGN KEY REFERENCES Tipo_Moneda(Moneda_Id)
);

CREATE TABLE Detalle_Factura(
    Detalle_Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Numero_Factura int FOREIGN KEY REFERENCES Factura(Numero_Factura),
	Producto_Id int FOREIGN KEY REFERENCES Producto(Producto_Id),
	Cantidad int,
	Precio float(2),
	Total_ventas float(2)
);

CREATE TABLE Proveedor_Producto(
	Proveedor_Id int FOREIGN KEY REFERENCES Proveedor(Proveedor_Id),
	Producto_Id int FOREIGN KEY REFERENCES Producto(Producto_Id)
)

--Tabla Proveedor
Insert into Proveedor(Nombre, Direccion) values ('Gonper', 'De la Policia, 1/2 al norte')
Insert into Proveedor(Nombre, Direccion) values ('Hispamer', '4PHJ+7X2, Managua 14003')
--Select * From Proveedor
--Tabla Telefono
Insert into Telefono(Proveedor_Id,Numero) values (1, 25222059)
Insert into Telefono(Proveedor_Id,Numero) values (2, 22781210)
--Select * From Telefono
--Tabla Categoria
Insert into Categoria(Nombre) values ('Boligrafos')
Insert into Categoria(Nombre) values ('Cuadernos')
Insert into Categoria(Nombre) values ('Lapiz')
Insert into Categoria(Nombre) values ('Libretas')
Insert into Categoria(Nombre) values ('Manualidades')
Insert into Categoria(Nombre) values ('Material De Arte')
Insert into Categoria(Nombre) values ('Material Escolar')
Insert into Categoria(Nombre) values ('Material Escritura')
Insert into Categoria(Nombre) values ('Material Oficina')
Insert into Categoria(Nombre) values ('Material Regalos')
Insert into Categoria(Nombre) values ('Papel')
--Select * From Categoria
--Tabla Marca
Insert into Marca(Nombre) values ('Artline')
Insert into Marca(Nombre) values ('BIC')
Insert into Marca(Nombre) values ('Kadio')
Insert into Marca(Nombre) values ('Lider')
Insert into Marca(Nombre) values ('Loro')
Insert into Marca(Nombre) values ('Merletto')
Insert into Marca(Nombre) values ('MEMO')
Insert into Marca(Nombre) values ('PaperMate')
Insert into Marca(Nombre) values ('Pentel')
Insert into Marca(Nombre) values ('Pontier')
Insert into Marca(Nombre) values ('Tukan')
Insert into Marca(Nombre) values ('Zebra')
Insert into Marca(Nombre) values ('Faber Castle')
Insert into Marca(Nombre) values ('Stabilo')
Insert into Marca(Nombre) values (Null)
--Select * From Marca
--Tabla Producto
--Boligrafos
Insert into Producto( Proveedor_Id, Descripcion, Cantidad, Categoria_Id, Costo, Precio, Marca_Id) 
values (1, 'Lapicero azul', 69, 1, 3.16,  5, 2)
Insert into Producto( Proveedor_Id, Descripcion, Cantidad, Categoria_Id, Costo, Precio, Marca_Id) 
values (1, 'Lapicero negro', 35, 1, 3.16,  5, 2)
Insert into Producto( Proveedor_Id, Descripcion, Cantidad, Categoria_Id, Costo, Precio, Marca_Id) 
values (1, 'Lapicero rojo', 22, 1, 3.16,  5, 2)
Insert into Producto( Proveedor_Id, Descripcion, Cantidad, Categoria_Id, Costo, Precio, Marca_Id) 
values (1, 'Lapicero Azul 0.7', 6, 1, 10.88,  14, 8)
Insert into Producto( Proveedor_Id, Descripcion, Cantidad, Categoria_Id, Costo, Precio, Marca_Id) 
values (1, 'Lapicero negro 0.5 Gel', 5, 1, 8.68,  12, 8)
-----Cuadernos
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Cuaderno Cosido Cuadriculado', 2, 6, 35.00,  40, 4)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Cuaderno Cosido Cuadriculado Normal', 2, 10, 2.38,  30, 4)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Cuaderno Cosido Doble Raya', 2, 4, 23.75,  30, 5)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Cuaderno Cosido Rayado', 2, 22, 24.77,  30, 5)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Cuaderno Espiral Rayado', 2, 16, 12.50, 16, 5)
---Lapiz
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Lapiz Con Puntas', 3, 24, 3.17, 5, 10)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Lapiz Grafito Jumbo', 3, 85, 2.02, 4, 9)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Lapiz Grafito Neon', 3, 12, 2.08, 4, 14)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Lapiz Grafito Mecanico', 3, 24, 5.83, 8, 14)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Lapiz Metalico', 3, 135, 1.75, 3, 10)
--Libretas
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Block Con Raya Cocido', 4, 16, 10.38, 13, 7)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Block Con Raya Doble Cara Cocido', 4, 6, 10.50, 13, 7)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Block Espiral Con Raya', 4, 6, 18, 22, 4)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Block Espiral Sin Raya', 4, 8, 10, 12, 4)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Libreta De Apuentes', 4, 9, 14.72, 18, 7)
--Manualidades
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Cartulina Corriente', 5, 45, 3.40, 5, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Cartulina Satinada', 5, 21, 8.33, 12, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Escarche Plateado Bolsitas', 5, 62, 2.50, 5, 6)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Foamy Moldeable', 5, 2, 45, 55, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Foamy Azul', 5, 18, 2.50, 5, 15)
--Materiales De Arte
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Acuarelas', 6, 3, 28, 30, 11)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Crayolas Pequeñas Cajas', 6, 20, 8.33, 12, 12)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Plastilina', 6, 7, 11.25, 14, 1)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (2, 'Tempera Tukan', 6, 5, 26, 32, 11)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Lapices De Colores Cajas', 6, 4, 45, 52, 13)
---Material Escolar
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Calculadora KD 815', 7, 4, 35, 40, 3)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Estuches GeométricoMetálico', 7, 9, 12.67, 18, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Fichas Bibliograficas 3x5cm peq', 7, 6, 0, 1, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Forros De Boletines', 7, 18, 15, 18, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Mapas', 7, 12, 1.67, 3, 15)
---Material Escritura
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Borradores De Cabeza De Lapiz', 8, 24, 1.07, 2, 10)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Borradores De Grafito Grande', 8, 29, 2.25, 4, 14)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Borradores Lapiz', 8, 62, 4.50, 6, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Corrector Lapiz', 8, 18, 4.50, 6, 10)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Marcador Acrilico AL', 8, 7, 0, 20, 9)
---Material Oficina
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Barra De Silicon', 9, 22, 3.18, 4, 10)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Carpeta', 9, 12, 7, 15, 15)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Chinche Cabeza Plastico', 9, 6, 17, 20, 10)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Chinche Metalico Caja', 9, 35, 3.50, 5, 12)
Insert into Producto( Proveedor_Id, Descripcion,  Categoria_Id, Cantidad, Costo, Precio, Marca_Id) 
values (1, 'Engrapadora Peq C/Grp', 9, 5, 32, 40, 15)
INSERT INTO Producto(Proveedor_Id, Descripcion, Categoria_Id, Cantidad, Costo, Precio, Marca_Id)
VALUES
    (1, 'Sellador 30yd', 9, 10, 8.00, 10.00, 10),
    (2, 'Tape de regalo', 9, 34, 1.67, 3.00, 6),
    (2, 'Tape Escarchado', 9, 12, 12.00, 15.00, 6),
    (1, 'Tijeras pequeña escolar S/P', 9, 17, 5.00, 7.00, 15),
    (1, 'Tijeras Stanless Steel', 9, 2, 10.00, 15.00, 15);
-----Material Regalos
INSERT INTO Producto(Proveedor_Id, Descripcion, Categoria_Id, Cantidad, Costo, Precio, Marca_Id)
VALUES
    (2, 'Banderas Plasticas', 10, 14, 1.50, 4.00, 15),
    (2, 'Banderas Plasticas', 10, 14, 1.50, 4.00, 15),
    (2, 'Bolsa de Regalo Grande', 10, 12, 9.58, 15.00, 15),
    (2, 'Bolsa de regalo mediana', 10, 11, 7.50, 10.00, 15),
    (2, 'Bolsa de regalo pequeña/panam', 10, 10, 6.00, 8.00, 15);
-----Papel
INSERT INTO Producto(Proveedor_Id, Descripcion, Categoria_Id, Cantidad, Costo, Precio, Marca_Id)
VALUES
    (1, 'Hojas de Colores', 11, 131, 0.60, 1.00, 15),
    (1, 'Pliegos de Papel Craff', 11, 44, 3.20, 5.00, 15),
    (1, 'Pliegos de papel de regalo', 11, 48, 3.20, 5.00, 15),
    (1, 'Pliegos de papelón Bond', 11, 82, 1.80, 4.00, 15),
    (1, 'Hojas blancas Resma de papel', 11, 467, 0.20, 0.50, 7);
--Select * From Producto
--Tabla Proveedor_Producto
Insert into Proveedor_Producto( Proveedor_Id, Producto_Id) 
values 
	(1, 1),
	(1, 2),
	(1, 3),
	(1, 4),
	(1, 5),
	(1, 6),
	(1, 7),
	(1, 8),
	(1, 9),
	(2, 10),
	(1, 11),
	(1, 12),
	(2, 13),
	(1, 14),
	(1, 15),
	(1, 16),
	(1, 17),
	(1, 18),
	(2, 19),
	(1, 20),
	(1, 21),
	(1, 22),
	(2, 23),
	(1, 24),
	(1, 25),
	(1, 26),
	(1, 27),
	(2, 28),
	(2, 29),
	(1, 30),
	(1, 31),
	(1, 32),
	(1, 33),
	(1, 34),
	(1, 35),
	(1, 36),
	(1, 37),
	(1, 38),
	(1, 39),
	(1, 40),
	(1, 41),
	(1, 42),
	(1, 43),
	(1, 44),
	(1, 45),
	(1, 46),
	(2, 47),
	(2, 48),
	(1, 49),
	(1, 50),
	(2, 51),
	(2, 52),
	(2, 53),
	(2, 54),
	(2, 55),
	(1, 56),
	(1, 57),
	(1, 58),
	(1, 59),
	(1, 60);
--Select * From Proveedor_Producto
--Tabla Clientes
Insert into Cliente (Nombre, Apellido) Values ('Maria', 'Urtecho')
Insert into Cliente (Nombre, Apellido) Values ('Victor', 'Guevara')
Insert into Cliente (Nombre, Apellido) Values ('Ana', 'Siu')
Insert into Cliente (Nombre, Apellido) Values ('Leonel', 'Chavez')
Insert into Cliente (Nombre, Apellido) Values ('Andrea', 'Lopez')
Insert into Cliente (Nombre, Apellido) Values ('Veronica', 'Martinez')
Insert into Cliente (Nombre, Apellido) Values ('Carolina', 'Cortez')
Insert into Cliente (Nombre, Apellido) Values ('Cristian', 'Tinoco')
Insert into Cliente (Nombre, Apellido) Values ('Josue', 'Herrera')
Insert into Cliente (Nombre, Apellido) Values ('Camila', 'Sanchez')
--SELECT * FROM Cliente
--Tabla Tipo Moneda
Insert Into Tipo_Moneda(Tipo) values ('Cordoba')
Insert Into	Tipo_Moneda(Tipo) values ('Dolar')
--Select * From Tipo_Moneda
--Tabla Factura
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-01-10', 1, 62, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-02-15', 2, 300, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-02-17', 3, 36, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-03-05', 4, 40, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-04-01', 5, 80, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-05-10', 6, 180, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-05-12', 7, 66, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-06-21', 8, 56, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-07-11', 9, 24, 1)
Insert into Factura (Fecha,Cliente_Id,Monto_Final,Moneda_Id) Values ('2023-08-25', 10, 1, 2)
--Select * From Factura
--
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (1, 2, 2,5, 10)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (1, 30, 1,52, 52)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (2, 7, 10,30, 300)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (3, 5, 3,12, 36)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (4, 6, 1,40, 40)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (5, 10, 5,16, 80)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (6, 20, 10,18, 180)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (7, 18, 3,22, 66)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (8, 28, 4,14, 56)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (9, 55, 3,8, 24)
Insert into Detalle_Factura (Numero_Factura,Producto_Id,Cantidad,Precio,Total_ventas) Values (10, 32, 2,18, 1)
--Select * From Detalle_Factura

/*
Select Sum(Monto_final)
From Factura
Where (Fecha Between '2023-03-02' and '2023-05-01')
*/

SELECT * FROM Proveedor;

SELECT * FROM Marca;

SELECT * FROM Producto;

SELECT * FROM Producto WHERE Descripcion = 'Lapicero verde';

SELECT Producto.Producto_Id AS Código, Producto.Descripcion, Proveedor.Nombre AS Proveedor, Categoria.Nombre AS Categoría, Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio
FROM Producto INNER JOIN Proveedor
ON Producto.Proveedor_Id = Proveedor.Proveedor_Id INNER JOIN Categoria
ON Producto.Categoria_Id = Categoria.Categoria_Id INNER JOIN Marca
ON Producto.Marca_Id = Marca.Marca_Id
WHERE LOWER(Marca.Nombre) LIKE '%memo%'
ORDER BY Marca.Nombre ASC

SELECT Factura.Numero_Factura AS Id, Cliente.Nombre + ' ' + Cliente.Apellido AS 'Cliente', Factura.Fecha, Producto.Descripcion, Detalle_Factura.Cantidad, Tipo_Moneda.Tipo, Detalle_Factura.Precio AS Monto, Detalle_Factura.Total_ventas AS Total, Factura.Monto_final AS Final
FROM Factura INNER JOIN Cliente
ON Factura.Cliente_Id = Cliente.Cliente_Id INNER JOIN Tipo_Moneda
ON Factura.Moneda_Id = Tipo_Moneda.Moneda_Id INNER JOIN Detalle_Factura
ON Factura.Numero_Factura = Detalle_Factura.Numero_Factura INNER JOIN Producto
ON Detalle_Factura.Producto_Id = Producto.Producto_Id
ORDER BY Factura.Fecha

INNER JOIN Producto
ON Factura.Producto_Id = Producto.Producto_Id;

select * from Detalle_Factura
select * from Factura
select * from Producto WHERE Producto_Id = 1;
select * from Cliente
SELECT Proveedor_Id FROM Proveedor WHERE Nombre = 'Gonper'
select * from Categoria

UPDATE Producto 
SET Descripcion = '@descripcion',
Proveedor_Id = '@proveedorId', 
Categoria_Id = '@categoriaId',
Marca_Id = '@marcaId',
Cantidad = '@cantidad',
Costo = '@costo',
Precio = '@precio'
WHERE Producto_Id = 1
/*
string query = "UPDATE Producto " +
                "SET Descripcion = @descripcion, " +
                "Proveedor_Id = @proveedorId, " +
                "Categoria_Id = @categoriaId, " +
                "Marca_Id = @marcaId, " +
                "Cantidad = @cantidad, " +
                "Costo = @costo, " +
                "Precio = @precio " +
                "WHERE Producto_Id = @id";
*/

SELECT Categoria_Id, Nombre FROM Categoria
