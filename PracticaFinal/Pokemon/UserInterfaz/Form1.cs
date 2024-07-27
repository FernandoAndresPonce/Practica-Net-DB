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

            cmbCampo.Items.Add("Numero");
            cmbCampo.Items.Add("Nombre");
            cmbCampo.Items.Add("Descripcion");


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
            if (dgvPokemones.CurrentRow != null)
            {
                Pokemon seleccionImagen = (Pokemon)dgvPokemones.CurrentRow.DataBoundItem;
                cargarImagenPrincipal(seleccionImagen.UrlImagen);
            }
        }

        private void ocultarColumnas()
        {
            dgvPokemones.Columns["UrlImagen"].Visible = false;
            dgvPokemones.Columns["Id"].Visible = false;
            dgvPokemones.Columns["IdTipo"].Visible = false;
            dgvPokemones.Columns["IdDebilidad"].Visible = false;
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
            Pokemon seleccionadoModicar = (Pokemon)dgvPokemones.CurrentRow.DataBoundItem;

            frmAltaPokemon modificarPokemon = new frmAltaPokemon(seleccionadoModicar);
            modificarPokemon.ShowDialog();
            cargarFormPrincipal();

        }

        private void BtnEliminacionFisica_Click(object sender, EventArgs e)
        {
            Pokemon pokemonEliminacionFisica = (Pokemon)dgvPokemones.CurrentRow.DataBoundItem;
            Negocio negocio = new Negocio();

            DialogResult respuesta = MessageBox.Show("Esta seguro de Eliminar el Regsitro", "Atencion...", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (respuesta == DialogResult.OK)
            {
                negocio.EliminacionFisica(pokemonEliminacionFisica);
                cargarFormPrincipal();
            }


        }

        private void btnEliminacionLogica_Click(object sender, EventArgs e)
        {
            Pokemon pokemonEliminacionLogica = (Pokemon)dgvPokemones.CurrentRow.DataBoundItem;
            Negocio negocio = new Negocio();

            DialogResult respuesta = MessageBox.Show("Desea Eliminar el Registro", "Atencion Elimanando...", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (respuesta == DialogResult.OK)
            {
                negocio.EliminacionLogica(pokemonEliminacionLogica);
                cargarFormPrincipal();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filtrar = txtBuscador.Text;
            List<Pokemon> listaFiltrar = new List<Pokemon>();

            if (filtrar != string.Empty)
                listaFiltrar = listaPokemones.FindAll(x => x.Nombre.ToLower().Contains(filtrar.ToLower()) || x.Tipo.Descripcion.ToLower().Contains(filtrar.ToLower()));
            else
                listaFiltrar = listaPokemones;

            dgvPokemones.DataSource = null;
            dgvPokemones.DataSource = listaFiltrar;
            ocultarColumnas();
        }



        private void cmbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtroCampo = cmbCampo.SelectedItem.ToString();

            switch (filtroCampo)
            {
                case "Numero":
                    cmbCriterio.Items.Clear();
                    cmbCriterio.Items.Add("Mayor a");
                    cmbCriterio.Items.Add("Menor a");
                    cmbCriterio.Items.Add("Igual a");
                    break;
                case "Nombre":
                    cmbCriterio.Items.Clear();
                    cmbCriterio.Items.Add("Empiza con");
                    cmbCriterio.Items.Add("Termina con");
                    cmbCriterio.Items.Add("Contiene");
                    break;
                default:
                    cmbCriterio.Items.Clear();
                    cmbCriterio.Items.Add("Empiza con");
                    cmbCriterio.Items.Add("Termina con");
                    cmbCriterio.Items.Add("Contiene");
                    break;
            }
        }

        public bool soloNumero(string consulta)
        {
            foreach (char numeros in consulta)
            {
                if (!(char.IsNumber(numeros)))
                    return true;

            }   return false;
        }

        public bool validarCombo()
        {
            if (cmbCampo.SelectedIndex < 0)
            {
                MessageBox.Show("El Campo esta Vacio");
                return true;
            }
            else if (cmbCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("El Criterio esta Vaacio");
                return true;

            }
            else if (soloNumero(txtFiltro.Text))
            {
                MessageBox.Show("Ingrese solamente numeros.");
                return true;
            }

            return false;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            List<Pokemon> filtrarPokemon = new List<Pokemon>();

            if (validarCombo())
                return;

            string campo = cmbCampo.SelectedItem.ToString();
            string criterio = cmbCriterio.SelectedItem.ToString();
            string filtro = txtFiltro.Text;


            filtrarPokemon = negocio.ListaFiltrada(campo, criterio, filtro);
            dgvPokemones.DataSource = null;
            dgvPokemones.DataSource = filtrarPokemon;

        }
    }
}
