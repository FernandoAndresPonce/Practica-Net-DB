using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using DataAccess;

namespace BusinessLogic
{
    public class Negocio
    {
        private AccesoDatos datos;
        private List<Pokemon> listaPokemones; // Ojo mas adelante.
        private Pokemon pokemonAuxiliar;

        public Negocio()
        {
            this.datos = new AccesoDatos();
            this.listaPokemones = new List<Pokemon>();
            
        }

        public List<Pokemon> Listar()
        {
                try
                { 
                datos.setearConsulta("Select P.Id, P.Numero, P.Nombre, P.Descripcion,P.UrlImagen, T.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad from POKEMONS P, ELEMENTOS T, ELEMENTOS D Where P.IdTipo = T.Id And P.IdDebilidad = D.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    pokemonAuxiliar = new Pokemon();
                    pokemonAuxiliar.Id = (int)datos.Lector["Id"];
                    pokemonAuxiliar.Numero = (int)datos.Lector["Numero"];
                    pokemonAuxiliar.Nombre = (string)datos.Lector["Nombre"];
                    pokemonAuxiliar.Descripcion = (string)datos.Lector["Descripcion"];
                    if(!(datos.Lector["UrlImagen"] is DBNull))
                        pokemonAuxiliar.UrlImagen = (string)datos.Lector["UrlImagen"];

                    pokemonAuxiliar.Tipo = new Elemento();
                    pokemonAuxiliar.Tipo.Id = (int)datos.Lector["IdTipo"];
                    pokemonAuxiliar.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    pokemonAuxiliar.Debilidad = new Elemento();
                    pokemonAuxiliar.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    pokemonAuxiliar.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    listaPokemones.Add(pokemonAuxiliar);

                }

                    return listaPokemones;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                finally
                {
                    datos.cerrarConexion();
                }

            

        }

        public void Agregar(Pokemon pokemon)
        {
            try
            {
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, UrlImagen, IdTipo, IdDebilidad, Activo) values (@Numero, @Nombre, @Descripcion, @UrlImagen, @IdTipo, @IdDebilidad, 1)");
                datos.setearParametro("@Numero", pokemon.Numero);
                datos.setearParametro("@Nombre", pokemon.Nombre);
                datos.setearParametro("@Descripcion", pokemon.Descripcion);
                datos.setearParametro("@UrlImagen", pokemon.UrlImagen);
                datos.setearParametro("@IdTipo", pokemon.Tipo.Id);
                datos.setearParametro("@IdDebilidad", pokemon.Debilidad.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminacionFisica(Pokemon pokemonEliminacionFisica)
        {
            try
            {
                datos.setearConsulta("delete from POKEMONS where id =@Id");
                datos.setearParametro("@Id", pokemonEliminacionFisica.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Pokemon pokemon)
        {
            try
            {
                datos.setearConsulta("Update POKEMONS set Numero =@Numero, Nombre =@Nombre, Descripcion =@Descripcion, UrlImagen =@UrlImagen, IdTipo =@IdTipo, IdDebilidad =@IdDebilidad  where Id =@Id");
                datos.setearParametro("@Numero", pokemon.Numero);
                datos.setearParametro("@Nombre", pokemon.Nombre);
                datos.setearParametro("@Descripcion", pokemon.Descripcion);
                datos.setearParametro("@UrlImagen", pokemon.UrlImagen);
                datos.setearParametro("@IdTipo", pokemon.Tipo.Id);
                datos.setearParametro("@IdDebilidad", pokemon.Debilidad.Id);
                datos.setearParametro("@Id", pokemon.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            { 
                datos.cerrarConexion();
            }
        }
    }
}
                
                


        
