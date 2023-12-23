using mi_formulario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mi_formulario
{
    public partial class FrmDocente : Form
    {
        /// <summary>
        /// Valiable globales de la clase
        /// </summary>
        private DocenteModel docente;
        private bool isValidModel;
        private string baseApiUrl = "https://localhost:44355";
        private bool errorApi = false;
        private bool isEdit = false;

        /// <summary>
        /// Cconstructor de la clase
        /// </summary>
        public FrmDocente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se ejecuta al cargar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmDocente_Load(object sender, EventArgs e)
        {
            docente = new DocenteModel();
            this.isValidModel = false;
            validateButtons();
            llenarGrid();
            llenarConboBoxDocente();

        }

        /// <summary>
        /// Evento para crear un nuevo docente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            crearDocente();
            if (!errorApi)
            {
                this.limpiarForm();
                this.llenarGrid();
                this.isEdit = false;
                this.isValidModel = validateModel();
                validateButtons();
            }
        }

        /// <summary>
        /// Evento para elinminar un docente por el id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de Eliminar el registro?", "Eliminar Docente", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                deleteDocente(TxtId.Text);
                if (!errorApi)
                {
                    this.limpiarForm();
                    this.llenarGrid();
                    this.isEdit = false;
                    this.isValidModel = validateModel();
                    validateButtons();
                }
            }
        }

        /// <summary>
        /// Evento para actualizar un docente seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            actualizarDocente();
            if (!errorApi)
            {
                this.limpiarForm();
                this.llenarGrid();
                this.isEdit = false;
                this.isValidModel = validateModel();
                validateButtons();
                TxtDocumento.Enabled = true;
            }
        }

        /// <summary>
        /// Evento del booton limpiar para resetear los campos del formulario y recargar la tabla 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
            this.isEdit = false;
            validateButtons();
        }

        /// <summary>
        /// Evento que cotrola el campo nombre cuando el usuario escribe - presiona tecllas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.docente.NOMBRE = TxtNombre.Text;

        }

        /// <summary>
        /// Evento que cotrola el campo documento cuando el usuario escribe - presiona tecllas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.docente.DOCUMENTO = TxtDocumento.Text;
        }

        /// <summary>
        /// Evento que cotrola el campo email cuando el usuario escribe - presiona tecllas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.docente.CORREO = TxtEmail.Text;
        }

        /// <summary>
        /// Evento cuando el campo nombre pierde el focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNombre_Leave(object sender, EventArgs e)
        {
            this.isValidModel = validateModel();
            this.BtnGuardar.Enabled = isValidModel;
        }
        /// <summary>
        /// Evento cuando el campo documento pierde el focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDocumento_Leave(object sender, EventArgs e)
        {
            this.isValidModel = validateModel();
            this.BtnGuardar.Enabled = isValidModel;
        }
        /// <summary>
        /// Evento cuando el campo emmail pierde el focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            this.isValidModel = validateModel();
            this.BtnGuardar.Enabled = isValidModel;
        }

        /// <summary>
        /// Evento cuando cambia el valor del campo id para controlar si es editar o registro nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtId_TextChanged(object sender, EventArgs e)
        {
            var id = 0;
            int.TryParse(TxtId.Text, out id);
            if (id > 0) this.docente.ID = id;
            if (TxtNombre.Text.Trim().Length > 0) BtnEliminar.Enabled = true;
            else BtnEliminar.Enabled = false;
        }


        /// <summary>
        /// Evento cuando se seleeciona cualquier celda de la tabla docente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvDocente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index < 0) return;
            this.isEdit = true;
            var id = DgvDocente.Rows[index].Cells[0].Value.ToString();
            this.setFormEditar(id);
            this.isValidModel = validateModel();
            validateButtons();
        }

        /// <summary>
        /// Valida si habilita o sehabilita botones
        /// </summary>
        private void validateButtons()
        {
            this.BtnGuardar.Enabled = isValidModel && !isEdit;
            this.BtnActualizar.Enabled = isValidModel && isEdit;
            this.BtnEliminar.Enabled = isEdit;
        }

        /// <summary>
        /// Carga el data grid - tabla con la lista de docentes obtenida desde la base de datos 
        /// </summary>
        private void llenarGrid()
        {
            var listaDocentes = getListaDocente();
            DgvDocente.AutoGenerateColumns = true;
            DgvDocente.DataSource = listaDocentes;
        }

        /// <summary>
        /// llena un combo box (lista desplegable) con los datos de una tabla
        /// </summary>
        private void llenarConboBoxDocente()
        {
            var listaDocentes = getListaDocente();
            ///origen de datos
            CbxDocente.DataSource = listaDocentes;
            ///propiedad que ve el usuario
            CbxDocente.DisplayMember = "NOMBRE";
            ///propiedad imterna para diferemcia el registro llave primaria de la tabla
            CbxDocente.ValueMember = "ID";
            //para que no se muestre item seleccionado y se vea en blanco
            CbxDocente.SelectedIndex = -1;
        }


        /// <summary>
        /// metodo para limpiar los campos del formulario
        /// </summary>
        private void limpiarForm()
        {
            TxtId.Text = string.Empty;
            TxtNombre.Text = string.Empty;
            TxtDocumento.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            this.isValidModel = validateModel();
            this.BtnGuardar.Enabled = isValidModel;
        }


        /// <summary>
        /// Valida si el modelo de docente es valido para guardar po actualizar
        /// </summary>
        /// <returns></returns>
        private bool validateModel()
        {
            return TxtNombre.Text.Trim().Length > 0 && TxtDocumento.Text.Trim().Length > 0;
        }


        /// <summary>
        /// Retorna la lista de los docentes que existen en la tabla base de datos
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DocenteModel> getListaDocente()
        {
            var listaDocente = Enumerable.Empty<DocenteModel>();
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{this.baseApiUrl}/api/Docente"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {

                MessageBox.Show("No se pudo establecer conexion con el api");
                return listaDocente;
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {

                MessageBox.Show(stringResponse, "¡Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return listaDocente;
            }
            else
            {
                listaDocente = JsonSerializer.Deserialize<IEnumerable<DocenteModel>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return listaDocente;
            }
        }

        /// <summary>
        /// carga los campos del formulario coon los datos del docente seleecionado para editarlo
        /// </summary>
        /// <param name="id"></param>
        private void setFormEditar(string id)
        {

            this.docente = this.getDocente(id);
            ///selecciona del combo box el registrp por el id que se le asigna
            CbxDocente.SelectedValue = int.Parse(id);
            //carga en los campos de texto los datos para actualizar
            TxtId.Text = this.docente.ID.ToString();
            TxtNombre.Text = this.docente.NOMBRE;
            TxtDocumento.Text = this.docente.DOCUMENTO;
            TxtDocumento.Enabled = false;
            TxtEmail.Text = this.docente.CORREO;
        }

        /// <summary>
        /// crea un nuevo docente en la base de datoos por medio del consumo api rest
        /// </summary>
        private void crearDocente()
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{this.baseApiUrl}/api/Docente"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
                Content = new StringContent(JsonSerializer.Serialize(this.docente), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                this.errorApi = true;
                MessageBox.Show("No se pudo establecer conexion con el api");
                return;
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                this.errorApi = true;
                MessageBox.Show(stringResponse, "¡Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                this.errorApi = false;
                MessageBox.Show("Agregado Exitosamente", "¡Success!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// actualiza un docente  seleccionado en la base de datoos por medio del consumo api rest
        /// </summary>
        private void actualizarDocente()
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{this.baseApiUrl}/api/Docente"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
                Content = new StringContent(JsonSerializer.Serialize(this.docente), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {
                this.errorApi = true;
                MessageBox.Show("No se pudo establecer conexion con el api");
                return;
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                this.errorApi = true;
                MessageBox.Show(stringResponse, "¡Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                this.errorApi = false;
                MessageBox.Show("Actualizado Exitosamente", "¡Success!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// obtiene los datos de un docente  por el id en la base de datoos por medio del consumo api rest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DocenteModel getDocente(string id)
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{this.baseApiUrl}/api/Docente/FindById?id={id}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {

                MessageBox.Show("No se pudo establecer conexion con el api");
                return null;
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {

                MessageBox.Show(stringResponse, "¡Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return null;
            }
            else
            {
                var docente = JsonSerializer.Deserialize<DocenteModel>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return docente;
            }
        }

        /// <summary>
        /// elimina un docente  seleccionado por el id en la base de datoos por medio del consumo api rest
        /// </summary>
        /// <param name="id"></param>
        private void deleteDocente(string id)
        {
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{this.baseApiUrl}/api/Docente?id={id}"),
                Headers =
                {
                    {HttpRequestHeader.Accept.ToString(),"application/json" },
                },
            };

            var response = client.SendAsync(httpRequestMessage).Result;
            if (response == null)
            {

                MessageBox.Show("No se pudo establecer conexion con el api");
                return;
            }
            var stringResponse = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                this.errorApi = true;
                if (string.IsNullOrEmpty(stringResponse)) stringResponse = response.ReasonPhrase;
                MessageBox.Show(stringResponse, "¡Error!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.errorApi = false;
                MessageBox.Show("Eliminado Exitosamente", "¡Success!", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;
            }
        }


    }

}
