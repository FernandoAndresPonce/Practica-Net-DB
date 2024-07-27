using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using BusinessLogic;


namespace UserInterfaz
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        
        

        public frmAltaPokemon()
        {
            InitializeComponent();
            
            
        }
        public frmAltaPokemon(Pokemon seleccionModificar)
        {
            InitializeComponent();
            this.pokemon = seleccionModificar;
            lblAltaPokemon.Text = "Modificar Pokemon";

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            NegocioElemento negocioElemento = new NegocioElemento();
            try
            {
                cmbTipo.DataSource = negocioElemento.Listar();
                cmbTipo.ValueMember = "Id";
                cmbTipo.DisplayMember = "Descripcion";

                CmbDebilidad.DataSource = negocioElemento.Listar();
                CmbDebilidad.ValueMember = "Id";
                CmbDebilidad.DisplayMember = "Descripcion";

                if (pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlimagen.Text = pokemon.UrlImagen;
                    cmbTipo.SelectedValue = pokemon.Tipo.Id;
                    CmbDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }    
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();

            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlimagen.Text;
                pokemon.Tipo = (Elemento)cmbTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)CmbDebilidad.SelectedItem;
                

                if (pokemon.Id != 0)
                {
                    negocio.Modificar(pokemon);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(pokemon);
                    MessageBox.Show("Agregado exitosamente");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Close();
            }
        }


        private void txtUrlimagen_Enter(object sender, EventArgs e)
        {
            cargarImagen(txtUrlimagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbAltaPokemon.Load(imagen);
            }
            catch (Exception ex)
            {
                pbAltaPokemon.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");

            }

        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}


