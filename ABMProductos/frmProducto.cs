using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ABMProductos
{
    public partial class frmProducto : Form

    {   
        SqlConnection conexion = new SqlConnection(@"Data Source=HOME\SQLEXPRESS;Initial Catalog=Informatica;Integrated Security=True");
        SqlCommand comando = new SqlCommand();

        bool nuevo = false;

        //int c;
        //const int tam = 100; Alternativa con un arreglo
        //Producto[] aProducto = new Producto[tam];

        SqlDataReader lector;
        List<Producto> lproducto = new List<Producto>();

        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            
            dtpFecha.Enabled = false;
            this.cargarLista(lstProducto, "Productos");
            this.cargarCombo(cboMarca, "Marcas");
            this.cargarGrilla(grdProductos, "Productos");
            
            habilitar(false);
        }

        private void cargarGrilla(DataGridView grilla, string nombreTabla)
        {
            DataTable tabla = consultarSQL("select codigo, detalle, m.nombreMarca Marca, precio from "+ nombreTabla + " p join Marcas m on m.idMarca=p.marca");


            grilla.Rows.Clear();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                grdProductos.Rows.Add(tabla.Rows[i]["codigo"], tabla.Rows[i]["detalle"], tabla.Rows[i]["Marca"], tabla.Rows[i][3]);
            }


        }

        private void limpiar()
        {
            txtCodigo.Text = " ";
            txtDetalle.Text = " ";
            cboMarca.SelectedIndex = -1;
            rbtNoteBook.Checked = true;
            txtPrecio.Text = "";
            dtpFecha.Value = DateTime.Now;

        }

        private void habilitar(bool x)
        {
            txtCodigo.Enabled = x;
            txtDetalle.Enabled = x;
            cboMarca.Enabled = x;
            rbtNetBook.Enabled = x;
            rbtNoteBook.Enabled = x;
            txtPrecio.Enabled = x;
            btnNuevo.Enabled = !x;
            btnEditar.Enabled = x;
            btnBorrar.Enabled = x;
            btnGrabar.Enabled = x;
            btnCancelar.Enabled = x;
            lstProducto.Enabled = !x;
            
        }

        private void cargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = consultarSQL("select * from " + nombreTabla+ " order by 2" );
            combo.DataSource = tabla;
            combo.ValueMember = tabla.Columns[0].ColumnName;
            combo.DisplayMember = tabla.Columns[1].ColumnName;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;


        }

        private DataTable consultarSQL(string consultaSQL)
        {
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = consultaSQL;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
            
        }


        private void cargarLista(ListBox lista, string nombreTabla)
        {
            int c = 0;
            lproducto.Clear();
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "select * from " + nombreTabla;
            lector=comando.ExecuteReader();
            while(lector.Read())
            {
                Producto p = new Producto();

                if (!lector.IsDBNull(0))
                    p.pCodigo = lector.GetInt32(0);
                if (!lector.IsDBNull(1))
                    p.pDetalle = lector.GetString(1);
                if (!lector.IsDBNull(2))
                    p.pTipo = lector.GetInt32(2);
                if (!lector.IsDBNull(3))
                    p.pMarca = lector.GetInt32(3);
                if (!lector.IsDBNull(4))
                    p.pPrecio = lector.GetDouble(4);
                if (!lector.IsDBNull(5))
                    p.pFecha = lector.GetDateTime(5);

                
                lproducto.Add(p);

                c++;

            }

            lector.Close();
            conexion.Close();
            lista.Items.Clear();

            for (int i = 0; i < lproducto.Count; i++)
            {
                lista.Items.Add(lproducto[i].ToString());
            }

            

        }

        private void lstProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargarCampos(lstProducto.SelectedIndex);
            
            
        }

        private void cargarCampos(int posicion)
        {
           
            txtCodigo.Text = lproducto[posicion].pCodigo.ToString();
            txtDetalle.Text = lproducto[posicion].pDetalle;
            cboMarca.SelectedValue = lproducto[posicion].pMarca;
            if (lproducto[posicion].pTipo == 1)
                rbtNoteBook.Checked = true;
            else
                rbtNetBook.Checked = true;
            txtPrecio.Text = lproducto[posicion].pPrecio.ToString();
            dtpFecha.Value = lproducto[posicion].pFecha;

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            if (validarCampos())
            {

                Producto p = new Producto();
                p.pCodigo = Convert.ToInt32(txtCodigo.Text);
                p.pDetalle = txtDetalle.Text;
                if (rbtNoteBook.Checked)
                    p.pTipo = 1;
                else
                    p.pTipo = 2;
                p.pMarca = Convert.ToInt32(cboMarca.SelectedValue);
                p.pPrecio = Convert.ToDouble(txtPrecio.Text);
                p.pFecha = dtpFecha.Value;

                if (nuevo)
                {
                    try
                    {
                        conexion.Open();
                        SqlCommand comando= new SqlCommand("InsertarProducto", conexion); //Insert con sp
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@codigo", p.pCodigo);
                        comando.Parameters.AddWithValue("@detalle", p.pDetalle);
                        comando.Parameters.AddWithValue("@tipo", p.pTipo);
                        comando.Parameters.AddWithValue("@marca", p.pMarca);
                        comando.Parameters.AddWithValue("@precio", p.pPrecio);
                        comando.Parameters.AddWithValue("@fecha", p.pFecha);
                        comando.ExecuteNonQuery();
                        comando.Parameters.Clear();
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Verifique que el código de producto no este repetido!");
                        
                    }
                    finally
                    {
                        conexion.Close();
                    }

                    this.cargarLista(lstProducto, "Productos");
                    this.cargarGrilla(grdProductos, "Productos");
                    MessageBox.Show("Producto ingresado con éxito");


                }
                else
                {    //Update con enviando texto, siempre es mas recomendable con parámetros 
                    string updateSQL = "Update Productos set codigo = " + p.pCodigo + ","
                                                         + " detalle = ' " + p.pDetalle + "',"
                                                         + " tipo = " + p.pTipo + " , "
                                                         + "marca = " + p.pMarca + ","
                                                         + "precio = " + p.pPrecio + ","
                                                         + "fecha = '" + p.pFecha + "' "
                                                         + " where codigo = " + p.pCodigo;
                actualizarSQL(updateSQL);
                this.cargarLista(lstProducto, "Productos");
                this.cargarGrilla(grdProductos, "Productos");
                    MessageBox.Show("Actualizacion correcta");

                }

            
                habilitar(false);
                limpiar();
                txtCodigo.Focus();
                nuevo = false;
            }
            
        }

        private bool validarCampos()
        {
            if (txtCodigo.Text == " " )
            {
                MessageBox.Show("Debes ingresar un código");
                txtCodigo.Focus();
                return false;
            }

            if (txtDetalle.Text == " ")
            {
                MessageBox.Show("Debes ingresar un detalle antes de continuar");
                txtDetalle.Focus();
                return false;
            }
            if (cboMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona una marca");
                cboMarca.Focus();
                return false;
            }
            if (!rbtNetBook.Checked && !rbtNoteBook.Checked)
            {
                MessageBox.Show("Debes seleccionar el tipo de portatil");
                rbtNoteBook.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("completa el precio");
                txtPrecio.Focus();
                return false;
            }

        
            return true;

        }


        private void actualizarSQL(string consultaSQL) //sirve para todos los nonquery // insert, update, delete
        {                                               
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = consultaSQL;
            comando.ExecuteNonQuery();
            conexion.Close();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nuevo = true;
            
            habilitar(true);
            limpiar();
            lstProducto.Enabled = false;

            txtCodigo.Focus();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            lstProducto.Enabled=true;
            nuevo = false;
            txtCodigo.Enabled = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
          
                limpiar();
                habilitar(false);
                nuevo = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Seguro de abandonar la aplicación ?",
                    "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    this.Close();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quiere eliminar este registro de forma pemanente? ", "Borrando", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
      
                string deleteSQL = "Delete from Productos where codigo = " + Convert.ToInt32(lproducto[lstProducto.SelectedIndex].pCodigo);

                actualizarSQL(deleteSQL);
                this.cargarLista(lstProducto, "Productos");
                this.cargarGrilla(grdProductos, "Productos");
            }

        }

        private void grdProductos_SelectionChanged(object sender, EventArgs e)
        {
           this.cargarCampos(grdProductos.SelectedRows[0].Index);
        }
    }
}
