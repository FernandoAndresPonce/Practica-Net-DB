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
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemones; // Ojo.
         // ojo

        public Form1()
        {
            InitializeComponent();

            this.listaPokemones = new List<Pokemon>();
            
        }

        private void frmTablaPrincipal_Load(object sender, EventArgs e)
        {
            cargarFormPrincipal();
            
        }
        private void cargarFormPrincipal()
        {
            Negocio negocioPokemon = new Negocio();
            try
            {
                listaPokemones = negocioPokemon.Listar();
                dgvPokemones.DataSource = listaPokemones;
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
          
        }
    

        private void dgvPokemones_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionImagen = (Pokemon)dgvPokemones.CurrentRow.DataBoundItem;
            cargarImagenPrincipal(seleccionImagen.UrlImagen);
        }

        private void ocultarColumnas()
        {
            dgvPokemones.Columns["UrlImagen"].Visible = false;
            dgvPokemones.Columns["Id"].Visible = false;
        }
        private void cargarImagenPrincipal(string imagenPokemon)
        {
            try
            {
                pbPokemon.Load(imagenPokemon);
            }
            catch (Exception)
            {
                pbPokemon.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");

            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon altaPokemon = new frmAltaPokemon();
            altaPokemon.ShowDialog();
            cargarFormPrincipal();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionadoModicar = (Pokemon) dgvPokemones.CurrentRow.DataBoundItem;

            frmAltaPokemon modificarPokemon = new frmAltaPokemon(seleccionadoModicar);
            modificarPokemon.ShowDialog();
            cargarFormPrincipal();

        }

        private void BtnEliminacionFisica_Click(object sender, EventArgs e)
        {
            Pokemon pokemonEliminacionFisica = (Pokemon) dgvPokemones.CurrentRow.DataBoundItem;
            Negocio negocio = new Negocio();

            DialogResult respuesta = MessageBox.Show("Esta seguro de Eliminar el Regsitro","Atencion...", MessageBoxButtons.OKCancel,MessageBoxIcon.Stop);
            if (respuesta == DialogResult.OK)
            {
                negocio.EliminacionFisica(pokemonEliminacionFisica);
                cargarFormPrincipal();
            }


        }
    }
}
