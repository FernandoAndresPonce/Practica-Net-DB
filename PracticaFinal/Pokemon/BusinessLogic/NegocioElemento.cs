using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using DataAccess;

namespace BusinessLogic
{
    public class NegocioElemento
    {
        
        private AccesoDatos datos;
        

        public NegocioElemento()
        {
   
            this.datos = new AccesoDatos();
        }

        public List<Elemento> Listar()
        {
            List<Elemento> listaElemento = new List<Elemento>();
            try
            {
                datos.setearConsulta("Select Id, Descripcion from ELEMENTOS");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    
                    Elemento auxiliarElemeto = new Elemento();
                    auxiliarElemeto.Id = (int)datos.Lector["Id"];
                    auxiliarElemeto.Descripcion = (string)datos.Lector["Descripcion"];

                    listaElemento.Add(auxiliarElemeto);
                }
                return listaElemento;
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
