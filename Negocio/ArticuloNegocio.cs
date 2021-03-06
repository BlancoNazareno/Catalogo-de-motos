﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data.SqlClient;


namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=catalogoMotos_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Select A.ID, A.Nombre, A.Descripcion, A.Cc, A.ImagenUrl, A.Precio, C.Descripcion Categoria, M.Descripcion Marca From ARTICULOS A join CATEGORIAS C on A.ID_Categoria = C.Id join MARCAS M on A.ID_Marca = M.Id"; 
            comando.Connection = conexion;

            conexion.Open();    
            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Articulo aux = new Articulo();

                aux.Id = (int)lector["ID"];
                aux.Nombre = lector.GetString(1);
                aux.Descripcion = lector.GetString(2);
                aux.Cc = lector.GetSqlDecimal(3); /*(float)lector["Cc"];*/
                aux.ImagenUrl = (string)lector["ImagenUrl"];
                aux.Precio = lector.GetSqlMoney(5);
                

                aux.Categoria = new Categoria();
                aux.Categoria.Descripcion = (string)lector["Categoria"];
                aux.Categoria.Id = (int)lector["ID"]; 

                aux.Marca = new Marca();
                aux.Marca.Descripcion = (string)lector["Marca"];
                aux.Marca.Id = (int)lector["ID"];         

                lista.Add(aux);
            }   

            conexion.Close();
            return lista;

        }

        public void agregar(Articulo nuevo)
        {


            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            List<Articulo> lista = new List<Articulo>();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=catalogoMotos_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Insert into ARTICULOS (Nombre, Descripcion, Cc, Precio, ImagenUrl, ID_Categoria, ID_Marca) Values (@Nombre, @Descripcion, @Cc, @Precio, @ImagenUrl, @IdCategoria, @IdMarca)";
            comando.Parameters.AddWithValue("@Cc", nuevo.Cc);
            comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);
            comando.Parameters.AddWithValue("@Precio", nuevo.Precio);
            comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenUrl);
            comando.Parameters.AddWithValue("@IdCategoria", nuevo.Categoria.Id);
            comando.Parameters.AddWithValue("@IdMarca", nuevo.Marca.Id);
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();

        }

        public void modificar(Articulo artic)
        {
            
            try
            {
                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = new SqlCommand();
                List<Articulo> lista = new List<Articulo>();

                conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=catalogoMotos_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Update ARTICULOS set Nombre=@Nombre, Descripcion=@Descripcion, Cc=@Cc, Precio=@Precio, ImagenUrl=@ImagenUrl, ID_Categoria=@IdCategoria, ID_Marca=@IdMarca where Id=@Id";

                comando.Parameters.AddWithValue("@Id", artic.Id);
                comando.Parameters.AddWithValue("@Cc", artic.Cc);
                comando.Parameters.AddWithValue("@Nombre", artic.Nombre);
                comando.Parameters.AddWithValue("@Descripcion",artic.Descripcion);
                comando.Parameters.AddWithValue("@Precio",artic.Precio);
                comando.Parameters.AddWithValue("@ImagenUrl",artic.ImagenUrl);
                comando.Parameters.AddWithValue("@IdCategoria",artic.Categoria.Id);
                comando.Parameters.AddWithValue("@IdMarca",artic.Marca.Id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();  
            }

            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void eliminar(int id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            conexion.ConnectionString = "data source=.\\sqlexpress; initial catalog=catalogoMotos_DB; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "Delete From ARTICULOS Where Id=@Id";

            comando.Parameters.AddWithValue("@Id", id);
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();

            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}

