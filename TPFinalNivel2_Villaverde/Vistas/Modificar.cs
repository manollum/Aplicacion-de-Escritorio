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
    public partial class Modificar : Form
    {
        private Articulo articulo = new Articulo();
        private ArticuloNegocio articuloNegocio = new ArticuloNegocio();    
        public Modificar()
        {
            InitializeComponent();
        }

        public Modificar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;   
 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Modificar_Load(object sender, EventArgs e)
        {
            txtNombreMod.Text = articulo.Nombre;
            txtDescripcionMod.Text = articulo.Descripcion;
            txtPrecioMod.Text = articulo.Precio.ToString("N2");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDAR NOMBRE (no vacío ni solo espacios)
                if (string.IsNullOrWhiteSpace(txtNombreMod.Text))
                {
                    MessageBox.Show("El nombre no puede estar vacío.");
                    return;
                }

                // VALIDAR PRECIO (solo números / decimales válidos)
                if (!decimal.TryParse(txtPrecioMod.Text, out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }

                articulo.Nombre = txtNombreMod.Text.Trim(); // Trim para limpiar espacios
                articulo.Descripcion = txtDescripcionMod.Text;
                articulo.Precio = precio; // ya validado

                articuloNegocio.modificarArticulo(articulo);
                MessageBox.Show("Artículo modificado exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
