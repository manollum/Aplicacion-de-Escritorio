using Entidades;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vistas
{
    public partial class main : Form
    {
        private ArticuloNegocio articuloNegocio = new ArticuloNegocio();
        private List<Articulo> listaFront = new List<Articulo>();
        private CategoriaNegocio categoriaNegocio = new CategoriaNegocio(); 
        private List<Categoria> listaCategoria = new List<Categoria>();
        private MarcaNegocio marcaNegocio = new MarcaNegocio();
        private List<Marca> listaMarca = new List<Marca>();
        
        public main ()
        {
            InitializeComponent();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var agregar = new Agregar())
            {
                var result = agregar.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Cargar(); 
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo(); 
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;


            Modificar modificar = new Modificar(seleccionado);
            modificar.ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        public void Cargar()
        {
            dgvArticulos.DataSource = null;
            listaFront.Clear();
            listaCategoria.Clear();
            listaMarca.Clear();
            listaFront = articuloNegocio.listar();
            listaCategoria = categoriaNegocio.listarCategoria();
            listaMarca = marcaNegocio.listarMarcas();
            dgvArticulos.DataSource = listaFront;
            manejoDeColumnas();
            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            cargarImagen(listaFront[0].ImagenUrl);
            cargarComboBox();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
        }

        //método aparte para cargar la imagen
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        //separo este metodo para que el Load no se haga eterno y poder reutilizarlo.
        
        public void cargarComboBox()
        {
            try
            {
                listaMarca.Insert(0, new Marca { Id = 0, DescripcionMarca = "       -- Elija opción --  " });
                //cboCategoria.DataSource = listaCategoria;
                cboMarca.DataSource = listaMarca;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar este registro?", "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
                if (resultado == DialogResult.Yes)
                {
                    articuloNegocio.eliminar(seleccionado);
                    MessageBox.Show("Registro eliminado con éxito");
                    Cargar();
                }     
            }
        }

        private void btnFIltrar_Click(object sender, EventArgs e)
        {
            string filtro = (txtFiltro.Text ?? "").Trim();
            string marca = (cboMarca.Text ?? "").Trim();

            bool filtraNombre = !string.IsNullOrWhiteSpace(filtro);
            bool filtraMarca = !string.IsNullOrWhiteSpace(marca)
                                && !marca.Equals("-- Elija opción --", StringComparison.OrdinalIgnoreCase);

            if (!filtraNombre && !filtraMarca)
            {
                MessageBox.Show("Ingresá un nombre o elegí una marca, por favor...");
                return;
            }

            // Siempre arrancamos de la lista completa
            List<Articulo> listaFiltrada = listaFront;

            if (filtraNombre)
            {
                listaFiltrada = listaFiltrada.FindAll(x =>
                    !string.IsNullOrWhiteSpace(x.Nombre) &&
                    x.Nombre.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            if (filtraMarca)
            {
                listaFiltrada = listaFiltrada.FindAll(x =>
                    x.IdMarca != null &&
                    !string.IsNullOrWhiteSpace(x.IdMarca.DescripcionMarca) &&
                    x.IdMarca.DescripcionMarca.IndexOf(marca, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            manejoDeColumnas();

          
            txtFiltro.Text = "";
        }


        private void manejoDeColumnas()
        {
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "N2";
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["IdMarca"].HeaderText = "Marca";
            dgvArticulos.Columns["IdCategoria"].HeaderText = "Categoría";

        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            Cargar();
            txtFiltro.Text = "";
        }
    }
}
