using CadParcial2Vcy;
using ClnParcial2Vcy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpParcial2Vcy
{
    public partial class FrmPrograma : Form
    {
        private bool esNuevo;
        public FrmPrograma()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var horarioPrograma = HorarioProgramaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = horarioPrograma;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["idCanal"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["canal"].HeaderText = "Canal";
            dgvLista.Columns["frecuencia"].HeaderText = "Frecuencia";
            dgvLista.Columns["titulo"].HeaderText = "Título";
            dgvLista.Columns["horaInicio"].HeaderText = "Hora Inicio";
            dgvLista.Columns["diaSemana"].HeaderText = "Día Semana";
            dgvLista.Columns["conductorPrincipal"].HeaderText = "Conductor Principal";

            if (horarioPrograma.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["canal"];

            btnEditar.Enabled = horarioPrograma.Count > 0;
            btnEliminar.Enabled = horarioPrograma.Count > 0;

            resetearCampos();
            resetearErrores();

        }

        private void cargarCanal()
        {
            cbxCanal.DataSource = CanalCln.listar();
            cbxCanal.DataSource = CanalCln.listar();
            cbxCanal.ValueMember = "id";
            cbxCanal.DisplayMember = "nombre";
            cbxCanal.SelectedIndex = -1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listar();
            cargarCanal();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void resetearCampos()
        {
            cbxCanal.SelectedIndex = -1;
            txtFrecuencia.Clear();
            txtTitulo.Clear();
            mtbxHoraInicio.Clear();
            txtDiaSemana.Clear();
            txtConductorPrincipal.Clear();
            resetearErrores();

        }
        private void resetearErrores()
        {
            erpCanal.Clear();
            erpFrecuencia.Clear();
            erpTitulo.Clear();
            erpHoraInicio.Clear();
            erpDiaSemana.Clear();
            erpConductorPrincipal.Clear();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAcciones.Enabled = true;
            resetearCampos();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = true;
            resetearCampos();
        }
        private bool validarCampos()
        {
            bool esValido = true;
            resetearErrores();

            if (cbxCanal.SelectedIndex == -1)
            {
                erpCanal.SetError(cbxCanal, "Seleccione un canal obligatorio");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtFrecuencia.Text))
            {
                erpFrecuencia.SetError(txtFrecuencia, "La frecuencia de programa es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                erpTitulo.SetError(txtTitulo, "El título del programa es obligatorio");
                esValido = false;
            }
            if (!mtbxHoraInicio.MaskFull)
            {
                erpHoraInicio.SetError(mtbxHoraInicio, "Ingrese una hora de inicio completa");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaSemana.Text))
            {
                erpDiaSemana.SetError(txtDiaSemana, "Seleccione o escriba el día de la semana");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtConductorPrincipal.Text))
            {
                erpConductorPrincipal.SetError(txtConductorPrincipal, "El conductor principal es obligatorio");
                esValido = false;
            }
            return esValido;
        }
       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validarCampos();

        }
    }
}
