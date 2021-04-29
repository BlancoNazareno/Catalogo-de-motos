create database catalogoMotos_DB
use catalogoMotos_DB

--Select A.ID, A.Nombre, A.Descripcion, A.Cc, A.ImagenUrl, A.Precio, C.Descripcion Categoria, M.Descripcion Marca 
--From ARTICULOS A join CATEGORIAS C on A.IdCategoria = C.Id join MARCAS M on A.IdMarca = M.Id

create table ARTICULOS(
ID int identity (1,1) not null primary key,
Nombre varchar(50) not null,
Descripcion varchar(100) null,
Cc decimal null,
ImagenUrl varchar (300) null,
ID_Categoria int not null,
ID_Marca int not null,
Precio money not null, 
)

create table CATEGORIAS(
ID int identity (1,1) primary key not null,
Descripcion varchar(50) not null,
)

create table MARCAS(
ID int identity (1,1) primary key not null,
Descripcion varchar(50) not null,
)


Alter table ARTICULOS ADD constraint FK_Categorias foreign key (ID_Categoria) references CATEGORIAS(ID)
Alter table ARTICULOS ADD constraint FK_Marcas foreign key (ID_Marcas) references MARCAS(ID)
