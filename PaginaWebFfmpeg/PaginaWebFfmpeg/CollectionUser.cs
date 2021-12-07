using MySql.Data.MySqlClient;
using PaginaWebFfmpeg.Controllers;
using PaginaWebFfmpeg.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaWebFfmpeg
{
    public class CollectionUser
    {

        public static CollectionUser instance;


        public static CollectionUser getInstance()
        {
            if (instance == null)
            {
                CollectionUser.instance = new CollectionUser();
            }

            return instance;

        }

        public List<User> verUsuarios()
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            queryBuilder.setQuery("SELECT * FROM usuarios");

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            var lista = new List<User>();
            while (dataReader.Read())
            {
                var usuario = new User(dataReader.GetString(1), dataReader.GetString(2));
                lista.Add(usuario);
            }

            return lista;

        }

        public User verUsuario(String nombre, String contraseña)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("SELECT * FROM usuarios WHERE nombre=@nombre AND contraseña=@contraseña");
            queryBuilder.addParam("@nombre", nombre);
            queryBuilder.addParam("@contraseña", contraseña);

            var dataReader = DBConnection.getInstance().select(queryBuilder);
            User usuario = null;
            while (dataReader.Read())
            {
                usuario = new User(dataReader.GetString(1), dataReader.GetString(2));
                usuario.licenciado = dataReader.GetBoolean(3);
            }

            return usuario;

        }


        /* public bool tieneLicencia(String nombre)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.storeProcedure("CALL tiene_licencia(@nombre, @respuesta)");
            queryBuilder.addParam("@nombre", nombre);
            var returnParameter = queryBuilder.addParamProcedure("@respuesta");
            returnParameter.Direction = ParameterDirection.ReturnValue;

            DBConnection.getInstance().abm(queryBuilder);
            var result = returnParameter.Value;
            return result;
        }   */

        public void agregarUsuario(User usuario)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("INSERT INTO usuarios (nombre,contraseña) VALUES (@nombre,@contraseña)");
            queryBuilder.addParam("@nombre", usuario.username);
            queryBuilder.addParam("@contraseña", usuario.password);
            DBConnection.getInstance().abm(queryBuilder);

        }

        public void eliminarUsuario(User usuario)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("DELETE FROM usuarios WHERE nombre=@nombre");
            queryBuilder.addParam("@nombre", usuario.username);

            DBConnection.getInstance().abm(queryBuilder);

        }

        public void eliminarUsuario(String nombre)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("DELETE FROM usuarios WHERE nombre=@nombre");
            queryBuilder.addParam("@nombre", nombre);

            DBConnection.getInstance().abm(queryBuilder);

        }

        public void actualizarUsuario(User usuario)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();

            queryBuilder.setQuery("UPDATE usuarios SET nombre=@nombre,contraseña=@contraseña WHERE nombre=@nombre");
            queryBuilder.addParam("@nombre", usuario.username);
            queryBuilder.addParam("@contraseña", usuario.password);

            DBConnection.getInstance().abm(queryBuilder);


        }

    }
}
