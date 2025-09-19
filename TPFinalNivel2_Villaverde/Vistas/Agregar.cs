using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vistas
{
    public partial class Agregar : Form
    {
        private MarcaNegocio marca = new MarcaNegocio();
        private CategoriaNegocio categoria = new CategoriaNegocio();
        public Agregar()
        {
            InitializeComponent();
            string imagen = "";
            cargarImagen(imagen);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            try
            {
                cboMarca.DisplayMember = "DescripcionMarca";
                cboMarca.ValueMember = "Id";
                cboMarca.DataSource = marca.listarMarcas();

                cboCategoria.DisplayMember = "DescripcionCategoria";
                cboCategoria.ValueMember = "Id";
                cboCategoria.DataSource = categoria.listarCategoria();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                // VALIDAR NOMBRE (no vacío ni solo espacios)
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre no puede estar vacío.");
                    return;
                }

                // VALIDAR PRECIO (solo números / decimales válidos)
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text; 
                articulo.Descripcion = txtDescripcion.Text;
                articulo.IdMarca = (Marca)cboMarca.SelectedItem;
                articulo.IdCategoria = (Categoria)cboCategoria.SelectedItem;
                articulo.ImagenUrl = txtUrl.Text;
                articulo.Precio = precio;

                articuloNegocio.insertar(articulo);
                MessageBox.Show("Artículo agregado exitosamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void txtUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrl.Text);
        }

        public void cargarImagen(string imagen)
        {
            try
            {
                pbxAgregando.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxAgregando.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }
    }
}
